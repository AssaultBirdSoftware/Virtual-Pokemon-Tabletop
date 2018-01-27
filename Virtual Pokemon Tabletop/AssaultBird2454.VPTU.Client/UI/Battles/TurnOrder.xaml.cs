using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AssaultBird2454.VPTU.Client.UI
{
    /// <summary>
    ///     Interaction logic for TurnOrder.xaml
    /// </summary>
    public partial class TurnOrder : UserControl
    {
        public TurnOrder()
        {
            InitializeComponent();
        }

        public void Clear()
        {
        }

        public void Add(BitmapImage IMG, string Name, int Inititive)
        {
            var todb = new TurnOrder_DB();
            todb.ImageBrush = new ImageBrush(IMG) {Stretch = Stretch.Uniform};
            todb.Inititive = Inititive;
            todb.Name = Name;

            Turn_List.Items.Add(todb);
        }
    }

    public class TurnOrder_DB
    {
        public ImageBrush ImageBrush { get; set; }
        public string Name { get; set; }
        public int Inititive { get; set; }
    }
}