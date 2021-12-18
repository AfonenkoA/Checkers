namespace WinFormsClient
{
    partial class ShopWindow
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
            this.CurrencyLabel = new System.Windows.Forms.Label();
            this.Animations = new System.Windows.Forms.FlowLayoutPanel();
            this.CheckersSkins = new System.Windows.Forms.FlowLayoutPanel();
            this.AnimationsLabel = new System.Windows.Forms.Label();
            this.CheckersLabel = new System.Windows.Forms.Label();
            this.Lootboxes = new System.Windows.Forms.FlowLayoutPanel();
            this.LootBoxesLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ReturnButton
            // 
            this.ReturnButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ReturnButton.Location = new System.Drawing.Point(698, 12);
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.Size = new System.Drawing.Size(90, 37);
            this.ReturnButton.TabIndex = 3;
            this.ReturnButton.Text = "Return";
            this.ReturnButton.UseVisualStyleBackColor = true;
            // 
            // CurrencyLabel
            // 
            this.CurrencyLabel.AutoSize = true;
            this.CurrencyLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CurrencyLabel.Location = new System.Drawing.Point(604, 18);
            this.CurrencyLabel.Name = "CurrencyLabel";
            this.CurrencyLabel.Size = new System.Drawing.Size(88, 25);
            this.CurrencyLabel.TabIndex = 4;
            this.CurrencyLabel.Text = "Currency";
            // 
            // Animations
            // 
            this.Animations.AutoScroll = true;
            this.Animations.Location = new System.Drawing.Point(12, 199);
            this.Animations.Name = "Animations";
            this.Animations.Size = new System.Drawing.Size(776, 100);
            this.Animations.TabIndex = 7;
            this.Animations.WrapContents = false;
            // 
            // CheckersSkins
            // 
            this.CheckersSkins.AutoScroll = true;
            this.CheckersSkins.Location = new System.Drawing.Point(12, 55);
            this.CheckersSkins.Name = "CheckersSkins";
            this.CheckersSkins.Size = new System.Drawing.Size(776, 100);
            this.CheckersSkins.TabIndex = 8;
            this.CheckersSkins.WrapContents = false;
            // 
            // AnimationsLabel
            // 
            this.AnimationsLabel.AutoSize = true;
            this.AnimationsLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.AnimationsLabel.Location = new System.Drawing.Point(12, 155);
            this.AnimationsLabel.Name = "AnimationsLabel";
            this.AnimationsLabel.Size = new System.Drawing.Size(174, 41);
            this.AnimationsLabel.TabIndex = 5;
            this.AnimationsLabel.Text = "Animations";
            // 
            // CheckersLabel
            // 
            this.CheckersLabel.AutoSize = true;
            this.CheckersLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.CheckersLabel.Location = new System.Drawing.Point(12, 8);
            this.CheckersLabel.Name = "CheckersLabel";
            this.CheckersLabel.Size = new System.Drawing.Size(137, 41);
            this.CheckersLabel.TabIndex = 6;
            this.CheckersLabel.Text = "Checkers";
            // 
            // Lootboxes
            // 
            this.Lootboxes.AutoScroll = true;
            this.Lootboxes.Location = new System.Drawing.Point(12, 346);
            this.Lootboxes.Name = "Lootboxes";
            this.Lootboxes.Size = new System.Drawing.Size(776, 100);
            this.Lootboxes.TabIndex = 10;
            this.Lootboxes.WrapContents = false;
            // 
            // LootBoxesLabel
            // 
            this.LootBoxesLabel.AutoSize = true;
            this.LootBoxesLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.LootBoxesLabel.Location = new System.Drawing.Point(12, 302);
            this.LootBoxesLabel.Name = "LootBoxesLabel";
            this.LootBoxesLabel.Size = new System.Drawing.Size(162, 41);
            this.LootBoxesLabel.TabIndex = 9;
            this.LootBoxesLabel.Text = "Loot boxes";
            // 
            // ShopWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Lootboxes);
            this.Controls.Add(this.LootBoxesLabel);
            this.Controls.Add(this.Animations);
            this.Controls.Add(this.CheckersSkins);
            this.Controls.Add(this.AnimationsLabel);
            this.Controls.Add(this.CheckersLabel);
            this.Controls.Add(this.CurrencyLabel);
            this.Controls.Add(this.ReturnButton);
            this.Name = "ShopWindow";
            this.Text = "ShopWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button ReturnButton;
        private Label CurrencyLabel;
        private FlowLayoutPanel Animations;
        private FlowLayoutPanel CheckersSkins;
        private Label AnimationsLabel;
        private Label CheckersLabel;
        private FlowLayoutPanel Lootboxes;
        private Label LootBoxesLabel;
    }
}