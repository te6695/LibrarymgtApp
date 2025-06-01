using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace librarymgtsystem
{
    public partial class IssueBookForm : Form
    {
        private string connectionString;
        private int selectedBorrowerId;
        private string selectedBorrowerName;

        public IssueBookForm(int borrowerId, string borrowerName)
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["LibraryDbConnection"].ConnectionString;
            selectedBorrowerId = borrowerId;
            selectedBorrowerName = borrowerName;

            lblBorrowerName.Text = $"Issuing to: {selectedBorrowerName}"; // Display the borrower name for clarity
            dtpIssueDate.Value = DateTime.Today; // Default issue date to today
            dtpDueDate.Value = DateTime.Today.AddDays(14); // Default due date to 14 days from now

            LoadAvailableBooks(); // Populate the books dropdown
        }

        private void LoadAvailableBooks()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Select books that have at least one AvailableCopy
                string query = "SELECT BookID, Title, Author, AvailableCopies FROM Books WHERE AvailableCopies > 0 ORDER BY Title";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();

                try
                {
                    con.Open();
                    da.Fill(dt);

                    cmbBooks.DataSource = dt;
                    cmbBooks.DisplayMember = "Title"; // What the user sees in the dropdown
                    cmbBooks.ValueMember = "BookID";  // The actual value (BookID) that is used
                    cmbBooks.SelectedIndex = -1; // No item selected by default
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error loading available books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"SQL Error: {ex.Message}\n{ex.StackTrace}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred loading available books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"General Error: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        

        private void btnCancel_Click(object sender, EventArgs e) // Cancel Button Click Event
        {
            this.DialogResult = DialogResult.Cancel; // Indicate cancellation
            this.Close();
        }

        private void btnIssue_Click_1(object sender, EventArgs e)
        {

           
            if (cmbBooks.SelectedValue == null)
            {
                MessageBox.Show("Please select a book to issue.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookId = Convert.ToInt32(cmbBooks.SelectedValue);
            DateTime issueDate = dtpIssueDate.Value.Date;
            DateTime dueDate = dtpDueDate.Value.Date;

            if (dueDate < issueDate)
            {
                MessageBox.Show("Due Date cannot be before Issue Date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open(); // Open connection once for the transaction
                SqlTransaction transaction = con.BeginTransaction(); // Start a transaction for atomicity

                try
                {
                    // 1. Insert record into IssuedBooks table
                    string insertQuery = "INSERT INTO IssuedBooks (BookID, BorrowerID, IssueDate, DueDate, ReturnDate) VALUES (@BookID, @BorrowerID, @IssueDate, @DueDate, NULL)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, con, transaction))
                    {
                        insertCmd.Parameters.AddWithValue("@BookID", bookId);
                        insertCmd.Parameters.AddWithValue("@BorrowerID", selectedBorrowerId); // Use the unique BorrowerID
                        insertCmd.Parameters.AddWithValue("@IssueDate", issueDate);
                        insertCmd.Parameters.AddWithValue("@DueDate", dueDate);
                        insertCmd.ExecuteNonQuery();
                    }

                    // 2. Decrement AvailableCopies in Books table
                    // Add a check to prevent available copies from going below zero (though LoadAvailableBooks should handle this)
                    string updateBookQuery = "UPDATE Books SET AvailableCopies = AvailableCopies - 1 WHERE BookID = @BookID AND AvailableCopies > 0";
                    using (SqlCommand updateBookCmd = new SqlCommand(updateBookQuery, con, transaction))
                    {
                        updateBookCmd.Parameters.AddWithValue("@BookID", bookId);
                        int rowsAffected = updateBookCmd.ExecuteNonQuery();

                        if (rowsAffected == 0) // This means the update didn't happen (e.g., copies were already 0)
                        {
                            throw new Exception("Failed to decrement available copies. The book might be out of stock.");
                        }
                    }

                    transaction.Commit(); // Commit transaction if both operations succeed
                    this.DialogResult = DialogResult.OK; // Indicate success to the calling form
                    this.Close();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback(); // Rollback on SQL error to undo changes
                    MessageBox.Show($"Database error issuing book: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"SQL Error: {ex.Message}\n{ex.StackTrace}");
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback on any other error
                    MessageBox.Show($"An unexpected error occurred issuing book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"General Error: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        private void cmbBooks_SelectedIndexChanged(object sender, EventArgs e){}
        private void dtpIssueDate_ValueChanged(object sender, EventArgs e){}
        private void dtpDueDate_ValueChanged(object sender, EventArgs e){}
        private void IssueBookForm_Load(object sender, EventArgs e){}
    }
    }
