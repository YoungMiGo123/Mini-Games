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


        int currentState = 0;

        BitmapImage[] thePics;  // Define a reference to an array of images


 

        public TrafficLightsMainWindow()
        {
            InitializeComponent();
            theTimer = new System.Windows.Threading.DispatcherTimer();
            theTimer.Interval = TimeSpan.FromMilliseconds(1000);
            theTimer.IsEnabled = true;
            theTimer.Tick += dispatcherTimer_Tick;

            // In the initializer method, create the array of pictures
            // and get the pictures loaded into the array ...

            // Now we need a magic spell that tells C# to find the images "in this application" ...
            string inThisProject = "pack://application:,,,/";
            thePics = new BitmapImage[] { 
                        new BitmapImage(new Uri(inThisProject + "TrafficLightGreen.png")),
                        new BitmapImage(new Uri(inThisProject + "TrafficLightAmber.png")),
                        new BitmapImage(new Uri(inThisProject + "TrafficLightRed.png")) };

   
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            stateControllerAdvance();
            updateView();
        }

        private void stateControllerAdvance()
        {
            switch (currentState)
            {
                case 0:
                    currentState = 1;
                    break;

                case 1:
                    currentState = 2;
                    break;

                case 2:
                    currentState = 0;
                    break;
            }

            this.Title = string.Format("State = {0}", currentState);
        }

        private void updateView()
        {
            mainPic.Source = thePics[currentState];
        }

  
    }
}
