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
        public Vettore PosPlayer { get; set; }
        public Vettore Speed { get; set; }
        public Player(Color c, Vettore initialPos, Vettore intialSpeed) 
        {
            Color = c;
            Speed = intialSpeed;
            PosPlayer = initialPos;
        }
        public void MoveStart(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                Speed.Y = -2;
            }
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                Speed.X = -2;
            }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                Speed.Y = 2;
            }
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                Speed.X = 2;
            }
        }
        public void MoveEnd(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.W || e.KeyCode == Keys.S)
            {
                Speed.Y = 0;
            }
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.A || e.KeyCode == Keys.D)
            {
                Speed.X = 0;
            }
        }
        public void MovePlayer(Graphics g, Color background)
        {
            g.FillRectangle(new SolidBrush(background), (float)PosPlayer.X, (float)PosPlayer.Y, 16, 16);
            PosPlayer.X += Speed.X;
            PosPlayer.Y += Speed.Y;
            g.FillRectangle(new SolidBrush(Color), (float)PosPlayer.X, (float)PosPlayer.Y, 16, 16);
        }
    }
}
