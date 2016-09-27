using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

/// <summary>
/// Important stuff to note here...
/// How we coded up the logic for a simple state machine with three states.
/// The  Model/View/Controller parts of the program are separated 
///            from each other.
///     The Timer is the Controller and generates the events.  
///         It could also be a button click, etc.
///     The Model of how the lights work internally is our state machine.
///     The View shows what is happening in the model. 
/// </summary>
namespace TrafficLights
{

    public partial class TrafficLightsMainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer theTimer;
        int CurrentState = 0;
        private BitmapImage[] thePics;
        // define the global timer variable
        
        public TrafficLightsMainWindow()
        {
            InitializeComponent();

            // Instantiate your timer here
            theTimer = new System.Windows.Threading.DispatcherTimer();
            theTimer.Interval = TimeSpan.FromMilliseconds(200);
            theTimer.IsEnabled = true;
            theTimer.Tick += TheTimer_Tick;
            string inThisProject = "pack://application:,,,/";

            thePics = new BitmapImage[] { new BitmapImage(new Uri(inThisProject + "TrafficLightGreen.png")), new BitmapImage(new Uri(inThisProject + "TrafficLightAmber.png")), new BitmapImage(new Uri(inThisProject + "TrafficLightRed.png")) };
            // In the initializer method, create the array of pictures
            // and get the pictures loaded into the array ..            
   
        }
        private void TheTimer_Tick(object sender, EventArgs e)
        {
            stateControllerAdvance();
            updateView();
            ControllingState();
        }
        private BitmapImage[] GivePic(int token)
        {
            BitmapImage[] TrafficPics;
            string FindInProject = "pack://application:,,,/";

            TrafficPics = new BitmapImage[] 
            {
                new BitmapImage(new Uri(FindInProject + "TrafficLightRed.png")),
                new BitmapImage(new Uri(FindInProject + "TrafficLightGreen.png")),
                new BitmapImage(new Uri(FindInProject + "TrafficLightAmber.png")),
                new BitmapImage(new Uri(FindInProject + "pedestrian.png"))
            };

            return TrafficPics;
        }
        private void ControllingState()
        {
            switch (CurrentState)
            {
                case 0: CurrentState = 1;
                    break;

                case 1: CurrentState = 2;
                    break;

                case 2: CurrentState = 0;
                    break;
            }
        }
        private void stateControllerAdvance()
        {
            BitmapImage[] TrafficPicsCopy = GivePic(CurrentState);

            if (CurrentState == 0)
            {
                theTimer.Interval = TimeSpan.FromSeconds(2.00);
                theTimer.IsEnabled = true;
                mainPic.Source = TrafficPicsCopy[CurrentState];
            }

            else if (CurrentState == 1)
            {
                theTimer.Interval = TimeSpan.FromSeconds(3.00);
                theTimer.IsEnabled = true;
                mainPic.Source = TrafficPicsCopy[CurrentState];
            }

            else if (CurrentState == 2)
            {
                theTimer.Interval = TimeSpan.FromSeconds(1.00);
                theTimer.IsEnabled = true;
                mainPic.Source = TrafficPicsCopy[CurrentState];
            }
        }

        private void PedestrianPic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BitmapImage[] TrafficPicsCopy = GivePic(CurrentState);

            theTimer.Interval = TimeSpan.FromSeconds(10.00);
            theTimer.IsEnabled = true;
            mainPic.Source = TrafficPicsCopy[1];
            MessageBox.Show("Pedestrian button pressed");
        }
  
        private void updateView()
        {
            mainPic.Source = thePics[CurrentState];
        }

        private void pedButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BitmapImage[] TrafficPicsCopy = GivePic(CurrentState);

            theTimer.Interval = TimeSpan.FromSeconds(10.00);
            theTimer.IsEnabled = true;
            mainPic.Source = TrafficPicsCopy[1];
      
        }

    }
}
