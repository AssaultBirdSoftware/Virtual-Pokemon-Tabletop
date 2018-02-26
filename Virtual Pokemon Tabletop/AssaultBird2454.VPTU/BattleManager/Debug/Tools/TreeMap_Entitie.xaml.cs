using AssaultBird2454.VPTU.BattleManager.Debug.Tools.TreeMap_Componants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace AssaultBird2454.VPTU.BattleManager.Debug.Tools
{
    /// <summary>
    /// Interaction logic for TreeMap_Entitie.xaml
    /// </summary>
    public partial class TreeMap_Entitie : UserControl
    {
        public TreeMap_Entitie()
        {
            InitializeComponent();
        }

        public EntitiesManager.Pokemon.PokemonCharacter PC;
        public EntitiesManager.Trainer.TrainerCharacter TC;
        string EType = "";

        private void Load_Type<T>(T obj, TreeViewItem item = null)
        {
            if (item == null)
            {
                foreach (var prop in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    if (prop.PropertyType == typeof(bool))
                    {
                        Display_Tree.Items.Add(new TreeMap_Componant_Bool() { });
                    }
                    else if (prop.PropertyType == typeof(string))
                    {
                        Display_Tree.Items.Add(new TreeMap_Componant_String() { Label = prop.Name, Value = (string)prop.GetValue(obj, null) });
                    }
                    else if (prop.PropertyType == typeof(int))
                    {
                        Display_Tree.Items.Add(new TreeMap_Componant_Number() { Label = prop.Name, Value = (int)prop.GetValue(obj, null) });
                    }
                    else if (prop.PropertyType == typeof(decimal))
                    {
                        Display_Tree.Items.Add(new TreeMap_Componant_Number() { Label = prop.Name, Value = (decimal)prop.GetValue(obj, null) });
                    }
                    else
                    {
                        TreeViewItem TVI = new TreeViewItem();
                        Display_Tree.Items.Add(TVI);

                        Load_Type(prop.GetValue(obj), TVI);
                    }
                }
            }
            else
            {
                foreach (var prop in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    if (prop.PropertyType == typeof(bool))
                    {
                        item.Items.Add(new TreeMap_Componant_Bool() { });
                    }
                    else if (prop.PropertyType == typeof(string))
                    {
                        item.Items.Add(new TreeMap_Componant_String() { Label = prop.Name, Value = (string)prop.GetValue(obj, null) });
                    }
                    else if (prop.PropertyType == typeof(int))
                    {
                        item.Items.Add(new TreeMap_Componant_Number() { Label = prop.Name, Value = (int)prop.GetValue(obj, null) });
                    }
                    else if (prop.PropertyType == typeof(decimal))
                    {
                        item.Items.Add(new TreeMap_Componant_Number() { Label = prop.Name, Value = (decimal)prop.GetValue(obj, null) });
                    }
                    else
                    {
                        TreeViewItem TVI = new TreeViewItem();
                        item.Items.Add(TVI);

                        Load_Type(prop.GetValue(obj), TVI);
                    }
                }
                //Console.WriteLine("Name: {0}, Value: {1}", prop.Name, prop.GetValue(myVar, null));
            }
        }

        public void Load(EntitiesManager.Pokemon.PokemonCharacter Pokemon, TreeViewItem item = null)
        {
            #region Structure

            #endregion
            #region Load

            #endregion
        }
        public void Load(EntitiesManager.Trainer.TrainerCharacter Trainer, TreeViewItem item = null)
        {
            #region Structure

            #endregion
            #region Load

            #endregion
        }

        private void Import_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Type_Selection_Pokemon.IsChecked == true)
            {
                TC = null;
                PC = (Newtonsoft.Json.JsonConvert.DeserializeObject<EntitiesManager.Pokemon.PokemonCharacter>(Data_Text.Text));
                EType = "PC";
                Load(PC);
            }
            else if (Type_Selection_Trainer.IsChecked == true)
            {
                PC = null;
                TC = (Newtonsoft.Json.JsonConvert.DeserializeObject<EntitiesManager.Trainer.TrainerCharacter>(Data_Text.Text));
                EType = "TC";
                Load(TC);
            }
        }
    }
}
