using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shoting_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //asdfas
        }

        Rectangle[] projectiles = new Rectangle[60];
        int projectileNum = 0;
        bool ProjectileExists = false;
        int Energy = 40;
        int SpeedX = 0, SpeedY = 0, PosPlayerX = 300, PosPlayerY = 300;
        double[] dx = new double[60], dy = new double[60];

        private void Form1_Shown(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //projectile
            if (Energy > 0)
            {
                dx[projectileNum] = e.X - PosPlayerX; dy[projectileNum] = PosPlayerY - e.Y;

                projectiles[projectileNum] = new Rectangle(PosPlayerX + 5, PosPlayerY + 5, 6, 6);
                projectileNum++;
                Energy--;
                ProjectileExists = true;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                SpeedY = -2;
            }
            if (e.KeyCode == Keys.Left)
            {
                SpeedX = -2;
            }
            if (e.KeyCode == Keys.Down)
            {
                SpeedY = 2;
            }
            if (e.KeyCode == Keys.Right)
            {
                SpeedX = 2;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();

            g.FillRectangle(new SolidBrush(BackColor), PosPlayerX, PosPlayerY, 16, 16);
            PosPlayerX += SpeedX;
            PosPlayerY += SpeedY;
            g.FillRectangle(Brushes.Black, PosPlayerX, PosPlayerY, 16, 16);

            //projectile
            if (ProjectileExists)
            {
                for (int i = 0; i < projectileNum; i++)
                {
                    g.FillRectangle(new SolidBrush(BackColor), projectiles[i]);
                    double slope = Math.Abs(dy[i] / dx[i]);
                    double k = 6 / Math.Sqrt((double)(16 + 16 * slope * slope));
                    projectiles[i].Location = new Point((int)(projectiles[i].X + 4 * Math.Sign(dx[i]) * k), (int)(projectiles[i].Y + 4 * slope * Math.Sign(-dy[i]) * k));
                    g.FillRectangle(Brushes.Red, projectiles[i]);
                }
                for (int i = 0; i < projectileNum; i++)
                {
                    if (projectiles[i].X >= -6 && projectiles[i].X <= ClientSize.Width + 6 && projectiles[i].Y >= -6 && projectiles[i].Y <= ClientSize.Height + 6)
                    {
                        ProjectileExists = true;
                        break;
                    }
                    else
                    {
                        ProjectileExists = false;
                    }
                }
            }
            else
            {
                projectileNum = 0;
                if (Energy < 40)
                {
                    Energy++;
                }
            }

            label1.Text = string.Format("{0}; {1}", Energy, ProjectileExists);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                SpeedY = 0;
            }
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                SpeedX = 0;
            }
        }
    }
}
