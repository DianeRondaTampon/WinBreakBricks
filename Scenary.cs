using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBreakBricks
{
    public enum ColorBrick
    {
        Blue,
        Green, 
        Purple
    }

    public class Brick
    {
        public Point location { get; set; }
        public ColorBrick color { get; set; }

        public Brick(Point location, ColorBrick color)
        {
            this.location = location;
            this.color = color;
        }
    }

    public class Scenary
    {
        public List<Brick> bricks;
        public Point boundariesTopLeft { get; set; }
        public Point boundariesDownRight { get; set; }
        public bool alive { get; set; }

        public Scenary(Point boundariesDownRight) 
        { 
            bricks = new List<Brick>();
            this.boundariesTopLeft = new Point(0,0);
            this.boundariesDownRight = boundariesDownRight;
            alive = true;
        }
    }
}
