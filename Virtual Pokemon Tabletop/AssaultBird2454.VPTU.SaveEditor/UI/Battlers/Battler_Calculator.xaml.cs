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

namespace AssaultBird2454.VPTU.SaveEditor.UI.Battlers
{
    public delegate void BattleInstance_Changed(BattleManager.Battle_Instance.Instance Instance);

    /// <summary>
    /// Interaction logic for Battler_Calculator.xaml
    /// </summary>
    public partial class Battler_Calculator : UserControl
    {
        public event BattleInstance_Changed BattleInstanceChanged_Event;

        #region Objects
        /// <summary>
        /// Battle Instance Used
        /// </summary>
        private BattleManager.Battle_Instance.Instance _Instance;
        /// <summary>
        /// Gets or Sets the Battle Instance that is used
        /// </summary>
        public BattleManager.Battle_Instance.Instance Instance
        {
            get
            {
                return _Instance;
            }
            set
            {
                _Instance = value;
                if (value != null)
                {
                    NoInstance_Error.Visibility = Visibility.Hidden;
                }
                else
                {
                    NoInstance_Error.Visibility = Visibility.Visible;
                }
                InstanceChanged();
                BattleInstanceChanged_Event?.Invoke(Instance);
            }
        }
        #endregion

        #region ContextMenus
        public ContextMenu Participants_ContextMenu;
        public MenuItem Participants_ContextMenu_AddParticipant;
        public MenuItem Participants_ContextMenu_EditParticipant;
        public MenuItem Participants_ContextMenu_RemoveParticipant;
        #endregion

        public Battler_Calculator()
        {
            InitializeComponent();
            
            Participants_ContextMenu = new ContextMenu();// Creates the context menu
            Participants_List.ContextMenu = Participants_ContextMenu;

            Participants_ContextMenu_AddParticipant = new MenuItem();
            Participants_ContextMenu_AddParticipant.Click += Participants_ContextMenu_AddParticipant_Click;
            Participants_ContextMenu_AddParticipant.Header = "Add Participant";

            Participants_ContextMenu_EditParticipant = new MenuItem();
            Participants_ContextMenu_EditParticipant.Click += Participants_ContextMenu_EditParticipant_Click;
            Participants_ContextMenu_EditParticipant.Header = "Edit Participant";

            Participants_ContextMenu_RemoveParticipant = new MenuItem();
            Participants_ContextMenu_RemoveParticipant.Click += Participants_ContextMenu_RemoveParticipant_Click;
            Participants_ContextMenu_RemoveParticipant.Header = "Remove Participant";

            Participants_ContextMenu.Items.Add(Participants_ContextMenu_AddParticipant);
            Participants_ContextMenu.Items.Add(Participants_ContextMenu_EditParticipant);
            Participants_ContextMenu.Items.Add(Participants_ContextMenu_RemoveParticipant);

        }

        private void Participants_ContextMenu_RemoveParticipant_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Participants_ContextMenu_EditParticipant_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Participants_ContextMenu_AddParticipant_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void InstanceChanged()
        {

        }

        private void NoInstance_Error_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Instance = new BattleManager.Battle_Instance.Instance();
            }
            else
            {
                return;
            }
        }


    }
}