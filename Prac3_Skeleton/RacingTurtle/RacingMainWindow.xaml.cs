using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using ThinkLib;

namespace RacingTurtle
{

    public partial class RacingMainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer theTimer;
        int tickCounter = 0;

        Turtle tess;
        double tSpeed = 5.0;

        public RacingMainWindow()
        {
            InitializeComponent();

            // Create the timer, start it running, bind its handler
            theTimer = new System.Windows.Threading.DispatcherTimer();
            theTimer.Interval = TimeSpan.FromMilliseconds(50);
            theTimer.IsEnabled = true;
            theTimer.Tick += dispatcherTimer_Tick;

            tess = new Turtle(playground, 287, 444);
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            tickCounter++;
         //   this.Title = string.Format("Ticks = {0}", tickCounter);

            updateTurtles();
        }

        private void updateTurtles()
        {
            if (isOnRoad(tess))
            {
                tess.Forward(tSpeed);
            }
            else
            {
                tess.Forward(tSpeed / 5.0);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Ignore repeated keys that fire because a key is held down.
           if (e.IsRepeat) return;
          
            switch (e.Key)
            {
                case Key.Escape:
                    this.Close();
                    break;

                case Key.Left:
                    tess.Left(15);
                    break;

                case Key.Right:
                    tess.Right(15);
                    break;

                case Key.F2:
                    theTimer.IsEnabled = !theTimer.IsEnabled;
                    break;

                case Key.Q:
                    theTimer.IsEnabled = false;
                    dispatcherTimer_Tick(null, null);
                    break;

                case Key.OemPlus:
                    tess.BrushWidth += 1;
                    break;
            }
        }

        private bool isOnRoad(Turtle t)
        {
            Color k = t.ColorUnderTurtle;
          //  this.Title = string.Format("color RGB= ({0},{1},{2})", k.R, k.G, k.B);
            return k.G < 66;
        }

  
    }
}
