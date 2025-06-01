namespace librarymgtsystem
{
    partial class Mainwindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageBooks = new System.Windows.Forms.TabPage();
            this.displaybook = new System.Windows.Forms.DataGridView();
            this.logout = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.refresh = new System.Windows.Forms.Button();
            this.Edit = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.tabPageBorrowers = new System.Windows.Forms.TabPage();
            this.displayborrower = new System.Windows.Forms.DataGridView();
            this.refreshpage = new System.Windows.Forms.Button();
            this.returnbook = new System.Windows.Forms.Button();
            this.Issuebook = new System.Windows.Forms.Button();
            this.Deleteborrower = new System.Windows.Forms.Button();
            this.Editborrower = new System.Windows.Forms.Button();
            this.addbrower = new System.Windows.Forms.Button();
            this.tabControlMain.SuspendLayout();
            this.tabPageBooks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displaybook)).BeginInit();
            this.tabPageBorrowers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayborrower)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageBooks);
            this.tabControlMain.Controls.Add(this.tabPageBorrowers);
            this.tabControlMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(800, 457);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabPageBooks
            // 
            this.tabPageBooks.Controls.Add(this.displaybook);
            this.tabPageBooks.Controls.Add(this.logout);
            this.tabPageBooks.Controls.Add(this.delete);
            this.tabPageBooks.Controls.Add(this.refresh);
            this.tabPageBooks.Controls.Add(this.Edit);
            this.tabPageBooks.Controls.Add(this.add);
            this.tabPageBooks.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageBooks.Location = new System.Drawing.Point(4, 34);
            this.tabPageBooks.Name = "tabPageBooks";
            this.tabPageBooks.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBooks.Size = new System.Drawing.Size(792, 419);
            this.tabPageBooks.TabIndex = 0;
            this.tabPageBooks.Text = "Books mgt";
            this.tabPageBooks.UseVisualStyleBackColor = true;
            this.tabPageBooks.Click += new System.EventHandler(this.tabPageBooks_Click);
            // 
            // displaybook
            // 
            this.displaybook.BackgroundColor = System.Drawing.SystemColors.Info;
            this.displaybook.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.displaybook.Location = new System.Drawing.Point(17, 50);
            this.displaybook.Name = "displaybook";
            this.displaybook.Size = new System.Drawing.Size(775, 366);
            this.displaybook.TabIndex = 5;
            this.displaybook.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.displaybook_CellContentClick_1);
            // 
            // logout
            // 
            this.logout.Location = new System.Drawing.Point(542, 7);
            this.logout.Name = "logout";
            this.logout.Size = new System.Drawing.Size(81, 37);
            this.logout.TabIndex = 4;
            this.logout.Text = "logout";
            this.logout.UseVisualStyleBackColor = true;
            this.logout.Click += new System.EventHandler(this.display_Click);
            // 
            // delete
            // 
            this.delete.Location = new System.Drawing.Point(384, 7);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(102, 37);
            this.delete.TabIndex = 3;
            this.delete.Text = "Delete book";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click_1);
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(243, 7);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(87, 37);
            this.refresh.TabIndex = 2;
            this.refresh.Text = "Refresh";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_Click_1);
            // 
            // Edit
            // 
            this.Edit.Location = new System.Drawing.Point(122, 7);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(89, 37);
            this.Edit.TabIndex = 1;
            this.Edit.Text = "Edit book";
            this.Edit.UseVisualStyleBackColor = true;
            this.Edit.Click += new System.EventHandler(this.Edit_Click_1);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(17, 7);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(83, 37);
            this.add.TabIndex = 0;
            this.add.Text = "Add book";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click_1);
            // 
            // tabPageBorrowers
            // 
            this.tabPageBorrowers.Controls.Add(this.displayborrower);
            this.tabPageBorrowers.Controls.Add(this.refreshpage);
            this.tabPageBorrowers.Controls.Add(this.returnbook);
            this.tabPageBorrowers.Controls.Add(this.Issuebook);
            this.tabPageBorrowers.Controls.Add(this.Deleteborrower);
            this.tabPageBorrowers.Controls.Add(this.Editborrower);
            this.tabPageBorrowers.Controls.Add(this.addbrower);
            this.tabPageBorrowers.Location = new System.Drawing.Point(4, 34);
            this.tabPageBorrowers.Name = "tabPageBorrowers";
            this.tabPageBorrowers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBorrowers.Size = new System.Drawing.Size(792, 419);
            this.tabPageBorrowers.TabIndex = 1;
            this.tabPageBorrowers.Text = "Borrowers mgt";
            this.tabPageBorrowers.UseVisualStyleBackColor = true;
            this.tabPageBorrowers.Click += new System.EventHandler(this.tabPageBorrowers_Click);
            // 
            // displayborrower
            // 
            this.displayborrower.BackgroundColor = System.Drawing.SystemColors.Info;
            this.displayborrower.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.displayborrower.Location = new System.Drawing.Point(0, 46);
            this.displayborrower.Name = "displayborrower";
            this.displayborrower.Size = new System.Drawing.Size(784, 373);
            this.displayborrower.TabIndex = 6;
            this.displayborrower.SelectionChanged += new System.EventHandler(this.SelectedRows);
            // 
            // refreshpage
            // 
            this.refreshpage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshpage.Location = new System.Drawing.Point(707, 7);
            this.refreshpage.Name = "refreshpage";
            this.refreshpage.Size = new System.Drawing.Size(77, 33);
            this.refreshpage.TabIndex = 5;
            this.refreshpage.Text = "Refresh";
            this.refreshpage.UseVisualStyleBackColor = true;
            this.refreshpage.Click += new System.EventHandler(this.refreshpage_Click);
            // 
            // returnbook
            // 
            this.returnbook.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.returnbook.Location = new System.Drawing.Point(573, 7);
            this.returnbook.Name = "returnbook";
            this.returnbook.Size = new System.Drawing.Size(118, 33);
            this.returnbook.TabIndex = 4;
            this.returnbook.Text = "Return book";
            this.returnbook.UseVisualStyleBackColor = true;
            this.returnbook.Click += new System.EventHandler(this.returnbook_Click);
            // 
            // Issuebook
            // 
            this.Issuebook.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Issuebook.Location = new System.Drawing.Point(454, 7);
            this.Issuebook.Name = "Issuebook";
            this.Issuebook.Size = new System.Drawing.Size(113, 33);
            this.Issuebook.TabIndex = 3;
            this.Issuebook.Text = "Issue book";
            this.Issuebook.UseVisualStyleBackColor = true;
            this.Issuebook.Click += new System.EventHandler(this.Issuebook_Click);
            // 
            // Deleteborrower
            // 
            this.Deleteborrower.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Deleteborrower.Location = new System.Drawing.Point(303, 7);
            this.Deleteborrower.Name = "Deleteborrower";
            this.Deleteborrower.Size = new System.Drawing.Size(145, 33);
            this.Deleteborrower.TabIndex = 2;
            this.Deleteborrower.Text = "Delete borrower";
            this.Deleteborrower.UseVisualStyleBackColor = true;
            this.Deleteborrower.Click += new System.EventHandler(this.Deleteborrower_Click);
            // 
            // Editborrower
            // 
            this.Editborrower.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Editborrower.Location = new System.Drawing.Point(133, 7);
            this.Editborrower.Name = "Editborrower";
            this.Editborrower.Size = new System.Drawing.Size(164, 33);
            this.Editborrower.TabIndex = 1;
            this.Editborrower.Text = "Edit borrower";
            this.Editborrower.UseVisualStyleBackColor = true;
            this.Editborrower.Click += new System.EventHandler(this.Editborrower_Click);
            // 
            // addbrower
            // 
            this.addbrower.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addbrower.Location = new System.Drawing.Point(9, 7);
            this.addbrower.Name = "addbrower";
            this.addbrower.Size = new System.Drawing.Size(125, 33);
            this.addbrower.TabIndex = 0;
            this.addbrower.Text = "Addborrower";
            this.addbrower.UseVisualStyleBackColor = true;
            this.addbrower.Click += new System.EventHandler(this.addbrower_Click);
            // 
            // Mainwindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Mainwindow";
            this.Text = "Managenentsection";
            this.TransparencyKey = System.Drawing.Color.Cyan;
            this.Load += new System.EventHandler(this.Mainwindow_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageBooks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.displaybook)).EndInit();
            this.tabPageBorrowers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.displayborrower)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageBooks;
        private System.Windows.Forms.Button logout;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.Button Edit;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.TabPage tabPageBorrowers;
        private System.Windows.Forms.DataGridView displaybook;
        private System.Windows.Forms.Button refreshpage;
        private System.Windows.Forms.Button returnbook;
        private System.Windows.Forms.Button Issuebook;
        private System.Windows.Forms.Button Deleteborrower;
        private System.Windows.Forms.Button Editborrower;
        private System.Windows.Forms.Button addbrower;
        private System.Windows.Forms.DataGridView displayborrower;
    }
}