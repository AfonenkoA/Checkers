
namespace OldWinFormsClient
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
            this.GameTitleLabel = new System.Windows.Forms.Label();
            this.PlayButton = new System.Windows.Forms.Button();
            this.AchievementsButton = new System.Windows.Forms.Button();
            this.ShopButton = new System.Windows.Forms.Button();
            this.ProfileButton = new System.Windows.Forms.Button();
            this.CollectionButton = new System.Windows.Forms.Button();
            this.ReplaysButton = new System.Windows.Forms.Button();
            this.FriendsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GameTitleLabel
            // 
            this.GameTitleLabel.AutoSize = true;
            this.GameTitleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GameTitleLabel.Location = new System.Drawing.Point(41, 9);
            this.GameTitleLabel.Name = "GameTitleLabel";
            this.GameTitleLabel.Size = new System.Drawing.Size(188, 32);
            this.GameTitleLabel.TabIndex = 0;
            this.GameTitleLabel.Text = "Checkers Online";
            // 
            // PlayButton
            // 
            this.PlayButton.AutoSize = true;
            this.PlayButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PlayButton.Location = new System.Drawing.Point(41, 53);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(44, 29);
            this.PlayButton.TabIndex = 1;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // AchievementsButton
            // 
            this.AchievementsButton.AutoSize = true;
            this.AchievementsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AchievementsButton.Location = new System.Drawing.Point(41, 88);
            this.AchievementsButton.Name = "AchievementsButton";
            this.AchievementsButton.Size = new System.Drawing.Size(104, 29);
            this.AchievementsButton.TabIndex = 2;
            this.AchievementsButton.Text = "Achievements";
            this.AchievementsButton.UseVisualStyleBackColor = true;
            this.AchievementsButton.Click += new System.EventHandler(this.AchievementsButton_Click);
            // 
            // ShopButton
            // 
            this.ShopButton.AutoSize = true;
            this.ShopButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ShopButton.Location = new System.Drawing.Point(41, 158);
            this.ShopButton.Name = "ShopButton";
            this.ShopButton.Size = new System.Drawing.Size(50, 29);
            this.ShopButton.TabIndex = 3;
            this.ShopButton.Text = "Shop";
            this.ShopButton.UseVisualStyleBackColor = true;
            this.ShopButton.Click += new System.EventHandler(this.ShopButton_Click);
            // 
            // ProfileButton
            // 
            this.ProfileButton.AutoSize = true;
            this.ProfileButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ProfileButton.Location = new System.Drawing.Point(41, 193);
            this.ProfileButton.Name = "ProfileButton";
            this.ProfileButton.Size = new System.Drawing.Size(57, 29);
            this.ProfileButton.TabIndex = 4;
            this.ProfileButton.Text = "Profile";
            this.ProfileButton.UseVisualStyleBackColor = true;
            this.ProfileButton.Click += new System.EventHandler(this.ProfileButton_Click);
            // 
            // CollectionButton
            // 
            this.CollectionButton.AutoSize = true;
            this.CollectionButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CollectionButton.Location = new System.Drawing.Point(41, 123);
            this.CollectionButton.Name = "CollectionButton";
            this.CollectionButton.Size = new System.Drawing.Size(79, 29);
            this.CollectionButton.TabIndex = 5;
            this.CollectionButton.Text = "Collection";
            this.CollectionButton.UseVisualStyleBackColor = true;
            this.CollectionButton.Click += new System.EventHandler(this.CollectionButton_Click);
            // 
            // ReplaysButton
            // 
            this.ReplaysButton.AutoSize = true;
            this.ReplaysButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ReplaysButton.Location = new System.Drawing.Point(41, 263);
            this.ReplaysButton.Name = "ReplaysButton";
            this.ReplaysButton.Size = new System.Drawing.Size(65, 29);
            this.ReplaysButton.TabIndex = 6;
            this.ReplaysButton.Text = "Replays";
            this.ReplaysButton.UseVisualStyleBackColor = true;
            // 
            // FriendsButton
            // 
            this.FriendsButton.AutoSize = true;
            this.FriendsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FriendsButton.Location = new System.Drawing.Point(41, 228);
            this.FriendsButton.Name = "FriendsButton";
            this.FriendsButton.Size = new System.Drawing.Size(63, 29);
            this.FriendsButton.TabIndex = 7;
            this.FriendsButton.Text = "Friends";
            this.FriendsButton.UseVisualStyleBackColor = true;
            this.FriendsButton.Click += new System.EventHandler(this.FriendsButton_Click);
            // 
            // MainMenuWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 315);
            this.Controls.Add(this.FriendsButton);
            this.Controls.Add(this.ReplaysButton);
            this.Controls.Add(this.CollectionButton);
            this.Controls.Add(this.ProfileButton);
            this.Controls.Add(this.ShopButton);
            this.Controls.Add(this.AchievementsButton);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.GameTitleLabel);
            this.Name = "MainMenuWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainMenuWindow_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GameTitleLabel;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Button AchievementsButton;
        private System.Windows.Forms.Button ShopButton;
        private System.Windows.Forms.Button ProfileButton;
        private System.Windows.Forms.Button CollectionButton;
        private System.Windows.Forms.Button ReplaysButton;
        private System.Windows.Forms.Button FriendsButton;
    }
}