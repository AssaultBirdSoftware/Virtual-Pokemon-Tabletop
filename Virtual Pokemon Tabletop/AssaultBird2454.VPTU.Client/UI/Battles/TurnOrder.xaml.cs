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

namespace AssaultBird2454.VPTU.Client.UI
{
    /// <summary>
    /// Interaction logic for TurnOrder.xaml
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
            TurnOrder_DB todb = new TurnOrder_DB();
            todb.ImageBrush = new ImageBrush(IMG) { Stretch = Stretch.Uniform };
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
