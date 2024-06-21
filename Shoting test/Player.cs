using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vettori;

namespace Shoting_test
{
    public class Player
    {
        public Color Color { get; set;}
        public int Size { get; set;}
        public Vettore PosPlayer { get; set; }
        public Vettore Speed { get; set; }
        public int SpeedValue { get; set; }
        public Player(Color color, int size, Vettore initialPos, Vettore intialSpeed, int speedValue) 
        {
            Color = color;
            Size = size;
            Speed = intialSpeed;
            PosPlayer = initialPos;
            SpeedValue = speedValue;
        }
        public void MoveStart(KeyEventArgs e, Keys up, Keys down, Keys left, Keys right)
        {
            if (e.KeyCode == up)
            {
                Speed.Y = -SpeedValue;
            }
            if (e.KeyCode == left)
            {
                Speed.X = -SpeedValue;
            }
            if (e.KeyCode == down)
            {
                Speed.Y = SpeedValue;
            }
            if (e.KeyCode == right)
            {
                Speed.X = SpeedValue;
            }
        }
        public void MoveStart(KeyEventArgs e, Keys up, Keys down, Keys left, Keys right, Keys up2, Keys down2, Keys left2, Keys right2)
        {
            if (e.KeyCode == up || e.KeyCode == up2)
            {
                Speed.Y = -SpeedValue;
            }
            if (e.KeyCode == left || e.KeyCode == left2)
            {
                Speed.X = -SpeedValue;
            }
            if (e.KeyCode == down || e.KeyCode == down2)
            {
                Speed.Y = SpeedValue;
            }
            if (e.KeyCode == right || e.KeyCode == right2)
            {
                Speed.X = SpeedValue;
            }
        }
        public void MoveEnd(KeyEventArgs e, Keys up, Keys down, Keys left, Keys right)
        {
            if (e.KeyCode == up || e.KeyCode == down)
            {
                Speed.Y = 0;
            }
            if (e.KeyCode == left || e.KeyCode == right)
            {
                Speed.X = 0;
            }
        }
        public void MoveEnd(KeyEventArgs e, Keys up, Keys down, Keys left, Keys right, Keys up2, Keys down2, Keys left2, Keys right2)
        {
            if (e.KeyCode == up || e.KeyCode == down || e.KeyCode == up2 || e.KeyCode == down2)
            {
                Speed.Y = 0;
            }
            if (e.KeyCode == left || e.KeyCode == right || e.KeyCode == left2 || e.KeyCode == right2)
            {
                Speed.X = 0;
            }
        }
        public void MovePlayer(Graphics g, Color background)
        {
            g.FillRectangle(new SolidBrush(background), (float)PosPlayer.X, (float)PosPlayer.Y, Size, Size);
            PosPlayer.X += Speed.X;
            PosPlayer.Y += Speed.Y;
            g.FillRectangle(new SolidBrush(Color), (float)PosPlayer.X, (float)PosPlayer.Y, Size, Size);
        }
    }
}
