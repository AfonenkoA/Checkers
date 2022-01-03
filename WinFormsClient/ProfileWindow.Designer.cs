namespace WinFormsClient
{
    partial class ProfileWindow
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
            this.avatarPictureBox = new System.Windows.Forms.PictureBox();
            this.NickLabel = new System.Windows.Forms.Label();
            this.RatingLabel = new System.Windows.Forms.Label();
            this.lastActivityLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.AchivementsLabel = new System.Windows.Forms.Label();
            this.RecentMatchesLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.avatarPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // avatarPictureBox
            // 
            this.avatarPictureBox.Location = new System.Drawing.Point(12, 12);
            this.avatarPictureBox.Name = "avatarPictureBox";
            this.avatarPictureBox.Size = new System.Drawing.Size(126, 120);
            this.avatarPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.avatarPictureBox.TabIndex = 4;
            this.avatarPictureBox.TabStop = false;
            // 
            // NickLabel
            // 
            this.NickLabel.AutoSize = true;
            this.NickLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NickLabel.Location = new System.Drawing.Point(189, 18);
            this.NickLabel.Name = "NickLabel";
            this.NickLabel.Size = new System.Drawing.Size(96, 25);
            this.NickLabel.TabIndex = 5;
            this.NickLabel.Text = "Nickname";
            // 
            // RatingLabel
            // 
            this.RatingLabel.AutoSize = true;
            this.RatingLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RatingLabel.Location = new System.Drawing.Point(189, 43);
            this.RatingLabel.Name = "RatingLabel";
            this.RatingLabel.Size = new System.Drawing.Size(113, 25);
            this.RatingLabel.TabIndex = 5;
            this.RatingLabel.Text = "SocialCredit";
            // 
            // lastActivityLabel
            // 
            this.lastActivityLabel.AutoSize = true;
            this.lastActivityLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lastActivityLabel.Location = new System.Drawing.Point(189, 68);
            this.lastActivityLabel.Name = "lastActivityLabel";
            this.lastActivityLabel.Size = new System.Drawing.Size(83, 25);
            this.lastActivityLabel.TabIndex = 5;
            this.lastActivityLabel.Text = "Matches";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 183);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(776, 100);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // AchivementsLabel
            // 
            this.AchivementsLabel.AutoSize = true;
            this.AchivementsLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.AchivementsLabel.Location = new System.Drawing.Point(12, 135);
            this.AchivementsLabel.Name = "AchivementsLabel";
            this.AchivementsLabel.Size = new System.Drawing.Size(201, 45);
            this.AchivementsLabel.TabIndex = 6;
            this.AchivementsLabel.Text = "Achivements";
            // 
            // RecentMatchesLabel
            // 
            this.RecentMatchesLabel.AutoSize = true;
            this.RecentMatchesLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.RecentMatchesLabel.Location = new System.Drawing.Point(12, 286);
            this.RecentMatchesLabel.Name = "RecentMatchesLabel";
            this.RecentMatchesLabel.Size = new System.Drawing.Size(243, 45);
            this.RecentMatchesLabel.TabIndex = 6;
            this.RecentMatchesLabel.Text = "Recent matches";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 334);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(776, 100);
            this.flowLayoutPanel2.TabIndex = 7;
            // 
            // ProfileWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.RecentMatchesLabel);
            this.Controls.Add(this.AchivementsLabel);
            this.Controls.Add(this.lastActivityLabel);
            this.Controls.Add(this.RatingLabel);
            this.Controls.Add(this.NickLabel);
            this.Controls.Add(this.avatarPictureBox);
            this.Name = "ProfileWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProfileWindow";
            ((System.ComponentModel.ISupportInitialize)(this.avatarPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private PictureBox avatarPictureBox;
        private Label NickLabel;
        private Label RatingLabel;
        private Label lastActivityLabel;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label AchivementsLabel;
        private Label RecentMatchesLabel;
        private FlowLayoutPanel flowLayoutPanel2;
    }
}