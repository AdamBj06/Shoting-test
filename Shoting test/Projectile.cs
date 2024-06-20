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
        public int Energy { get; }
        public int CurrentEnergy { get; set;}
        private RectangleF[] Projectiles { get; set; }
        private int ProjectileNum { get; set; }
        private bool ProjectileExists { get; set; }
        public Vettore[] Direction { get; set; }
        private bool SetPrj { get; set; }
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
        public Projectile(Vettore[] direction, Color color, int size, double speed)
        {
            Direction = direction;
            Color = color;
            Size = size;
            Speed = speed;
            Projectiles = new RectangleF[direction.Length];
            ProjectileExists = false;
            SetPrj = true;
        }
        public void Shoot(MouseEventArgs e, Player p)
        {
            if (CurrentEnergy > 0)
            {
                Direction[ProjectileNum] = new Vettore(e.X - p.PosPlayer.X, p.PosPlayer.Y - e.Y).Versore();

                Projectiles[ProjectileNum] = new RectangleF((float)p.PosPlayer.X + 8, (float)p.PosPlayer.Y + 8, Size, Size);
                ProjectileNum++;
                CurrentEnergy--;
                ProjectileExists = true;
            }
        }
        public void Shoot(Player p)
        {
            for(int i = 0; i < Projectiles.Length; i++)
            {
                Direction[i] = Direction[i].Versore();
                Projectiles[i] = new RectangleF((float)p.PosPlayer.X + 8, (float)p.PosPlayer.Y + 8, Size, Size);
            }
            ProjectileExists = true;
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
            else if(!SetPrj)
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
