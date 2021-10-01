using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompGraf3
{
    public partial class Form3 : Form
    {
        private Form1 main;
        Point p1;
        Point p2;
        public Form3(Form1 form1)
        {
            main = form1;
            InitializeComponent();
            p1 = new Point(50, 20);
            p2 = new Point(70, 150);
            textBox1.Text = p1.X.ToString();
            textBox2.Text = p1.Y.ToString();
            textBox3.Text = p2.X.ToString();
            textBox4.Text = p2.Y.ToString();
        }
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            main.Visible = true;
        }
        private void drawByBresenham(Point p1, Point p2)
        {
            if (p1.X < 0 || p1.X >= pictureBox1.Width || p2.X < 0 || p2.X >= pictureBox1.Width
                || p1.Y < 0 || p1.Y >= pictureBox1.Height || p2.Y < 0 || p2.Y >= pictureBox1.Height)
            {
                MessageBox.Show("Неверные координаты");
                return;
            }
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //Console.WriteLine(bitmap.Width + " " + bitmap.Height);
            //Console.WriteLine(pictureBox1.Width + " " + pictureBox1.Height);
            //Console.WriteLine(button5.Width + " " + button5.Height);
            int dx = Math.Abs(p2.X - p1.X);
            int dy = Math.Abs(p2.Y - p1.Y);
            bool lowK = dy / dx < 1;
            int error = 0;
            int de = lowK ? dy + 1 : dx + 1;
            int initial = lowK ? p1.Y : p1.X;
            int dir = lowK ? p2.Y - p1.Y : p2.X - p1.X;
            if (dir > 0)
            {
                dir = 1;
            }
            if (dir < 0)
            {
                dir = -1;
            }
            int counter = lowK ? (p1.X < p2.X ? 1 : -1) : (p1.Y < p2.Y ? 1 : -1);
            for (int looper = lowK ? p1.X : p1.Y; looper != (lowK ? p2.X : p2.Y); looper += counter)
            {
                if (lowK)
                {
                    bitmap.SetPixel(looper, initial, Color.Blue);
                }
                else
                {
                    bitmap.SetPixel(initial, looper, Color.Blue);
                }
                error += de;
                if (error >= 1.0)
                {
                    initial += dir;
                    error -= lowK ? (dx + 1) : (dy + 1);
                }

            }
            pictureBox1.Image = bitmap;
        }

        private void drawByWu(Point p1, Point p2)
        {
            if (p1.X < 0 || p1.X >= pictureBox1.Width || p2.X < 0 || p2.X >= pictureBox1.Width 
                || p1.Y < 0 || p1.Y >= pictureBox1.Height || p2.Y < 0 || p2.Y >= pictureBox1.Height)
            {
                MessageBox.Show("Неверные координаты");
                return;
            }
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Color color = Color.Red;
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            int x1 = p1.X;
            int x2 = p2.X;
            int y1 = p1.Y;
            int y2 = p2.Y;
            if (Math.Abs(dx) > Math.Abs(dy))
            {
                if (p1.X > p2.X)
                {
                    (x1, x2) = (x2, x1);
                    (y1, y2) = (y2, y1);
                }

                float coef = dy / dx;
                float y = y1;
                for (int x = x1 + 1; x <= x2; x++)
                {
                    bitmap.SetPixel(x, (int)y, Color.FromArgb((int)((1 - (y - (int)y)) * 255), color.R, color.G, color.B));
                    bitmap.SetPixel(x, (int)y + 1, Color.FromArgb((int)((y - (int)y) * 255), color.R, color.G, color.B));
                    y += coef;
                }
            }
            else
            {
                if (y1 > y2)
                {
                    (x1, x2) = (x2, x1);
                    (y1, y2) = (y2, y1);
                }
                float coef = dx / dy;
                float x = x1;
                for (int y = y1 + 1; y <= y2; y++)
                {
                    bitmap.SetPixel((int)x, y, Color.FromArgb((int)((1 - (x - (int)x)) * 255), color.R, color.G, color.B));
                    bitmap.SetPixel((int)x + 1, y, Color.FromArgb((int)((x - (int)x) * 255), color.R, color.G, color.B));
                    x += coef;
                }
            }
            pictureBox1.Image = bitmap;
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            drawByBresenham(p1, p2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            drawByWu(p1, p2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                p1.X = int.Parse(textBox1.Text);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                p1.Y = int.Parse(textBox2.Text);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                p2.X = int.Parse(textBox3.Text);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                p2.Y = int.Parse(textBox4.Text);
            }
        }
    
    }
}
