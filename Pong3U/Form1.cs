using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong3U
{
    public partial class Form1 : Form
    {
        //global variables
        Rectangle player1 = new Rectangle(235, 150, 60, 10);
        Rectangle player2 = new Rectangle(235, 450, 60, 10);
        Rectangle ball = new Rectangle(250, 290, 30, 30);

        Rectangle line = new Rectangle(0, 300, 528, 20);

        Rectangle collision1Square = new Rectangle(213, 595, 100, 5);
        Rectangle collision2Square = new Rectangle(213, 0, 100, 5);

        

        int player1Score = 0;
        int player2Score = 0;

        int playerTurn = 1;

        int playerSpeed = 6;
        int ballXSpeed = -6;
        int ballYSpeed = 6;

        bool wDown = false;
        bool aDown = false;
        bool sDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush orangeBrush = new SolidBrush(Color.Orange);
        Pen whitePen = new Pen(Color.White, 5);

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
            ////move ball
            //ball.X += ballXSpeed;
            //ball.Y += ballYSpeed;

            //move player1
            if (wDown == true && player1.Y < 285)
            {
                player1.Y += playerSpeed;
            }

            if (sDown == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
            }

            if (aDown == true && player1.X > 0)
            {
                player1.X -= playerSpeed;
            }

            if (dDown == true && player1.X < this.Width - player1.Width)
            {
                player1.X += playerSpeed;
            }

            //move player2
            if (upArrowDown == true && player2.Y > 325)
            {
                player2.Y -= playerSpeed;
            }

            if (downArrowDown == true && player2.Y < ((this.Height - player2.Height) - 5))
            {
                player2.Y += playerSpeed;
            }

            if (leftArrowDown == true && player2.X > 0)
            {
                player2.X -= playerSpeed;
            }

            if (rightArrowDown == true && player2.X < this.Width - player2.Width)
            {
                player2.X += playerSpeed;
            }

            ////ball collision with top and bottom walls
            //if (ball.Y < 0 || ball.Y > this.Height - ball.Height)
            //{
            //    ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed; 
            //}

            ////ball collision with right side wall
            //if (ball.X >= this.Width - ball.Width)
            //{
            //    ballXSpeed *= -1;

            //}

            ////ball collision with player
            //if (ballXSpeed < 0)
            //{
            //    if (player1.IntersectsWith(ball) && playerTurn == 1)
            //    {
            //        ballXSpeed *= -1;
            //        ball.X = player1.X + ball.Width;
            //        playerTurn = 2;
            //    }
            //    else if (player2.IntersectsWith(ball) && playerTurn == 2)
            //    {
            //        ballXSpeed *= -1;
            //        ball.X = player2.X + ball.Width;
            //        playerTurn = 1;
            //    }
            //}

            ////check for point scored 
            //if (ball.X < 0)
            //{
            //    if (playerTurn == 2)
            //    {
            //        player1Score++;
            //        p1ScoreLabel.Text = $"{player1Score}";
            //    }
            //    else
            //    {
            //        player2Score++;
            //        p2ScoreLabel.Text = $"{player2Score}";
            //    }

            //    ball.X = 295;
            //    ball.Y = 195;

            //    player1.Y = 210;
            //    player1.X = 10;
            //    player2.Y = 130;
            //    player2.X = 10;
            //}


            ////check for game over
            //if (player1Score == 3)
            //{
            //    gameTimer.Enabled = false;
            //    winLabel.Visible = true;
            //    winLabel.Text = "Player 1 Wins!!";
            //}
            //else if (player2Score == 3)
            //{
            //    gameTimer.Enabled = false;
            //    winLabel.Visible = true;
            //    winLabel.Text = "Player 2 Wins!!";
            //}

            Refresh();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(redBrush, player1);
            e.Graphics.FillRectangle(blueBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, ball);
            e.Graphics.FillRectangle(whiteBrush, line);
            e.Graphics.FillRectangle(greenBrush, collision1Square);
            e.Graphics.FillRectangle(greenBrush, collision2Square);
            e.Graphics.FillRectangle(orangeBrush, ball);

            e.Graphics.DrawArc(whitePen, 212, 550, 100, 100, 0, -180);
            e.Graphics.DrawArc(whitePen, 212, -50, 100, 100, 0, 180);

            //if (playerTurn == 1)
            //{
            //    e.Graphics.DrawRectangle(whitePen, player1);
            //}
            //else
            //{
            //    e.Graphics.DrawRectangle(whitePen, player2);
            //}
        }
    }
}
