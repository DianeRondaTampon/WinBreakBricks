using System.Drawing;
using System.Numerics;
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

        List<Image> padImages;
        Image ballImage;
        List<Image> brickImages;

        Point padSize;
        Point ballSize;
        Point brickSize;
        Point scenarySize;

        public Form1()
        {
            InitializeComponent();

            // This property determines whether the form receives keyboard events before the event is passed to the control that has focus.
            KeyPreview = true;

            // Set focus to the form
            this.Focus();
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

            padSize = new Point(150, 40);
            ballSize = new Point(75, 75);
            brickSize = new Point(170, 60);
            scenarySize = new Point(1190, 600);

            // Load individual images into variables
            // Call the method to copy a portion of the source image to the destination image
            padImages = new List<Image>();
            Image padImage = CopyImagePortion(new Rectangle(398, 17, padSize.X, padSize.Y), spriteImage); //the pad (1st animation) is in 390,10 size 160,50
            padImages.Add(padImage);
            padImage = CopyImagePortion(new Rectangle(566, 17, padSize.X, padSize.Y), spriteImage); //the pad (2st animation) is in 390,10 size 160,50
            padImages.Add(padImage);
            padImage = CopyImagePortion(new Rectangle(738, 17, padSize.X, padSize.Y), spriteImage); //the pad (3st animation) is in 390,10 size 160,50
            padImages.Add(padImage);

            ballImage = CopyImagePortion(new Rectangle(805, 548, ballSize.X, ballSize.Y), spriteImage); //the pad (1st animation) is in 390,10 size 160,50

            brickImages = new List<Image>();
            Image brickImage = CopyImagePortion(new Rectangle(20, 18, brickSize.X, brickSize.Y), spriteImage); //the brick (blue) is in 16,16 size 175,60
            brickImages.Add(brickImage);
            brickImage = CopyImagePortion(new Rectangle(20, 94, brickSize.X, brickSize.Y), spriteImage); //the brick (green) is in 16,16 size 175,60
            brickImages.Add(brickImage);
            brickImage = CopyImagePortion(new Rectangle(20, 169, brickSize.X, brickSize.Y), spriteImage); //the brick (purple) is in 16,16 size 175,60
            brickImages.Add(brickImage);
            brickImage = CopyImagePortion(new Rectangle(20, 241, brickSize.X, brickSize.Y), spriteImage); //the brick (red) is in 16,16 size 175,60
            brickImages.Add(brickImage);
            brickImage = CopyImagePortion(new Rectangle(20, 319, brickSize.X, brickSize.Y), spriteImage); //the brick (orange) is in 16,16 size 175,60
            brickImages.Add(brickImage);
            brickImage = CopyImagePortion(new Rectangle(20, 394, brickSize.X, brickSize.Y), spriteImage); //the brick (sky) is in 16,16 size 175,60
            brickImages.Add(brickImage);
            brickImage = CopyImagePortion(new Rectangle(20, 470, brickSize.X, brickSize.Y), spriteImage); //the brick (yellow) is in 16,16 size 175,60
            brickImages.Add(brickImage);
            brickImage = CopyImagePortion(new Rectangle(20, 548, brickSize.X, brickSize.Y), spriteImage); //the brick (dark green) is in 16,16 size 175,60
            brickImages.Add(brickImage);
            brickImage = CopyImagePortion(new Rectangle(20, 622, brickSize.X, brickSize.Y), spriteImage); //the brick (grey) is in 16,16 size 175,60
            brickImages.Add(brickImage);
            brickImage = CopyImagePortion(new Rectangle(20, 697, brickSize.X, brickSize.Y), spriteImage); //the brick (brown) is in 16,16 size 175,60
            brickImages.Add(brickImage);


            //classes
            int centerX = scenarySize.X / 2;
            int centery = scenarySize.Y / 2;
            pad = new Pad(new Point(centerX - padSize.X / 2, 540), padSize);
            ball = new Ball(centerX - ballSize.X / 2, 440, ballSize);
            scenary = new Scenary(scenarySize);

            //stage 1 of scenary
            scenary.Initialize(brickSize);


            //start the game
            timer.Interval = 20; //each second will be exec 50 times
            timer.Enabled = true;
        }

        private void InitializeBallPad()
        {
            int centerX = scenary.boundariesDownRight.X / 2;
            int centery = scenary.boundariesDownRight.Y / 2;
            pad.location = new Point(centerX - padSize.X / 2, 540);
            ball.locationX = centerX - ballSize.X / 2;
            ball.locationY = 440;
            ball.direction = Vector.NormalizeVector(new Vector2(1, -1)); //to diagonal North East
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
            ball.Move(scenary, pad);
        }

        private void DragGame()
        {
            Bitmap bitmap = new Bitmap(pcbGraphics.Width, pcbGraphics.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {

                // Clear the background
                g.Clear(Color.Black);

                // draw scenary
                DrawBricks(g);

                // draw ball
                DrawBall(g);

                // draw the pad of the player
                DrawPad(g);

                // Draw the game over if you not alive or win
                DrawGameOver(g);

                // Draw score and stage
                DrawLabels();
            }

            // Draw the off-screen bitmap to the PictureBox
            pcbGraphics.Image?.Dispose(); // Dispose the previous image to avoid memory leaks
            pcbGraphics.Image = bitmap;
        }

        private void DrawPad(Graphics g)
        {
            g.DrawImage(padImages[pad.numSprite], pad.location.X, pad.location.Y);
            pad.timeSprite++;
            if (pad.timeSprite >= pad.maxTimeSprites)
            {
                pad.timeSprite = 0;
                pad.numSprite++;
                if (pad.numSprite >= pad.maxSprites)
                {
                    pad.numSprite = 0;
                }
            }
        }

        private void DrawBall(Graphics g)
        {
            g.DrawImage(ballImage, (int)ball.locationX, (int)ball.locationY);
        }

        private void DrawBricks(Graphics g)
        {
            foreach (Brick brick in scenary.bricks)
            {
                g.DrawImage(brickImages[(int)brick.color], brick.location.X, brick.location.Y);
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

                timer.Enabled = false;
                btnRestart.Visible = true;
            }

            // Draw the game over if you won
            if (scenary.win)
            {
                // Set the font and brush for drawing "Game Over"
                Font font = new Font("Arial", 40, FontStyle.Bold);
                Brush brush = new SolidBrush(Color.Green);

                // Measure the size of the text to center it on the screen
                SizeF textSize = g.MeasureString("Win!!!", font);
                float x = (pcbGraphics.Width - textSize.Width) / 2;
                float y = (pcbGraphics.Height - textSize.Height) / 2;

                // Draw "Game Over" in big red letters
                g.DrawString("Win!!!", font, brush, x, y);

                timer.Enabled = false;
                btnNext.Visible = true;
            }
        }

        private void DrawLabels()
        {
            lblScore.Text = scenary.score.ToString();
            lblStage.Text = scenary.stage.ToString();
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

        private void btnRestart_Click(object sender, EventArgs e)
        {
            scenary.stage = 1;
            scenary.score = 0;
            scenary.Initialize(brickSize);
            InitializeBallPad();

            timer.Enabled = true;
            btnRestart.Visible = false;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            scenary.stage++;
            scenary.Initialize(brickSize);
            InitializeBallPad();

            timer.Enabled = true;
            btnNext.Visible = false;
        }
    }
}