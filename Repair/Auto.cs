using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Repair
{
    public partial class Form_Auto : Form
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=159632;Database=Repair";
        public Form_Auto()
        {
            InitializeComponent();
        }
        private (int id, int role) Check_user(string login, string password)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                int id = 0;
                int role = 0;
                connection.Open();
                string sql = "SELECT id, role FROM users where login = @login AND password = @password";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            id = reader.GetInt32(0);
                            role = reader.GetInt32(1);
                        }
                    }
                }
                return (id, role);
            }
        }
        private void button_reg_Click(object sender, EventArgs e)
        {
            Reg reg = new Reg();
            reg.FormClosed += (s, args) => Close();
            reg.Show();
        }

        private void button_enter_Click(object sender, EventArgs e)
        {
            if (Check_user(textBox_login.Text, textBox_password.Text).role == 1)
            {
                Admin admin = new Admin();
                admin.FormClosed += (s, args) => Close();
                admin.Show();
            }
            if (Check_user(textBox_login.Text,textBox_password.Text).role == 2)
            {
                Request request = new Request(2);
                request.FormClosed += (s, args) => Close();
                request.Show();
            }
            if (Check_user(textBox_login.Text,textBox_password.Text).role == 3)
            {
                Request request = new Request(3);
                request.FormClosed += (s, args) => Close();
                request.Show();
            }
            if (Check_user(textBox_login.Text, textBox_password.Text).role == 4)
            {
                Repairer request = new Repairer(Check_user(textBox_login.Text, textBox_password.Text).id);
                request.FormClosed += (s, args) => Close();
                request.Show();
            }
        }
    }
}
