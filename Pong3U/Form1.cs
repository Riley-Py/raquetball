using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.Threading;


namespace Pong3U
{
    public partial class Form1 : Form
    {
        //global variables
        //Objects on the screen
        Rectangle player1 = new Rectangle(235, 150, 60, 10);
        Rectangle player2 = new Rectangle(235, 450, 60, 10);
        Rectangle ball = new Rectangle(250, 295, 30, 30);

        //The mid-lines
        Rectangle line = new Rectangle(0, 300, 528, 20);

        //The "Goals"
        Rectangle collision1Square = new Rectangle(210, 580, 105, 30);
        Rectangle collision2Square = new Rectangle(210, 0, 105, 20);

        //Boundaries
        Rectangle rightWall = new Rectangle(510, 1, 20, 600);
        Rectangle leftWall = new Rectangle(0, 0, 20, 600);
        Rectangle topLeftWall = new Rectangle(0, 0, 213, 20);
        Rectangle topRightWall = new Rectangle(315, 0, 213, 20);
        Rectangle bottomLeftWall = new Rectangle(20, 580, 190, 20);
        Rectangle bottomRightWall = new Rectangle(315, 580, 213, 20);

        //For back bounces on paddles
        Rectangle bottomPlayer1 = new Rectangle(235, 150, 60, 1);
        Rectangle bottomPlayer2 = new Rectangle(235, 460, 60, 1);


        //Tracks the score
        int player1Score = 0;
        int player2Score = 0;

        //Speed of everything
        int playerSpeed = 8;
        int ballXSpeed = -15;
        int ballYSpeed = 15;
        //Keys pressed
        bool wDown = false;
        bool aDown = false;
        bool sDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

        //Colors of everything
        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush orangeBrush = new SolidBrush(Color.Orange);
        Pen whitePen = new Pen(Color.White, 5);
        Pen greenPen = new Pen(Color.Green, 5);

        //Tracks the ball at the beginning to see if it has been hit
        int tracker = 1;

        //All the sounds the game uses
        SoundPlayer bonkSound = new SoundPlayer(Properties.Resources.Bonk);
        SoundPlayer ballOnPaddle = new SoundPlayer(Properties.Resources.Paddle);
        SoundPlayer goalSound = new SoundPlayer(Properties.Resources.GandalfVictory);
        SoundPlayer winnerMusic = new SoundPlayer(Properties.Resources.Victory);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {


            //move player 1
            player1Movement();
            //move player 2
            player2Movement();
            //Physics engine/scoring
            physics();
            //Refreshes everything on screen
            Refresh();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {   //Objects on screen
            e.Graphics.FillRectangle(redBrush, player1);
            e.Graphics.FillRectangle(blueBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, ball);
            e.Graphics.FillRectangle(redBrush, line);
            e.Graphics.FillRectangle(greenBrush, collision1Square);
            e.Graphics.FillRectangle(greenBrush, collision2Square);
            e.Graphics.FillRectangle(orangeBrush, ball);
            e.Graphics.FillRectangle(whiteBrush, rightWall);
            e.Graphics.FillRectangle(whiteBrush, leftWall);
            e.Graphics.FillRectangle(whiteBrush, topLeftWall);
            e.Graphics.FillRectangle(whiteBrush, topRightWall);
            e.Graphics.FillRectangle(whiteBrush, bottomLeftWall);
            e.Graphics.FillRectangle(whiteBrush, bottomRightWall);
            e.Graphics.FillRectangle(redBrush, bottomPlayer1);
            e.Graphics.FillRectangle(blueBrush, bottomPlayer2);

            //Window dressing (or decorations)
            e.Graphics.DrawArc(whitePen, 212, 550, 100, 100, 0, -180);
            e.Graphics.DrawArc(whitePen, 212, -50, 100, 100, 0, 180);
            e.Graphics.DrawEllipse(whitePen, 225, 275, 75, 75);

        }
        public void player1Movement()
        {
            //Movement for the player and the checks
            if (wDown == true && player1.IntersectsWith(line) == false)
            {
                player1.Y += playerSpeed;
                bottomPlayer1.Y += playerSpeed;
            }

            if (sDown == true && bottomPlayer1.IntersectsWith(topLeftWall) == false && bottomPlayer1.IntersectsWith(topRightWall) == false && bottomPlayer1.IntersectsWith(collision2Square) == false)
            {
                player1.Y -= playerSpeed;
                bottomPlayer1.Y -= playerSpeed;
            }

            if (aDown == true && player1.IntersectsWith(leftWall) == false)
            {
                player1.X -= playerSpeed;
                bottomPlayer1.X -= playerSpeed;
            }

            if (dDown == true && player1.IntersectsWith(rightWall) == false)
            {
                player1.X += playerSpeed;
                bottomPlayer1.X += playerSpeed;
            }

        }
        public void player2Movement()
        {
            //Same as player 1
            if (upArrowDown == true && player2.IntersectsWith(line) == false)
            {
                player2.Y -= playerSpeed;
                bottomPlayer2.Y -= playerSpeed;
            }

            if (downArrowDown == true && bottomPlayer2.IntersectsWith(bottomLeftWall) == false && bottomPlayer2.IntersectsWith(bottomRightWall) == false && bottomPlayer2.IntersectsWith(collision1Square) == false)
            {
                player2.Y += playerSpeed;
                bottomPlayer2.Y += playerSpeed;
            }

            if (leftArrowDown == true && player2.IntersectsWith(leftWall) == false)
            {
                player2.X -= playerSpeed;
                bottomPlayer2.X -= playerSpeed;
            }

            if (rightArrowDown == true && player2.IntersectsWith(rightWall) == false)
            {
                player2.X += playerSpeed;
                bottomPlayer2.X += playerSpeed;
            }

        }
        public void physics()
        {
            //Physics of the game (hard to do, but satisfying in the end!)
            //Starting the game
            if (player1.IntersectsWith(ball) && tracker == 1)
            {
                tracker = 0;
                ballOnPaddle.Play();
            }
            else if (player2.IntersectsWith(ball) && tracker == 1)
            {

                tracker = 2;
                ballOnPaddle.Play();
            }

            switch (tracker)
            {
                case 0:
                    ball.X += ballXSpeed;
                    ball.Y += ballYSpeed;
                    break;
                case 2:
                    ball.X -= ballXSpeed;
                    ball.Y -= ballYSpeed;
                    break;
            }

            //General collision with the walls (adjust later on)
            if (ball.IntersectsWith(leftWall))
            {
                ballXSpeed *= -1;
                ballXSpeed++;
                ball.X = leftWall.X + ball.Width;
                bonkSound.Play();

            }
            if (ball.IntersectsWith(bottomLeftWall))
            {
                ballYSpeed *= -1;
                ballYSpeed += 3;
                ball.Y = bottomRightWall.Y - ball.Height;
                bonkSound.Play();

            }
            if (ball.IntersectsWith(rightWall))
            {
                ballXSpeed *= -1;
                ballXSpeed++;
                ball.X = rightWall.X - ball.Width;
                bonkSound.Play();

            }
            if (ball.IntersectsWith(topRightWall))
            {
                ballYSpeed *= -1;
                ballYSpeed += 3;
                ball.Y = topRightWall.Y + ball.Height;
                bonkSound.Play();

            }
            if (ball.IntersectsWith(bottomRightWall))
            {
                ballYSpeed *= -1;
                ballYSpeed += 3;
                ball.Y = bottomRightWall.Y - ball.Height;
                bonkSound.Play();

            }
            if (ball.IntersectsWith(topLeftWall))
            {
                ballYSpeed *= -1;
                ballYSpeed += 3;
                ball.Y = topRightWall.Y + ball.Height;
                bonkSound.Play();


            }

            //For goals (resets everything)
            if (ball.IntersectsWith(collision1Square))
            {
                tracker = 1;

                ball.X = 250;
                ball.Y = 295;

                ballXSpeed = -15;
                ballYSpeed = 15;

                player1.X = 235;
                player1.Y = 150;

                player2.X = 235;
                player2.Y = 450;

                bottomPlayer1.X = 235;
                bottomPlayer1.Y = 150;

                bottomPlayer2.X = 235;
                bottomPlayer2.Y = 460;

                player1Score++;
                scoring();

            }
            if (ball.IntersectsWith(collision2Square))
            {
                tracker = 1;

                ball.X = 250;
                ball.Y = 295;

                ballXSpeed = -15;
                ballYSpeed = 15;

                player1.X = 235;
                player1.Y = 150;

                player2.X = 235;
                player2.Y = 450;

                bottomPlayer1.X = 235;
                bottomPlayer1.Y = 150;

                bottomPlayer2.X = 235;
                bottomPlayer2.Y = 460;

                player2Score++;
                scoring();

            }

            //Paddles
            if (player1.IntersectsWith(ball) && bottomPlayer1.IntersectsWith(ball) == false)
            {

                ballYSpeed *= -1;
                ballYSpeed++;
                ball.Y = player1.Y + ball.Height;
                ballOnPaddle.Play();

            }

            if (bottomPlayer1.IntersectsWith(ball) && player1.IntersectsWith(ball))
            {

                ballYSpeed *= -1;
                ballYSpeed++;
                ball.Y = player1.Y - ball.Height;
                ballOnPaddle.Play();
            }
            if (player2.IntersectsWith(ball) && bottomPlayer2.IntersectsWith(ball) == false)
            {

                ballYSpeed *= -1;
                ballYSpeed++;
                ball.Y = player2.Y - ball.Height;
                ballOnPaddle.Play();

            }
            if (player2.IntersectsWith(ball) && bottomPlayer2.IntersectsWith(ball))
            {
                ballYSpeed *= -1;
                ballYSpeed++;
                ball.Y = player2.Y + ball.Height;
                ballOnPaddle.Play();
            }

        }
        public void scoring()
        {
            p1ScoreLabel.Text = $"Player 1: {player1Score}";
            p2ScoreLabel.Text = $"Player 2: {player2Score}";

            goalSound.Play();

            if (player1Score == 3)
            {
               
                winLabel.Visible = true;
                gameTimer.Stop();
                gameTimer.Dispose();
                winLabel.Text = "Player 1 has won!";
                winnerMusic.Play();
                playAgain.Visible = true;
                exitButton.Visible = true;
            }
            else if (player2Score == 3)
            {

                gameTimer.Stop();
                gameTimer.Dispose();
                winLabel.Visible = true;
                winLabel.Text = "Player 2 has won!";
                winnerMusic.Play();
                playAgain.Visible = true;
                exitButton.Visible = true;

            }

        }

        private void playAgain_Click(object sender, EventArgs e)
        {
            //Button to play again 
            player1Score = 0;
            player2Score = 0;
            p1ScoreLabel.Text = "Player 1: 0";
            p2ScoreLabel.Text = "Player 2: 0";
            winnerMusic.Stop();
            winLabel.Visible = false;
            playAgain.Visible = false;
            exitButton.Visible = false;
            gameTimer.Start();
            this.Focus();
        }

        private void startButton_Click(object sender, EventArgs e)
        {   
            //Starts the game
            startButton.Visible = false;
            gameTimer.Enabled = true;
            gameTimer.Start();
            this.Focus();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            //Closes the application
            this.Close();
        }
    }
}
