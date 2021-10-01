﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace CompGraf3
{
    public partial class Form4 : Form
    {
        private Form1 main;
        int gr1x = 100,gr1y = 100;
        int gr2x = 150, gr2y = 400;
        int gr3x = 600, gr3y = 200;
        Pen p3 = new Pen(Color.Red, 1), p1 = new Pen(Color.Green, 1), p2 = new Pen(Color.Blue, 1);
        Color gr1 = Color.Green, gr2 = Color.Blue, gr3 = Color.Red;
        private Graphics g;
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
            //g.DrawEllipse(p1, gr1x-25, gr1y - 25, 50, 50);
            //g.DrawEllipse(p2, gr2x - 25, gr2y - 25, 50, 50);
            //g.DrawEllipse(p3, gr3x - 25, gr3y - 25, 50, 50);
            //уравнения сторон
            double h1 = (gr2x - gr1x) / (double)(gr2y - gr1y);
            (double, double) ur1_2 = (h1,-gr1y*h1 + gr1x);
            double h2 = (gr3x - gr1x) / (double)(gr3y - gr1y);
            (double, double) ur1_3 = (h2, -(gr1y * h2) + gr1x);
            double h3 = (gr3x - gr2x) / (double)(gr3y - gr2y);
            (double, double) ur2_3 = (h3, -(gr2y * h3) + gr2x);
            //уравнения цветов стороны 1-2
            if (gr2x - gr1x != 0)
                h1 = (p2.Color.R - p1.Color.R) / (double)(gr2x - gr1x);
            else
                h1 = 0;
            (double, double) r1 = (h1, -gr1x * h1 + p1.Color.R);
            if (gr2x - gr1x != 0)
                h2 = (p2.Color.G - p1.Color.G) / (double)(gr2x - gr1x);
            else
                h2 = 0;
            (double, double) g1 = (h2, -gr1x * h2 + p1.Color.G);
            if (gr2x - gr1x != 0)
                h3 = (p2.Color.B - p1.Color.B) / (double)(gr2x - gr1x);
            else
                h3 = 0;
            (double, double) b1 = (h3, -gr1x * h3 + p1.Color.B);
            //уравнения цветов стороны 1-3
            if (gr3x - gr1x != 0)
                h1 = (p3.Color.R - p1.Color.R) / (double)(gr3x - gr1x);
            else
                h1 = 0;
            (double, double) r2 = (h1, -gr1x * h1 + p1.Color.R);
            if (gr3x - gr1x != 0)
                h2 = (p3.Color.G - p1.Color.G) / (double)(gr3x - gr1x);
            else
                h2 = 0;
            (double, double) g2 = (h2, -gr1x * h2 + p1.Color.G);
            if (gr3x - gr1x != 0)
                h3 = (p3.Color.B - p1.Color.B) / (double)(gr3x - gr1x);
            else
                h3 = 0;
            (double, double) b2 = (h3, -gr1x * h3 + p1.Color.B);
            //уравнения цветов стороны 2-3
            if (gr3x - gr2x != 0)
                h1 = (p3.Color.R - p2.Color.R) / (double)(gr3x - gr2x);
            else
                h1 = 0;
            (double, double) r3 = (h1, -gr2x * h1 + p2.Color.R);
            if (gr3x - gr2x != 0)
                h2 = (p3.Color.G - p2.Color.G) / (double)(gr3x - gr2x);
            else
                h2 = 0;
            (double, double) g3 = (h2, -gr2x * h2 + p2.Color.G);
            if (gr3x - gr1x != 0)
                h3 = (p3.Color.B - p2.Color.B) / (double)(gr3x - gr2x);
            else
                h3 = 0;
            (double, double) b3 = (h3, -gr2x * h3 + p2.Color.B);
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
                p1.Color = gr1;
                ((Bitmap)pictureBox1.Image).SetPixel(gr1x, gr1y, gr1);
                pictureBox1.Invalidate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                gr2 = colorDialog1.Color;
                p2.Color = gr2;
                ((Bitmap)pictureBox1.Image).SetPixel(gr2x, gr2y, gr2);
                pictureBox1.Invalidate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                gr3 = colorDialog1.Color;
                p3.Color = gr3;
                ((Bitmap)pictureBox1.Image).SetPixel(gr3x, gr3y, gr3);
                pictureBox1.Invalidate();
            }
        }
    }
}
