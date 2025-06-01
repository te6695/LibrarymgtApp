namespace librarymgtsystem
{
    partial class ReturnBookForm
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
            this.dgvIssuedBooks = new System.Windows.Forms.DataGridView();
            this.btnReturnSelected = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIssuedBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvIssuedBooks
            // 
            this.dgvIssuedBooks.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dgvIssuedBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIssuedBooks.Location = new System.Drawing.Point(1, 56);
            this.dgvIssuedBooks.Name = "dgvIssuedBooks";
            this.dgvIssuedBooks.Size = new System.Drawing.Size(787, 365);
            this.dgvIssuedBooks.TabIndex = 0;
            this.dgvIssuedBooks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIssuedBooks_CellContentClick);
            // 
            // btnReturnSelected
            // 
            this.btnReturnSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturnSelected.Location = new System.Drawing.Point(22, 12);
            this.btnReturnSelected.Name = "btnReturnSelected";
            this.btnReturnSelected.Size = new System.Drawing.Size(141, 38);
            this.btnReturnSelected.TabIndex = 1;
            this.btnReturnSelected.Text = "ReturnSelected";
            this.btnReturnSelected.UseVisualStyleBackColor = true;
            this.btnReturnSelected.Click += new System.EventHandler(this.btnReturnSelected_Click_1);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(273, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(117, 38);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ReturnBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReturnSelected);
            this.Controls.Add(this.dgvIssuedBooks);
            this.Name = "ReturnBookForm";
            this.Text = "ReturnBookForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvIssuedBooks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvIssuedBooks;
        private System.Windows.Forms.Button btnReturnSelected;
        private System.Windows.Forms.Button btnCancel;
    }
}