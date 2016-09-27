using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using ThinkLib;
namespace RacingTurtle
{

    public partial class RacingMainWindow : Window
    {
        //  define your time global variable
        // define your turtle object
        Turtle tess;
        System.Windows.Threading.DispatcherTimer theTimer;
        System.Windows.Threading.DispatcherTimer Start;

        Point x;
        Point y;

        int time = 0;
        int beg = 0;
        double speed = 15;
        // define initial speed

        public RacingMainWindow()
        {
            InitializeComponent();

            // Create the timer, start it running, bind its handler
            
            tess = new Turtle(playground, 287, 444);
            tess.LineBrush = Brushes.Transparent;
            theTimer = new System.Windows.Threading.DispatcherTimer();
            theTimer.Interval = TimeSpan.FromMilliseconds(100);
            Start = new System.Windows.Threading.DispatcherTimer();
            Start.IsEnabled = true;
            Start.Interval = TimeSpan.FromSeconds(1);
            theTimer.IsEnabled = true;
            theTimer.Tick += TheTimer_Tick;
            theTimer.Tick += theTimer_Tick2;
        }

        void theTimer_Tick2(object sender, EventArgs e)
        {
            beg++;
        }
        private void TheTimer_Tick(object sender, EventArgs e)
        {
            if (beg >= 3)
            {
                tess.Clear();
                updateTess();

                time++;
                if (time > 10)
                {
                    Ending();
                }
            }
          
        }

        private void updateTess()
        {
            //if (onTheRoad(tess) && )
            //{

            //}
            if (onTheRoad(tess))
            {
                tess.Forward(speed);
            }
            else
            {
                tess.Forward(speed / 5);
            }
        }

        private bool onTheRoad(Turtle t)
        {
            Color input = t.ColorUnderTurtle;
            if (input.G >= 225 && input.B >= 0 && input.B <= 100)
            {
                return true;
            }
            return (input.G < 65);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            //Point pos = e.GetPosition(playground);
            //tess.Position = pos;
            //Color input = tess.ColorUnderTurtle;
            //this.Title = string.Format("Mouse at {0}\n {1}", pos, input);

        }
        private void Ending()
        {
            Point pos = tess.Position;
            if ((pos.X >= 296 && pos.X <= 312) && (pos.Y >= 419 && pos.Y <= 466))
            {
                MessageBox.Show("You Won!");
                theTimer.IsEnabled = false;
            }
        }

        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape: this.Close(); break;
                case Key.Left: tess.Left(15); break;
                case Key.Right: tess.Right(15); break;
               

            }
            
        }
    }
}
