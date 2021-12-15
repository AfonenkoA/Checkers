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
            this.LootBoxes = new System.Windows.Forms.FlowLayoutPanel();
            this.Animations = new System.Windows.Forms.FlowLayoutPanel();
            this.AnimationsLabel = new System.Windows.Forms.Label();
            this.CheckersLabel = new System.Windows.Forms.Label();
            this.CheckersSkins = new System.Windows.Forms.FlowLayoutPanel();
            this.LootBoxesLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ReturnButton
            // 
            this.ReturnButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ReturnButton.Location = new System.Drawing.Point(698, 14);
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.Size = new System.Drawing.Size(90, 42);
            this.ReturnButton.TabIndex = 3;
            this.ReturnButton.Text = "Return";
            this.ReturnButton.UseVisualStyleBackColor = true;
            
            // 
            // CurrencyLabel
            // 
            this.CurrencyLabel.AutoSize = true;
            this.CurrencyLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CurrencyLabel.Location = new System.Drawing.Point(604, 20);
            this.CurrencyLabel.Name = "CurrencyLabel";
            this.CurrencyLabel.Size = new System.Drawing.Size(88, 25);
            this.CurrencyLabel.TabIndex = 4;
            this.CurrencyLabel.Text = "Currency";
            // 
            // LootBoxes
            // 
            this.LootBoxes.AutoScroll = true;
            this.LootBoxes.Location = new System.Drawing.Point(12, 226);
            this.LootBoxes.Name = "LootBoxes";
            this.LootBoxes.Size = new System.Drawing.Size(776, 113);
            this.LootBoxes.TabIndex = 7;
            this.LootBoxes.WrapContents = false;
            // 
            // Animations
            // 
            this.Animations.AutoScroll = true;
            this.Animations.Location = new System.Drawing.Point(12, 62);
            this.Animations.Name = "Animations";
            this.Animations.Size = new System.Drawing.Size(776, 113);
            this.Animations.TabIndex = 8;
            this.Animations.WrapContents = false;
            // 
            // AnimationsLabel
            // 
            this.AnimationsLabel.AutoSize = true;
            this.AnimationsLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.AnimationsLabel.Location = new System.Drawing.Point(12, 176);
            this.AnimationsLabel.Name = "AnimationsLabel";
            this.AnimationsLabel.Size = new System.Drawing.Size(174, 41);
            this.AnimationsLabel.TabIndex = 5;
            this.AnimationsLabel.Text = "Animations";
            // 
            // CheckersLabel
            // 
            this.CheckersLabel.AutoSize = true;
            this.CheckersLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.CheckersLabel.Location = new System.Drawing.Point(12, 9);
            this.CheckersLabel.Name = "CheckersLabel";
            this.CheckersLabel.Size = new System.Drawing.Size(137, 41);
            this.CheckersLabel.TabIndex = 6;
            this.CheckersLabel.Text = "Checkers";
            // 
            // CheckersSkins
            // 
            this.CheckersSkins.AutoScroll = true;
            this.CheckersSkins.Location = new System.Drawing.Point(12, 392);
            this.CheckersSkins.Name = "CheckersSkins";
            this.CheckersSkins.Size = new System.Drawing.Size(776, 113);
            this.CheckersSkins.TabIndex = 10;
            this.CheckersSkins.WrapContents = false;
            // 
            // LootBoxesLabel
            // 
            this.LootBoxesLabel.AutoSize = true;
            this.LootBoxesLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.LootBoxesLabel.Location = new System.Drawing.Point(12, 342);
            this.LootBoxesLabel.Name = "LootBoxesLabel";
            this.LootBoxesLabel.Size = new System.Drawing.Size(162, 41);
            this.LootBoxesLabel.TabIndex = 9;
            this.LootBoxesLabel.Text = "Loot boxes";
            // 
            // ShopWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 510);
            this.Controls.Add(this.CheckersSkins);
            this.Controls.Add(this.LootBoxesLabel);
            this.Controls.Add(this.LootBoxes);
            this.Controls.Add(this.Animations);
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
        private FlowLayoutPanel LootBoxes;
        private FlowLayoutPanel Animations;
        private Label AnimationsLabel;
        private Label CheckersLabel;
        private FlowLayoutPanel CheckersSkins;
        private Label LootBoxesLabel;
    }
}