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
            // g.DrawString("This is a diagonal line drawn on the control", fnt, System.Drawing.Brushes.Blue, new Point(200, 200));
            // g.DrawLine(new Pen(Color.DarkRed), pictureBox1.Left, pictureBox1.Top, pictureBox1.Right, pictureBox1.Bottom);

            Brush aBrush = new SolidBrush(Color.DarkRed);
            Brush bBrush = new SolidBrush(Color.DarkGray);

            // cool lines
            /*for (int y = 0; y < 540; y++)
            {
                for (int x = 0; x < 960; x++)
                {
                    if ((y / 5) % 2 == 0)
                    {
                        e.Graphics.FillRectangle(aBrush, x, y, 1, 1);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(bBrush, x, y, 1, 1);
                    }
                }
            }*/

            // background
            for (int y = 0; y < 540; y++)
            {
                for (int x = 0; x < 960; x++)
                {
                    e.Graphics.FillRectangle(bBrush, x, y, 1, 1);
                }
            }

            // propose the camera is at (0, 0, 0)
            // propose, also, that there happens to be a square at (-2, 3, 6) with a side length 5
            // how would one render that?

            //        ___________
            //       /     |
            //      /      |
            //     /       |
            //    /        |  T
            //   /         |
            //  /          |
            // C           |
            //  \          |
            //   \         |
            //    \        |
            //     \       |
            //      \      |
            //       \_____|_____
            //
            // very hi-tec drawing

            // propose F, the distance from the camera to the 3D plane you are projecting onto
            // in the following calculations, all x,y,z co-ords refer to the square's position, as the camera is at the origin

            // new X = X * (F/Z) 
            // new Y = Y * (F/Z)

            // a 10 side length square at z = 5 would fill the y axis completely
            // therefore, the size of our cube in pixels must be adjusted for
            // (540 / 2) * 1.2 = 324

            // at an F value of 5, the projected square would have 2D co-ords of (-1.67, 2.5)
            // assume a full FOV of 90 degrees
            // at a screen size of 960x540, that translates to screen co-ords of roughly (320, 135)

            // for some reason, the size of a square is determined from the top left vertex, that stays constant while it grows away from it
            // therefore we must offset the x and y by half the square side length

            // let's render
            e.Graphics.FillRectangle(aBrush, 320 - 162, 135 - 162, 324, 324);

            // The scene set up in Unity:
            // https://cdn.discordapp.com/attachments/625378398503698438/1079925549771792445/image.png
            // https://cdn.discordapp.com/attachments/625378398503698438/1079925714658279454/image.png

            // The output
            // https://cdn.discordapp.com/attachments/625378398503698438/1079925790688428042/image.png

            // I'd say that's cause enough for celebration

            //  _\|/ ^
            //   (_oo /
            //  /-|--/
            //  \ |
            //    / -i
            //   /   L
            //   L
            // WOOOOOOOOOOOOOOOOOOO
        }
    }
}
