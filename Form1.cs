using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace WinBreakBricks
{
    public partial class Form1 : Form
    {
        //global variables
        Pad pad;
        Ball ball;
        Scenary scenary;

        Image padImage;
        Image ballImage;
        Image brickBlueImage;
        Image brickGreenImage;
        Image brickPurpleImage;

        Point padSize;
        Point ballSize;
        Point brickSize;
        Point scenarySize;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //load resources

            //folder of images, is diferent in debug than in release
            string imagePath;
#if DEBUG
            // Load your snake image for debug build
            imagePath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "images");
#else
                // Load your snake image for release build
            imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");
#endif

            string spritePath = Path.Combine(imagePath, "sprites.png");
            Image spriteImage; //all the images are inside this sprite image
            spriteImage = Image.FromFile(spritePath); //all the images are inside this sprite image

            padSize = new Point(155, 45);
            ballSize = new Point(75, 75);
            brickSize = new Point(170, 60);
            scenarySize = new Point(1190, 600);

            // Load individual images into variables
            // Call the method to copy a portion of the source image to the destination image
            padImage = CopyImagePortion(new Rectangle(395, 15, padSize.X, padSize.Y), spriteImage); //the pad (1st animation) is in 390,10 size 160,50
            ballImage = CopyImagePortion(new Rectangle(805, 548, ballSize.X, ballSize.Y), spriteImage); //the pad (1st animation) is in 390,10 size 160,50
            brickBlueImage = CopyImagePortion(new Rectangle(20, 18, brickSize.X, brickSize.Y), spriteImage); //the brick (blue) is in 16,16 size 175,60
            brickGreenImage = CopyImagePortion(new Rectangle(20, 94, brickSize.X, brickSize.Y), spriteImage); //the brick (blue) is in 16,16 size 175,60
            brickPurpleImage = CopyImagePortion(new Rectangle(20, 169, brickSize.X, brickSize.Y), spriteImage); //the brick (blue) is in 16,16 size 175,60


            //classes
            int centerX = scenarySize.X / 2;
            int centery = scenarySize.Y / 2;
            pad = new Pad(new Point(centerX - padSize.X / 2, 540), padSize);
            ball = new Ball(centerX - ballSize.X / 2, 440, ballSize);
            scenary = new Scenary(scenarySize);

            //stage scenary
            InicializateScenary(1);


            //start the game
            timer.Interval = 20; //each second will be exec 50 times
            timer.Enabled = true;
        }

        private void InicializateScenary(int stage)
        {
            switch (stage)
            {
                case 1:
                    //stage 1 scenary
                    //7 columns and 3 rows
                    ColorBrick color = ColorBrick.Blue;
                    for (int y = 0; y < 3; y++)
                    {
                        if (y == 0)
                            color = ColorBrick.Blue;
                        else if (y == 1)
                            color = ColorBrick.Green;
                        else if (y == 2)
                            color = ColorBrick.Purple;
                        for (int x = 0; x < 7; x++)
                        {
                            scenary.bricks.Add(new Brick(new Point(x * brickSize.X, y * brickSize.Y), color));
                        }
                    }
                    break;
                case 2:
                    //stage 1 scenary
                    break;
            }
        }

        private Image CopyImagePortion(Rectangle sourceRectangle, Image sourceImage)
        {
            // Convert Image objects to Bitmap objects
            Bitmap sourceBitmap = new Bitmap(sourceImage);

            // Create a new Bitmap for the destination image
            Bitmap destinationBitmap = new Bitmap(sourceRectangle.Width, sourceRectangle.Height);

            // Create a Graphics object from the destination bitmap
            using (Graphics g = Graphics.FromImage(destinationBitmap))
            {
                // Copy the specified portion of the source bitmap to the destination bitmap
                g.DrawImage(sourceBitmap, new Rectangle(0, 0, sourceRectangle.Width, sourceRectangle.Height), sourceRectangle, GraphicsUnit.Pixel);
            }

            // Convert the destination Bitmap back to an Image and return it
            Image destinationImage = Image.FromHbitmap(destinationBitmap.GetHbitmap());
            return destinationImage;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //each second will be exec 50 times

            //update
            UpdateGame();

            //draw
            DragGame();
        }

        private void UpdateGame()
        {
            UpdateBall();
        }

        private void UpdateBall()
        {
            ball.Move(scenary);
        }

        private void DragGame()
        {
            Bitmap bitmap = new Bitmap(pcbGraphics.Width, pcbGraphics.Height);
            Graphics g = Graphics.FromImage(bitmap);

            // Clear the background
            g.Clear(Color.Black);

            // draw scenary
            DrawBricks(g);

            // draw ball
            DrawBall(g);

            // draw the pad of the player
            DrawPad(g);

            // Draw the game over if you not alive
            DrawGameOver(g);

            // Draw the off-screen bitmap to the PictureBox
            pcbGraphics.Image?.Dispose(); // Dispose the previous image to avoid memory leaks
            pcbGraphics.Image = bitmap;
        }

        private void DrawPad(Graphics g)
        {
            g.DrawImage(padImage, pad.location.X, pad.location.Y);
        }

        private void DrawBall(Graphics g)
        {
            g.DrawImage(ballImage, (int)ball.locationX, (int)ball.locationY);
        }

        private void DrawBricks(Graphics g)
        {
            foreach (Brick brick in scenary.bricks)
            {
                switch (brick.color)
                {
                    case ColorBrick.Blue:
                        g.DrawImage(brickBlueImage, brick.location.X, brick.location.Y);
                        break;
                    case ColorBrick.Green:
                        g.DrawImage(brickGreenImage, brick.location.X, brick.location.Y);
                        break;
                    case ColorBrick.Purple:
                        g.DrawImage(brickPurpleImage, brick.location.X, brick.location.Y);
                        break;
                }
            }
        }

        private void DrawGameOver(Graphics g)
        {
            // Draw the game over if you not alive
            if (!scenary.alive)
            {
                // Set the font and brush for drawing "Game Over"
                Font font = new Font("Arial", 40, FontStyle.Bold);
                Brush brush = new SolidBrush(Color.Red);

                // Measure the size of the text to center it on the screen
                SizeF textSize = g.MeasureString("Game Over", font);
                float x = (pcbGraphics.Width - textSize.Width) / 2;
                float y = (pcbGraphics.Height - textSize.Height) / 2;

                // Draw "Game Over" in big red letters
                g.DrawString("Game Over", font, brush, x, y);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show("Form1_KeyDown");
            // Handle arrow key presses
            switch (e.KeyData)
            {
                case Keys.Left:
                    pad.moveLeft(scenary);
                    break;
                case Keys.Right:
                    pad.moveRight(scenary);
                    break;
            }
            // Set focus to the form again to ensure continued keyboard input
            this.Focus();
        }
    }
}