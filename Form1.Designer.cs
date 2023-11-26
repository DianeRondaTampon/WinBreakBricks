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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1192, 603);
            Controls.Add(pcbGraphics);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pcbGraphics).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pcbGraphics;
        private System.Windows.Forms.Timer timer;
    }
}