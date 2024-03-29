using System.Drawing;
using System.Numerics;
using WinFormsTimer = System.Windows.Forms.Timer;


namespace Rendering_Engine_NEA_Project
{
    public partial class Form1 : Form
    {
        private WinFormsTimer timer;
        public Form1()
        {
            InitializeComponent();

            timer = new WinFormsTimer();
            timer.Interval = 1000 / 24; // 24 times per second
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            pictureBox1.Invalidate();
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
                            "#-------------#",
                            "#---------#---#",
                            "#--c----------#",
                            "###############"
                           };

        private Vector2 FindCamera(string[] room)
        {
            Vector2 cameraPosition = new Vector2(0f, 0f);

            // Find camera
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (room[y][x] == 'c')
                    {
                        cameraPosition.X = x * 10 + 5;
                        cameraPosition.Y = 95 - y * 10;
                        return cameraPosition;
                    }
                }
            }

            // Fail State, no cam found
            return new Vector2(0f, 0f);
        }

        private Vector2 cameraPosition = new Vector2(0, 0);
        private Vector2 move_offset = new Vector2(0, 0);

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
            // background
            for (int y = 0; y < 540; y++)
            {
                for (int x = 0; x < 960; x++)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Black), x, y, 1, 1);
                }
            }

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

            if (cameraPosition == new Vector2(0, 0))
            {
                cameraPosition = FindCamera(room);
            }

            if (move_offset != new Vector2(0f, 0f))
            {
                if (FindGridChar(cameraPosition + move_offset, room) == '#')
                {
                    move_offset = new Vector2(0f, 0f);
                }
                else
                {
                    cameraPosition += move_offset;
                }
            }

            float step = 0.2f;
            float[] heights = new float[960];
            Color[] colours = new Color[960];
            float max_dist = 125f;

            for (int ray = 0; ray < 960; ray++)
            {
                Vector2 ray_pos = cameraPosition;
                float length = 0f;

                while (true) 
                {
                    float FOV = 45f;
                    float angle = ((float)Math.PI / 180f) * ((FOV / 2) - (FOV / 2) * ((float)(ray) / 480f));

                    if (FindGridChar(ray_pos, room) == '#')
                    {
                        // weird fix here
                        // using the 'Euclidean distance' produces a strange fisheye effect
                        // this adjusts for that
                        float adjusted_dist = (float)Math.Cos(angle) * length;
                        heights[ray] = adjusted_dist;
                        int intesity = (int)Math.Max(255f - (255f * (length / max_dist)), 0);
                        colours[ray] = Color.FromArgb(intesity, intesity, intesity);
                        break;
                    }

                    length += step;
                    float new_x = ray_pos.X + (float)Math.Cos(angle) * step;
                    float new_y = ray_pos.Y + (float)Math.Sin(angle) * step;
                    ray_pos = new Vector2(new_x, new_y);
                }
            }

            int screen_width = 960;
            int screen_height = 540;
            int wallheight = 10; // pretty much just a guess
            float horizontal_view = 45 * 2 * (float)Math.PI / 360f; // in radians

            // draw heights centered
            for (int i = 0; i < 960; i++)
            {
                // pixels is the amount of pixels long the sliver should be
                // int pixels = (int)(540 - (540 * heights[i] / max_dist));

                // not a clue how these two lines work
                // god bless stack overflow, god bless @Jongs
                float vertical_view = (horizontal_view / screen_width) * screen_height;
                int pixels = (int)Math.Min(wallheight / (2 * Math.Tan(0.5f * vertical_view) * heights[i]) * screen_height, screen_height);

                int offset = (540 - pixels) / 2;
                Brush aBrush = new SolidBrush(colours[i]);
                for (int p = offset; p < pixels + offset; p++)
                {
                    e.Graphics.FillRectangle(aBrush, i, p, 1, 1);
                }

            }

            e.Graphics.DrawString(Convert.ToString(move_offset), fnt, new SolidBrush(Color.Red), 25f, 25f);

            /*
       
            // temp draw debug view

            for (int y = 0; y < 540; y++)
            {
                for (int x = 0; x < 960; x++)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Black), x, y, 1, 1);
                }
            }

            // centre = (480, 270) ish
            // top left = (380, 170) ish / (0, 100)
            // bottom right = (580, 370) ish / (100, 0)

            for (int y = 99; y >= 0; y--)
            {
                for (int x = 0; x < 150; x++)
                {
                    Vector2 room_coord = new Vector2(x, y);
                    Vector2 screen_coord = new Vector2(380 + x, 270 - y);

                    Color pixelColour;

                    char c = FindGridChar(room_coord, room);
                    if (c == '#')
                    {
                        pixelColour = Color.White;
                    }
                    else if (c == 'c')
                    {
                        pixelColour = Color.Green;
                    }
                    else
                    {
                        pixelColour = Color.Black;
                    }

                    e.Graphics.FillRectangle(new SolidBrush(pixelColour), screen_coord.X, screen_coord.Y, 1, 1);
                }
            }

            List<Vector2> points = new List<Vector2>();

            for (int ray = 0; ray < 960; ray++)
            {
                Vector2 ray_pos = cameraPosition;

                while (true)
                {
                    float FOV = 45f;
                    float angle = ((float)Math.PI / 180f) * ((FOV / 2) - (FOV / 2) * ((float)(ray) / 480f));

                    if (FindGridChar(ray_pos, room) == '#')
                    {
                        break;
                    }

                    float new_x = ray_pos.X + (float)Math.Cos(angle) * step;
                    float new_y = ray_pos.Y + (float)Math.Sin(angle) * step;
                    ray_pos = new Vector2(new_x, new_y);
                    points.Add(ray_pos * new Vector2(1, -1) + new Vector2(378, 270));
                }
            }

            foreach (Vector2 point in points)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, 255, 0, 0)), point.X, point.Y, 1, 1);
            } 
            
            */
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Vector2 offset = new Vector2(0f, 0f);
            float move_dist = 3f;

            switch (e.KeyCode)
            {
                case Keys.A:
                    // MessageBox.Show("Go left");
                    offset.X = -move_dist;
                    break;
                case Keys.D:
                    // MessageBox.Show("Go right");
                    offset.X = move_dist;
                    break;
                case Keys.W:
                    // MessageBox.Show("Go up");
                    offset.Y = move_dist;
                    break;
                case Keys.S:
                    // MessageBox.Show("Go down");
                    offset.Y = -move_dist;
                    break;
                case Keys.X:
                    // No more movement
                    offset = new Vector2(0f, 0f);
                    break;
            }

            move_offset = offset;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            move_offset = new Vector2(0f, 0f);
        }
    }
}
