using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssaultBird2454.VPTU.Client.UI.Entity
{
    /// <summary>
    /// Interaction logic for EntityListItem.xaml
    /// </summary>
    public partial class EntityListItem : UserControl
    {
        public EntityListItem()
        {
            InitializeComponent();
        }

        public void Update(Bitmap Image, string Name, List<KeyValuePair<System.Windows.Media.Color, string>> Viewers)
        {
            Entity_PlayerIndicators.Children.Clear();

            Entity_Image.Source = Imaging.CreateBitmapSourceFromHBitmap(Image.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            Entity_Name.Content = Name;
            
            foreach(KeyValuePair<System.Windows.Media.Color, string> cn in Viewers)
            {
                Border bord = new Border()
                {
                    Width = 8,
                    Height = 12,
                    BorderThickness = new Thickness(0, 0, 0, 0),
                    Background = new SolidColorBrush(cn.Key),
                    ToolTip = new ToolTip()
                    {
                        Content = cn.Value
                    }
                };
            }
        }
    }
}
