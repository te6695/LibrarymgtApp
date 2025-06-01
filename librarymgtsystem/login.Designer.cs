namespace librarymgtsystem
{
    partial class login
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
            this.btnlogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.uname = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.exite = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnlogin
            // 
            this.btnlogin.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnlogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlogin.Location = new System.Drawing.Point(339, 282);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(104, 60);
            this.btnlogin.TabIndex = 0;
            this.btnlogin.Text = "login";
            this.btnlogin.UseVisualStyleBackColor = false;
            this.btnlogin.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(128, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 36);
            this.label1.TabIndex = 1;
            this.label1.Text = "username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(129, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 36);
            this.label2.TabIndex = 2;
            this.label2.Text = "password";
            // 
            // uname
            // 
            this.uname.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.uname.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uname.Location = new System.Drawing.Point(416, 59);
            this.uname.Multiline = true;
            this.uname.Name = "uname";
            this.uname.Size = new System.Drawing.Size(154, 62);
            this.uname.TabIndex = 3;
            this.uname.TextChanged += new System.EventHandler(this.uname_TextChanged);
            // 
            // pass
            // 
            this.pass.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pass.Location = new System.Drawing.Point(416, 159);
            this.pass.Multiline = true;
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(161, 56);
            this.pass.TabIndex = 4;
            this.pass.TextChanged += new System.EventHandler(this.pass_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(307, 242);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 36);
            this.label3.TabIndex = 5;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // exite
            // 
            this.exite.BackColor = System.Drawing.SystemColors.ControlLight;
            this.exite.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exite.ForeColor = System.Drawing.SystemColors.Desktop;
            this.exite.Location = new System.Drawing.Point(511, 274);
            this.exite.Name = "exite";
            this.exite.Size = new System.Drawing.Size(104, 77);
            this.exite.TabIndex = 6;
            this.exite.Text = "exit";
            this.exite.UseVisualStyleBackColor = false;
            this.exite.Click += new System.EventHandler(this.signup_Click);
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.BackgroundImage = global::librarymgtsystem.Properties.Resources.th;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.exite);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.uname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnlogin);
            this.Name = "login";
            this.Text = "library system";
            this.Load += new System.EventHandler(this.login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnlogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox uname;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button exite;
    }
}

