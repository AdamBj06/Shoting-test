using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shoting_test
{
    public class Player
    {
        public Color Color { get; set;}
        public int PosPlayerX { get; set; }
        public int PosPlayerY { get; set; }
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }
        public Player(Color c,int initialPosX, int initialPosY, int intialSpeedX, int initialSpeedY) 
        {
            Color = c;
            SpeedX = intialSpeedX; SpeedY = initialSpeedY;
            PosPlayerX = initialPosX; PosPlayerY = initialPosY;
        }
        public void MoveStart(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                SpeedY = -2;
            }
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                SpeedX = -2;
            }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                SpeedY = 2;
            }
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                SpeedX = 2;
            }
        }
        public void MoveEnd(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.W || e.KeyCode == Keys.S)
            {
                SpeedY = 0;
            }
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.A || e.KeyCode == Keys.D)
            {
                SpeedX = 0;
            }
        }
        public void MovePlayer(Graphics g, Color background)
        {
            g.FillRectangle(new SolidBrush(background), PosPlayerX, PosPlayerY, 16, 16);
            PosPlayerX += SpeedX;
            PosPlayerY += SpeedY;
            g.FillRectangle(new SolidBrush(Color), PosPlayerX, PosPlayerY, 16, 16);
        }
    }
}
