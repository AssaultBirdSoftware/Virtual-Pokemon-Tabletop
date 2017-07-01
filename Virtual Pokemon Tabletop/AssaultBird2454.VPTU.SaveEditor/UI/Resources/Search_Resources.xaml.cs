using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AssaultBird2454.VPTU.SaveEditor.UI.Resources
{
    /// <summary>
    /// Interaction logic for Search_Resources.xaml
    /// </summary>
    public partial class Search_Resources : Window
    {
        private SaveManager.SaveManager Mgr;
        
        private Thread LoadPreviewThread;

        public Search_Resources(SaveManager.SaveManager _Mgr)
        {
            InitializeComponent();

            Mgr = _Mgr;

            LoadItems();
            /*
            LoadPreviewThread = new Thread(new ThreadStart(() => LoadItems()));
            LoadPreviewThread.SetApartmentState(ApartmentState.STA);
            LoadPreviewThread.IsBackground = true;
            LoadPreviewThread.Start();
            */
        }
        
        private void LoadItems()
        {
            Resource_List.Items.Clear();

            foreach (SaveManager.Resource_Data.Resources res in Mgr.SaveData.ImageResources)
            {
                Resource_List.Items.Add(res);
            }
        }

        private void ShowPreview(SaveManager.Resource_Data.Resources res)
        {
            ImageBrush imb = new ImageBrush(Mgr.LoadImage(res.Path));
            imb.Stretch = Stretch.Uniform;
            imb.TileMode = TileMode.None;

            SelectedName.Content = res.Name;
            Preview.Background = imb;
        }

        private void Resource_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowPreview((SaveManager.Resource_Data.Resources)Resource_List.SelectedItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
