namespace WinFormsClient.Control.Collection
{
    sealed partial class CollectionSkinPanel
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
            this.SelectItemButton = new System.Windows.Forms.Button();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.Picture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectItemButton
            // 
            this.SelectItemButton.Location = new System.Drawing.Point(154, 74);
            this.SelectItemButton.Name = "SelectItemButton";
            this.SelectItemButton.Size = new System.Drawing.Size(75, 23);
            this.SelectItemButton.TabIndex = 9;
            this.SelectItemButton.Text = "Select";
            this.SelectItemButton.UseVisualStyleBackColor = true;
            this.SelectItemButton.Click += new System.EventHandler(this.SelectItemButton_Click);
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DescriptionLabel.Location = new System.Drawing.Point(130, 27);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(67, 15);
            this.DescriptionLabel.TabIndex = 8;
            this.DescriptionLabel.Text = "Description";
            this.DescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(130, 12);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(29, 15);
            this.TitleLabel.TabIndex = 7;
            this.TitleLabel.Text = "Title";
            // 
            // Picture
            // 
            this.Picture.Location = new System.Drawing.Point(13, 0);
            this.Picture.Name = "Picture";
            this.Picture.Size = new System.Drawing.Size(111, 100);
            this.Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Picture.TabIndex = 6;
            this.Picture.TabStop = false;
            // 
            // CollectionSkinPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SelectItemButton);
            this.Controls.Add(this.DescriptionLabel);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.Picture);
            this.Name = "CollectionSkinPanel";
            this.Size = new System.Drawing.Size(242, 100);
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button SelectItemButton;
        private Label DescriptionLabel;
        private Label TitleLabel;
        private PictureBox Picture;
    }
}
