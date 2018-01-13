using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MultiSelectComboBox
{
    /// <summary>
    ///     Interaction logic for MultiSelectComboBox.xaml
    /// </summary>
    public partial class MultiSelectComboBox : UserControl
    {
        private readonly ObservableCollection<Node> _nodeList;

        public MultiSelectComboBox()
        {
            InitializeComponent();
            _nodeList = new ObservableCollection<Node>();
        }

        #region Dependency Properties

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(Dictionary<string, object>), typeof(MultiSelectComboBox),
                new FrameworkPropertyMetadata(null,
                    OnItemsSourceChanged));

        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(Dictionary<string, object>),
                typeof(MultiSelectComboBox), new FrameworkPropertyMetadata(null,
                    OnSelectedItemsChanged));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MultiSelectComboBox),
                new UIPropertyMetadata(string.Empty));

        public static readonly DependencyProperty DefaultTextProperty =
            DependencyProperty.Register("DefaultText", typeof(string), typeof(MultiSelectComboBox),
                new UIPropertyMetadata(string.Empty));


        public Dictionary<string, object> ItemsSource
        {
            get => (Dictionary<string, object>) GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public Dictionary<string, object> SelectedItems
        {
            get => (Dictionary<string, object>) GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string DefaultText
        {
            get => (string) GetValue(DefaultTextProperty);
            set => SetValue(DefaultTextProperty, value);
        }

        #endregion

        #region Events

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (MultiSelectComboBox) d;
            control.DisplayInControl();
        }

        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (MultiSelectComboBox) d;
            control.SelectNodes();
            control.SetText();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var clickedBox = (CheckBox) sender;

            if (clickedBox.Content == "All")
            {
                if (clickedBox.IsChecked.Value)
                    foreach (var node in _nodeList)
                        node.IsSelected = true;
                else
                    foreach (var node in _nodeList)
                        node.IsSelected = false;
            }
            else
            {
                var _selectedCount = 0;
                foreach (var s in _nodeList)
                    if (s.IsSelected && s.Title != "All")
                        _selectedCount++;
                if (_selectedCount == _nodeList.Count - 1)
                    _nodeList.FirstOrDefault(i => i.Title == "All").IsSelected = true;
                else
                    _nodeList.FirstOrDefault(i => i.Title == "All").IsSelected = false;
            }
            SetSelectedItems();
            SetText();
        }

        #endregion


        #region Methods

        private void SelectNodes()
        {
            foreach (var keyValue in SelectedItems)
            {
                var node = _nodeList.FirstOrDefault(i => i.Title == keyValue.Key);
                if (node != null)
                    node.IsSelected = true;
            }
        }

        private void SetSelectedItems()
        {
            if (SelectedItems == null)
                SelectedItems = new Dictionary<string, object>();
            SelectedItems.Clear();
            foreach (var node in _nodeList)
                if (node.IsSelected && node.Title != "All")
                    if (ItemsSource.Count > 0)

                        SelectedItems.Add(node.Title, ItemsSource[node.Title]);
        }

        private void DisplayInControl()
        {
            _nodeList.Clear();
            if (ItemsSource.Count > 0)
                _nodeList.Add(new Node("All"));
            foreach (var keyValue in ItemsSource)
            {
                var node = new Node(keyValue.Key);
                _nodeList.Add(node);
            }
            MultiSelectCombo.ItemsSource = _nodeList;
        }

        private void SetText()
        {
            if (SelectedItems != null)
            {
                var displayText = new StringBuilder();
                foreach (var s in _nodeList)
                    if (s.IsSelected && s.Title == "All")
                    {
                        displayText = new StringBuilder();
                        displayText.Append("All");
                        break;
                    }
                    else if (s.IsSelected && s.Title != "All")
                    {
                        displayText.Append(s.Title);
                        displayText.Append(',');
                    }
                Text = displayText.ToString().TrimEnd(',');
            }
            // set DefaultText if nothing else selected
            if (string.IsNullOrEmpty(Text))
                Text = DefaultText;
        }

        #endregion
    }

    public class Node : INotifyPropertyChanged
    {
        private bool _isSelected;

        private string _title;

        #region ctor

        public Node(string title)
        {
            Title = title;
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Properties

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }

        #endregion
    }
}