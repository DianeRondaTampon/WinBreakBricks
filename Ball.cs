using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WinBreakBricks
{
    public class Ball
    {
        public decimal locationX { get; set; }
        public decimal locationY { get; set; }
        public Point size { get; set; }
        public Vector2 direction { get; set; }

        public Ball(decimal locationX, decimal locationY, Point size) 
        { 
            this.locationX = locationX;
            this.locationY = locationY;
            this.size = size;
            this.direction = new Vector2(1, -1); //to diagonal North East
        }

        public void Move(Scenary scenary)
        {
            decimal speed = 3; //pixels to move per update

            decimal newLocationX = locationX + (decimal)direction.X * speed;
            decimal newLocationY = locationY + (decimal)direction.Y * speed;

            Vector2? newDirectionBcuzCollition = null;

            //check collition with boundaries
            if (newLocationX < scenary.boundariesTopLeft.X || newLocationX + size.X > scenary.boundariesDownRight.X)
            {
                //boundary is vertical, so the x will be inverted and the y will be the same
                newDirectionBcuzCollition = new Vector2(-direction.X, direction.Y);
            }
            if (newLocationY < scenary.boundariesTopLeft.Y) // || newLocationY + size.Y > scenary.boundariesDownRight.Y)
            {
                //boundary is horizontal, so the y will be inverted and the x will be the same
                newDirectionBcuzCollition = new Vector2(direction.X, -direction.Y);
            }
            if (newLocationY > scenary.boundariesDownRight.Y)
            {
                //death
                scenary.alive = false;
            }

            //check collition with bricks
            //if ()

            if (newDirectionBcuzCollition == null)
            {
                //no collition, go on move
                locationX = newLocationX;
                locationY = newLocationY;
            }
            else
            {
                //collition, change direction
                direction = (Vector2)newDirectionBcuzCollition;
            }
        }
    }
}
