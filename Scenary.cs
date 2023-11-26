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
        Purple,
        Red,
        Orange,
        Sky,
        Yellow,
        DarkGreen,
        Grey,
        Brown
    }

    public class Brick
    {
        public Point location { get; set; }
        public ColorBrick color { get; set; }
        public Point size { get; set; }

        public Brick(Point location, ColorBrick color, Point size)
        {
            this.location = location;
            this.color = color;
            this.size = size;
        }
    }

    public class Scenary
    {
        public List<Brick> bricks;
        public Point boundariesTopLeft { get; set; }
        public Point boundariesDownRight { get; set; }
        public bool alive { get; set; }
        public bool win { get; set; }
        public int stage { get; set; }
        public int score { get; set; }

        public Scenary(Point boundariesDownRight) 
        { 
            bricks = new List<Brick>();
            this.boundariesTopLeft = new Point(0,0);
            this.boundariesDownRight = boundariesDownRight;
            alive = true;
            win = false;
            stage = 1;
            score = 0;
        }

        public void Initialize(Point brickSize)
        {
            bricks.Clear();
            ColorBrick color = ColorBrick.Blue;
            Random random = new Random();
            switch (stage)
            {
                case 1:
                    //stage 1 scenary
                    //7 columns and 1 rows
                    for (int y = 0; y < 1; y++)
                    {
                        color = (ColorBrick)y;
                        for (int x = 0; x < 7; x++)
                        {
                            bricks.Add(new Brick(new Point(x * brickSize.X, y * brickSize.Y), color, brickSize));
                        }
                    }
                    break;
                case 2:
                    //7 columns and 2 rows
                    for (int y = 0; y < 2; y++)
                    {
                        color = (ColorBrick)y;
                        for (int x = 0; x < 7; x++)
                        {
                            bricks.Add(new Brick(new Point(x * brickSize.X, y * brickSize.Y), color, brickSize));
                        }
                    }
                    break;
                case 3:
                    //7 columns and 3 rows
                    for (int y = 0; y < 3; y++)
                    {
                        color = (ColorBrick)y;
                        for (int x = 0; x < 7; x++)
                        {
                            bricks.Add(new Brick(new Point(x * brickSize.X, y * brickSize.Y), color, brickSize));
                        }
                    }
                    break;
                case 4:
                    //7 columns and 4 rows
                    for (int y = 0; y < 4; y++)
                    {
                        color = (ColorBrick)y;
                        for (int x = 0; x < 7; x++)
                        {
                            bricks.Add(new Brick(new Point(x * brickSize.X, y * brickSize.Y), color, brickSize));
                        }
                    }
                    break;
                case 5:
                    //7 columns and 5 rows
                    for (int y = 0; y < 5; y++)
                    {
                        color = (ColorBrick)y;
                        for (int x = 0; x < 7; x++)
                        {
                            bricks.Add(new Brick(new Point(x * brickSize.X, y * brickSize.Y), color, brickSize));
                        }
                    }
                    break;
                case 6:
                    //7 columns and 1 rows
                    for (int y = 0; y < 6; y++)
                    {
                        for (int x = 0; x < 7; x++)
                        {
                            int randomNumber = random.Next(0, 10);
                            color = (ColorBrick)randomNumber;
                            bricks.Add(new Brick(new Point(x * brickSize.X, y * brickSize.Y), color, brickSize));
                        }
                    }
                    break;
            }

            alive = true;
            win = false;
        }
    }
}
