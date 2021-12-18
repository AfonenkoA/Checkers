namespace WinFormsClient.Control.Shop
{
    partial class SoldCheckersShowPanel
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
            this.Picture = new System.Windows.Forms.PictureBox();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.BuyItemButton = new System.Windows.Forms.Button();
            this.PriceLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            this.SuspendLayout();
            // 
            // Picture
            // 
            this.Picture.Location = new System.Drawing.Point(0, 0);
            this.Picture.Name = "Picture";
            this.Picture.Size = new System.Drawing.Size(111, 100);
            this.Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Picture.TabIndex = 2;
            this.Picture.TabStop = false;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(117, 11);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(29, 15);
            this.TitleLabel.TabIndex = 3;
            this.TitleLabel.Text = "Title";
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DescriptionLabel.Location = new System.Drawing.Point(117, 26);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(67, 15);
            this.DescriptionLabel.TabIndex = 4;
            this.DescriptionLabel.Text = "Description";
            this.DescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BuyItemButton
            // 
            this.BuyItemButton.Location = new System.Drawing.Point(154, 74);
            this.BuyItemButton.Name = "BuyItemButton";
            this.BuyItemButton.Size = new System.Drawing.Size(75, 23);
            this.BuyItemButton.TabIndex = 5;
            this.BuyItemButton.Text = "Buy";
            this.BuyItemButton.UseVisualStyleBackColor = true;
            this.BuyItemButton.Click += new System.EventHandler(this.BuyItemButton_Click);
            // 
            // PriceLabel
            // 
            this.PriceLabel.AutoSize = true;
            this.PriceLabel.Location = new System.Drawing.Point(196, 56);
            this.PriceLabel.Name = "PriceLabel";
            this.PriceLabel.Size = new System.Drawing.Size(33, 15);
            this.PriceLabel.TabIndex = 6;
            this.PriceLabel.Text = "Price";
            // 
            // SoldCheckersShowPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PriceLabel);
            this.Controls.Add(this.BuyItemButton);
            this.Controls.Add(this.DescriptionLabel);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.Picture);
            this.Name = "SoldCheckersShowPanel";
            this.Size = new System.Drawing.Size(242, 100);
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox Picture;
        private Label TitleLabel;
        private Label DescriptionLabel;
        private Button BuyItemButton;
        private Label PriceLabel;
    }
}
