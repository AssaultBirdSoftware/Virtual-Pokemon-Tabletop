using System;
using System.Windows;

namespace AssaultBird2454.VPTU.SaveEditor.UI
{
    /// <summary>
    ///     Interaction logic for RAW_JSON.xaml
    /// </summary>
    public partial class RAW_JSON : Window
    {
        public RAW_JSON()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Gets JSON for object and displays it in the panel
        /// </summary>
        /// <typeparam name="T">The Type of object used for JSON Serializing</typeparam>
        /// <param name="Obj">The Object</param>
        public void Export<T>(T Obj)
        {
            Data_JSON.SelectAll();
            Data_JSON.Selection.Text = Newtonsoft.Json.JsonConvert.SerializeObject(Obj,
                new Newtonsoft.Json.JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Include
                });
        }

        public T Import<T>()
        {
            Data_JSON.SelectAll();
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Data_JSON.Selection.Text);
            }
            catch
            {
                MessageBox.Show("Invalid JSON!");
                return (T) Activator.CreateInstance(typeof(T), new object[] { });
            }
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

        private void Option_Overwrite_Unchecked(object sender, RoutedEventArgs e)
        {
        }

        private void Option_Overwrite_Checked(object sender, RoutedEventArgs e)
        {
        }
    }
}