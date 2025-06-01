using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace librarymgtsystem
{
    public partial class BookDetailsForm : Form
    {
        private string connectionString;
        private int bookId = 0; // 0 indicates Add mode, > 0 indicates Edit mode

        // Constructor for adding a new book
        public BookDetailsForm()
        {
            InitializeComponent();
            this.Text = "Add New Book";
            connectionString = ConfigurationManager.ConnectionStrings["LibraryDbConnection"].ConnectionString;
        }

        // Constructor for editing an existing book
        public BookDetailsForm(int bookIdToEdit) : this() // Call the default constructor first
        {
            this.bookId = bookIdToEdit;
            this.Text = "Edit Book Details";
            LoadBookDetails(); // Load existing book data for editing
        }

        private void LoadBookDetails()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Title, Author, Year, TotalCopies FROM Books WHERE BookID = @BookID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@BookID", bookId);
                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txtTitle.Text = reader["Title"].ToString();
                            txtAuthor.Text = reader["Author"].ToString();
                            txtYear.Text = reader["Year"].ToString();
                            txtTotalCopies.Text = reader["TotalCopies"].ToString();
                        }
                        reader.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Database error loading book details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine($"SQL Error: {ex.Message}\n{ex.StackTrace}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An unexpected error occurred loading book details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine($"General Error: {ex.Message}\n{ex.StackTrace}");
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e) // Save Button
        {
            // Input Validation
            if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
                string.IsNullOrWhiteSpace(txtAuthor.Text) ||
                string.IsNullOrWhiteSpace(txtYear.Text) ||
                string.IsNullOrWhiteSpace(txtTotalCopies.Text))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int year;
            // Basic year validation: greater than 0, not too far in the future
            if (!int.TryParse(txtYear.Text, out year) || year <= 0 || year > DateTime.Now.Year + 5)
            {
                MessageBox.Show("Please enter a valid year (e.g., 2023).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int totalCopies;
            if (!int.TryParse(txtTotalCopies.Text, out totalCopies) || totalCopies < 0)
            {
                MessageBox.Show("Please enter a valid number of copies (0 or more).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string query;

                if (bookId == 0) // Add New Book
                {
                    query = "INSERT INTO Books (Title, Author, Year, TotalCopies, AvailableCopies) VALUES (@Title, @Author, @Year, @TotalCopies, @AvailableCopies)";
                    cmd.Parameters.AddWithValue("@AvailableCopies", totalCopies); // Initially, all copies are available
                }
                else // Edit Existing Book
                {
                    // Fetch current AvailableCopies and TotalCopies to correctly calculate new AvailableCopies
                    int currentAvailableCopies = 0;
                    int currentTotalCopies = 0;

                    try
                    {
                        con.Open(); // Open connection to read current values
                        string checkQuery = "SELECT AvailableCopies, TotalCopies FROM Books WHERE BookID = @BookID";
                        SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                        checkCmd.Parameters.AddWithValue("@BookID", bookId);
                        SqlDataReader reader = checkCmd.ExecuteReader();
                        if (reader.Read())
                        {
                            currentAvailableCopies = Convert.ToInt32(reader["AvailableCopies"]);
                            currentTotalCopies = Convert.ToInt32(reader["TotalCopies"]);
                        }
                        reader.Close();
                        con.Close(); // Close after reading, will reopen for UPDATE
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error checking current book copies: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int issuedCopies = currentTotalCopies - currentAvailableCopies;

                    if (totalCopies < issuedCopies) // Cannot set total copies below issued copies
                    {
                        MessageBox.Show($"Cannot reduce total copies below the number of currently issued copies ({issuedCopies}).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int newAvailableCopies = totalCopies - issuedCopies; // Update available copies based on new total and existing issued

                    query = "UPDATE Books SET Title = @Title, Author = @Author, Year = @Year, TotalCopies = @TotalCopies, AvailableCopies = @AvailableCopies WHERE BookID = @BookID";
                    cmd.Parameters.AddWithValue("@BookID", bookId);
                    cmd.Parameters.AddWithValue("@AvailableCopies", newAvailableCopies);
                }

                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                cmd.Parameters.AddWithValue("@Author", txtAuthor.Text.Trim());
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@TotalCopies", totalCopies);

                try
                {
                    con.Open(); // Reopen connection for INSERT/UPDATE
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        this.DialogResult = DialogResult.OK; // Indicate success to calling form
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to save book details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error saving book: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"SQL Error: {ex.Message}\n{ex.StackTrace}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred saving book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"General Error: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) // Cancel Button
        {
            this.DialogResult = DialogResult.Cancel; // Indicate cancellation
            this.Close();
        }

        private void txtTitle_TextChanged(object sender, EventArgs e){}
        private void BookDetailsForm_Load(object sender, EventArgs e){}

       
    }
}