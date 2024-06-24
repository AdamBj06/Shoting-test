using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Vettori;

namespace Shoting_test
{
    public class Player : IFormattable
    {
        public Color Color { get; set;}
        public int Size { get; set;}
        public int Health { get; set; }
        public Vettore Position { get; set; }
        public Vettore Speed { get; set; }
        public int SpeedValue { get; set; }
        public Player(Color color, int size, int health, Vettore initialPos, Vettore intialSpeed, int speedValue) 
        {
            Color = color;
            Size = size;
            Health = health;
            Speed = intialSpeed;
            Position = initialPos;
            SpeedValue = speedValue;
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
                    return $"Pos: {Position.X}x, {Position.Y}y; Speed: {Speed.X}x, {Speed.Y}y;";
                default:
                    throw new FormatException(string.Format("The {0} format string is not supported.", format));
            }
        }

        //https://stackoverflow.com/questions/47041203/c-sharp-if-more-keys-are-pressed-at-once-how-to-not-stop-functions-of-others
        public void MovePlayer(Graphics g, Color background, HashSet<Keys> keysPressed, Keys up, Keys down, Keys left, Keys right)
        {
            if (!keysPressed.Contains(up) || !keysPressed.Contains(down))
            {
                Speed.Y = 0;
            }
            if (!keysPressed.Contains(left) || !keysPressed.Contains(right))
            {
                Speed.X = 0;
            }

            if (keysPressed.Contains(up))
            {
                Speed.Y = -SpeedValue;
            }
            if (keysPressed.Contains(left))
            {
                Speed.X = -SpeedValue;
            }
            if (keysPressed.Contains(down))
            {
                Speed.Y = SpeedValue;
            }
            if (keysPressed.Contains(right))
            {
                Speed.X = SpeedValue;
            }

            DrawPlayer(g, background);
        }

        public void MovePlayer(Graphics g, Color background, HashSet<Keys> keysPressed, Keys up, Keys down, Keys left, Keys right, Keys up2, Keys down2, Keys left2, Keys right2)
        {
            if ((!keysPressed.Contains(up) && !keysPressed.Contains(down)) || (!keysPressed.Contains(down) && !keysPressed.Contains(up)) ||
                (!keysPressed.Contains(up2) && !keysPressed.Contains(down2)) || (!keysPressed.Contains(down2) && !keysPressed.Contains(up2)))
            {
                Speed.Y = 0;
            }
            if ((!keysPressed.Contains(left) && !keysPressed.Contains(right)) || (!keysPressed.Contains(right) && !keysPressed.Contains(left)) || 
                (!keysPressed.Contains(left2) && !keysPressed.Contains(right2)) || (!keysPressed.Contains(right2) && !keysPressed.Contains(left2)))
            {
                Speed.X = 0;
            }

            if (keysPressed.Contains(up) || keysPressed.Contains(up2))
            {
                Speed.Y = -SpeedValue;
            }
            if (keysPressed.Contains(left) || keysPressed.Contains(left2))
            {
                Speed.X = -SpeedValue;
            }
            if (keysPressed.Contains(down) || keysPressed.Contains(down2))
            {
                Speed.Y = SpeedValue;
            }
            if (keysPressed.Contains(right) || keysPressed.Contains(right2))
            {
                Speed.X = SpeedValue;
            }

            DrawPlayer(g, background);
        }

        private void DrawPlayer(Graphics g, Color background)
        {
            g.FillRectangle(new SolidBrush(background), (float)Position.X, (float)Position.Y, Size, Size);
            Position.X += Speed.X;
            Position.Y += Speed.Y;
            g.FillRectangle(new SolidBrush(Color), (float)Position.X, (float)Position.Y, Size, Size);
        }
    }
}
