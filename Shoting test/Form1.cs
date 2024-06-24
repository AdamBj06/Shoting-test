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
            Main_Timer.Enabled = true;
            Refresh_Timer.Enabled = true;
            Prj2_Timer.Enabled = true;
            SpecialAtk_Timer.Enabled = true;
            Special2Atk_Timer.Enabled = true;
        }

        private HashSet<Keys> keysPressed = new HashSet<Keys>();
        private HashSet<MouseButtons> mouseButtonsPressed = new HashSet<MouseButtons>();
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            keysPressed.Add(e.KeyCode);
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            keysPressed.Remove(e.KeyCode);
        }

        Player p1 = new Player(Color.Black, 16, 100, new Vettore(300, 300), new Vettore(0, 0), 2);
        Player p2 = new Player(Color.Green, 16, 100, new Vettore(600, 300), new Vettore(0, 0), 2);
        Projectile prj = new Projectile("Normal projectile", Color.Red, 6, 5, 6, 40);
        Projectile bomb = new Projectile("Bomb", Color.DarkGray, 10, 15, 4, 4);
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseButtonsPressed.Add(e.Button);

            if (e.Button == MouseButtons.Left)
            {
                prj.Shoot(e.X, e.Y, p1);
            }
            else if(e.Button == MouseButtons.Right)
            {
                bomb.Shoot(e.X, e.Y, p1);
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseButtonsPressed.Remove(e.Button);
        }

        Projectile prj2 = new Projectile("Minigun projectile", Color.Red, 4, 1, 6, 400);
        Projectile special = new Projectile("Special", Color.Yellow, 4, 40, 7, 0, 16);
        Projectile special2 = new Projectile("2° special", Color.Yellow, 4, 25, 7, 6, 30);
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

            p1.MovePlayer(g, BackColor, keysPressed, Keys.W, Keys.S, Keys.A, Keys.D);
            p2.MovePlayer(g, BackColor, keysPressed, Keys.Up, Keys.Down, Keys.Left, Keys.Right);

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
            prj2.MoveProjectile(g, BackColor, ClientSize.Width, ClientSize.Height);
            bomb.MoveProjectile(g, BackColor, ClientSize.Width, ClientSize.Height);
            special.MoveProjectile(g, BackColor, ClientSize.Width, ClientSize.Height);
            special2.MoveProjectile(g, BackColor, ClientSize.Width, ClientSize.Height);

            prj.CheckHit(p1);
            prj.CheckHit(p2);
            bomb.CheckHit(p1);
            bomb.CheckHit(p2);
            prj2.CheckHit(p1);
            prj2.CheckHit(p2);
            special.CheckHit(p1);
            special.CheckHit(p2);
            special2.CheckHit(p1);
            special2.CheckHit(p2);

            label1.Text = string.Format("{0}; {1}; {2}; {3}; {4}\n{5} p1 hp; {6} p2 hp"
                                        , prj.ToString(), prj2.ToString(), bomb.ToString(), special.ToString("4"), special2.ToString("3"), p1.Health, p2.Health);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < fruits.Count(); i++)
            {
                g.FillRectangle(Brushes.Blue, fruits[i]);
            }
        }

        private void SpecialAtk_Timer_Tick(object sender, EventArgs e)
        {
            if (keysPressed.Contains(Keys.Q) && special.CurrentEnergy >= 4)
            {
                special.Shoot(new Vettore(0, 1), p1);
                special.Shoot(new Vettore(0, -1), p1);
                special.Shoot(new Vettore(1, 0), p1);
                special.Shoot(new Vettore(-1, 0), p1);
                special.Energy -= 4;
            }
        }

        private void Special2Atk_Timer_Tick(object sender, EventArgs e)
        {
            if (keysPressed.Contains(Keys.E) && special2.CurrentEnergy >= 3)
            {
                special2.Shoot(MousePosition.X, MousePosition.Y, p1);
                special2.Shoot(MousePosition.X + 20, MousePosition.Y + 20, p1);
                special2.Shoot(MousePosition.X - 20, MousePosition.Y - 20, p1);
                special2.Energy -= 3;
            }
        }

        private void Prj2_Timer_Tick(object sender, EventArgs e)
        {
            if (mouseButtonsPressed.Contains(MouseButtons.Middle))
            {
                prj2.Shoot(MousePosition.X, MousePosition.Y, p1);
            }
        }
    }
}

/* TO DO:
 * (maybe):
 * add speed of recharging
 * (must):
 */