namespace Rendering_Engine_NEA_Project
{
    partial class Form1
    {
        ///  Required designer variable.
        private System.ComponentModel.IContainer components = null;

        ///  Clean up any resources being used.
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor. <summary>

        // This sign can't stop me because i can't read
        private void InitializeComponent()
        {
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            SuspendLayout();
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGray;
            ClientSize = new Size(960, 540);
            Name = "Form1";
            Text = "Rendering Engine";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            ResumeLayout(false);
        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}