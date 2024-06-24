using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vettori;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Shoting_test
{
    public class Projectile : IFormattable
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public int Size { get; set; }
        public double Speed { get; set; }
        public int Damage { get; set; }
        public int Energy { get; set; }
        public int MaxEnergy { get; set; }
        public int CurrentEnergy { get; set;}
        public RectangleF[] Projectiles { get; set; }
        public int ProjectileNum { get; set; }
        private bool ProjectileExists { get; set; }
        public Vettore[] Direction { get; set; }
        public Player[] Shooter { get; set; }
        public Projectile(string name, Color color, int size, int damage, double speed, int energy)
        {
            Name = name;
            Color = color;
            Size = size;
            Damage = damage;
            Speed = speed;
            Energy = energy;
            CurrentEnergy = Energy;
            Projectiles = new RectangleF[Energy];
            ProjectileNum = 0;
            ProjectileExists = false;
            Direction = new Vettore[Energy];
            Shooter = new Player[Energy];
        }
        public Projectile(string name, Color color, int size, int damage, double speed, int initialEnergy, int maxEnergy)
        {
            Name = name;
            Color = color;
            Size = size;
            Damage = damage;
            Speed = speed;
            Energy = initialEnergy;
            MaxEnergy = maxEnergy;
            CurrentEnergy = Energy;
            Projectiles = new RectangleF[MaxEnergy];
            ProjectileNum = 0;
            ProjectileExists = false;
            Direction = new Vettore[MaxEnergy];
            Shooter = new Player[MaxEnergy];
        }
        public override string ToString()
        {
            return this.ToString("G", CultureInfo.CurrentCulture);
        }

        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(format)) format = "G";
            if (provider == null) provider = CultureInfo.CurrentCulture;

            switch (format.ToUpperInvariant())
            {
                case "G":
                    return $"{CurrentEnergy}/{Energy} {Name}s left";
                case "3":
                    return $"{CurrentEnergy/3}/{Energy/3} {Name}s left";
                case "4":
                    return $"{CurrentEnergy/4}/{Energy/4} {Name}s left";
                default:
                    throw new FormatException(string.Format("The {0} format string is not supported.", format));
            }
        }
        
        public void Shoot(int mx, int my, Player p)
        {
            if (CurrentEnergy > 0)
            {
                Direction[ProjectileNum] = new Vettore(mx - p.Position.X, p.Position.Y + p.Size / 2 - my).Versore();

                Projectiles[ProjectileNum] = new RectangleF((float)p.Position.X + p.Size / 2, (float)p.Position.Y + p.Size / 2, Size, Size);
                Shooter[ProjectileNum] = p;
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

                Projectiles[ProjectileNum] = new RectangleF((float)p.Position.X + p.Size / 2, (float)p.Position.Y + p.Size / 2, Size, Size);
                Shooter[ProjectileNum] = p;
                ProjectileNum++;
                CurrentEnergy--;
                ProjectileExists = true;
            }
        }
        private Color Background;
        private Graphics G;
        public void MoveProjectile(Graphics g, Color background, int BorderX, int BorderY)
        {
            Background = background;
            G = g;
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

        public void CheckHit(Player p)
        {
            if(G == null || Background == null)
            {
                throw new Exception("Need to put a \"MoveProjectile\" first");
            }
            for (int i = 0; i < ProjectileNum; i++)
            {
                if (new RectangleF((float)p.Position.X, (float)p.Position.Y, 16, 16).IntersectsWith(Projectiles[i]) && p != Shooter[i])
                {
                    p.Health -= Damage;
                    G.FillRectangle(new SolidBrush(Background), Projectiles[i]);
                    Projectiles[i].Location = new Point(5000, 5000);
                }
            }
        }
    }
}
