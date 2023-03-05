using System.Drawing;
using System.Numerics;

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

        string[] room = new string[10] {
                            "###############",
                            "#-------------#",
                            "#---##--------#",
                            "#---##--------#",
                            "#---------#---#",
                            "#---------#---#",
                            "#--c----------#",
                            "#---------#---#",
                            "#-------------#",
                            "###############"
                           };

        private char FindGridChar(Vector2 pos, string[] room)
        {
            float x = pos.X;
            float y = pos.Y;

            int new_x = ((int)(x / 10));
            int new_y = 9 - ((int)(y / 10));

            return room[new_y][new_x];
        }

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

            // background
            for (int y = 0; y < 540; y++)
            {
                for (int x = 0; x < 960; x++)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Black), x, y, 1, 1);
                }
            }

            // g.DrawString("This is a diagonal line drawn on the control", fnt, System.Drawing.Brushes.Blue, new Point(200, 200));
            // g.DrawLine(new Pen(Color.White), pictureBox1.Left, pictureBox1.Top, pictureBox1.Right, pictureBox1.Bottom);

            Brush aBrush = new SolidBrush(Color.White);


            // Next project: Raycasting

            // A map for the room

            // "###############"
            // "#-------------#"
            // "#---##--------#"
            // "#---##--------#"
            // "#---------#---#"
            // "#---------#---#"
            // "#--c----------#"
            // "#---------#---#"
            // "#-------------#"
            // "###############"

            // The # is a wall tile, the - is empty space, and the c is the camera's position
            // Each tile is 10 units wide and tall, making the grid 100ux100u

            // The bottom left position is (0, 0)
            // The top left position is (0, 100)
            // The bottom right position is (100, 0)
            // The top right position is (100, 100)

            // The camera is currently at (35, 35)
            // At start, the camera aims due right

            Vector2 cameraPosition = new Vector2(0, 0);
            
            // Find camera
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (room[y][x] == 'c')
                    {
                        cameraPosition.X = x * 10 + 5;
                        cameraPosition.Y = 95 - y * 10;
                        goto break_out;
                    }
                }
            }

            break_out:
            float step = 0.2f;
            float[] heights = new float[960];

            for (int ray = 0; ray < 960; ray++)
            {
                Vector2 ray_pos = cameraPosition;
                float length = 0f;

                while (true)
                {
                    if (FindGridChar(ray_pos, room) == '#')
                    {
                        heights[ray] = length;
                        break;
                    }

                    length += step;
                    float angle = ((float)Math.PI / 180f) * (45f - 45f * ((float)(ray) / 480f));
                    float new_x = ray_pos.X + (float)Math.Cos(angle) * step;
                    float new_y = ray_pos.Y + (float)Math.Sin(angle) * step;
                    ray_pos = new Vector2(new_x, new_y);
                }
            }

            // draw heights from bottom
            for (int i = 0; i < 960; i++)
            {
                // int pixels = 540 * (int)(141.42f - heights[i]);
                int pixels = (int)(540 - (540 * (heights[i]) / 150f));
                int offset = (540 - pixels) / 2;
                for (int p = offset; p < pixels + offset; p++)
                {
                    e.Graphics.FillRectangle(aBrush, i, p, 1, 1);
                }

            }

            // draw heights centered
        }
    }
}
