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
    public partial class RequestForm : Form
    {
        private Request_ selectedRequest;
        private NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=159632;Database=Repair");

        public RequestForm(Request_ request)
        {
            InitializeComponent();
            selectedRequest = request;
            FillRepaiererComboBox();
            FillPriorityComboBox();
        }
        private void FillRepaiererComboBox()
        {
            try
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT name FROM employer", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string repaiererName = reader.GetString(0);
                        repairerComboBox.Items.Add(repaiererName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных из базы данных: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void FillPriorityComboBox()
        {
            try
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT title FROM priority", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string priorityTitle = reader.GetString(0);
                        priorityComboBox.Items.Add(priorityTitle);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных из базы данных: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private int GetEmployerId(string selectedRepairer)
        {
            int repairerId = -1;
            try
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT id FROM employer WHERE name = @repairerName", connection))
                {
                    command.Parameters.AddWithValue("@repairerName", selectedRepairer);
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        repairerId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении ID ремонтника из базы данных: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return repairerId;
        }

        private int GetPriorityId(string selectedPriority)
        {
            int priorityId = -1; // Значение по умолчанию, если не удастся найти ID
            try
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT id FROM priority WHERE title = @priorityTitle", connection))
                {
                    command.Parameters.AddWithValue("@priorityTitle", selectedPriority);
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        priorityId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении ID приоритета из базы данных: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return priorityId;
        }

        private void InsertRequest(int requestId, int repairerId, int priorityId)
        {
            try
            {
                connection.Open();
                using (var command = new NpgsqlCommand("UPDATE request SET employer = @repairerId, id_priority = @priorityId WHERE id = @requestId", connection))
                {
                    command.Parameters.AddWithValue("@repairerId", repairerId);
                    command.Parameters.AddWithValue("@priorityId", priorityId);
                    command.Parameters.AddWithValue("@requestId", requestId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при выполнении запроса на обновление данных в таблице request: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedRepairer = repairerComboBox.SelectedItem.ToString();
            string selectedPriority = priorityComboBox.SelectedItem.ToString();

            int repairerId = GetEmployerId(selectedRepairer);

            int priorityId = GetPriorityId(selectedPriority);

            InsertRequest(selectedRequest.Id, repairerId, priorityId);

            this.Close();
        }
    }
}
