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

namespace AssaultBird2454.VPTU.Tabletop.Map
{
    /// <summary>
    /// Interaction logic for Table.xaml
    /// </summary>
    public partial class Table : UserControl
    {
        public Table()
        {
            InitializeComponent();
        }

        public void AddToken(string Image)
        {
            Token.Pokemon_Token token = new Token.Pokemon_Token();
            token.ChangeToken_BG(Image);
            Layer_Tokens.Children.Add(token);
            token.MouseUp += UserControl_MouseUp;
            token.MouseMove += UserControl_MouseMove;
            token.MouseDown += UserControl_MouseDown;
        }

        private double FirstXPos { get; set; }
        private double FirstYPos { get; set; }
        private double FirstArrowXPos { get; set; }
        private double FirstArrowYPos { get; set; }
        private object MovingObject { get; set; }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //In this event, we get current mouse position on the control to use it in the MouseMove event.
            FirstXPos = e.GetPosition(sender as Control).X;
            FirstYPos = e.GetPosition(sender as Control).Y;
            FirstArrowXPos = e.GetPosition((sender as Control).Parent as Control).X - FirstXPos;
            FirstArrowYPos = e.GetPosition((sender as Control).Parent as Control).Y - FirstYPos;
            MovingObject = sender;
        }
        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MovingObject = null;
        }
        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                (MovingObject as FrameworkElement).SetValue(Canvas.LeftProperty,
                     e.GetPosition((MovingObject as FrameworkElement).Parent as FrameworkElement).X - FirstXPos);

                (MovingObject as FrameworkElement).SetValue(Canvas.TopProperty,
                     e.GetPosition((MovingObject as FrameworkElement).Parent as FrameworkElement).Y - FirstYPos);
            }
        }
    }
}
