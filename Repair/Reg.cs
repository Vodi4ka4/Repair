using Npgsql;
using NpgsqlTypes;
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
    public partial class Reg : Form
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=159632;Database=Repair";
        public Reg()
        {
            InitializeComponent();
        }

        private void button_reg_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // Prepare the SQL command outside the loop
                string sql = "INSERT INTO users (role,login, password) VALUES (@role,@login, @password)";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    // Add parameters to the command
                    command.Parameters.Add("@role", NpgsqlDbType.Integer);
                    command.Parameters.Add("@login", NpgsqlDbType.Text);
                    command.Parameters.Add("@password", NpgsqlDbType.Text);
                    // Set parameter values
                    command.Parameters["@role"].Value = 3;
                    command.Parameters["@login"].Value = textBox_login.Text;
                    command.Parameters["@password"].Value = textBox_password.Text;

                    // Execute the command
                    command.ExecuteNonQuery();
                    MessageBox.Show("Вы зарегестрировались");
                }
            }
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            Form_Auto auto = new Form_Auto();
            auto.FormClosed += (s, args) => Close();
            auto.Show();
        }
    }
}
