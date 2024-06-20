using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vettori;

namespace Shoting_test
{
    public class Projectile
    {
        public Color Color { get; }
        public int Energy { get; }
        public int CurrentEnergy { get; set;}
        private Rectangle[] Projectiles { get; set; }
        private int ProjectileNum { get; set; }
        private bool ProjectileExists { get; set; }
        public Vettore[] Direction { get; set; }
        public double Speed { get; set; }
        public Projectile(Color c, int en, double s)
        {
            Color = c;
            Energy = en;
            CurrentEnergy = en;
            Projectiles = new Rectangle[en];
            ProjectileNum = 0;
            ProjectileExists = false;
            Direction = new Vettore[en];
            Speed = s;
        }
        public void Shoot(MouseEventArgs e, Player p)
        {
            if (CurrentEnergy > 0)
            {
                Direction[ProjectileNum] = new Vettore(e.X - p.PosPlayer.X, e.Y - p.PosPlayer.Y).Versore();

                Projectiles[ProjectileNum] = new Rectangle((int)p.PosPlayer.X + 5, (int)p.PosPlayer.Y + 5, 6, 6);
                ProjectileNum++;
                CurrentEnergy--;
                ProjectileExists = true;
            }
        }
        public void MoveProjectile(Graphics g, Color background, int BorderX, int BorderY)
        {
            if (ProjectileExists)
            {
                for (int i = 0; i < ProjectileNum; i++)
                {
                    g.FillRectangle(new SolidBrush(background), Projectiles[i]);
                    Projectiles[i].Location = new Point((int)(Projectiles[i].X + Speed * Direction[i].X), (int)(Projectiles[i].Y + Speed * Direction[i].Y));
                    g.FillRectangle(new SolidBrush(Color), Projectiles[i]);
                }
                for (int i = 0; i < ProjectileNum; i++)
                {
                    if (Projectiles[i].X >= -6 && Projectiles[i].X <= BorderX + 6 && Projectiles[i].Y >= -6 && Projectiles[i].Y <= BorderY + 6)
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
                ProjectileNum = 0;
                if (CurrentEnergy < Energy)
                {
                    CurrentEnergy++;
                }
            }
        }
    }
}
