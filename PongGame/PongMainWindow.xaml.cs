using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Media;
using System.Windows.Resources;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ThinkLib;
using System.Reflection;
using System.Collections.Generic;



/// <summary>
/// Some notable ideas here:
///    This is an event-driven style of computation.
///    We respond to events.
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
        Turtle tess;
        Point posBall, posPaddle;
        DispatcherTimer theTimer, getReady;
        //TimeSpan time;
         
        double velX = 9, velY = 9;
        double paddleSpeed = 10;
        string paddleState = "stopped";
        double z = 53;
        double o = 10;
        double i = 0;
        int Lives = 4;
        
        int level = 1;
        int count = 0;
        // BitmapImage ball;

        System.Diagnostics.Stopwatch timing = new System.Diagnostics.Stopwatch();
        public PongMainWindow()
        {
            InitializeComponent();

            // Always initialize your own things AFTER the line above.

           
            MessageBox.Show("Read The 'Read Me' For Instructions in the PongGame folder");
            posPaddle = new Point(210, 58);
            posBall = new Point(192, 245);
            theTimer = new DispatcherTimer();
         
            theTimer.Interval = TimeSpan.FromMilliseconds(31);
            
            theTimer.IsEnabled = false;
            theTimer.Tick += theTimer_Tick;
            
            getReady = new DispatcherTimer();
            getReady.Interval = TimeSpan.FromMilliseconds(25);
            getReady.Tick += getReady_Tick;
            getReady.IsEnabled = false;
            // You may play a looping background file.  It must be a wav file.
            timing.Start();
        }

        private void getReady_Tick(object sender, EventArgs e)
        {
            
            updatePaddle();
            //if (count % 10 == 0 && count > 0)
            //{
            //    Lives += 1;
                

            //}
        }
       

        
        private void makeBounceSound()
        {
            //Uri path = new Uri("pack://application:,,,/Bounce.wav");
            //StreamResourceInfo stream = Application.GetResourceStream(path);
            //SoundPlayer sp = new SoundPlayer(stream.Stream);
            //sp.Play();
           
            SoundPlayer go = new SoundPlayer("..\\..\\Bounce.wav"); //I couldn't get it to work the other way!
                                                                                                                                            //All the sound effects are embeded inside the project so just change the filepath.
            go.Play();
        }
        private void background()
        {

            SoundPlayer run = new SoundPlayer("..\\..\\Metroid_Door.wav");
            run.Play();
        }
        private void ForeverPlaying()
        {
            SoundPlayer play = new SoundPlayer("..\\..\\Triumph.wav");
            play.Play();
        }
       

        void theTimer_Tick(object sender, EventArgs e)
        {
              
                updateBall();
                
              
               
                
                lifeBalls();
                
                scoreBox.Content = "" + count;
                lifeBox.Content = "" + Lives;
           
        }
        void lifeBalls()
        {
          
            if (Canvas.GetTop(ball) + ball.ActualHeight + 10 >= home.ActualHeight)
            {
                Lives -= 1;
                background();
                velY = -velY;
            
                if (Lives == 0)
                {
                    ForeverPlaying();
                    theTimer.IsEnabled = false;
                    getReady.IsEnabled = false;
                    MessageBox.Show("You lose");

                }
                if (count % 10 == 0 && count > 0)
                {
                    Lives += 1;
                    

                }

            }
        }
        private void updatePaddle()
        {
            
            double nextX1 = Canvas.GetLeft(paddle) - paddleSpeed;
            double nextX2 = Canvas.GetLeft(paddle) + paddleSpeed;
            if (nextX2 < 0 || nextX2 + paddle.ActualWidth > home.ActualWidth && paddleSpeed > 0)
            {
                nextX2 = nextX1;

            }
            if (nextX1 < 0 || nextX1 + paddle.ActualWidth > home.ActualWidth && paddleSpeed > 0)
            {
                nextX1 = nextX2;
            }

            switch (paddleState)
            {
                case "stopped": break;
                case "Left": Canvas.SetLeft(paddle, nextX1); break;
                case "Right": Canvas.SetLeft(paddle, nextX2); break;
              

            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left: paddleState = "Left"; break;
                case Key.Right: paddleState = "Right"; break;
                case Key.P: theTimer.IsEnabled = !(theTimer.IsEnabled); getReady.IsEnabled = !getReady.IsEnabled; break;
                case Key.R: ResetGame(); break;
                case Key.S: theTimer.IsEnabled = true;
                    getReady.IsEnabled = true; break;
                default: paddleState = "stopped"; break;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            paddleState = "stopped";

        }

        private Brush PickBrush()
        {
            Brush result = Brushes.Transparent;

            Random rnd = new Random();

            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            int random = rnd.Next(properties.Length);
            result = (Brush)properties[random].GetValue(null, null);

            return result;
        }
        private void ResetGame()
        {
            Canvas.SetLeft(ball, 192);
            Canvas.SetLeft(paddle, 192);
            Canvas.SetTop(ball, 43);
            Canvas.SetLeft(paddle, 245);
            count = 0;
            Lives = 4;
            theTimer.IsEnabled = true;
            getReady.IsEnabled = true;
        }
        private void updateBall()
        {

            double nextX = Canvas.GetLeft(ball) + velX;
            Canvas.SetLeft(ball, nextX);

            double nextY = Canvas.GetTop(ball) + velY;
            Canvas.SetTop(ball, nextY);
          
            // tess.Goto(j, i);

           

            if (nextY < 0 || nextY + ball.ActualHeight > home.ActualHeight && velY > 0)
            {
                velY = -velY;

            }
            
            double lengthPaddle = Canvas.GetLeft(paddle) + paddle.ActualWidth;
            if (Canvas.GetTop(ball) + ball.ActualHeight + 10 >= Canvas.GetTop(paddle) && Canvas.GetLeft(ball) + 10 >= Canvas.GetLeft(paddle) && Canvas.GetLeft(ball) <= lengthPaddle && velY > 0)
            {
                makeBounceSound();
                velY = -velY; //Change direction
                count++;
                home.Background = PickBrush();
            }
            if (nextX < 0 || nextX + ball.ActualWidth > home.ActualWidth && velX > 0)
            {
                velX = -velX;
               
            }
         
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (Lives > 0)
            {
                theTimer.IsEnabled = true;
                getReady.IsEnabled = true;
            }
          
        }


    }
    }
   
         

    

        

    

    


