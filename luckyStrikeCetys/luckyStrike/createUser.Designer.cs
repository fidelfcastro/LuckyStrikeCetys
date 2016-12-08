namespace luckyStrike
{
    partial class createUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(createUser));
            this.NameTextbox = new System.Windows.Forms.TextBox();
            this.LastnameTextbox = new System.Windows.Forms.TextBox();
            this.UsernameTextbox = new System.Windows.Forms.TextBox();
            this.passwordTextbox = new System.Windows.Forms.TextBox();
            this.CreateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NameTextbox
            // 
            this.NameTextbox.Location = new System.Drawing.Point(287, 130);
            this.NameTextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NameTextbox.Name = "NameTextbox";
            this.NameTextbox.Size = new System.Drawing.Size(285, 22);
            this.NameTextbox.TabIndex = 5;
            // 
            // LastnameTextbox
            // 
            this.LastnameTextbox.Location = new System.Drawing.Point(287, 210);
            this.LastnameTextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LastnameTextbox.Name = "LastnameTextbox";
            this.LastnameTextbox.Size = new System.Drawing.Size(285, 22);
            this.LastnameTextbox.TabIndex = 6;
            // 
            // UsernameTextbox
            // 
            this.UsernameTextbox.Location = new System.Drawing.Point(287, 287);
            this.UsernameTextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UsernameTextbox.Name = "UsernameTextbox";
            this.UsernameTextbox.Size = new System.Drawing.Size(285, 22);
            this.UsernameTextbox.TabIndex = 7;
            // 
            // passwordTextbox
            // 
            this.passwordTextbox.Location = new System.Drawing.Point(287, 375);
            this.passwordTextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.passwordTextbox.Name = "passwordTextbox";
            this.passwordTextbox.PasswordChar = '*';
            this.passwordTextbox.Size = new System.Drawing.Size(285, 22);
            this.passwordTextbox.TabIndex = 8;
            // 
            // CreateButton
            // 
            this.CreateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateButton.ForeColor = System.Drawing.Color.Black;
            this.CreateButton.Location = new System.Drawing.Point(233, 459);
            this.CreateButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(191, 60);
            this.CreateButton.TabIndex = 9;
            this.CreateButton.Text = "CREATE";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // createUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(669, 535);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.passwordTextbox);
            this.Controls.Add(this.UsernameTextbox);
            this.Controls.Add(this.LastnameTextbox);
            this.Controls.Add(this.NameTextbox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "createUser";
            this.Text = "createUser";
            this.Load += new System.EventHandler(this.createUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox NameTextbox;
        private System.Windows.Forms.TextBox LastnameTextbox;
        private System.Windows.Forms.TextBox UsernameTextbox;
        private System.Windows.Forms.TextBox passwordTextbox;
        private System.Windows.Forms.Button CreateButton;
    }
}