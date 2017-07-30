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

namespace AssaultBird2454.VPTU.SaveEditor.UI.Table
{
    /// <summary>
    /// Interaction logic for TableLayer.xaml
    /// </summary>
    public partial class TableLayer : UserControl
    {
        public TableLayer()
        {
            InitializeComponent();

            UI.Table.Objects.Token tok = new UI.Table.Objects.Token();
        }

        public double FirstXPos { get; private set; }
        public double FirstYPos { get; private set; }
        public double FirstArrowXPos { get; private set; }
        public double FirstArrowYPos { get; private set; }
        public object MovingObject { get; private set; }

        public void AddToken(Control ctrl)
        {
            ctrl.MouseLeftButtonDown += Ctrl_MouseLeftButtonDown;
            ctrl.MouseLeftButtonUp += Ctrl_MouseLeftButtonUp;
            ctrl.MouseMove += Ctrl_MouseMove;
        }

        private void Ctrl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                (MovingObject as FrameworkElement).SetValue(Canvas.LeftProperty,
                     e.GetPosition((MovingObject as FrameworkElement).Parent as FrameworkElement).X - FirstXPos);

                (MovingObject as FrameworkElement).SetValue(Canvas.TopProperty,
                     e.GetPosition((MovingObject as FrameworkElement).Parent as FrameworkElement).Y - FirstYPos);
            }
        }

        private void Ctrl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MovingObject = null;
        }

        private void Ctrl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FirstXPos = e.GetPosition(sender as Control).X;
            FirstYPos = e.GetPosition(sender as Control).Y;
            FirstArrowXPos = e.GetPosition((sender as Control).Parent as Control).X - FirstXPos;
            FirstArrowYPos = e.GetPosition((sender as Control).Parent as Control).Y - FirstYPos;
            MovingObject = sender;
        }
    }
}
