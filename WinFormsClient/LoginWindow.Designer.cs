namespace WinFormsClient
{
    partial class LoginWindow
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
            this.TitleLabel = new System.Windows.Forms.Label();
            this.AuthorisationLabel = new System.Windows.Forms.Label();
            this.LoginLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.LoginTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.EnterButton = new System.Windows.Forms.Button();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.Font = new System.Drawing.Font("Segoe UI", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.TitleLabel.Location = new System.Drawing.Point(93, 55);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(328, 153);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Checkers Online";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AuthorisationLabel
            // 
            this.AuthorisationLabel.AutoSize = true;
            this.AuthorisationLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.AuthorisationLabel.Location = new System.Drawing.Point(144, 208);
            this.AuthorisationLabel.Name = "AuthorisationLabel";
            this.AuthorisationLabel.Size = new System.Drawing.Size(216, 45);
            this.AuthorisationLabel.TabIndex = 1;
            this.AuthorisationLabel.Text = "Authorisation";
            // 
            // LoginLabel
            // 
            this.LoginLabel.AutoSize = true;
            this.LoginLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LoginLabel.Location = new System.Drawing.Point(93, 277);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(55, 25);
            this.LoginLabel.TabIndex = 2;
            this.LoginLabel.Text = "login";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PasswordLabel.Location = new System.Drawing.Point(56, 326);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(92, 25);
            this.PasswordLabel.TabIndex = 3;
            this.PasswordLabel.Text = "password";
            // 
            // LoginTextBox
            // 
            this.LoginTextBox.Location = new System.Drawing.Point(167, 282);
            this.LoginTextBox.Name = "LoginTextBox";
            this.LoginTextBox.Size = new System.Drawing.Size(193, 23);
            this.LoginTextBox.TabIndex = 4;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(167, 328);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(193, 23);
            this.PasswordTextBox.TabIndex = 5;
            // 
            // EnterButton
            // 
            this.EnterButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EnterButton.Location = new System.Drawing.Point(213, 362);
            this.EnterButton.Name = "EnterButton";
            this.EnterButton.Size = new System.Drawing.Size(100, 36);
            this.EnterButton.TabIndex = 6;
            this.EnterButton.Text = "Enter";
            this.EnterButton.UseVisualStyleBackColor = true;
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Location = new System.Drawing.Point(213, 418);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(0, 15);
            this.ErrorLabel.TabIndex = 7;
            // 
            // LoginWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.EnterButton);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.LoginTextBox);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.LoginLabel);
            this.Controls.Add(this.AuthorisationLabel);
            this.Controls.Add(this.TitleLabel);
            this.Name = "LoginWindow";
            this.Text = "LoginWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label TitleLabel;
        private Label AuthorisationLabel;
        private Label LoginLabel;
        private Label PasswordLabel;
        private TextBox LoginTextBox;
        private TextBox PasswordTextBox;
        private Button EnterButton;
        private Label ErrorLabel;
    }
}