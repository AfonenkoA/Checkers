using System.ComponentModel;
using Checkers.Api.Interface;
using Checkers.Api.WebImplementation;

namespace WinFormsClient
{
    partial class MainMenuWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.LogOutButton = new Button();
            this.pictureBox1 = new PictureBox();
            this.PlayButton = new Button();
            this.CollectionButton = new Button();
            this.ShopButton = new Button();
            this.ProfileButton = new Button();
            this.FriendsButton = new Button();
            ((ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // LogOutButton
            // 
            this.LogOutButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            this.LogOutButton.Location = new Point(382, 12);
            this.LogOutButton.Name = "LogOutButton";
            this.LogOutButton.Size = new Size(90, 37);
            this.LogOutButton.TabIndex = 0;
            this.LogOutButton.Text = "Log Out";
            this.LogOutButton.UseVisualStyleBackColor = true;
            this.LogOutButton.Click += new EventHandler(this.LogOutButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Point(219, 92);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(262, 357);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // PlayButton
            // 
            this.PlayButton.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            this.PlayButton.Location = new Point(12, 125);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new Size(158, 37);
            this.PlayButton.TabIndex = 2;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new EventHandler(this.PlayButton_Click);
            // 
            // CollectionButton
            // 
            this.CollectionButton.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            this.CollectionButton.Location = new Point(12, 168);
            this.CollectionButton.Name = "CollectionButton";
            this.CollectionButton.Size = new Size(158, 37);
            this.CollectionButton.TabIndex = 2;
            this.CollectionButton.Text = "Collcetion";
            this.CollectionButton.UseVisualStyleBackColor = true;
            this.CollectionButton.Click += new EventHandler(this.CollectionButton_Click);
            // 
            // ShopButton
            // 
            this.ShopButton.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            this.ShopButton.Location = new Point(12, 211);
            this.ShopButton.Name = "ShopButton";
            this.ShopButton.Size = new Size(158, 37);
            this.ShopButton.TabIndex = 2;
            this.ShopButton.Text = "Shop";
            this.ShopButton.UseVisualStyleBackColor = true;
            this.ShopButton.Click += new EventHandler(this.ShopButton_Click);
            // 
            // ProfileButton
            // 
            this.ProfileButton.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            this.ProfileButton.Location = new Point(12, 254);
            this.ProfileButton.Name = "ProfileButton";
            this.ProfileButton.Size = new Size(158, 37);
            this.ProfileButton.TabIndex = 2;
            this.ProfileButton.Text = "Profile";
            this.ProfileButton.UseVisualStyleBackColor = true;
            this.ProfileButton.Click += new EventHandler(this.ProfileButton_Click);
            // 
            // FriendsButton
            // 
            this.FriendsButton.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            this.FriendsButton.Location = new Point(12, 297);
            this.FriendsButton.Name = "FriendsButton";
            this.FriendsButton.Size = new Size(158, 37);
            this.FriendsButton.TabIndex = 2;
            this.FriendsButton.Text = "Friends";
            this.FriendsButton.UseVisualStyleBackColor = true;
            this.FriendsButton.Click += new EventHandler(this.FriendsButton_Click);
            // 
            // MainMenuWindow
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(484, 461);
            this.Controls.Add(this.FriendsButton);
            this.Controls.Add(this.ProfileButton);
            this.Controls.Add(this.ShopButton);
            this.Controls.Add(this.CollectionButton);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.LogOutButton);
            this.Name = "MainMenuWindow";
            this.Text = "MainMenuWindow";
            this.FormClosed += new FormClosedEventHandler(this.MainMenuWindow_FormClosed);
            ((ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button LogOutButton;
        private PictureBox pictureBox1;
        private Button PlayButton;
        private Button CollectionButton;
        private Button ShopButton;
        private Button ProfileButton;
        private Button FriendsButton;
    }
}