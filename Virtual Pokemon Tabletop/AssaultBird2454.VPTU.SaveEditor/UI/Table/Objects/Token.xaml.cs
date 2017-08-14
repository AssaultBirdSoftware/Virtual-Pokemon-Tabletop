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

namespace AssaultBird2454.VPTU.SaveEditor.UI.Table.Objects
{
    /// <summary>
    /// Interaction logic for Token.xaml
    /// </summary>
    public partial class Token : UserControl
    {
        public Token()
        {
            InitializeComponent();

            //SetupDragDrop();// Sets the tokens DragDrop
        }

        #region Token Drag Drop
        private bool DD_Holding = false;
        private Point DD_StartPosition;
        
        private void SetupDragDrop()
        {
            MouseLeftButtonDown += Token_MouseLeftButtonDown;
            MouseLeftButtonUp += Token_MouseLeftButtonUp;
            MouseMove += Token_MouseMove;
        }

        private void Token_MouseMove(object sender, MouseEventArgs e)
        {
            var draggableControl = sender as UserControl;

            if (DD_Holding && draggableControl != null)
            {
                Point currentPosition = e.GetPosition(this.Parent as UIElement);

                var transform = draggableControl.RenderTransform as TranslateTransform;
                if (transform == null)
                {
                    transform = new TranslateTransform();
                    draggableControl.RenderTransform = transform;
                }

                transform.X = currentPosition.X;
                transform.Y = currentPosition.Y;
            }
        }

        private void Token_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DD_Holding = false;
            var draggable = sender as UserControl;
            draggable.ReleaseMouseCapture();
        }

        private void Token_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DD_Holding = true;
            var draggableControl = sender as UserControl;
            DD_StartPosition = e.GetPosition(this);
            draggableControl.CaptureMouse();
        }
        #endregion
    }
}
