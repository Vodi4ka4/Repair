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
    public partial class Admin : Form
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=159632;Database=Repair";
        public Admin()
        {
            InitializeComponent();
        }

        private void comboBox_table_SelectedIndexChanged(object sender, EventArgs e)
        {
            Admin_Load();
            SelectedIndex();
        }

        private void Admin_Load()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                // Открываем подключение
                connection.Open();

                // Напишем SQL-запрос для выборки данных
                string sql = "SELECT * FROM " + SelectedIndex();

                // Создаем объект адаптера данных
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, connection))
                {
                    // Создаем объект DataTable для хранения данных
                    DataTable dataTable = new DataTable();

                    // Заполняем DataTable данными из базы данных
                    adapter.Fill(dataTable);

                    // Привязываем DataTable к DataGridView
                    dataGridView_table.DataSource = dataTable;
                }
            }
            dataGridView_table.AllowUserToAddRows = true;
            dataGridView_table.ReadOnly = false;
        }
        private void dataGridView_table_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dataTable = (DataTable)dataGridView_table.DataSource;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM " + SelectedIndex(), connection))
                {
                    new NpgsqlCommandBuilder(adapter);
                    adapter.UpdateCommand = new NpgsqlCommandBuilder(adapter).GetUpdateCommand();
                    adapter.Update(dataTable);
                }
            }
        }

        private void dataGridView_table_RowDeleted(object sender, DataGridViewRowEventArgs e)
        {
            DataTable dataTable = (DataTable)dataGridView_table.DataSource;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM " + SelectedIndex(), connection))
                {
                    new NpgsqlCommandBuilder(adapter);
                    adapter.DeleteCommand = new NpgsqlCommandBuilder(adapter).GetDeleteCommand();
                    adapter.Update(dataTable);
                }
            }
        }
        private string SelectedIndex()
        {
            if (comboBox_table.SelectedIndex == 0)
            {
                string text = "employer";
                return text;
            }
            else if (comboBox_table.SelectedIndex == 1)
            {
                string text = "priority";
                return text;
            }
            else if (comboBox_table.SelectedIndex == 2)
            {
                string text = "repairreport";
                return text;
            }
            else if (comboBox_table.SelectedIndex == 3)
            {
                string text = "request";
                return text;
            }
            else if (comboBox_table.SelectedIndex == 4)
            {
                string text = "role";
                return text;
            }
            else if (comboBox_table.SelectedIndex == 5)
            {
                string text = "sevetity";
                return text;
            }
            else if (comboBox_table.SelectedIndex == 6)
            {
                string text = "spare";
                return text;
            }
            else if (comboBox_table.SelectedIndex == 7)
            {
                string text = "status";
                return text;
            }
            else if (comboBox_table.SelectedIndex == 8)
            {
                string text = "type";
                return text;
            }
            else if (comboBox_table.SelectedIndex == 9)
            {
                string text = "users";
                return text;
            }
            string result = "order_";
            return result;
        }
    }
}
