namespace memory_match {
    partial class MenuForm {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            btnStartGame = new Button();
            noCardPairs = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)noCardPairs).BeginInit();
            SuspendLayout();
            // 
            // btnStartGame
            // 
            btnStartGame.Location = new Point(243, 246);
            btnStartGame.Name = "btnStartGame";
            btnStartGame.Size = new Size(94, 29);
            btnStartGame.TabIndex = 1;
            btnStartGame.Text = "Start Game";
            btnStartGame.UseVisualStyleBackColor = true;
            btnStartGame.Click += btnStartGame_Click;
            // 
            // noCardPairs
            // 
            noCardPairs.Location = new Point(219, 172);
            noCardPairs.Name = "noCardPairs";
            noCardPairs.Size = new Size(150, 27);
            noCardPairs.TabIndex = 2;
            noCardPairs.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(243, 104);
            label1.Name = "label1";
            label1.Size = new Size(109, 20);
            label1.TabIndex = 3;
            label1.Text = "Memory Match";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(219, 149);
            label2.Name = "label2";
            label2.Size = new Size(156, 20);
            label2.TabIndex = 4;
            label2.Text = "How many card pairs?";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(24, 417);
            label3.Name = "label3";
            label3.Size = new Size(486, 40);
            label3.TabIndex = 5;
            label3.Text = "Click a card to flip it over. When you select two cards that are matching, \r\nthey will be added to your score. Match every card to win";
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(615, 487);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(noCardPairs);
            Controls.Add(btnStartGame);
            Name = "MenuForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)noCardPairs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnStartGame;
        private NumericUpDown noCardPairs;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
