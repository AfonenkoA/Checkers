
namespace WinFormsClient
{
    partial class AchievementsWindow
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
            this.AchievementsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.BackToMenuButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AchievementsPanel
            // 
            this.AchievementsPanel.AutoScroll = true;
            this.AchievementsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.AchievementsPanel.Location = new System.Drawing.Point(12, 12);
            this.AchievementsPanel.Name = "AchievementsPanel";
            this.AchievementsPanel.Size = new System.Drawing.Size(427, 137);
            this.AchievementsPanel.TabIndex = 0;
            this.AchievementsPanel.WrapContents = false;
            // 
            // BackToMenuButton
            // 
            this.BackToMenuButton.AutoSize = true;
            this.BackToMenuButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackToMenuButton.Location = new System.Drawing.Point(171, 155);
            this.BackToMenuButton.Name = "BackToMenuButton";
            this.BackToMenuButton.Size = new System.Drawing.Size(55, 29);
            this.BackToMenuButton.TabIndex = 1;
            this.BackToMenuButton.Text = "Menu";
            this.BackToMenuButton.UseVisualStyleBackColor = true;
            this.BackToMenuButton.Click += new System.EventHandler(this.BackToMenuButton_Click);
            // 
            // AchievementsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 196);
            this.Controls.Add(this.BackToMenuButton);
            this.Controls.Add(this.AchievementsPanel);
            this.Name = "AchievementsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AchievementsWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AchievementsWindow_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel AchievementsPanel;
        private System.Windows.Forms.Button BackToMenuButton;
    }
}