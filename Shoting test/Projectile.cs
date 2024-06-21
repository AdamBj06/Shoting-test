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
        public int Size { get; set; }
        public double Speed { get; set; }
        public int Energy { get; set; }
        public int MaxEnergy { get; set; }
        public int CurrentEnergy { get; set;}
        public RectangleF[] Projectiles { get; set; }
        private int ProjectileNum { get; set; }
        private bool ProjectileExists { get; set; }
        public Vettore[] Direction { get; set; }
        public Projectile(Color color, int size, double speed, int energy)
        {
            Color = color;
            Size = size;
            Speed = speed;
            Energy = energy;
            CurrentEnergy = Energy;
            Projectiles = new RectangleF[Energy];
            ProjectileNum = 0;
            ProjectileExists = false;
            Direction = new Vettore[Energy];
        }
        public Projectile(Color color, int size, double speed, int initialEnergy, int maxEnergy)
        {
            Color = color;
            Size = size;
            Speed = speed;
            Energy = initialEnergy;
            MaxEnergy = maxEnergy;
            CurrentEnergy = Energy;
            Projectiles = new RectangleF[maxEnergy];
            ProjectileNum = 0;
            ProjectileExists = false;
            Direction = new Vettore[maxEnergy];
        }
        public void Shoot(int mx, int my, Player p)
        {
            if (CurrentEnergy > 0)
            {
                Direction[ProjectileNum] = new Vettore(mx - p.PosPlayer.X, p.PosPlayer.Y + p.Size / 2 - my).Versore();

                Projectiles[ProjectileNum] = new RectangleF((float)p.PosPlayer.X + p.Size / 2, (float)p.PosPlayer.Y + p.Size / 2, Size, Size);
                ProjectileNum++;
                CurrentEnergy--;
                ProjectileExists = true;
            }
        }
        public void Shoot(Vettore direction, Player p)
        {
            if (CurrentEnergy > 0)
            {
                Direction[ProjectileNum] = direction.Versore();

                Projectiles[ProjectileNum] = new RectangleF((float)p.PosPlayer.X + p.Size / 2, (float)p.PosPlayer.Y + p.Size / 2, Size, Size);
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
                    Projectiles[i].Location = new PointF((float)(Projectiles[i].X + Speed * Direction[i].X), (float)(Projectiles[i].Y + Speed * (-Direction[i].Y)));
                    g.FillRectangle(new SolidBrush(Color), Projectiles[i]);
                }
                for (int i = 0; i < ProjectileNum; i++)
                {
                    if (Projectiles[i].X >= -Size && Projectiles[i].X <= BorderX + Size && Projectiles[i].Y >= -Size && Projectiles[i].Y <= BorderY + Size)
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
