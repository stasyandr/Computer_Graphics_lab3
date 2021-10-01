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
    public partial class Form4 : Form
    {
        private Form1 main;
        int gr1x = 100,gr1y = 100;
        int gr2x = 150, gr2y = 400;
        int gr3x = 600, gr3y = 200;
        //Pen p3 = new Pen(Color.Red, 1), p1 = new Pen(Color.Green, 1), p2 = new Pen(Color.Blue, 1);
        Color gr1 = Color.Green, gr2 = Color.Blue, gr3 = Color.Red;
        private Graphics g;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {
                    gr1x = int.Parse(textBox1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text != "")
                {
                    gr1y = int.Parse(textBox2.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text != "")
                {
                    gr2x = int.Parse(textBox3.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox4.Text != "")
                {
                    gr2y = int.Parse(textBox4.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text != "")
                {
                    gr3x = int.Parse(textBox5.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox6.Text != "")
                {
                    gr3y = int.Parse(textBox6.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Form4(Form1 form1)
        {
            main = form1;
            InitializeComponent();
            //Console.WriteLine(pictureBox1.Width + " " + pictureBox1.Height);
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
            ((Bitmap)pictureBox1.Image).SetPixel(gr1x, gr1y, gr1);
            ((Bitmap)pictureBox1.Image).SetPixel(gr2x, gr2y, gr2);
            ((Bitmap)pictureBox1.Image).SetPixel(gr3x, gr3y, gr3);
            pictureBox1.Invalidate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (gr1x >= pictureBox1.Width || gr1x < 0 || gr2x >= pictureBox1.Width || gr2x < 0 || gr3x >= pictureBox1.Width || gr3x < 0 ||
                gr1y >= pictureBox1.Height || gr1y < 0 || gr2y >= pictureBox1.Height || gr2y < 0 || gr3y >= pictureBox1.Height || gr3y < 0)
            {
                MessageBox.Show("Неверные координаты");
                return;
            }
            //g.DrawEllipse(p1, gr1x-25, gr1y - 25, 50, 50);
            //g.DrawEllipse(p2, gr2x - 25, gr2y - 25, 50, 50);
            //g.DrawEllipse(p3, gr3x - 25, gr3y - 25, 50, 50);
            g.Clear(Color.White);
            List<(int, int,Color)> sort = new List<(int, int,Color)>();
            sort.Add((gr1y, gr1x,gr1)); sort.Add((gr2y, gr2x,gr2)); sort.Add((gr3y, gr3x,gr3));
            sort.Sort();
            Console.WriteLine(sort[0] + " " + sort[1] + " " + sort[2]);
            gr1x = sort[0].Item2; gr1y = sort[0].Item1; gr1 = sort[0].Item3;
            gr3x = sort[1].Item2; gr3y = sort[1].Item1; gr3 = sort[1].Item3;
            gr2x = sort[2].Item2; gr2y = sort[2].Item1; gr2 = sort[2].Item3;
            //уравнения сторон
            double h1 = (gr2x - gr1x) / (double)(gr2y - gr1y);
            (double, double) ur1_2 = (h1,-gr1y*h1 + gr1x);
            double h2 = (gr3x - gr1x) / (double)(gr3y - gr1y);
            (double, double) ur1_3 = (h2, -(gr1y * h2) + gr1x);
            double h3 = (gr3x - gr2x) / (double)(gr3y - gr2y);
            (double, double) ur2_3 = (h3, -(gr2y * h3) + gr2x);
            //уравнения цветов стороны 1-2
            if (gr2x - gr1x != 0)
                h1 = (gr2.R - gr1.R) / (double)(gr2x - gr1x);
            else
                h1 = 0;
            (double, double) r1 = (h1, -gr1x * h1 + gr1.R);
            if (gr2x - gr1x != 0)
                h2 = (gr2.G - gr1.G) / (double)(gr2x - gr1x);
            else
                h2 = 0;
            (double, double) g1 = (h2, -gr1x * h2 + gr1.G);
            if (gr2x - gr1x != 0)
                h3 = (gr2.B - gr1.B) / (double)(gr2x - gr1x);
            else
                h3 = 0;
            (double, double) b1 = (h3, -gr1x * h3 + gr1.B);
            //уравнения цветов стороны 1-3
            if (gr3x - gr1x != 0)
                h1 = (gr3.R - gr1.R) / (double)(gr3x - gr1x);
            else
                h1 = 0;
            (double, double) r2 = (h1, -gr1x * h1 + gr1.R);
            if (gr3x - gr1x != 0)
                h2 = (gr3.G - gr1.G) / (double)(gr3x - gr1x);
            else
                h2 = 0;
            (double, double) g2 = (h2, -gr1x * h2 + gr1.G);
            if (gr3x - gr1x != 0)
                h3 = (gr3.B - gr1.B) / (double)(gr3x - gr1x);
            else
                h3 = 0;
            (double, double) b2 = (h3, -gr1x * h3 + gr1.B);
            //уравнения цветов стороны 2-3
            if (gr3x - gr2x != 0)
                h1 = (gr3.R - gr2.R) / (double)(gr3x - gr2x);
            else
                h1 = 0;
            (double, double) r3 = (h1, -gr2x * h1 + gr2.R);
            if (gr3x - gr2x != 0)
                h2 = (gr3.G - gr2.G) / (double)(gr3x - gr2x);
            else
                h2 = 0;
            (double, double) g3 = (h2, -gr2x * h2 + gr2.G);
            if (gr3x - gr1x != 0)
                h3 = (gr3.B - gr2.B) / (double)(gr3x - gr2x);
            else
                h3 = 0;
            (double, double) b3 = (h3, -gr2x * h3 + gr2.B);
            //Console.WriteLine(r1 + " " + g1 + " " + b1);            
            for (int i = gr1y + 1; i <= gr3y; i++)
            {
                double x1 = ur1_2.Item1 * i + ur1_2.Item2, x2 = (ur1_3.Item1 * i + ur1_3.Item2);
                Color tekcl = Color.FromArgb((int)(r1.Item1 * x1 + r1.Item2), (int)(g1.Item1 * x1 + g1.Item2), 
                    (int)(b1.Item1 * x1 + b1.Item2));
                Color tekc2 = Color.FromArgb((int)(r2.Item1 * x2 + r2.Item2), (int)(g2.Item1 * x2 + g2.Item2),
                    (int)(b2.Item1 * x2 + b2.Item2));
                Color tekc = Color.FromArgb((tekcl.R + tekc2.R) / 2, (tekcl.G + tekc2.G) / 2, (tekcl.B + tekc2.B) / 2);
                Pen tekp = new Pen(tekc, 1);
                //Console.WriteLine(i + " " + (float)(ur1_2.Item1 * i + ur1_2.Item2) + " " + (float)(ur1_3.Item1 * i + ur1_3.Item2));
                g.DrawLine(tekp, (float)(x1), i, (float)(x2), i);
            }
            for (int i = gr3y + 1; i < gr2y; i++)
            {
                double x1 = ur1_2.Item1 * i + ur1_2.Item2, x2 = (ur2_3.Item1 * i + ur2_3.Item2);
                Color tekcl = Color.FromArgb((int)(r1.Item1 * x1 + r1.Item2), (int)(g1.Item1 * x1 + g1.Item2), 
                    (int)(b1.Item1 * x1 + b1.Item2));
                Color tekc3 = Color.FromArgb((int)(r3.Item1 * x2 + r3.Item2), (int)(g3.Item1 * x2 + g3.Item2),
                    (int)(b3.Item1 * x2 + b3.Item2));
                Color tekc = Color.FromArgb((tekcl.R + tekc3.R) / 2, (tekcl.G + tekc3.G) / 2, (tekcl.B + tekc3.B) / 2);
                Pen tekp = new Pen(tekc, 1);
                //Console.WriteLine(i + " " + (float)(ur1_2.Item1 * i + ur1_2.Item2) + " " + (float)(ur1_3.Item1 * i + ur1_3.Item2));
                g.DrawLine(tekp, (float)(x1), i, (float)(x2), i);
            }
            pictureBox1.Invalidate();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            main.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                gr1 = colorDialog1.Color;
                //p1.Color = gr1;
                ((Bitmap)pictureBox1.Image).SetPixel(gr1x, gr1y, gr1);
                pictureBox1.Invalidate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                gr2 = colorDialog1.Color;
                //p2.Color = gr2;
                ((Bitmap)pictureBox1.Image).SetPixel(gr2x, gr2y, gr2);
                pictureBox1.Invalidate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                gr3 = colorDialog1.Color;
                //p3.Color = gr3;
                ((Bitmap)pictureBox1.Image).SetPixel(gr3x, gr3y, gr3);
                pictureBox1.Invalidate();
            }
        }
    }
}
