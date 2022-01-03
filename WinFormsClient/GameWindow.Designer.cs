namespace WinFormsClient
{
    sealed partial class GameWindow
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
            this.Board = new System.Windows.Forms.PictureBox();
            this.SelfNick = new System.Windows.Forms.Label();
            this.EnemyNick = new System.Windows.Forms.Label();
            this.SelfPicture = new System.Windows.Forms.PictureBox();
            this.EnemyPicture = new System.Windows.Forms.PictureBox();
            this.SelfSocialCredit = new System.Windows.Forms.Label();
            this.EnemySocialCredit = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Board)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelfPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnemyPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // Board
            // 
            this.Board.Location = new System.Drawing.Point(220, 59);
            this.Board.Name = "Board";
            this.Board.Size = new System.Drawing.Size(300, 300);
            this.Board.TabIndex = 0;
            this.Board.TabStop = false;
            // 
            // SelfNick
            // 
            this.SelfNick.AutoSize = true;
            this.SelfNick.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SelfNick.Location = new System.Drawing.Point(44, 39);
            this.SelfNick.Name = "SelfNick";
            this.SelfNick.Size = new System.Drawing.Size(80, 25);
            this.SelfNick.TabIndex = 1;
            this.SelfNick.Text = "SelfNick";
            // 
            // EnemyNick
            // 
            this.EnemyNick.AutoSize = true;
            this.EnemyNick.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EnemyNick.Location = new System.Drawing.Point(633, 39);
            this.EnemyNick.Name = "EnemyNick";
            this.EnemyNick.Size = new System.Drawing.Size(105, 25);
            this.EnemyNick.TabIndex = 2;
            this.EnemyNick.Text = "EnemyNick";
            // 
            // SelfPicture
            // 
            this.SelfPicture.Location = new System.Drawing.Point(22, 77);
            this.SelfPicture.Name = "SelfPicture";
            this.SelfPicture.Size = new System.Drawing.Size(123, 99);
            this.SelfPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.SelfPicture.TabIndex = 3;
            this.SelfPicture.TabStop = false;
            // 
            // EnemyPicture
            // 
            this.EnemyPicture.Location = new System.Drawing.Point(624, 77);
            this.EnemyPicture.Name = "EnemyPicture";
            this.EnemyPicture.Size = new System.Drawing.Size(123, 99);
            this.EnemyPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.EnemyPicture.TabIndex = 4;
            this.EnemyPicture.TabStop = false;
            // 
            // SelfSocialCredit
            // 
            this.SelfSocialCredit.AutoSize = true;
            this.SelfSocialCredit.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SelfSocialCredit.Location = new System.Drawing.Point(22, 191);
            this.SelfSocialCredit.Name = "SelfSocialCredit";
            this.SelfSocialCredit.Size = new System.Drawing.Size(144, 25);
            this.SelfSocialCredit.TabIndex = 5;
            this.SelfSocialCredit.Text = "SelfSocialCredit";
            // 
            // EnemySocialCredit
            // 
            this.EnemySocialCredit.AutoSize = true;
            this.EnemySocialCredit.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EnemySocialCredit.Location = new System.Drawing.Point(624, 191);
            this.EnemySocialCredit.Name = "EnemySocialCredit";
            this.EnemySocialCredit.Size = new System.Drawing.Size(169, 25);
            this.EnemySocialCredit.TabIndex = 6;
            this.EnemySocialCredit.Text = "EnemySocialCredit";
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.EnemySocialCredit);
            this.Controls.Add(this.SelfSocialCredit);
            this.Controls.Add(this.EnemyPicture);
            this.Controls.Add(this.SelfPicture);
            this.Controls.Add(this.EnemyNick);
            this.Controls.Add(this.SelfNick);
            this.Controls.Add(this.Board);
            this.Name = "GameWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameWindow_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.Board)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelfPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnemyPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox Board;
        private Label SelfNick;
        private Label EnemyNick;
        private PictureBox SelfPicture;
        private PictureBox EnemyPicture;
        private Label SelfSocialCredit;
        private Label EnemySocialCredit;
    }
}