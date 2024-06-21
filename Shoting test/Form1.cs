using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vettori;

namespace Shoting_test
{
    public partial class Form1 : Form
    {
        Graphics g;
        Random rnd = new Random();
        Player p1 = new Player(Color.Black, 16, new Vettore(300, 300), new Vettore(0, 0));
        Projectile prj = new Projectile(Color.Red, 6, 6, 40);
        Projectile bomb = new Projectile(Color.DarkGray, 10, 4, 4);
        Projectile special = new Projectile(Color.Yellow, 4, 7, 0, 16);
        Projectile special2 = new Projectile(Color.Yellow, 4, 7, 6, 30);
        List<Rectangle> fruits = new List<Rectangle>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            g = CreateGraphics();
            timer1.Enabled = true;
            timer2.Enabled = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            p1.MoveStart(e);
            if(e.KeyCode == Keys.Q && special.CurrentEnergy >= 4)
            {
                special.Shoot(new Vettore(0, 1), p1);
                special.Shoot(new Vettore(0, -1), p1);
                special.Shoot(new Vettore(1, 0), p1);
                special.Shoot(new Vettore(-1, 0), p1);
                special.Energy -= 4;
            }
            if (e.KeyCode == Keys.E && special2.CurrentEnergy >= 3)
            {
                special2.Shoot(MousePosition.X, MousePosition.Y, p1);
                special2.Shoot(MousePosition.X + 20, MousePosition.Y + 20, p1);
                special2.Shoot(MousePosition.X - 20, MousePosition.Y - 20, p1);
                special2.Energy -= 3;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            p1.MoveEnd(e);
        }
        
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                prj.Shoot(e.X, e.Y, p1);
            }
            else if(e.Button == MouseButtons.Right)
            {
                bomb.Shoot(e.X, e.Y, p1);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (fruits.Count() <= 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    fruits.Add(new Rectangle(rnd.Next(0, ClientSize.Width - 10), rnd.Next(0, ClientSize.Height - 10), 10, 10));
                    g.FillRectangle(Brushes.Blue, fruits[i]);
                }
            }
            
            p1.MovePlayer(g, BackColor);

            for (int i = 0; i < fruits.Count(); i++)
            {
                if (new Rectangle((int)p1.PosPlayer.X, (int)p1.PosPlayer.Y, 16, 16).IntersectsWith(fruits[i]))
                {
                    g.FillRectangle(new SolidBrush(BackColor), fruits[i]);
                    fruits.RemoveAt(i);

                    if(special.Energy <= special.MaxEnergy - 4)
                    {
                        special.Energy += 4;
                    }
                    if (special2.Energy <= special2.MaxEnergy - 3)
                    {
                        special2.Energy += 3;
                    }
                }
            }

            prj.MoveProjectile(g, BackColor, ClientSize.Width, ClientSize.Height);
            bomb.MoveProjectile(g, BackColor, ClientSize.Width, ClientSize.Height);
            special.MoveProjectile(g, BackColor, ClientSize.Width, ClientSize.Height);
            special2.MoveProjectile(g, BackColor, ClientSize.Width, ClientSize.Height);

            label1.Text = string.Format("{0}/{3} projectiles left; {1}/{4} bombs left; {2}/{5} special left; {6}/{7} 2° special left"
                                        , prj.CurrentEnergy, bomb.CurrentEnergy, special.CurrentEnergy / 4, prj.Energy, bomb.Energy, special.Energy / 4
                                        , special2.CurrentEnergy / 3, special2.Energy / 3);
        }

        private void timer2_Tick(object sender, EventArgs e)//refreshes
        {
            for (int i = 0; i < fruits.Count(); i++)
            {
                g.FillRectangle(Brushes.Blue, fruits[i]);
            }
        }
    }
}
