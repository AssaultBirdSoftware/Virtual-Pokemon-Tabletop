using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AssaultBird2454.VPTU.SaveEditor.UI.Resources
{
    /// <summary>
    ///     Interaction logic for Search_Resources.xaml
    /// </summary>
    public partial class Search_Resources : Window
    {
        private Thread LoadPreviewThread;
        private readonly SaveManager.SaveManager Mgr;

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

            foreach (var res in Mgr.SaveData.ImageResources)
                Resource_List.Items.Add(res);
        }

        private void ShowPreview(SaveManager.Resource_Data.Resources res)
        {
            var imb = new ImageBrush(Mgr.LoadImage(res.Path));
            imb.Stretch = Stretch.Uniform;
            imb.TileMode = TileMode.None;

            SelectedName.Content = res.Name;
            Preview.Background = imb;
        }

        private void Resource_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowPreview((SaveManager.Resource_Data.Resources) Resource_List.SelectedItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}