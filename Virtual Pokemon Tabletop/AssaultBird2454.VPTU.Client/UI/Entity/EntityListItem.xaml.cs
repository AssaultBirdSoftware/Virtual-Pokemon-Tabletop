using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Windows.Media.Color;

namespace AssaultBird2454.VPTU.Client.UI.Entities
{
    /// <summary>
    ///     Interaction logic for EntitiesListItem.xaml
    /// </summary>
    public partial class EntitiesListItem : UserControl
    {
        public EntitiesListItem()
        {
            InitializeComponent();
        }

        public void Update(Bitmap Image, string Name, List<KeyValuePair<Color, string>> Viewers)
        {
            Entities_PlayerIndicators.Children.Clear();

            Entities_Image.Source = Imaging.CreateBitmapSourceFromHBitmap(Image.GetHbitmap(), IntPtr.Zero,
                Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            Entities_Name.Content = Name;

            foreach (var cn in Viewers)
            {
                var bord = new Border
                {
                    Width = 8,
                    Height = 12,
                    Margin = new Thickness(0, 0, 5, 0),
                    BorderThickness = new Thickness(0, 0, 0, 0),
                    Background = new SolidColorBrush(cn.Key),
                    ToolTip = new ToolTip
                    {
                        Content = cn.Value
                    }
                };

                Entities_PlayerIndicators.Children.Add(bord);
            }
        }

        public void Update(string Name, List<KeyValuePair<Color, string>> Viewers)
        {
            Entities_PlayerIndicators.Children.Clear();

            Entities_Name.Content = Name;

            foreach (var cn in Viewers)
            {
                var bord = new Border
                {
                    Width = 8,
                    Height = 12,
                    BorderThickness = new Thickness(0, 0, 0, 0),
                    Background = new SolidColorBrush(cn.Key),
                    ToolTip = new ToolTip
                    {
                        Content = cn.Value
                    }
                };

                Entities_PlayerIndicators.Children.Add(bord);
            }
        }

        public void Update(Bitmap Image)
        {
            Entities_PlayerIndicators.Children.Clear();

            Entities_Image.Source = Imaging.CreateBitmapSourceFromHBitmap(Image.GetHbitmap(), IntPtr.Zero,
                Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
    }
}