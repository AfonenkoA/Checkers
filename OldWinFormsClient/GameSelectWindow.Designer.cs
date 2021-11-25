
namespace OldWinFormsClient
{
    partial class GameSelectWindow
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
            this.GameTitleWindow = new System.Windows.Forms.Label();
            this.PlayOnlineButton = new System.Windows.Forms.Button();
            this.BackToMenuButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GameTitleWindow
            // 
            this.GameTitleWindow.AutoSize = true;
            this.GameTitleWindow.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GameTitleWindow.Location = new System.Drawing.Point(22, 17);
            this.GameTitleWindow.Name = "GameTitleWindow";
            this.GameTitleWindow.Size = new System.Drawing.Size(188, 32);
            this.GameTitleWindow.TabIndex = 0;
            this.GameTitleWindow.Text = "Checkers Online";
            // 
            // PlayOnlineButton
            // 
            this.PlayOnlineButton.AutoSize = true;
            this.PlayOnlineButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PlayOnlineButton.Location = new System.Drawing.Point(55, 57);
            this.PlayOnlineButton.Name = "PlayOnlineButton";
            this.PlayOnlineButton.Size = new System.Drawing.Size(82, 27);
            this.PlayOnlineButton.TabIndex = 1;
            this.PlayOnlineButton.Text = "Play Online";
            this.PlayOnlineButton.UseVisualStyleBackColor = true;
            this.PlayOnlineButton.Click += new System.EventHandler(this.PlayOnlineButton_Click);
            // 
            // BackToMenuButton
            // 
            this.BackToMenuButton.AutoSize = true;
            this.BackToMenuButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackToMenuButton.Location = new System.Drawing.Point(55, 89);
            this.BackToMenuButton.Name = "BackToMenuButton";
            this.BackToMenuButton.Size = new System.Drawing.Size(51, 27);
            this.BackToMenuButton.TabIndex = 2;
            this.BackToMenuButton.Text = "Menu";
            this.BackToMenuButton.UseVisualStyleBackColor = true;
            this.BackToMenuButton.Click += new System.EventHandler(this.BackToMenuButton_Click);
            // 
            // GameSelectWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 128);
            this.Controls.Add(this.BackToMenuButton);
            this.Controls.Add(this.PlayOnlineButton);
            this.Controls.Add(this.GameTitleWindow);
            this.Name = "GameSelectWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameSelectWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameSelectWindow_FormClosed);
            this.Load += new System.EventHandler(this.GameSelectWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GameTitleWindow;
        private System.Windows.Forms.Button PlayOnlineButton;
        private System.Windows.Forms.Button BackToMenuButton;
    }
}