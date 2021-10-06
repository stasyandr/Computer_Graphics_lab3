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
    public partial class Form2 : Form
    {
        private Form1 main;
        private Graphics g;
        Pen lines, pzal;
        Bitmap piczal;
        List<Point> ls = new List<Point>();
        List<(SByte, SByte)> t = new List<(sbyte, sbyte)>();
        Dictionary<int, List<(int, int)>> dic = new Dictionary<int, List<(int, int)>>();
        (SByte, SByte) d6 = (0,1),d2=(0,-1),d4=(-1,0),d0=(1,0),d1 = (1,-1),d3=(-1,-1),d5=(-1,1),d7=(1,1);
        public Form2(Form1 form1)
        {
            main = form1;
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
            pictureBox1.Invalidate();
            lines = new Pen(Color.Black, 1);
            pzal = new Pen(Color.Green, 1);
            //g.DrawEllipse(lines, pictureBox1.Width / 2, pictureBox1.Height / 2, 200, 200);
            radioButton1.Checked = true;
            t.Add(d0); t.Add(d1); t.Add(d2); t.Add(d3); t.Add(d4); t.Add(d5); t.Add(d6); t.Add(d7);
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
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
            g.Clear(Color.White);
            pictureBox1.Invalidate();
        }

        private Point prev;
        private bool draw_lines = false;
        
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (draw_lines)
            {
                //Console.WriteLine("lines");
                g.DrawLine(lines, prev.X, prev.Y, e.X, e.Y);
                prev = e.Location;
                pictureBox1.Invalidate();
                //pictureBox1.Refresh();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog image = new OpenFileDialog();
            image.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG;*.JPEG)|*.BMP;*.JPG;*.GIF;*.PNG;*.JPEG|All files (*.*)|*.*";
            if (image.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    piczal = new Bitmap(image.FileName);
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            prev = e.Location;
            draw_lines = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            draw_lines = false;
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
                zal1(e.X, e.Y);
            if (radioButton2.Checked)
            {
                if (piczal == null)
                    MessageBox.Show("Изображение для заливки не выбрано");
                else
                    zal2(e.X, e.Y);
                dic.Clear();
            }
            if (radioButton3.Checked)
            {
                if (piczal == null)
                    MessageBox.Show("Изображение для заливки не выбрано");
                else
                    zal3(e.X, e.Y);
                ls.Clear();
            }
            pictureBox1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pzal.Color = colorDialog1.Color;
                //button2.BackColor = colorDialog1.Color;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                button2.Enabled = true;
                button3.Enabled = false;
            }
            else
            {
                button2.Enabled = false;
                button3.Enabled = true;
            }
        }

        private void zal1(int x, int y)
        {
            if (y == pictureBox1.Height || y == 0)
                return;
            int left = x, right = x;
            Color tek = ((Bitmap)pictureBox1.Image).GetPixel(left, y);
            if ((tek.G == pzal.Color.G) && (tek.B == pzal.Color.B) && (tek.R == pzal.Color.R)
                || (tek.G == lines.Color.G) && (tek.B == lines.Color.B) && (tek.R == lines.Color.R)
                )
                return;
            while (left > 0 && !((tek.G == lines.Color.G) && (tek.B == lines.Color.B) && (tek.R == lines.Color.R)))
            {
                tek = ((Bitmap)pictureBox1.Image).GetPixel(left, y);
                left--;
            }
            tek = ((Bitmap)pictureBox1.Image).GetPixel(right, y);
            while (right < pictureBox1.Width && !((tek.G == lines.Color.G) && (tek.B == lines.Color.B) && (tek.R == lines.Color.R)))
            {
                tek = ((Bitmap)pictureBox1.Image).GetPixel(right, y);
                right++;
            }
            g.DrawLine(pzal, left + 2, y, right - 2, y);
            for (int i = left+2; i < right-1; i++)
            {
                zal1(i, y + 1);
                zal1(i, y - 1);
            }
        }
        private void zal2(int x, int y)
        {
            if (y == pictureBox1.Height || y == 0)
                return;
            int left = x, right = x;
            Color tek = ((Bitmap)pictureBox1.Image).GetPixel(left, y);
            if (dic.ContainsKey(y))
            {
                foreach (var a in dic[y])
                    if (a.Item1 <= x && x <= a.Item2)
                        return;
            }
            if (//(tek.G == pzal.Color.G) && (tek.B == pzal.Color.B) && (tek.R == pzal.Color.R) ||
                (tek.G == lines.Color.G) && (tek.B == lines.Color.B) && (tek.R == lines.Color.R)
                //|| !((tek.G == 255) && (tek.B == 255) && (tek.R == 255) 
                    //|| (tek.G == pzal.Color.G) && (tek.B == pzal.Color.B) && (tek.R == pzal.Color.R))
                    )
                return;
            while (left > 0 && !((tek.G == lines.Color.G) && (tek.B == lines.Color.B) && (tek.R == lines.Color.R)))
            {
                tek = ((Bitmap)pictureBox1.Image).GetPixel(left, y);
                left--;
            }
            tek = ((Bitmap)pictureBox1.Image).GetPixel(right, y);
            while (right < pictureBox1.Width && !((tek.G == lines.Color.G) && (tek.B == lines.Color.B) && (tek.R == lines.Color.R)))
            {
                tek = ((Bitmap)pictureBox1.Image).GetPixel(right, y);
                right++;
            }
            left += 2; right -= 2;
            if (dic.ContainsKey(y))
                if (dic[y].Contains((left, right)))
                {
                    return;
                }
                else
                {
                    dic[y].Add((left, right));
                }
            else
            {
                dic[y] = new List<(int, int)>();
                dic[y].Add((left, right));
            }
            int picright = right - left;
            int tekl = left, tekr = right+1;
            while (picright / piczal.Width > 0)
            {
                tekr = tekl + piczal.Width;
                for (int i = 0; i <= tekr - tekl; i++)
                {
                    Color tekc = piczal.GetPixel((tekl + i) % piczal.Width, y % piczal.Height);
                    ((Bitmap)pictureBox1.Image).SetPixel(tekl + i, y, tekc);
                }
                tekl += piczal.Width;
                picright -= piczal.Width;
                if (picright < piczal.Width)
                    tekr += picright;
                else
                    tekr += piczal.Width;
                
            }
            for (int i = 0; i <= tekr - tekl-1; i++)
            {
                Color tekc = piczal.GetPixel((tekl + i) % piczal.Width, y % piczal.Height);
                ((Bitmap)pictureBox1.Image).SetPixel(tekl + i, y, tekc);
            }
            for (int i = left; i <= right; i++)
            {
                zal2(i, y + 1);
                zal2(i, y - 1);
            }
        }
        private (SByte, SByte) help_dir(int d)
        {
            switch (d)
            {
                case 0:
                    return d0;
                case 1:
                    return d1;
                case 2:
                    return d2;
                case 3:
                    return d3;
                case 4:
                    return d4;
                case 5:
                    return d5;
                case 6:
                    return d6;
                case 7:
                    return d7;
                default:
                    return (0, 0);
            }
        }
        private void zal3(int x, int y)
        {
            int tekx = x, teky = y;
            int dir = 6;
            Color tek = ((Bitmap)pictureBox1.Image).GetPixel(tekx, teky);
            while (!((tek.G == lines.Color.G) && (tek.B == lines.Color.B) && (tek.R == lines.Color.R)))
            {
                tekx++;
                if (tekx == pictureBox1.Width)
                {
                    MessageBox.Show("Вызов за пределами связной области");
                    return;
                }
                tek = ((Bitmap)pictureBox1.Image).GetPixel(tekx, teky);
            }
            ls.Add(new Point(tekx, teky));
            int finx = tekx;
            (SByte, SByte) del = (0, 0);
            del = help_dir(dir);
            if ((tek.G == lines.Color.G) && (tek.B == lines.Color.B) && (tek.R == lines.Color.R))
            {
                ls.Add(new Point(tekx + del.Item1, teky + del.Item2));
                dir = (dir + 6) % 8;
                tekx += del.Item1;
                teky += del.Item2;
            }
            else
            {
                while (!((tek.G == lines.Color.G) && (tek.B == lines.Color.B) && (tek.R == lines.Color.R)))
                {
                    dir = (dir + 1) % 8;
                    del = help_dir(dir);
                    tek = ((Bitmap)pictureBox1.Image).GetPixel(tekx + del.Item1, teky + del.Item2);
                }
                ls.Add(new Point(tekx + del.Item1, teky + del.Item2));
                dir = (dir + 6) % 8;
                tekx += del.Item1;
                teky += del.Item2;
            }
            del = help_dir(dir);
            tek = ((Bitmap)pictureBox1.Image).GetPixel(tekx + del.Item1, teky + del.Item2);
            int count = 0;
            while (tekx != finx || teky != y)
            {
                if ((tek.G == lines.Color.G) && (tek.B == lines.Color.B) && (tek.R == lines.Color.R)) {
                    ls.Add(new Point(tekx + del.Item1, teky + del.Item2));
                    //dirwas = dir;
                    dir = (dir + 6) % 8;
                    tekx += del.Item1;
                    teky += del.Item2;
                }
                else 
                {
                    while (!((tek.G == lines.Color.G) && (tek.B == lines.Color.B) && (tek.R == lines.Color.R)))
                    {
                        dir = (dir + 1) % 8;
                        del = help_dir(dir);
                        if ((tekx + del.Item1 >= pictureBox1.Width) || (teky + del.Item2 >= pictureBox1.Height) || 
                            (tekx + del.Item1 <= 0) || (teky + del.Item2 <= 0))
                        {
                            MessageBox.Show("Выход за границы области изображения");
                            return;
                        }
                        tek = ((Bitmap)pictureBox1.Image).GetPixel(tekx + del.Item1, teky + del.Item2);
                    }
                    if (ls.Contains(new Point(tekx + del.Item1, teky + del.Item2)))
                        count++;
                    if (count == 10)
                    {
                        MessageBox.Show("Область не связна");
                        return;
                    }
                    ls.Add(new Point(tekx + del.Item1, teky + del.Item2));
                    dir = (dir + 6) % 8;
                    tekx += del.Item1;
                    teky += del.Item2;
                }
                del = help_dir(dir);
                if ((tekx >= pictureBox1.Width + del.Item1) || (teky >= pictureBox1.Height + del.Item2) ||
                            (tekx + del.Item1 <= 0) || (teky + del.Item2 <= 0))
                {
                    MessageBox.Show("Выход за границы области изображения");
                    return;
                }
                tek = ((Bitmap)pictureBox1.Image).GetPixel(tekx + del.Item1, teky + del.Item2);
            }
            foreach (var a in ls)
            {
                Color p = piczal.GetPixel(a.X % piczal.Width, a.Y % piczal.Height);
                ((Bitmap)pictureBox1.Image).SetPixel(a.X,a.Y, p);
            }
        }
    }
}
