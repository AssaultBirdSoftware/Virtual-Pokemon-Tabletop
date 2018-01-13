using System;
using System.Collections.Generic;
using System.Windows.Controls;
using AssaultBird2454.VPTU.BattleManager.Data;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect.Data.Actions.UI
{
    /// <summary>
    ///     Interaction logic for HasStatus.xaml
    /// </summary>
    public partial class HasStatus : UserControl, EffectAction_Designer
    {
        public HasStatus()
        {
            InitializeComponent();

            var itemSource = new Dictionary<string, object>();

            foreach (Status_Afflictions effect in Enum.GetValues(typeof(Status_Afflictions)))
                itemSource.Add(effect.ToString(), effect);

            Effects.ItemsSource = itemSource;

            //FunctionName.Items.Add();
        }

        public void Load(object Data)
        {
            var HasStatusData = (Actions.HasStatus) Data;

            HasAll.IsChecked = HasStatusData.HasAll;
            FunctionName.Text = HasStatusData.FunctionName;

            var selecteditems = new Dictionary<string, object>();
            foreach (var effect in Effects.ItemsSource)
                if (HasStatusData.CheckedStatus.Contains((Status_Afflictions) effect.Value))
                    selecteditems.Add(effect.Key, effect.Value);
            Effects.SelectedItems = selecteditems;
        }

        public void Save(object Data)
        {
            var HasStatusData = (Actions.HasStatus) Data;

            HasStatusData.HasAll = (bool) HasAll.IsChecked;
            HasStatusData.FunctionName = FunctionName.Text;

            var status = new List<Status_Afflictions>();
            foreach (var sel in Effects.SelectedItems)
                status.Add((Status_Afflictions) sel.Value);
            HasStatusData.CheckedStatus = status;
        }
    }
}