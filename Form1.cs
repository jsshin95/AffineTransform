using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AffineTransform
{
    public partial class Form1 : Form
    {
        double a, b, c, d, e, f;
        double x1, y1, x2, y2, x3, y3;
        double xp1, yp1, xp2, yp2, xp3, yp3;
        double den;
        double x4, y4, xp4, yp4;
        double r = 0;
        int pcResult1 = 0;
        int pcResult2 = 0;
        int b1flag =0;
        double MaxLen = 0;
        double mx = 0;
        double my = 0;
        double ratio = 0;
        PointF P1, P2, P3;
        PointF Pp1, Pp2, Pp3;
        float tx,ty,tpx,tpy;
        double MaxLen2 = 0;
        double ratio2 = 0;
        PointF Q1, Q2, Q3;
        PointF Qp1, Qp2, Qp3;
        double size = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs ee)
        {
            if (tbx1.Text == "")
            {
                x1 = 0;
                tbx1.Text = "0";
            }
            else x1 = Convert.ToDouble(tbx1.Text);

            if (tby1.Text == "")
            {
                y1 = 0;
                tby1.Text = "0";
            }
            else y1 = Convert.ToDouble(tby1.Text);

            if (tbx2.Text == "")
            {
                x2 = 0;
                tbx2.Text = "0";
            }
            else x2 = Convert.ToDouble(tbx2.Text);

            if (tby2.Text == "")
            {
                y2 = 0;
                tby2.Text = "0";
            }
            else y2 = Convert.ToDouble(tby2.Text);

            if (tbx3.Text == "")
            {
                x3 = 0;
                tbx3.Text = "0";
            }
            else x3 = Convert.ToDouble(tbx3.Text);

            if (tby3.Text == "")
            {
                y3 = 0;
                tby3.Text = "0";
            }
            else y3 = Convert.ToDouble(tby3.Text);

            if (tbxp1.Text == "")
            {
                xp1 = 0;
                tbxp1.Text = "0";
            }
            else xp1 = Convert.ToDouble(tbxp1.Text);

            if (tbyp1.Text == "")
            {
                yp1 = 0;
                tbyp1.Text = "0";
            }
            else yp1 = Convert.ToDouble(tbyp1.Text);

            if (tbxp2.Text == "")
            {
                xp2 = 0;
                tbxp2.Text = "0";
            }
            else xp2 = Convert.ToDouble(tbxp2.Text);

            if (tbyp2.Text == "")
            {
                yp2 = 0;
                tbyp2.Text = "0";
            }
            else yp2 = Convert.ToDouble(tbyp2.Text);

            if (tbxp3.Text == "")
            {
                xp3 = 0;
                tbxp3.Text = "0";
            }
            else xp3 = Convert.ToDouble(tbxp3.Text);

            if (tbyp3.Text == "")
            {
                yp3 = 0;
                tbyp3.Text = "0";
            }
            else yp3 = Convert.ToDouble(tbyp3.Text);

            tbx4.Text = "";
            tby4.Text = "";
            tbxp4.Text = "";
            tbyp4.Text = "";

            pcResult1 = PointCheck(x1, y1, x2, y2, x3, y3);
            pcResult2 = PointCheck(xp1, yp1, xp2, yp2, xp3, yp3);

            if (pcResult1==1 || pcResult1==1)
            {
                MessageBox.Show("서로 다른 세 점을 입력해주십시오.");
            }
            else if(pcResult1==2)
            {
                MessageBox.Show("P1, P2, P3가 일직선 상에 있습니다.");
            }
            else if (pcResult2 == 2)
            {
                MessageBox.Show("P'1, P'2, P'3가 일직선 상에 있습니다.");
            }
            else
            {
                den = x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2);

                a = xp1 * (y2 - y3) + xp2 * (y3 - y1) + xp3 * (y1 - y2);
                b = xp1 * (x3 - x2) + xp2 * (x1 - x3) + xp3 * (x2 - x1);
                c = yp1 * (y2 - y3) + yp2 * (y3 - y1) + yp3 * (y1 - y2);
                d = yp1 * (x3 - x2) + yp2 * (x1 - x3) + yp3 * (x2 - x1);
                e = xp1 * (x2 * y3 - x3 * y2) + xp2 * (x3 * y1 - x1 * y3) + xp3 * (x1 * y2 - x2 * y1);
                f = yp1 * (x2 * y3 - x3 * y2) + yp2 * (x3 * y1 - x1 * y3) + yp3 * (x1 * y2 - x2 * y1);

                a = a / den;
                b = b / den;
                c = c / den;
                d = d / den;
                e = e / den;
                f = f / den;

                size = a * d - b * c; //x축배율*y축배율
                tbsize.Text = size.ToString("F2");

                if(Math.Abs(a * c + b * d) < 0.2) //no shearing
                {
                    r = Math.Atan2(c,d) * (180 / Math.PI);
                    tbrotation.Text = r.ToString("F2") + "º";
                }
                else
                {
                    r = Math.Atan2((a * c) - (b * d), 2d * (a * d)) * (180 / Math.PI);
                    tbrotation.Text= r.ToString("F2")+"º (shearing 발생)";
                }

                mx = FindMaxAbs(x1, x2, x3, xp1, xp2, xp3);
                my = FindMaxAbs(y1, y2, y3, yp1, yp2, yp3);
                if (mx >= my) MaxLen = mx;
                else MaxLen = my;

                MaxLen = MaxLen * 1.1d;
                ratio = 200d / MaxLen;

                P1.X = (float)(200d + x1 * ratio);
                P1.Y = (float)(200d - y1 * ratio);
                P2.X = (float)(200d + x2 * ratio);
                P2.Y = (float)(200d - y2 * ratio);
                P3.X = (float)(200d + x3 * ratio);
                P3.Y = (float)(200d - y3 * ratio);

                Pp1.X = (float)(200d + xp1 * ratio);
                Pp1.Y = (float)(200d - yp1 * ratio);
                Pp2.X = (float)(200d + xp2 * ratio);
                Pp2.Y = (float)(200d - yp2 * ratio);
                Pp3.X = (float)(200d + xp3 * ratio);
                Pp3.Y = (float)(200d - yp3 * ratio);

                Graphics g = panel1.CreateGraphics();
                g.Clear(Color.White);
                g.DrawLine(Pens.Black, 200, 0, 200, 400);  //y축
                g.DrawLine(Pens.Black, 0, 200, 400, 200);  //x축
                g.DrawLine(Pens.DimGray, P1, P2);
                g.DrawLine(Pens.DimGray, P2, P3);
                g.DrawLine(Pens.DimGray, P3, P1);
                g.DrawLine(Pens.DarkBlue, Pp1, Pp2);
                g.DrawLine(Pens.DarkBlue, Pp2, Pp3);
                g.DrawLine(Pens.DarkBlue, Pp3, Pp1);

                b1flag = 1;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            tbx1.Text = "";
            tby1.Text = "";
            tbx2.Text = "";
            tby2.Text = "";
            tbx3.Text = "";
            tby3.Text = "";
            tbxp1.Text = "";
            tbyp1.Text = "";
            tbxp2.Text = "";
            tbyp2.Text = "";
            tbxp3.Text = "";
            tbyp3.Text = "";
            tbx4.Text = "";
            tby4.Text = "";
            tbxp4.Text = "";
            tbyp4.Text = "";
            b1flag = 0;
            Graphics g = panel1.CreateGraphics();
            g.Clear(Color.White);
        }

        private void button2_Click(object sender, EventArgs ee)
        {
            if (b1flag == 0)
            {
                MessageBox.Show("변환이 설정되지 않았습니다. P->P'변환 설정을 먼저 진행해주세요.");
                return;
            }

            if (tbx4.Text == "")
            {
                x4 = 0;
                tbx4.Text = "0";
            }
            else x4 = Convert.ToDouble(tbx4.Text);

            if (tby4.Text == "")
            {
                y4 = 0;
                tby4.Text = "0";
            }
            else y4 = Convert.ToDouble(tby4.Text);

            xp4 = a * x4 + b * y4 + e;
            yp4 = c * x4 + d * y4 + f;

            tbxp4.Text = xp4.ToString("F2");
            tbyp4.Text = yp4.ToString("F2");

            if ((Math.Abs(x4) >= MaxLen) || (Math.Abs(y4) >= MaxLen) || (Math.Abs(xp4) >= MaxLen) || (Math.Abs(yp4) >= MaxLen))
            {
                MaxLen2 = FindMaxAbs(x4, y4, xp4, yp4, MaxLen, 0);
                MaxLen2 = MaxLen2 * 1.1d;
                ratio2 = 200d / MaxLen2;
            }
            else ratio2 = ratio;

            tx = (float)(200d + x4 * ratio2);
            ty = (float)(200d - y4 * ratio2);
            tpx = (float)(200d + xp4 * ratio2);
            tpy = (float)(200d - yp4 * ratio2);

            Q1.X = (float)(200d + x1 * ratio2);
            Q1.Y = (float)(200d - y1 * ratio2);
            Q2.X = (float)(200d + x2 * ratio2);
            Q2.Y = (float)(200d - y2 * ratio2);
            Q3.X = (float)(200d + x3 * ratio2);
            Q3.Y = (float)(200d - y3 * ratio2);

            Qp1.X = (float)(200d + xp1 * ratio2);
            Qp1.Y = (float)(200d - yp1 * ratio2);
            Qp2.X = (float)(200d + xp2 * ratio2);
            Qp2.Y = (float)(200d - yp2 * ratio2);
            Qp3.X = (float)(200d + xp3 * ratio2);
            Qp3.Y = (float)(200d - yp3 * ratio2);

            Graphics g = panel1.CreateGraphics();
            g.Clear(Color.White);
            g.DrawLine(Pens.Black, 200, 0, 200, 400);  //y축
            g.DrawLine(Pens.Black, 0, 200, 400, 200);  //x축
            g.DrawLine(Pens.DimGray, Q1, Q2);
            g.DrawLine(Pens.DimGray, Q2, Q3);
            g.DrawLine(Pens.DimGray, Q3, Q1);
            g.DrawLine(Pens.DarkBlue, Qp1, Qp2);
            g.DrawLine(Pens.DarkBlue, Qp2, Qp3);
            g.DrawLine(Pens.DarkBlue, Qp3, Qp1);
            
            g.FillEllipse(Brushes.Green, tx - 2f, ty - 2f, 4f, 4f);
            g.FillEllipse(Brushes.Red, tpx - 2f, tpy - 2f, 4f, 4f);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (b1flag == 0)
            {
                MessageBox.Show("변환이 설정되지 않았습니다. P->P'변환 설정을 먼저 진행해주세요.");
                return;
            }
            tbx4.Text = "";
            tby4.Text = "";
            tbxp4.Text = "";
            tbyp4.Text = "";

            Graphics g = panel1.CreateGraphics();
            g.Clear(Color.White);
            g.DrawLine(Pens.Black, 200, 0, 200, 400);  //y축
            g.DrawLine(Pens.Black, 0, 200, 400, 200);  //x축
            g.DrawLine(Pens.DimGray, P1, P2);
            g.DrawLine(Pens.DimGray, P2, P3);
            g.DrawLine(Pens.DimGray, P3, P1);
            g.DrawLine(Pens.DarkBlue, Pp1, Pp2);
            g.DrawLine(Pens.DarkBlue, Pp2, Pp3);
            g.DrawLine(Pens.DarkBlue, Pp3, Pp1);
        }

        private int PointCheck(double x1, double y1, double x2, double y2, double x3, double y3) //0 : 삼각형 , 1: 일치 , 2 : 직선
        { 
            double slope1, slope2;

            if((x1==x2)&& (y1 == y2)|| (x1 == x3) && (y1 == y3)|| (x3 == x2) && (y3 == y2))
            {
                return 1;
            }
            else if(x1==x2)
            {
                if (x1 == x3) return 2;
                else return 0;
            }
            else if(x1==x3)
            {
                return 0;
            }
            else
            {
                slope1 = (y2 - y1) / (x2 - x1);
                slope2 = (y3 - y1) / (x3 - x1);
                if (slope1 == slope2) return 2;
                else return 0;
            }
        }

        private double FindMaxAbs(double a,double b,double c,double d,double e, double f)
        {
            double m = 0;
            if (Math.Abs(a) >= m) m = Math.Abs(a);
            if (Math.Abs(b) >= m) m = Math.Abs(b);
            if (Math.Abs(c) >= m) m = Math.Abs(c);
            if (Math.Abs(d) >= m) m = Math.Abs(d);
            if (Math.Abs(e) >= m) m = Math.Abs(e);
            if (Math.Abs(f) >= m) m = Math.Abs(f);
            return m;
        }

        private void tbx1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && Convert.ToChar(Keys.Back) != e.KeyChar && e.KeyChar != '.' && e.KeyChar != '-') e.Handled = true;
        }

        private void tby1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && Convert.ToChar(Keys.Back) != e.KeyChar && e.KeyChar != '.' && e.KeyChar != '-') e.Handled = true;
        }

        private void tbx2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && Convert.ToChar(Keys.Back) != e.KeyChar && e.KeyChar != '.' && e.KeyChar != '-') e.Handled = true;
        }

        private void tby2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && Convert.ToChar(Keys.Back) != e.KeyChar && e.KeyChar != '.' && e.KeyChar != '-') e.Handled = true;
        }
        
        private void tbx3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && Convert.ToChar(Keys.Back) != e.KeyChar && e.KeyChar != '.' && e.KeyChar != '-') e.Handled = true;
        }

        private void tby3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && Convert.ToChar(Keys.Back) != e.KeyChar && e.KeyChar != '.' && e.KeyChar != '-') e.Handled = true;
        }

        private void tbxp1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && Convert.ToChar(Keys.Back) != e.KeyChar && e.KeyChar != '.' && e.KeyChar != '-') e.Handled = true;
        }

        private void tbyp1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && Convert.ToChar(Keys.Back) != e.KeyChar && e.KeyChar != '.' && e.KeyChar != '-') e.Handled = true;
        }

        private void tbxp2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && Convert.ToChar(Keys.Back) != e.KeyChar && e.KeyChar != '.' && e.KeyChar != '-') e.Handled = true;
        }

        private void tbyp2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && Convert.ToChar(Keys.Back) != e.KeyChar && e.KeyChar != '.' && e.KeyChar != '-') e.Handled = true;
        }

        private void tbxp3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && Convert.ToChar(Keys.Back) != e.KeyChar && e.KeyChar != '.' && e.KeyChar != '-') e.Handled = true;
        }

        private void tbyp3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && Convert.ToChar(Keys.Back) != e.KeyChar && e.KeyChar != '.' && e.KeyChar != '-') e.Handled = true;
        }

        private void tbx4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && Convert.ToChar(Keys.Back) != e.KeyChar && e.KeyChar != '.' && e.KeyChar != '-') e.Handled = true;
        }

        private void tby4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && Convert.ToChar(Keys.Back) != e.KeyChar && e.KeyChar != '.' && e.KeyChar != '-') e.Handled = true;
        }
    }
}
