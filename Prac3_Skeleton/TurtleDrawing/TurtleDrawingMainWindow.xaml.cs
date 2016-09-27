using System.Windows;
using System.Windows.Input;
using ThinkLib;

namespace TurtleDrawing
{

    public partial class TurtleDrawingMainWindow : Window
    {
        Turtle tess;

        public TurtleDrawingMainWindow()
        {
            InitializeComponent();

            tess = new Turtle(playground);
            tess.BrushWidth = 3;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Point p1 = e.GetPosition(playground);
            this.Title = string.Format("Pos={0}", p1);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                tess.Goto(p1);
            }
            else
            {
                tess.WarpTo(p1);
            }
        }
 
    }
}
