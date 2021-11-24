
namespace WinFormsClient
{
    internal sealed partial class ProfileWindow
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
            this.ProfilePictureBox = new System.Windows.Forms.PictureBox();
            this.NickLabel = new System.Windows.Forms.Label();
            this.RaitingLabel = new System.Windows.Forms.Label();
            this.LastActivityLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ProfilePictureBox
            // 
            this.ProfilePictureBox.InitialImage = null;
            this.ProfilePictureBox.Location = new System.Drawing.Point(12, 12);
            this.ProfilePictureBox.Name = "ProfilePictureBox";
            this.ProfilePictureBox.Size = new System.Drawing.Size(191, 188);
            this.ProfilePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ProfilePictureBox.TabIndex = 0;
            this.ProfilePictureBox.TabStop = false;
            // 
            // NickLabel
            // 
            this.NickLabel.AutoSize = true;
            this.NickLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NickLabel.Location = new System.Drawing.Point(234, 12);
            this.NickLabel.Name = "NickLabel";
            this.NickLabel.Size = new System.Drawing.Size(49, 25);
            this.NickLabel.TabIndex = 2;
            this.NickLabel.Text = "Nick";
            // 
            // RaitingLabel
            // 
            this.RaitingLabel.AutoSize = true;
            this.RaitingLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RaitingLabel.Location = new System.Drawing.Point(234, 37);
            this.RaitingLabel.Name = "RaitingLabel";
            this.RaitingLabel.Size = new System.Drawing.Size(71, 25);
            this.RaitingLabel.TabIndex = 3;
            this.RaitingLabel.Text = "Raiting";
            // 
            // LastActivityLabel
            // 
            this.LastActivityLabel.AutoSize = true;
            this.LastActivityLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LastActivityLabel.Location = new System.Drawing.Point(234, 62);
            this.LastActivityLabel.Name = "LastActivityLabel";
            this.LastActivityLabel.Size = new System.Drawing.Size(106, 25);
            this.LastActivityLabel.TabIndex = 4;
            this.LastActivityLabel.Text = "LastActivtiy";
            // 
            // ProfileWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 211);
            this.Controls.Add(this.LastActivityLabel);
            this.Controls.Add(this.RaitingLabel);
            this.Controls.Add(this.NickLabel);
            this.Controls.Add(this.ProfilePictureBox);
            this.Name = "ProfileWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProfileWindow";
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ProfilePictureBox;
        private System.Windows.Forms.Label NickLabel;
        private System.Windows.Forms.Label RaitingLabel;
        private System.Windows.Forms.Label LastActivityLabel;
    }
}