namespace WinFormsClient
{
    partial class GameSelectionWindow
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
            this.PlayRunkedGameButton = new System.Windows.Forms.Button();
            this.PlayUnrankedGameButton = new System.Windows.Forms.Button();
            this.PlayWithBotButton = new System.Windows.Forms.Button();
            this.PlayLocalButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ReturnButton
            // 
            this.ReturnButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ReturnButton.Location = new System.Drawing.Point(382, 12);
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.Size = new System.Drawing.Size(90, 37);
            this.ReturnButton.TabIndex = 1;
            this.ReturnButton.Text = "Return";
            this.ReturnButton.UseVisualStyleBackColor = true;
            this.ReturnButton.Click += new System.EventHandler(this.ReturnButton_Click);
            // 
            // PlayRunkedGameButton
            // 
            this.PlayRunkedGameButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PlayRunkedGameButton.Location = new System.Drawing.Point(12, 130);
            this.PlayRunkedGameButton.Name = "PlayRunkedGameButton";
            this.PlayRunkedGameButton.Size = new System.Drawing.Size(224, 33);
            this.PlayRunkedGameButton.TabIndex = 2;
            this.PlayRunkedGameButton.Text = "Play ranked game";
            this.PlayRunkedGameButton.UseVisualStyleBackColor = true;
            // 
            // PlayUnrankedGameButton
            // 
            this.PlayUnrankedGameButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PlayUnrankedGameButton.Location = new System.Drawing.Point(12, 169);
            this.PlayUnrankedGameButton.Name = "PlayUnrankedGameButton";
            this.PlayUnrankedGameButton.Size = new System.Drawing.Size(224, 33);
            this.PlayUnrankedGameButton.TabIndex = 2;
            this.PlayUnrankedGameButton.Text = "Play unranked game";
            this.PlayUnrankedGameButton.UseVisualStyleBackColor = true;
            // 
            // PlayWithBotButton
            // 
            this.PlayWithBotButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PlayWithBotButton.Location = new System.Drawing.Point(12, 208);
            this.PlayWithBotButton.Name = "PlayWithBotButton";
            this.PlayWithBotButton.Size = new System.Drawing.Size(224, 33);
            this.PlayWithBotButton.TabIndex = 2;
            this.PlayWithBotButton.Text = "Play versus BOT";
            this.PlayWithBotButton.UseVisualStyleBackColor = true;
            // 
            // PlayLocalButton
            // 
            this.PlayLocalButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PlayLocalButton.Location = new System.Drawing.Point(12, 247);
            this.PlayLocalButton.Name = "PlayLocalButton";
            this.PlayLocalButton.Size = new System.Drawing.Size(224, 33);
            this.PlayLocalButton.TabIndex = 2;
            this.PlayLocalButton.Text = "Play local game";
            this.PlayLocalButton.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(255, 115);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(225, 341);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // GameSelectionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.PlayLocalButton);
            this.Controls.Add(this.PlayWithBotButton);
            this.Controls.Add(this.PlayUnrankedGameButton);
            this.Controls.Add(this.PlayRunkedGameButton);
            this.Controls.Add(this.ReturnButton);
            this.Name = "GameSelectionWindow";
            this.Text = "GameSelectionWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameSelectionWindow_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button ReturnButton;
        private Button PlayRunkedGameButton;
        private Button PlayUnrankedGameButton;
        private Button PlayWithBotButton;
        private Button PlayLocalButton;
        private PictureBox pictureBox1;
    }
}