namespace WinFormsClient.Control
{
    partial class UserInfoPanel
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.NickButton = new System.Windows.Forms.Button();
            this.SocialCreditLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(105, 99);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // NickButton
            // 
            this.NickButton.Location = new System.Drawing.Point(131, 3);
            this.NickButton.Name = "NickButton";
            this.NickButton.Size = new System.Drawing.Size(75, 23);
            this.NickButton.TabIndex = 1;
            this.NickButton.UseVisualStyleBackColor = true;
            // 
            // SocialCreditLabel
            // 
            this.SocialCreditLabel.AutoSize = true;
            this.SocialCreditLabel.Location = new System.Drawing.Point(131, 56);
            this.SocialCreditLabel.Name = "SocialCreditLabel";
            this.SocialCreditLabel.Size = new System.Drawing.Size(0, 15);
            this.SocialCreditLabel.TabIndex = 2;
            // 
            // UserInfopanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SocialCreditLabel);
            this.Controls.Add(this.NickButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "UserInfoPanel";
            this.Size = new System.Drawing.Size(242, 102);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pictureBox1;
        private Button NickButton;
        private Label SocialCreditLabel;
    }
}
