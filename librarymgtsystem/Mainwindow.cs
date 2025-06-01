using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace librarymgtsystem
{
    public partial class Mainwindow : Form
    {
        private string connectionString;

        public Mainwindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["LibraryDbConnection"].ConnectionString;

            LoadBooksData();
            Edit.Enabled = false;
            delete.Enabled = false;
            displaybook.SelectionChanged += new EventHandler(displaybook_SelectionChanged);

            LoadBorrowersData();
            addbrower.Enabled = true;
            Editborrower.Enabled = false;
            Deleteborrower.Enabled = false;
            Issuebook.Enabled = false;
            returnbook.Enabled = false;
            refreshpage.Enabled = true;

            displayborrower.SelectionChanged += new EventHandler(displayborrower_SelectionChanged);

            if (this.Controls.ContainsKey("tabControlMain") && this.tabControlMain is TabControl)
            {
                this.tabControlMain.SelectedIndexChanged += new EventHandler(tabControlMain_SelectedIndexChanged);
            }
        }

        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab != null)
            {
                if (tabControlMain.SelectedTab.Name == "tabPageBooks")
                {
                    LoadBooksData();
                }
                else if (tabControlMain.SelectedTab.Name == "tabPageBorrowers")
                {
                    LoadBorrowersData();
                }
            }
        }

        private void LoadBooksData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT BookID, Title, Author, Year, AvailableCopies, TotalCopies FROM Books";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();

                try
                {
                    con.Open();
                    da.Fill(dt);
                    displaybook.DataSource = dt;
                    displaybook.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                    Edit.Enabled = displaybook.SelectedRows.Count > 0;
                    delete.Enabled = displaybook.SelectedRows.Count > 0;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error loading books: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"SQL Error: {ex.Message}\n{ex.StackTrace}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred loading books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"General Error: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        private void displaybook_SelectionChanged(object sender, EventArgs e)
        {
            Edit.Enabled = displaybook.SelectedRows.Count > 0;
            delete.Enabled = displaybook.SelectedRows.Count > 0;
        }

        private void LoadBorrowersData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT BorrowerID, Name, Email, Phone FROM Borrowers";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();

                try
                {
                    con.Open();
                    da.Fill(dt);

                    displayborrower.DataSource = dt;

                    displayborrower.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                    Editborrower.Enabled = displayborrower.SelectedRows.Count > 0;
                    Deleteborrower.Enabled = displayborrower.SelectedRows.Count > 0;
                    Issuebook.Enabled = displayborrower.SelectedRows.Count > 0;

                    returnbook.Enabled = true;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error loading borrowers: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"SQL Error: {ex.Message}\n{ex.StackTrace}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred loading borrowers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"General Error: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        private void addbrower_Click(object sender, EventArgs e)
        {
            BorrowerDetailsForm borrowerDetailsForm = new BorrowerDetailsForm();
            if (borrowerDetailsForm.ShowDialog() == DialogResult.OK)
            {
                LoadBorrowersData();
                MessageBox.Show("Borrower added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Editborrower_Click(object sender, EventArgs e)
        {

            if (displayborrower.SelectedRows.Count > 0)
            {

                int borrowerId = Convert.ToInt32(displayborrower.SelectedRows[0].Cells["BorrowerID"].Value);
                BorrowerDetailsForm borrowerDetailsForm = new BorrowerDetailsForm(borrowerId);

                if (borrowerDetailsForm.ShowDialog() == DialogResult.OK)
                {
                    LoadBorrowersData();
                    MessageBox.Show("Borrower updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a borrower to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Deleteborrower_Click(object sender, EventArgs e)
        {

            if (displayborrower.SelectedRows.Count > 0)
            {

                int borrowerId = Convert.ToInt32(displayborrower.SelectedRows[0].Cells["BorrowerID"].Value);
                string borrowerName = displayborrower.SelectedRows[0].Cells["Name"].Value.ToString();


                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string checkIssuedQuery = "SELECT COUNT(1) FROM IssuedBooks WHERE BorrowerID = @BorrowerID AND ReturnDate IS NULL";
                    using (SqlCommand checkCmd = new SqlCommand(checkIssuedQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@BorrowerID", borrowerId);
                        try
                        {
                            con.Open();
                            int issuedBooksCount = (int)checkCmd.ExecuteScalar();
                            if (issuedBooksCount > 0)
                            {
                                MessageBox.Show($"Cannot delete '{borrowerName}'. This borrower has {issuedBooksCount} book(s) currently issued. All books must be returned first.", "Deletion Restricted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show($"Database error checking issued books: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine($"SQL Error: {ex.Message}\n{ex.StackTrace}");
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An unexpected error occurred checking issued books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine($"General Error: {ex.Message}\n{ex.StackTrace}");
                            return;
                        }
                    }
                }


                DialogResult confirmResult = MessageBox.Show(
                    $"Are you sure you want to delete the borrower '{borrowerName}'? This action cannot be undone.",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM Borrowers WHERE BorrowerID = @BorrowerID";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@BorrowerID", borrowerId);
                            try
                            {
                                con.Open();
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Borrower deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadBorrowersData();
                                }
                                else
                                {
                                    MessageBox.Show("Borrower not found or could not be deleted.", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            catch (SqlException ex)
                            {
                                MessageBox.Show($"Database error deleting borrower: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Console.WriteLine($"SQL Error: {ex.Message}\n{ex.StackTrace}");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"An unexpected error occurred deleting borrower: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Console.WriteLine($"General Error: {ex.Message}\n{ex.StackTrace}");
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a borrower to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Issuebook_Click(object sender, EventArgs e)
        {

            if (displayborrower.SelectedRows.Count > 0)
            {

                int borrowerId = Convert.ToInt32(displayborrower.SelectedRows[0].Cells["BorrowerID"].Value);
                string borrowerName = displayborrower.SelectedRows[0].Cells["Name"].Value.ToString();

                IssueBookForm issueForm = new IssueBookForm(borrowerId, borrowerName);
                if (issueForm.ShowDialog() == DialogResult.OK)
                {
                    LoadBorrowersData();
                    LoadBooksData();
                    MessageBox.Show("Book issued successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a borrower to issue a book to.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void returnbook_Click(object sender, EventArgs e)
        {

            ReturnBookForm returnForm = new ReturnBookForm();
            if (returnForm.ShowDialog() == DialogResult.OK)
            {
                LoadBorrowersData();
                LoadBooksData();
                MessageBox.Show("Book returned successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void refreshpage_Click(object sender, EventArgs e)
        {
            LoadBorrowersData();
            MessageBox.Show("Borrower list refreshed.", "Refreshed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void displayborrower_SelectionChanged(object sender, EventArgs e)
        {
           
            Editborrower.Enabled = displayborrower.SelectedRows.Count > 0;
            Deleteborrower.Enabled = displayborrower.SelectedRows.Count > 0;
            Issuebook.Enabled = displayborrower.SelectedRows.Count > 0;

        }

        private void displaybook_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void display_Click_1(object sender, EventArgs e) { }


        private void tabPageBorrowers_Click(object sender, EventArgs e) { }

        private void tabPageBooks_Click(object sender, EventArgs e) { }
        private void displaybook_CellContentClick_1(object sender, DataGridViewCellEventArgs e) { }
        private void SelectedRows(object sender, EventArgs e)
        {

        }

        private void add_Click_1(object sender, EventArgs e)
        {

            BookDetailsForm bookDetailsForm = new BookDetailsForm();
            if (bookDetailsForm.ShowDialog() == DialogResult.OK)
            {
                LoadBooksData();
                MessageBox.Show("Book added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Edit_Click_1(object sender, EventArgs e)
        {

            if (displaybook.SelectedRows.Count > 0)
            {
                int bookId = Convert.ToInt32(displaybook.SelectedRows[0].Cells["BookID"].Value);
                BookDetailsForm bookDetailsForm = new BookDetailsForm(bookId);

                if (bookDetailsForm.ShowDialog() == DialogResult.OK)
                {
                    LoadBooksData();
                    MessageBox.Show("Book updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a book to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void refresh_Click_1(object sender, EventArgs e)
        {

            LoadBooksData();
            MessageBox.Show("Book list refreshed.", "Refreshed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void delete_Click_1(object sender, EventArgs e)
        {

            if (displaybook.SelectedRows.Count > 0)
            {
                int bookId = Convert.ToInt32(displaybook.SelectedRows[0].Cells["BookID"].Value);
                string bookTitle = displaybook.SelectedRows[0].Cells["Title"].Value.ToString();
                int availableCopies = Convert.ToInt32(displaybook.SelectedRows[0].Cells["AvailableCopies"].Value);
                int totalCopies = Convert.ToInt32(displaybook.SelectedRows[0].Cells["TotalCopies"].Value);

                if (availableCopies < totalCopies)
                {
                    MessageBox.Show("Cannot delete this book. There are still copies currently issued. Return all copies first.", "Deletion Restricted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirmResult = MessageBox.Show(
                    $"Are you sure you want to delete the book '{bookTitle}'? This action cannot be undone.",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM Books WHERE BookID = @BookID";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@BookID", bookId);
                            try
                            {
                                con.Open();
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Book deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadBooksData();
                                }
                                else
                                {
                                    MessageBox.Show("Book not found or could not be deleted.", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            catch (SqlException ex)
                            {
                                MessageBox.Show($"Database error deleting book: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Console.WriteLine($"SQL Error: {ex.Message}\n{ex.StackTrace}");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"An unexpected error occurred deleting book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Console.WriteLine($"General Error: {ex.Message}\n{ex.StackTrace}");
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a book to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void display_Click(object sender, EventArgs e)
        {
            login loginForm = new login();
            this.Hide(); // Hides the current Mainwindow
            loginForm.Show(); // Shows the login form
        }

        private void Mainwindow_Load(object sender, EventArgs e)
        {

        }
    }
}