namespace WinFormsClient
{
    partial class MainMenuWindow
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
            this.LogOutButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.PlayButton = new System.Windows.Forms.Button();
            this.CollectionButton = new System.Windows.Forms.Button();
            this.ShopButton = new System.Windows.Forms.Button();
            this.ProfileButton = new System.Windows.Forms.Button();
            this.FriendsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // LogOutButton
            // 
            this.LogOutButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LogOutButton.Location = new System.Drawing.Point(382, 12);
            this.LogOutButton.Name = "LogOutButton";
            this.LogOutButton.Size = new System.Drawing.Size(90, 37);
            this.LogOutButton.TabIndex = 0;
            this.LogOutButton.Text = "Log Out";
            this.LogOutButton.UseVisualStyleBackColor = true;
            
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(219, 92);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(262, 357);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // PlayButton
            // 
            this.PlayButton.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PlayButton.Location = new System.Drawing.Point(12, 125);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(158, 37);
            this.PlayButton.TabIndex = 2;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            
            // 
            // CollectionButton
            // 
            this.CollectionButton.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CollectionButton.Location = new System.Drawing.Point(12, 168);
            this.CollectionButton.Name = "CollectionButton";
            this.CollectionButton.Size = new System.Drawing.Size(158, 37);
            this.CollectionButton.TabIndex = 2;
            this.CollectionButton.Text = "Collcetion";
            this.CollectionButton.UseVisualStyleBackColor = true;
            
            // 
            // ShopButton
            // 
            this.ShopButton.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ShopButton.Location = new System.Drawing.Point(12, 211);
            this.ShopButton.Name = "ShopButton";
            this.ShopButton.Size = new System.Drawing.Size(158, 37);
            this.ShopButton.TabIndex = 2;
            this.ShopButton.Text = "Shop";
            this.ShopButton.UseVisualStyleBackColor = true;
          
            // 
            // ProfileButton
            // 
            this.ProfileButton.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ProfileButton.Location = new System.Drawing.Point(12, 254);
            this.ProfileButton.Name = "ProfileButton";
            this.ProfileButton.Size = new System.Drawing.Size(158, 37);
            this.ProfileButton.TabIndex = 2;
            this.ProfileButton.Text = "Profile";
            this.ProfileButton.UseVisualStyleBackColor = true;
            
            // 
            // FriendsButton
            // 
            this.FriendsButton.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FriendsButton.Location = new System.Drawing.Point(12, 297);
            this.FriendsButton.Name = "FriendsButton";
            this.FriendsButton.Size = new System.Drawing.Size(158, 37);
            this.FriendsButton.TabIndex = 2;
            this.FriendsButton.Text = "Friends";
            this.FriendsButton.UseVisualStyleBackColor = true;
            
            // 
            // MainMenuWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.FriendsButton);
            this.Controls.Add(this.ProfileButton);
            this.Controls.Add(this.ShopButton);
            this.Controls.Add(this.CollectionButton);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.LogOutButton);
            this.Name = "MainMenuWindow";
            this.Text = "MainMenuWindow";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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