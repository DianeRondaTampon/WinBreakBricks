using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.DataFormats;

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
            this.direction = Vector.NormalizeVector(new Vector2(1, -1)); //to diagonal North East
        }

        public void Move(Scenary scenary, Pad pad)
        {
            decimal speed = 10; //pixels to move per update

            decimal newLocationX = locationX + (decimal)direction.X * speed;
            decimal newLocationY = locationY + (decimal)direction.Y * speed;

            Vector2? newDirectionBcuzCollition = null;

            //check collition with boundaries
            if (newLocationX < scenary.boundariesTopLeft.X || newLocationX + size.X > scenary.boundariesDownRight.X)
            {
                //collision is vertical, so the x will be inverted and the y will be the same
                newDirectionBcuzCollition = new Vector2(-direction.X, direction.Y);
            }
            if (newLocationY < scenary.boundariesTopLeft.Y) // || newLocationY + size.Y > scenary.boundariesDownRight.Y)
            {
                //collision is horizontal, so the y will be inverted and the x will be the same
                newDirectionBcuzCollition = new Vector2(direction.X, -direction.Y);
            }
            if (newLocationY > scenary.boundariesDownRight.Y)
            {
                //death
                scenary.alive = false;
            }

            //check collition with bricks
            foreach(Brick brick in scenary.bricks.ToList())
            {
                if (brick.location.X + brick.size.X > newLocationX && brick.location.X < newLocationX + size.X
                    && brick.location.Y < newLocationY + size.Y && brick.location.Y + brick.size.Y > newLocationY)
                {
                    //collision is horizontal, so the y will be inverted and the x will be the same
                    newDirectionBcuzCollition = new Vector2(direction.X, -direction.Y);
                    //remove brick
                    scenary.bricks.Remove(brick);

                    //increase score
                    scenary.score++;

                    //did you won?
                    if(scenary.bricks.Count == 0)
                    {
                        scenary.win = true;
                        scenary.score += 10;
                    }
                }
            }

            //check collition with pad
            if (pad.location.X + pad.size.X > newLocationX && pad.location.X < newLocationX + size.X 
                && pad.location.Y < newLocationY + size.Y && pad.location.Y + pad.size.Y > newLocationY) 
            {
                //if touch edge of the pad, the reboud should be affected
                //pad is 150. 0-20 super left, 21-49 normal left, 50-75 upper left
                //76 - 101 upper rigth, 102 - 130 normal right, 131 - 150 super right
                decimal centerBallX = newLocationX + size.X / 2;
                if(centerBallX <= pad.location.X + 20)
                {
                    newDirectionBcuzCollition = Vector.NormalizeVector(new Vector2(-2, -1));
                }
                else if (centerBallX + 21 >= pad.location.X && centerBallX <= pad.location.X + 49)
                {
                    newDirectionBcuzCollition = Vector.NormalizeVector(new Vector2(-1, -1));
                }
                else if (centerBallX + 50 >= pad.location.X && centerBallX <= pad.location.X + 75)
                {
                    newDirectionBcuzCollition = Vector.NormalizeVector(new Vector2(-1, -2));
                }
                else if (centerBallX + 76 >= pad.location.X && centerBallX <= pad.location.X + 101)
                {
                    newDirectionBcuzCollition = Vector.NormalizeVector(new Vector2(1, -2));
                }
                else if (centerBallX + 102 >= pad.location.X && centerBallX <= pad.location.X + 130)
                {
                    newDirectionBcuzCollition = Vector.NormalizeVector(new Vector2(1, -1));
                }
                else if (centerBallX + 131>= pad.location.X)
                {
                    newDirectionBcuzCollition = Vector.NormalizeVector(new Vector2(2, -1));
                }
            }

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
                newLocationX = locationX + (decimal)direction.X * speed;
                newLocationY = locationY + (decimal)direction.Y * speed;
                locationX = newLocationX;
                locationY = newLocationY;
            }
        }
    }
}
