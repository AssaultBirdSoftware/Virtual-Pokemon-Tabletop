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
using System.Windows.Shapes;

namespace AssaultBird2454.VPTU.SaveEditor.UI
{
    /// <summary>
    /// Interaction logic for RAW_JSON.xaml
    /// </summary>
    public partial class RAW_JSON : Window
    {
        public RAW_JSON()
        {
            InitializeComponent();
        }

        public void Export<T>(T Obj)
        {
            Data_JSON.Text = Newtonsoft.Json.JsonConvert.SerializeObject(Obj, new Newtonsoft.Json.JsonSerializerSettings()
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Include
            });
        }

        public T Import<T>()
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Data_JSON.Text);
        }

        private void Submit_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
