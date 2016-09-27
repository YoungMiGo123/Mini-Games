using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Media;
using System.Windows.Resources;
using System.Windows.Media;


/// <summary>
/// Some notable ideas here:
///    This is an event-driven style of computation.
///          We respond to events.
///    A timer is a good source of regular events for games.
///    We have a way of moving controls (image, button) from code.
///    We've done some interesting logic to bounce the ball.
///    We use a State Machine to manage paddle movement.
///       The keyboard events changed the state machine.
///       When the timer ticks, we move the paddle according to the state.
/// </summary>
/// 
namespace PongGame
{
    public partial class PongMainWindow : Window
    {

        DispatcherTimer theTimer;

        int tickCounter = 0;

        double velX = 3, velY = 3;

        double paddleSpeed = 10;
        string paddleState = "stopped";

        SoundPlayer sp;

        public PongMainWindow()
        {
            InitializeComponent();

            // Always initialize your own things AFTER the line above.

            theTimer = new System.Windows.Threading.DispatcherTimer();
            theTimer.Interval = TimeSpan.FromMilliseconds(20);
            theTimer.IsEnabled = true;
            theTimer.Tick += dispatcherTimer_Tick;

            // Play a lloping background file.  It must be a wav file.
            //sp = new SoundPlayer("C:\\temp\\music.wav");
            //sp.PlayLooping();
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            tickCounter++;
            updatePaddle();
            updateBall();
        }


        private void updateBall()
        {
            // How to use Sin to get a periodic heartbeat.  Sin will return a value between -1 and 1.
            // After scaling, this will allow sz to vary between 20 and 120.
            // I had to also change the fill property of the Image control `ball` to allow it to stretch
            // out of aspect ratio. 
            // double sz = 70 + 50 * Math.Sin(0.05 * tickCounter);
            // ball.Height = sz;

            // Uncomment this code to get your canvas to rotate.
            //double theta = 1 * tickCounter;
            //canvas1.RenderTransform = new RotateTransform(theta,
            //    canvas1.ActualWidth / 2, canvas1.ActualHeight / 2);
             

            double nextX = Canvas.GetLeft(ball) + velX;
            if ((nextX < 0 && velX < 0) || (nextX + ball.ActualWidth > canvas1.ActualWidth && velX > 0))
            {
                velX = -velX;         // Change direction
                makeBounceSound();
            }
            Canvas.SetLeft(ball, nextX);

            double nextY = Canvas.GetTop(ball) + velY;
            if ((nextY < 0 && velY < 0) ||  ballCollidesWithPaddle() || nextY + ball.ActualHeight > canvas1.ActualHeight && velY > 0)
            {
                velY = -velY;         // Change direction
                makeBounceSound();
            }
            Canvas.SetTop(ball, nextY);
        }

        private void makeBounceSound()
        {
            Uri pathToFile = new Uri("pack://application:,,,/bounce.wav");
            StreamResourceInfo strm = Application.GetResourceStream(pathToFile);
            sp = new SoundPlayer(strm.Stream);
            sp.Play();
        }
 
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    this.Close();
                    break;
                case Key.P:
                    theTimer.IsEnabled = !theTimer.IsEnabled;
                    break;

                case Key.Space:
                    theTimer.IsEnabled = false;
                    dispatcherTimer_Tick(null, null);
                    break;

                case Key.OemPlus:
                    velY *= 1.1;
                    break;
                case Key.OemMinus:
                    velY /= 1.1;
                    break;

                case Key.Left:
                    paddleState = "movingLeft";
                    break;

                case Key.Right:
                    paddleState = "movingRight";
                    break;

                default:
                    paddleState = "stopped";
                    break;
            }
        }
  
        private void updatePaddle()
        {
            switch (paddleState)    // See what state the paddle is in, and respond appropriately
            {
                case "stopped":
                    break;
                case "movingLeft":
                    double nextX1 = Canvas.GetLeft(paddle) - paddleSpeed;
                    Canvas.SetLeft(paddle, nextX1);
                    break;
                case "movingRight":
                    double nextX2 = Canvas.GetLeft(paddle) + paddleSpeed;
                    Canvas.SetLeft(paddle, nextX2);
                    break;
            }

            // Keep paddle near bottom of canvas even if canvas size changes
            Canvas.SetTop(paddle, canvas1.ActualHeight - 80);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            paddleState = "stopped";
        }

        private bool ballCollidesWithPaddle()
        {
            double ballX = Canvas.GetLeft(ball) + ball.ActualWidth / 2.0;
            double ballY = Canvas.GetTop(ball) + ball.ActualHeight;

            double pX0 = Canvas.GetLeft(paddle);
            double pX1 = pX0 + paddle.ActualWidth;
            double pY = Canvas.GetTop(paddle);

            bool hits = ballX >= pX0 && ballX < pX1 && ballY >= pY  &&  velY > 0;
            return hits;
        }
    }
}
