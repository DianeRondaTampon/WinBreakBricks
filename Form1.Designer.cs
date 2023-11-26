namespace WinBreakBricks
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pcbGraphics = new PictureBox();
            timer = new System.Windows.Forms.Timer(components);
            btnRestart = new Button();
            btnNext = new Button();
            label1 = new Label();
            lblStage = new Label();
            lblScore = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)pcbGraphics).BeginInit();
            SuspendLayout();
            // 
            // pcbGraphics
            // 
            pcbGraphics.Location = new Point(0, 0);
            pcbGraphics.Name = "pcbGraphics";
            pcbGraphics.Size = new Size(1190, 600);
            pcbGraphics.TabIndex = 0;
            pcbGraphics.TabStop = false;
            // 
            // timer
            // 
            timer.Tick += timer_Tick;
            // 
            // btnRestart
            // 
            btnRestart.Location = new Point(1196, 12);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(84, 45);
            btnRestart.TabIndex = 1;
            btnRestart.Text = "Restart";
            btnRestart.UseVisualStyleBackColor = true;
            btnRestart.Visible = false;
            btnRestart.Click += btnRestart_Click;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(1196, 12);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(84, 45);
            btnNext.TabIndex = 2;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Visible = false;
            btnNext.Click += btnNext_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1204, 71);
            label1.Name = "label1";
            label1.Size = new Size(47, 20);
            label1.TabIndex = 3;
            label1.Text = "Stage";
            // 
            // lblStage
            // 
            lblStage.AutoSize = true;
            lblStage.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblStage.Location = new Point(1213, 101);
            lblStage.Name = "lblStage";
            lblStage.Size = new Size(23, 28);
            lblStage.TabIndex = 4;
            lblStage.Text = "1";
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblScore.Location = new Point(1213, 179);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(23, 28);
            lblScore.TabIndex = 6;
            lblScore.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1204, 149);
            label3.Name = "label3";
            label3.Size = new Size(46, 20);
            label3.TabIndex = 5;
            label3.Text = "Score";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1292, 603);
            Controls.Add(lblScore);
            Controls.Add(label3);
            Controls.Add(lblStage);
            Controls.Add(label1);
            Controls.Add(btnNext);
            Controls.Add(btnRestart);
            Controls.Add(pcbGraphics);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pcbGraphics).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pcbGraphics;
        private System.Windows.Forms.Timer timer;
        private Button btnRestart;
        private Button btnNext;
        private Label label1;
        private Label lblStage;
        private Label lblScore;
        private Label label3;
    }
}