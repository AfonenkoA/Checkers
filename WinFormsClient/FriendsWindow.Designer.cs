
namespace WinFormsClient
{
    internal sealed partial class FriendsWindow
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
            this.BackToMenuButton = new System.Windows.Forms.Button();
            this.FriendsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // BackToMenuButton
            // 
            this.BackToMenuButton.AutoSize = true;
            this.BackToMenuButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackToMenuButton.Location = new System.Drawing.Point(110, 118);
            this.BackToMenuButton.Name = "BackToMenuButton";
            this.BackToMenuButton.Size = new System.Drawing.Size(55, 29);
            this.BackToMenuButton.TabIndex = 0;
            this.BackToMenuButton.Text = "Menu";
            this.BackToMenuButton.UseVisualStyleBackColor = true;
            this.BackToMenuButton.Click += new System.EventHandler(this.BackToMenuButton_Click);
            // 
            // FriendsPanel
            // 
            this.FriendsPanel.AutoScroll = true;
            this.FriendsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FriendsPanel.Location = new System.Drawing.Point(12, 12);
            this.FriendsPanel.Name = "FriendsPanel";
            this.FriendsPanel.Size = new System.Drawing.Size(250, 100);
            this.FriendsPanel.TabIndex = 0;
            this.FriendsPanel.WrapContents = false;
            // 
            // FriendsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 153);
            this.Controls.Add(this.FriendsPanel);
            this.Controls.Add(this.BackToMenuButton);
            this.Name = "FriendsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FriendsWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FriendsWindow_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BackToMenuButton;
        private System.Windows.Forms.FlowLayoutPanel FriendsPanel;
    }
}