using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AssaultBird2454.VPTU.Tabletop.Objects
{
    public class MoveThumb : Thumb
    {
        public MoveThumb()
        {
            DragDelta += MoveThumb_DragDelta;
        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var item = DataContext as Control;

            if (item != null)
            {
                var left = Canvas.GetLeft(item);
                var top = Canvas.GetTop(item);

                Canvas.SetLeft(item, left + e.HorizontalChange);
                Canvas.SetTop(item, top + e.VerticalChange);
            }
        }
    }
}