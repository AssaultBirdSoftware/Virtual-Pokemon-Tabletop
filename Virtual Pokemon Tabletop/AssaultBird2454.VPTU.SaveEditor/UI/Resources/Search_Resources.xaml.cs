using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AssaultBird2454.VPTU.SaveEditor.UI.Resources
{
    /// <summary>
    ///     Interaction logic for Search_Resources.xaml
    /// </summary>
    public partial class Search_Resources : Window
    {
        private Thread SearchThread;

        public Search_Resources()
        {
            InitializeComponent();

            LoadItems();
        }

        private SaveManager.SaveManager Mgr => MainWindow.SaveManager;

        public string Selected_Resource { get; private set; }

        private void LoadItems()
        {
            try
            {
                SearchThread.Abort();
                SearchThread = null;
            }
            catch
            {
                /* Dont Care */
            }

            SearchThread = new Thread(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    Resource_List.Items.Clear();

                    foreach (var res in Mgr.SaveData.ImageResources)
                        if (res.Name.ToLower().Contains(Search.Text.ToLower()))
                            Resource_List.Items.Add(res);
                });
            });
            SearchThread.SetApartmentState(ApartmentState.STA);
            SearchThread.IsBackground = true;
            SearchThread.Start();
        }

        private void ShowPreview(SaveManager.Resource_Data.Resources res)
        {
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(Mgr.LoadImage(res.ID).GetHbitmap(), IntPtr.Zero,
                Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            Preview.Background = new ImageBrush(bitmapSource)
            {
                Stretch = Stretch.Uniform,
                TileMode = TileMode.None
            };
        }

        private void Resource_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowPreview((SaveManager.Resource_Data.Resources) Resource_List.SelectedItem);
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            Selected_Resource = ((SaveManager.Resource_Data.Resources) Resource_List.SelectedItems[0]).ID;
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadItems();
        }
    }
}