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
        public Form1()
        {
            InitializeComponent();
        }

        Graphics g;
        private void Form1_Shown(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            g = CreateGraphics();
            timer1.Enabled = true;
            timer2.Enabled = true;
        }

        Player p1 = new Player(Color.Black, 16, new Vettore(300, 300), new Vettore(0, 0), 2);
        Player p2 = new Player(Color.Green, 16, new Vettore(600, 300), new Vettore(0, 0), 2);
        Projectile special = new Projectile("Special", Color.Yellow, 4, 7, 0, 16);
        Projectile special2 = new Projectile("2° special", Color.Yellow, 4, 7, 60000, 300000);
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            p1.MoveStart(e, Keys.W, Keys.S, Keys.A, Keys.D);
            p2.MoveStart(e, Keys.Up, Keys.Down, Keys.Left, Keys.Right);

            if (e.KeyCode == Keys.Q && special.CurrentEnergy >= 4)
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
            p1.MoveEnd(e, Keys.W, Keys.S, Keys.A, Keys.D);
            p2.MoveEnd(e, Keys.Up, Keys.Down, Keys.Left, Keys.Right);
        }

        Projectile prj = new Projectile("Normal projectile", Color.Red, 6, 6, 40);
        Projectile bomb = new Projectile("Bomb", Color.DarkGray, 10, 4, 4);
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

        List<Rectangle> fruits = new List<Rectangle>();
        Random rnd = new Random();
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
            p2.MovePlayer(g, BackColor);

            for (int i = 0; i < fruits.Count(); i++)
            {
                if (new Rectangle((int)p1.Position.X, (int)p1.Position.Y, 16, 16).IntersectsWith(fruits[i]))
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

            label1.Text = string.Format("{0}; {1}; {2}; {3}", prj.ToString(), bomb.ToString(), special.ToString("4"), special2.ToString("3"));
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < fruits.Count(); i++)
            {
                g.FillRectangle(Brushes.Blue, fruits[i]);
            }
        }
    }
}
