using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace librarymgtsystem
{
    public partial class login : Form
    {
        private string connectionString;

        public login()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["LibraryDbConnection"].ConnectionString;
            pass.UseSystemPasswordChar = true;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = uname.Text.Trim();
            string password = pass.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hashedPassword = HashPassword(password);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                    try
                    {
                        con.Open();
                        int count = (int)cmd.ExecuteScalar();

                        if (count == 1)
                        {
                            MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            Mainwindow mainForm = new Mainwindow();
                            mainForm.Show();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Username or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Database error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine($"SQL Error during login: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine($"General Error during login: {ex.Message}");
                    }
                }
            }
        }

        private void signup_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void uname_TextChanged(object sender, EventArgs e) { }
        private void pass_TextChanged(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void login_Load(object sender, EventArgs e) { }

       
    }
}