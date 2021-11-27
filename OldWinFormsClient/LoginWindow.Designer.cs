﻿
namespace OldWinFormsClient
{
    sealed partial class LoginWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AuthorizationLabel = new System.Windows.Forms.Label();
            this.LoginTextBox = new System.Windows.Forms.TextBox();
            this.LoginLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.EnterButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AuthorizationLabel
            // 
            this.AuthorizationLabel.AutoSize = true;
            this.AuthorizationLabel.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AuthorizationLabel.Location = new System.Drawing.Point(46, 36);
            this.AuthorizationLabel.Name = "AuthorizationLabel";
            this.AuthorizationLabel.Size = new System.Drawing.Size(190, 40);
            this.AuthorizationLabel.TabIndex = 0;
            this.AuthorizationLabel.Text = "Authorization";
            // 
            // LoginTextBox
            // 
            this.LoginTextBox.Location = new System.Drawing.Point(111, 87);
            this.LoginTextBox.Name = "LoginTextBox";
            this.LoginTextBox.Size = new System.Drawing.Size(88, 25);
            this.LoginTextBox.TabIndex = 1;
            // 
            // LoginLabel
            // 
            this.LoginLabel.AutoSize = true;
            this.LoginLabel.Location = new System.Drawing.Point(47, 89);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(40, 17);
            this.LoginLabel.TabIndex = 2;
            this.LoginLabel.Text = "Login";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(47, 126);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(64, 17);
            this.PasswordLabel.TabIndex = 3;
            this.PasswordLabel.Text = "Password";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(111, 123);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(88, 25);
            this.PasswordTextBox.TabIndex = 4;
            // 
            // EnterButton
            // 
            this.EnterButton.Location = new System.Drawing.Point(92, 163);
            this.EnterButton.Name = "EnterButton";
            this.EnterButton.Size = new System.Drawing.Size(66, 21);
            this.EnterButton.TabIndex = 5;
            this.EnterButton.Text = "Enter";
            this.EnterButton.UseVisualStyleBackColor = true;
            this.EnterButton.Click += new System.EventHandler(this.EnterButton_Click);
            // 
            // LoginWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 207);
            this.Controls.Add(this.EnterButton);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.LoginLabel);
            this.Controls.Add(this.LoginTextBox);
            this.Controls.Add(this.AuthorizationLabel);
            this.Name = "LoginWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Checkers";
            this.Load += new System.EventHandler(this.LoginWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AuthorizationLabel;
        private System.Windows.Forms.TextBox LoginTextBox;
        private System.Windows.Forms.Label LoginLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Button EnterButton;
    }
}
