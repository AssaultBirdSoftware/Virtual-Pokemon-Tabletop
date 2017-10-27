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

namespace AssaultBird2454.VPTU.Tabletop.Token
{
    /// <summary>
    /// Interaction logic for Pokemon_Token.xaml
    /// </summary>
    public partial class Pokemon_Token : UserControl
    {
        public Pokemon_Token()
        {
            InitializeComponent();
        }

        public void ChangeToken_BG(string File)
        {
            Bitmap bmap = new Bitmap(File);
            Pokemon_Icon.Fill = CreateBrushFromBitmap(bmap);
        }

        public void ChangeToken_Ring()
        {

        }

        public static System.Windows.Media.Brush CreateBrushFromBitmap(Bitmap bmp)
        {
            return new ImageBrush(Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions()));
        }
    }
}
