using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBreakBricks
{
    public class Pad
    {
        public Point location { get; set; }
        public Point size { get; set; }
        private decimal speed = (decimal)10.0;

        public Pad(Point location, Point size) 
        { 
            this.location = location;
            this.size = size;
        }

        public void moveLeft(Scenary scenary)
        {
            int locationX = location.X - (int)speed;

            if(locationX < scenary.boundariesTopLeft.X) 
            {
                locationX = scenary.boundariesTopLeft.X;
            }

            this.location = new Point(locationX, location.Y);
        }

        public void moveRight(Scenary scenary)
        {
            int locationX = location.X + (int)speed;

            if (locationX > scenary.boundariesDownRight.X - size.X)
            {
                locationX = scenary.boundariesDownRight.X - size.X;
            }

            this.location = new Point(locationX, location.Y);

        }

    }
}
