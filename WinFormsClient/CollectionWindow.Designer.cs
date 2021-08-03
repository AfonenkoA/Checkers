
namespace WinFormsClient.Windows
{
    partial class CollectionWindow
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
            this.ItemsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.BackToMenuButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ItemsPanel
            // 
            this.ItemsPanel.AutoScroll = true;
            this.ItemsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ItemsPanel.Location = new System.Drawing.Point(12, 12);
            this.ItemsPanel.Name = "ItemsPanel";
            this.ItemsPanel.Size = new System.Drawing.Size(494, 125);
            this.ItemsPanel.TabIndex = 0;
            this.ItemsPanel.WrapContents = false;
            // 
            // BackToMenuButton
            // 
            this.BackToMenuButton.AutoSize = true;
            this.BackToMenuButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackToMenuButton.Location = new System.Drawing.Point(218, 147);
            this.BackToMenuButton.Name = "BackToMenuButton";
            this.BackToMenuButton.Size = new System.Drawing.Size(55, 29);
            this.BackToMenuButton.TabIndex = 1;
            this.BackToMenuButton.Text = "Menu";
            this.BackToMenuButton.UseVisualStyleBackColor = true;
            this.BackToMenuButton.Click += new System.EventHandler(this.BackToMenuButton_Click);
            // 
            // CollectionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 188);
            this.Controls.Add(this.BackToMenuButton);
            this.Controls.Add(this.ItemsPanel);
            this.Name = "CollectionWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CollectionWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CollectionWindow_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel ItemsPanel;
        private System.Windows.Forms.Button BackToMenuButton;
    }
}