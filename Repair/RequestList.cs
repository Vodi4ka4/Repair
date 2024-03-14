using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Repair
{
    public partial class RequestList : Form
    {
        private NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=159632;Database=Repair");
        private List<Request_> requests;
        public RequestList()
        {
            InitializeComponent();
            LoadRequestsList();
        }
        public void LoadRequestsList()
        {
            requestsListView.Rows.Clear();
            requests = GetAllRequests();
            requestsListView.Columns[9].DefaultCellStyle.Format = "dd.MM.yyyy";
            foreach (var request in requests)
            {
                requestsListView.Rows.Add(
                    request.Id,
                    request.Description,
                    request.Requester_Full_Name,
                    request.Requester_Email,
                    request.Repaierer_Full_Name,
                    request.Equipment_type_title,
                    request.Severity_title,
                    request.Priority_title,
                    request.Status_title,
                    request.Request_Date
                );
            }
        }
        public List<Request_> GetAllRequests()
        {
            var result = new List<Request_>();
            try
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT request.id, request.description, request.full_name, request.email, " +
                    "employer.name, type.title, severity.title, " +
                    "priority.title, status.title, request.date_ FROM request " +
                    "INNER JOIN type ON request.id_type = type.id " +
                    "INNER JOIN severity ON request.id_severity = severity.id " +
                    "INNER JOIN status ON request.id_status = status.id " +
                    "LEFT JOIN priority ON request.id_priority = priority.id " +
                    "LEFT JOIN employer ON request.employer = employer.id", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var request = new Request_();
                        request.Id = reader.GetInt32(0);
                        request.Description = reader.GetString(1);
                        request.Requester_Full_Name = reader.GetString(2);
                        request.Requester_Email = reader.GetString(3);
                        request.Repaierer_Full_Name = reader.IsDBNull(4) ? "Не назначен" : reader.GetString(4);
                        request.Equipment_type_title = reader.GetString(5);
                        request.Severity_title = reader.GetString(6);
                        request.Priority_title = reader.IsDBNull(7) ? "Не назначен" : reader.GetString(7);
                        request.Status_title = reader.GetString(8);
                        request.Request_Date = reader.GetDateTime(9);
                        result.Add(request);
                    }
                }
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return result;
        }

        private void requestsListView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                int selectedId = Convert.ToInt32(requestsListView.Rows[e.RowIndex].Cells[0].Value);

                Request_ selectedRequest = GetAllRequests().FirstOrDefault(r => r.Id == selectedId);

                if (selectedRequest != null)
                {
                    RequestForm editForm = new RequestForm(selectedRequest);
                    editForm.ShowDialog();

                    LoadRequestsList();
                }
                else
                {
                    MessageBox.Show("Не удалось найти запрос с указанным идентификатором.");
                }
            }
        }
    }
}
