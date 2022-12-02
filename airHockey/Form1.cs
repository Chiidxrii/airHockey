using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace airHockey
{
    public partial class AirHockey : Form
    {
        Rectangle player1 = new Rectangle(10, 200, 40, 40);
        Rectangle player2 = new Rectangle(735, 200, 40, 40);
        Rectangle ball = new Rectangle(367, 200, 30, 30);
        Rectangle centre = new Rectangle(380, 0, 5, 470);
        Rectangle goal1 = new Rectangle(0, 130, 1, 190);
        Rectangle goal2 = new Rectangle(783, 130, 1, 190);
        

        int player1Score = 0;
        int player2Score = 0;

        int playerSpeed = 6;
        int ballXSpeed = -8;
        int ballYSpeed = 8;

        bool wDown = false;
        bool aDown = false;
        bool sDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool leftArrowDown = false;
        bool downArrowDown = false;
        bool rightArrowDown = false;

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        Pen pen = new Pen(Color.Black, 5);

        public AirHockey()
        {
            InitializeComponent();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move ball 
            ball.X += ballXSpeed;
            ball.Y += ballYSpeed;

            //move player 1 
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
            }

            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
            }
            if (aDown == true && player1.X > 0)
            {
                player1.X -= playerSpeed;
            }
            if (dDown == true && player1.X < 340)
            {
                player1.X += playerSpeed;
            }

            //move player 2 
            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
            }

            if (downArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += playerSpeed;
            }
            if (leftArrowDown == true && player2.X > 385)
            {
                player2.X -= playerSpeed;
            }
            if (rightArrowDown == true && player2.X < 740)
            {
                player2.X += playerSpeed;
            }
            //check if ball hit top or bottom wall and change direction if it does 
            if (ball.Y < 0 || ball.Y > this.Height - ball.Height)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed; 
            }
            

            Refresh();

            //check if ball hits either player. If it does change the direction 
            //and place the ball in front of the player hit 
            if (player1.IntersectsWith(ball))
            {
                ballXSpeed *= -1;
                ball.X = player1.X + player1.Width;
            }
            else if (player2.IntersectsWith(ball))
            {
                ballXSpeed *= -1;                
                ball.X = player2.X - player2.Width;
            }

            //check if a player missed the ball and if true add 1 to score of other player  
            if (ball.IntersectsWith(goal1))
            {
                player2Score++;
                p2Score.Text = $"{player2Score}";

                ball.X = 367;
                ball.Y = 195;

                player1.Y = 200;
                player2.Y = 200;
            }
            else if (ball.IntersectsWith(goal2))
            {
                player1Score++;
                p1Score.Text = $"{player1Score}";

                ball.X = 367;
                ball.Y = 195;

                player1.Y = 200;
                player2.Y = 200;
            }

            //check if a player missed the ball 
            if (ball.X < 0)
            {
                ballXSpeed *= -1;
            }
            else if (ball.X > 775)
            {
                ballXSpeed *= -1;
            }

            // check score and stop game if either player is at 3 
            if (player1Score == 3)
            {
                gameTimer.Enabled = false;
                outputLabel.Visible = true;
                outputLabel.Text = "Player 1 \nWins!!";
            }
            else if (player2Score == 3)
            {
                gameTimer.Enabled = false;
                outputLabel.Visible = true;
                outputLabel.Text = "Player 2 \nWins!!";
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Visible = false;
            gameTimer.Start();

            this.Focus();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
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
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawArc(pen, -130, 125, 200, 200, 270, 180);
            e.Graphics.DrawArc(pen, 715, 125, 200, 200, 70, 200);
            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(redBrush, player2);
            e.Graphics.FillRectangle(blackBrush,centre);
            e.Graphics.FillRectangle(whiteBrush, ball);
            e.Graphics.FillRectangle(blackBrush, goal1);
            e.Graphics.FillRectangle(blackBrush, goal2);    
        }
    }
}
