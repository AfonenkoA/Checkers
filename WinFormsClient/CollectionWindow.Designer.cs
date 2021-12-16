namespace WinFormsClient
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
            this.ReturnButton = new System.Windows.Forms.Button();
            this.CheckersLabel = new System.Windows.Forms.Label();
            this.AnimationsLabel = new System.Windows.Forms.Label();
            this.Aniamtions = new System.Windows.Forms.FlowLayoutPanel();
            this.CheckersSkins = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // ReturnButton
            // 
            this.ReturnButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ReturnButton.Location = new System.Drawing.Point(698, 12);
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.Size = new System.Drawing.Size(90, 37);
            this.ReturnButton.TabIndex = 2;
            this.ReturnButton.Text = "Return";
            this.ReturnButton.UseVisualStyleBackColor = true;
            
            // 
            // CheckersLabel
            // 
            this.CheckersLabel.AutoSize = true;
            this.CheckersLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.CheckersLabel.Location = new System.Drawing.Point(12, 92);
            this.CheckersLabel.Name = "CheckersLabel";
            this.CheckersLabel.Size = new System.Drawing.Size(146, 45);
            this.CheckersLabel.TabIndex = 3;
            this.CheckersLabel.Text = "Checkers";
            // 
            // AnimationsLabel
            // 
            this.AnimationsLabel.AutoSize = true;
            this.AnimationsLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.AnimationsLabel.Location = new System.Drawing.Point(12, 258);
            this.AnimationsLabel.Name = "AnimationsLabel";
            this.AnimationsLabel.Size = new System.Drawing.Size(185, 45);
            this.AnimationsLabel.TabIndex = 3;
            this.AnimationsLabel.Text = "Animations";
            // 
            // Aniamtions
            // 
            this.Aniamtions.Location = new System.Drawing.Point(12, 140);
            this.Aniamtions.Name = "Aniamtions";
            this.Aniamtions.Size = new System.Drawing.Size(776, 100);
            this.Aniamtions.TabIndex = 4;
            // 
            // CheckersSkins
            // 
            this.CheckersSkins.Location = new System.Drawing.Point(12, 306);
            this.CheckersSkins.Name = "CheckersSkins";
            this.CheckersSkins.Size = new System.Drawing.Size(776, 100);
            this.CheckersSkins.TabIndex = 4;
            // 
            // CollectionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CheckersSkins);
            this.Controls.Add(this.Aniamtions);
            this.Controls.Add(this.AnimationsLabel);
            this.Controls.Add(this.CheckersLabel);
            this.Controls.Add(this.ReturnButton);
            this.Name = "CollectionWindow";
            this.Text = "CollectionWindow";
            
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button ReturnButton;
        private Label CheckersLabel;
        private Label AnimationsLabel;
        private FlowLayoutPanel Aniamtions;
        private FlowLayoutPanel CheckersSkins;
    }
}