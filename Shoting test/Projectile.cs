using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private double[] dx { get; set; }
        private double[] dy { get; set; }
        public Projectile(Color c, int en)
        {
            Color = c;
            Energy = en;
            CurrentEnergy = en;
            Projectiles = new Rectangle[en];
            ProjectileNum = 0;
            ProjectileExists = false;
            dx = new double[en]; dy = new double[en];
        }
        public void Shoot(MouseEventArgs e, Player p)
        {
            if (CurrentEnergy > 0)
            {
                dx[ProjectileNum] = e.X - p.PosPlayerX; dy[ProjectileNum] = p.PosPlayerY - e.Y;

                Projectiles[ProjectileNum] = new Rectangle(p.PosPlayerX + 5, p.PosPlayerY + 5, 6, 6);
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
                    double slope = Math.Abs(dy[i] / dx[i]);
                    double k = 6 / Math.Sqrt((double)(16 + 16 * slope * slope));
                    Projectiles[i].Location = new Point((int)(Projectiles[i].X + 4 * Math.Sign(dx[i]) * k), (int)(Projectiles[i].Y + 4 * slope * Math.Sign(-dy[i]) * k));
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
