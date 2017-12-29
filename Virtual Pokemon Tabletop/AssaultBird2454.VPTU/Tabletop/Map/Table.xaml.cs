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
using System.Windows.Interop;
using System.Windows.Media.Media3D;
using System.Threading;
using SharpGL;

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

        private void Table_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {

        }
    }
}
