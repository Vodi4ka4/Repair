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
    public partial class Repairer : Form
    {
        private NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=159632;Database=Repair");
        private List<Request_> requests;
        private int user_id;
        public Repairer(int id_user)
        {
            InitializeComponent();
            user_id = id_user;
            LoadRequestsList();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string partName = txtPartName.Text;
            int quantity = Convert.ToInt32(txtQuantity.Text);
            decimal cost = Convert.ToDecimal(txtCost.Text);

            if (string.IsNullOrWhiteSpace(partName) || quantity <= 0 || cost <= 0)
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int requestId = -1;

            if (requestsList.SelectedRows.Count > 0)
            {
                requestId = Convert.ToInt32(requestsList.SelectedRows[0].Cells[0].Value);
            }

            try
            {
                connection.Open();

                using (var command = new NpgsqlCommand("INSERT INTO spare (request_id, title, quantity, cost) VALUES (@requestId, @partName, @quantity, @cost)", connection))
                {
                    command.Parameters.AddWithValue("@requestId", requestId);
                    command.Parameters.AddWithValue("@partName", partName);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@cost", cost);

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Информация о запчасти добавлена.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении информации о запчасти: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void LoadRequestsList()
        {
            try
            {
                connection.Open();
                string query = "SELECT request.id, request.description, request.full_name, request.email, " +
                    "employer.name, type.title, severity.title, " +
                    "priority.title, status.title, request.date_ FROM request " +
                    "INNER JOIN type ON request.id_type = type.id " +
                    "INNER JOIN severity ON request.id_severity = severity.id " +
                    "INNER JOIN status ON request.id_status = status.id " +
                    "LEFT JOIN priority ON request.id_priority = priority.id " +
                    "LEFT JOIN employer ON request.employer = employer.id WHERE employer.id_user = @id_user ";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@id_user", user_id);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);

                requestsList.DataSource = dataSet.Tables[0];

                requestsList.Columns[9].DefaultCellStyle.Format = "dd.MM.yyyy";

                requestsList.Columns[0].HeaderText = "ID";
                requestsList.Columns[1].HeaderText = "Описание";
                requestsList.Columns[2].HeaderText = "Имя заявителя";
                requestsList.Columns[3].HeaderText = "Email заявителя";
                requestsList.Columns[4].HeaderText = "Исполнитель";
                requestsList.Columns[5].HeaderText = "Тип оборудования";
                requestsList.Columns[6].HeaderText = "Серьёзность поломки";
                requestsList.Columns[7].HeaderText = "Приоритет";
                requestsList.Columns[8].HeaderText = "Статус";
                requestsList.Columns[9].HeaderText = "Дата запроса";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (requestsList.SelectedRows.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите заявку для создания отчёта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int requestId = Convert.ToInt32(requestsList.SelectedRows[0].Cells["ID"].Value);
            int repairerId = GetRepairerIdByFullName(user_id); // Получение ID сотрудника по его имени

            using (Report reportForm = new Report(requestId, repairerId))
            {
                if (reportForm.ShowDialog() == DialogResult.OK)
                {
                    UpdateRequestStatusToCompleted(requestId);
                    LoadRequestsList();
                }
            }
        }
        private void UpdateRequestStatusToCompleted(int requestId)
        {
            try
            {
                connection.Open();
                using (var command = new NpgsqlCommand("UPDATE request SET status_id = @completedStatusId WHERE id = @requestId", connection))
                {
                    command.Parameters.AddWithValue("@completedStatusId", 3);
                    command.Parameters.AddWithValue("@requestId", requestId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении статуса заявки: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }
        private int GetRepairerIdByFullName(int user_id)
        {
            int repairerId = -1; // Значение по умолчанию, если не удастся найти сотрудника
            try
            {
                using (var command = new NpgsqlCommand("SELECT id FROM employer WHERE id_user = @id_user", connection))
                {
                    command.Parameters.AddWithValue("@id_user", user_id);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        repairerId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении ID сотрудника: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return repairerId;
        }
    }
}
