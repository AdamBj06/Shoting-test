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
        Player p1 = new Player(Color.Black, new Vettore(300, 300), new Vettore(0, 0));
        Projectile prj = new Projectile(Color.Red, 40, 6);
        Projectile bomb = new Projectile(Color.DarkGray, 4, 6);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            p1.MoveStart(e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            p1.MoveEnd(e);
        }
        
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                prj.Shoot(e, p1);
            }
            else if(e.Button == MouseButtons.Right)
            {
                bomb.Shoot(e, p1);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();

            p1.MovePlayer(g, BackColor);

            prj.MoveProjectile(g, BackColor, ClientSize.Width, ClientSize.Height);
            bomb.MoveProjectile(g, BackColor, ClientSize.Width, ClientSize.Height);

            label1.Text = string.Format("{0} projectiles left; {1} bombs left", prj.CurrentEnergy, bomb.CurrentEnergy);
        }
    }
}
