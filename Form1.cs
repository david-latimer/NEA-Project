using System.Drawing;

namespace Rendering_Engine_NEA_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private PictureBox pictureBox1 = new PictureBox();
        private Font fnt = new Font("Helvetica", 18);

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.BackColor = Color.White;
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.Controls.Add(pictureBox1);
        }

        private void pictureBox1_Paint(object? sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawString("This is a diagonal line drawn on the control", fnt, System.Drawing.Brushes.Blue, new Point(200, 200));
            // g.DrawLine(new Pen(Color.DarkRed), pictureBox1.Left, pictureBox1.Top, pictureBox1.Right, pictureBox1.Bottom);
            
            Brush aBrush = new SolidBrush(Color.DarkRed);

            for (int y = 0; y < 540; y++)
            {
                for (int x = 0; x < 960; x++)
                {
                    if (x % 2 == 0 && y % 2 == 0)
                    {
                        e.Graphics.FillRectangle(aBrush, x, y, 1, 1);
                    }
                }
            }

        }
    }
}