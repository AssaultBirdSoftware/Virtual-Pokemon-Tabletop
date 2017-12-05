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
using System.Windows.Interop;
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
        private SaveManager.SaveManager Mgr
        {
            get
            {
                return MainWindow.SaveManager;
            }
        }

        private Thread SearchThread;

        public string Selected_Resource { get; private set; }

        public Search_Resources()
        {
            InitializeComponent();

            LoadItems();

        }

        private void LoadItems()
        {
            try
            {
                SearchThread.Abort();
                SearchThread = null;
            }
            catch { /* Dont Care */ }

            SearchThread = new Thread(new ThreadStart(() =>
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    Resource_List.Items.Clear();

                    foreach (SaveManager.Resource_Data.Resources res in Mgr.SaveData.ImageResources)
                    {
                        if (res.Name.ToLower().Contains(Search.Text.ToLower()))
                        {
                            Resource_List.Items.Add(res);
                        }
                    }
                }));
            }));
            SearchThread.SetApartmentState(ApartmentState.STA);
            SearchThread.IsBackground = true;
            SearchThread.Start();
        }

        private void ShowPreview(SaveManager.Resource_Data.Resources res)
        {
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(Mgr.LoadImage(res.ID).GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            Preview.Background = new ImageBrush(bitmapSource)
            {
                Stretch = Stretch.Uniform,
                TileMode = TileMode.None
            };
        }

        private void Resource_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowPreview((SaveManager.Resource_Data.Resources)Resource_List.SelectedItem);
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            Selected_Resource = ((SaveManager.Resource_Data.Resources)Resource_List.SelectedItems[0]).ID;
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
