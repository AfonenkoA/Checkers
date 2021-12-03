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
            this.ReturnButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.NickLabel = new System.Windows.Forms.Label();
            this.RatingLabel = new System.Windows.Forms.Label();
            this.MatchesLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.AchivementsLabel = new System.Windows.Forms.Label();
            this.RecentMatchesLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ReturnButton
            // 
            this.ReturnButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ReturnButton.Location = new System.Drawing.Point(698, 14);
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.Size = new System.Drawing.Size(90, 42);
            this.ReturnButton.TabIndex = 3;
            this.ReturnButton.Text = "Return";
            this.ReturnButton.UseVisualStyleBackColor = true;
            this.ReturnButton.Click += new System.EventHandler(this.ReturnButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(126, 136);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // NickLabel
            // 
            this.NickLabel.AutoSize = true;
            this.NickLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NickLabel.Location = new System.Drawing.Point(189, 20);
            this.NickLabel.Name = "NickLabel";
            this.NickLabel.Size = new System.Drawing.Size(96, 25);
            this.NickLabel.TabIndex = 5;
            this.NickLabel.Text = "Nickname";
            // 
            // RatingLabel
            // 
            this.RatingLabel.AutoSize = true;
            this.RatingLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RatingLabel.Location = new System.Drawing.Point(189, 49);
            this.RatingLabel.Name = "RatingLabel";
            this.RatingLabel.Size = new System.Drawing.Size(113, 25);
            this.RatingLabel.TabIndex = 5;
            this.RatingLabel.Text = "SocialCredit";
            // 
            // MatchesLabel
            // 
            this.MatchesLabel.AutoSize = true;
            this.MatchesLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MatchesLabel.Location = new System.Drawing.Point(189, 77);
            this.MatchesLabel.Name = "MatchesLabel";
            this.MatchesLabel.Size = new System.Drawing.Size(83, 25);
            this.MatchesLabel.TabIndex = 5;
            this.MatchesLabel.Text = "Matches";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 207);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(776, 113);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // AchivementsLabel
            // 
            this.AchivementsLabel.AutoSize = true;
            this.AchivementsLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.AchivementsLabel.Location = new System.Drawing.Point(12, 153);
            this.AchivementsLabel.Name = "AchivementsLabel";
            this.AchivementsLabel.Size = new System.Drawing.Size(201, 45);
            this.AchivementsLabel.TabIndex = 6;
            this.AchivementsLabel.Text = "Achivements";
            // 
            // RecentMatchesLabel
            // 
            this.RecentMatchesLabel.AutoSize = true;
            this.RecentMatchesLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.RecentMatchesLabel.Location = new System.Drawing.Point(12, 324);
            this.RecentMatchesLabel.Name = "RecentMatchesLabel";
            this.RecentMatchesLabel.Size = new System.Drawing.Size(243, 45);
            this.RecentMatchesLabel.TabIndex = 6;
            this.RecentMatchesLabel.Text = "Recent matches";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 379);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(776, 113);
            this.flowLayoutPanel2.TabIndex = 7;
            // 
            // ProfileWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 510);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.RecentMatchesLabel);
            this.Controls.Add(this.AchivementsLabel);
            this.Controls.Add(this.MatchesLabel);
            this.Controls.Add(this.RatingLabel);
            this.Controls.Add(this.NickLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ReturnButton);
            this.Name = "ProfileWindow";
            this.Text = "ProfileWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProfileWindow_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button ReturnButton;
        private PictureBox pictureBox1;
        private Label NickLabel;
        private Label RatingLabel;
        private Label MatchesLabel;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label AchivementsLabel;
        private Label RecentMatchesLabel;
        private FlowLayoutPanel flowLayoutPanel2;
    }
}