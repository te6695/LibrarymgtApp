using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions; // For email validation
using System.Windows.Forms;

namespace librarymgtsystem
{
    public partial class BorrowerDetailsForm : Form
    {
        private string connectionString;
        private int borrowerId = 0; // 0 indicates Add mode, > 0 indicates Edit mode

        // Constructor for adding a new borrower
        public BorrowerDetailsForm()
        {
            InitializeComponent();
            this.Text = "Add New Borrower";
            connectionString = ConfigurationManager.ConnectionStrings["LibraryDbConnection"].ConnectionString;
        }

        // Constructor for editing an existing borrower
        public BorrowerDetailsForm(int borrowerIdToEdit) : this() // Call the default constructor
        {
            this.borrowerId = borrowerIdToEdit;
            this.Text = "Edit Borrower Details";
            LoadBorrowerDetails(); // Load existing borrower data
        }

        private void LoadBorrowerDetails()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Name, Email, Phone FROM Borrowers WHERE BorrowerID = @BorrowerID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@BorrowerID", borrowerId);
                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txtName.Text = reader["Name"].ToString();
                            txtEmail.Text = reader["Email"].ToString();
                            txtPhone.Text = reader["Phone"].ToString();
                        }
                        reader.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Database error loading borrower details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine($"SQL Error: {ex.Message}\n{ex.StackTrace}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An unexpected error occurred loading borrower details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine($"General Error: {ex.Message}\n{ex.StackTrace}");
                    }
                }
            }
        }

        
        private void btnCancel_Click(object sender, EventArgs e) // Cancel Button
        {
            this.DialogResult = DialogResult.Cancel; // Indicate cancellation
            this.Close();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

            // Input Validation
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Name and Email are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Email format validation
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(txtEmail.Text.Trim(), emailPattern))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string query;

                if (borrowerId == 0) // Add New Borrower
                {
                    query = "INSERT INTO Borrowers (Name, Email, Phone) VALUES (@Name, @Email, @Phone)";
                }
                else // Edit Existing Borrower
                {
                    query = "UPDATE Borrowers SET Name = @Name, Email = @Email, Phone = @Phone WHERE BorrowerID = @BorrowerID";
                    cmd.Parameters.AddWithValue("@BorrowerID", borrowerId);
                }

                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim()); // Phone can be empty

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        this.DialogResult = DialogResult.OK; // Indicate success
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to save borrower details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627 || ex.Number == 2601) // Unique constraint violation (for Email)
                    {
                        MessageBox.Show("An borrower with this email already exists.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Database error saving borrower: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Console.WriteLine($"SQL Error: {ex.Message}\n{ex.StackTrace}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred saving borrower: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"General Error: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        private void BorrowerDetailsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
