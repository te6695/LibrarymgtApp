using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace librarymgtsystem
{
    public partial class ReturnBookForm : Form
    {
        private string connectionString;

        public ReturnBookForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["LibraryDbConnection"].ConnectionString;
            LoadIssuedBooksData(); // Load issued books when the form opens
            btnReturnSelected.Enabled = false; // Disable initially
            dgvIssuedBooks.SelectionChanged += new EventHandler(dgvIssuedBooks_SelectionChanged);
        }

        private void LoadIssuedBooksData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Select currently issued books (where ReturnDate is NULL)
                string query = @"
                    SELECT
                        ib.IssueID,
                        b.BookID,        -- Necessary for updating AvailableCopies
                        b.Title AS BookTitle,
                        b.Author AS BookAuthor,
                        br.BorrowerID,   -- Include for context if needed
                        br.Name AS BorrowerName,
                        ib.IssueDate,
                        ib.DueDate
                    FROM IssuedBooks ib
                    JOIN Books b ON ib.BookID = b.BookID
                    JOIN Borrowers br ON ib.BorrowerID = br.BorrowerID
                    WHERE ib.ReturnDate IS NULL -- Only show unreturned books
                    ORDER BY ib.IssueDate DESC"; // Order by most recent issues first

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();

                try
                {
                    con.Open();
                    da.Fill(dt);
                    dgvIssuedBooks.DataSource = dt; // Assuming 'dgvIssuedBooks' is your DataGridView
                    dgvIssuedBooks.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                    // Enable/disable return button based on selection
                    btnReturnSelected.Enabled = dgvIssuedBooks.SelectedRows.Count > 0;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error loading issued books: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"SQL Error: {ex.Message}\n{ex.StackTrace}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred loading issued books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"General Error: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        
        private void btnCancel_Click(object sender, EventArgs e) // Cancel Button Click Event
        {
            this.DialogResult = DialogResult.Cancel; // Indicate cancellation
            this.Close();
        }

        // Enables/disables the Return Selected button based on row selection
        private void dgvIssuedBooks_SelectionChanged(object sender, EventArgs e)
        {
            btnReturnSelected.Enabled = dgvIssuedBooks.SelectedRows.Count > 0;
        }

        private void btnReturnSelected_Click_1(object sender, EventArgs e)
        {

            if (dgvIssuedBooks.SelectedRows.Count > 0)
            {
                // Get IssueID and BookID from the selected row
                int issueId = Convert.ToInt32(dgvIssuedBooks.SelectedRows[0].Cells["IssueID"].Value);
                int bookId = Convert.ToInt32(dgvIssuedBooks.SelectedRows[0].Cells["BookID"].Value);
                string bookTitle = dgvIssuedBooks.SelectedRows[0].Cells["BookTitle"].Value.ToString();
                string borrowerName = dgvIssuedBooks.SelectedRows[0].Cells["BorrowerName"].Value.ToString();

                DialogResult confirmResult = MessageBox.Show(
                    $"Are you sure you want to mark '{bookTitle}' issued to '{borrowerName}' as returned?",
                    "Confirm Return",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        SqlTransaction transaction = con.BeginTransaction(); // Start a transaction

                        try
                        {
                            // 1. Update ReturnDate in IssuedBooks table
                            string updateIssueQuery = "UPDATE IssuedBooks SET ReturnDate = @ReturnDate WHERE IssueID = @IssueID";
                            using (SqlCommand updateIssueCmd = new SqlCommand(updateIssueQuery, con, transaction))
                            {
                                updateIssueCmd.Parameters.AddWithValue("@ReturnDate", DateTime.Today);
                                updateIssueCmd.Parameters.AddWithValue("@IssueID", issueId);
                                updateIssueCmd.ExecuteNonQuery();
                            }

                            // 2. Increment AvailableCopies in Books table
                            string updateBookQuery = "UPDATE Books SET AvailableCopies = AvailableCopies + 1 WHERE BookID = @BookID";
                            using (SqlCommand updateBookCmd = new SqlCommand(updateBookQuery, con, transaction))
                            {
                                updateBookCmd.Parameters.AddWithValue("@BookID", bookId);
                                updateBookCmd.ExecuteNonQuery();
                            }

                            transaction.Commit(); // Commit transaction if both operations succeed
                            this.DialogResult = DialogResult.OK; // Indicate success
                            this.Close();
                        }
                        catch (SqlException ex)
                        {
                            transaction.Rollback(); // Rollback on SQL error
                            MessageBox.Show($"Database error returning book: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine($"SQL Error: {ex.Message}\n{ex.StackTrace}");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); // Rollback on any other error
                            MessageBox.Show($"An unexpected error occurred returning book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine($"General Error: {ex.Message}\n{ex.StackTrace}");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an issued book to return.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvIssuedBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
