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

namespace AssaultBird2454.VPTU.SaveEditor.UI.Entities
{
    /// <summary>
    /// Interaction logic for EntitiesListItem.xaml
    /// </summary>
    public partial class EntitiesListItem : UserControl
    {
        public EntitiesListItem()
        {
            InitializeComponent();
        }

        public void Update(Bitmap Image, string Name, List<KeyValuePair<System.Windows.Media.Color, string>> Viewers)
        {
            Entities_PlayerIndicators.Children.Clear();

            try { Entities_Image.Source = Imaging.CreateBitmapSourceFromHBitmap(Image.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions()); } catch { }
            Entities_Name.Content = Name;

            if (Viewers != null)
            {
                foreach (KeyValuePair<System.Windows.Media.Color, string> cn in Viewers)
                {
                    Border bord = new Border()
                    {
                        Width = 8,
                        Height = 12,
                        Margin = new Thickness(0, 0, 5, 0),
                        BorderThickness = new Thickness(0, 0, 0, 0),
                        Background = new SolidColorBrush(cn.Key),
                        ToolTip = new ToolTip()
                        {
                            Content = cn.Value
                        }
                    };

                    Entities_PlayerIndicators.Children.Add(bord);
                }
            }
        }
    }
}
