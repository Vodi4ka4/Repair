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
    public partial class Request : Form
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=159632;Database=Repair";
        public Request(int role)
        {
            InitializeComponent();
            FillComboBoxes();
            if (role == 2)
            {
                button_List_request.Visible = true;
            }
        }
        private void FillComboBoxes()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string equipmentTypeQuery = "SELECT (title) FROM type";
                string severityQuery = "SELECT (title) FROM severity";

                NpgsqlCommand equipmentTypeCommand = new NpgsqlCommand(equipmentTypeQuery, connection);
                NpgsqlCommand severityCommand = new NpgsqlCommand(severityQuery, connection);

                NpgsqlDataReader equipmentTypeReader = equipmentTypeCommand.ExecuteReader();
                while (equipmentTypeReader.Read())
                {
                    equipmentTypeBox.Items.Add(equipmentTypeReader.GetString(0));
                }
                equipmentTypeReader.Close();

                NpgsqlDataReader severityReader = severityCommand.ExecuteReader();
                while (severityReader.Read())
                {
                    severityBox.Items.Add(severityReader.GetString(0));
                }
                severityReader.Close();

                connection.Close();
            }
        }
        private void createRequest_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand(@"INSERT INTO request (description, full_name, email, 
            id_type, id_severity, id_status, date_) SELECT @description, @fullName, @email, 
            (SELECT id FROM type WHERE title = @equipmentType), 
            (SELECT id FROM severity WHERE title = @severity), 
            @statusId, @date_", connection);

                command.Parameters.AddWithValue("@description", descriptionText.Text);
                command.Parameters.AddWithValue("@fullName", fullNameText.Text);
                command.Parameters.AddWithValue("@email", emailText.Text);
                command.Parameters.AddWithValue("@equipmentType", equipmentTypeBox.SelectedItem);
                command.Parameters.AddWithValue("@severity", severityBox.SelectedItem);
                command.Parameters.AddWithValue("@statusId", 1);
                command.Parameters.AddWithValue("@date_", DateTime.Now);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Заявка успешно создана");
            }
        }

        private void button_List_request_Click(object sender, EventArgs e)
        {
            RequestList request = new RequestList();
            request.FormClosed += (s, args) => Close();
            request.Show();
        }
    }
}
