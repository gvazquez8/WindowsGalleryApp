
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using WindowsGallaryApp.Scripts;


namespace WindowsGallaryApp.Scripts
{
    public sealed partial class Pager : Control
    {
        public enum DisplayModes { NumberBox, ComboBox, ButtonPanel }

        private Button      _NB_ForwardPageButton, _NB_PreviousPageButton,
                            _CB_ForwardPageButton, _CB_PreviousPageButton,
                            _BP_ForwardPageButton, _BP_PreviousPageButton;
        private NumberBox   _PageNumberBox;
        private ComboBox    _PageComboBox;
        private Windows.UI.Xaml.Controls.ScrollViewer _ButtonPanelView;
        private ItemsRepeater _ButtonPanelItems;
        private double _ButtonPanel_ButtonWidth;

        public event EventHandler<PagerRoutedEventArgs> IndexChanged;
        public Pager()
        {
            this.DefaultStyleKey = typeof(Pager);
            this.Loaded += (s, args) => { PagerTemplateSettings = new PagerTemplateSettings(this); };
        }

        public void ApplyNumberBoxTemplate() {
            _NB_ForwardPageButton = GetTemplateChild<Button>("NB_ForwardPageButton");
            _NB_PreviousPageButton = GetTemplateChild<Button>("NB_PreviousPageButton");
            _PageNumberBox = GetTemplateChild<NumberBox>("PageNumberBox");

            _NB_ForwardPageButton.Click += OnForwardButton_Click;
            _NB_PreviousPageButton.Click += OnPreviousButton_Click;
            _PageNumberBox.ValueChanged += OnNumberBoxValueChanged;

            _PageNumberBox.ValueChanged += (s, args) => IndexChanged?.Invoke(s, new PagerRoutedEventArgs(CurrentIndex+1));
        }

        public void ApplyComboBoxTemplate()
        {
            _CB_ForwardPageButton = GetTemplateChild<Button>("CB_ForwardPageButton");
            _CB_PreviousPageButton = GetTemplateChild<Button>("CB_PreviousPageButton");
            _PageComboBox = GetTemplateChild<ComboBox>("PageComboBox");

            _CB_ForwardPageButton.Click += OnForwardButton_Click;
            _CB_PreviousPageButton.Click += OnPreviousButton_Click;
            _PageComboBox.SelectionChanged += OnComboBoxSelectionChanged;

            _PageComboBox.SelectionChanged += (s, args) => IndexChanged?.Invoke(s, new PagerRoutedEventArgs(CurrentIndex+1));

        }

        private void ApplyButtonPanelTemplate()
        {
            _BP_ForwardPageButton = GetTemplateChild<Button>("BP_ForwardPageButton");
            _BP_PreviousPageButton = GetTemplateChild<Button>("BP_PreviousPageButton");
            _ButtonPanelView = GetTemplateChild<Windows.UI.Xaml.Controls.ScrollViewer>("ButtonPanelViewer");
            _ButtonPanelItems = GetTemplateChild<ItemsRepeater>("ButtonPanelItemsRepeater");

            _BP_ForwardPageButton.Click += OnForwardButton_Click;
            _BP_PreviousPageButton.Click += OnPreviousButton_Click;

            _ButtonPanelItems.ElementPrepared += ButtonPanelFirstButtonPrepared;
            _ButtonPanelItems.ElementPrepared += ButtonPanelSetButtonEvents;
        }

        private void ButtonPanelSetButtonEvents(ItemsRepeater sender, ItemsRepeaterElementPreparedEventArgs args)
        {
            ((Button)args.Element).Click += OnButtonPanelButtonClick;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ApplyNumberBoxTemplate();
            ApplyComboBoxTemplate();
            ApplyButtonPanelTemplate();

            switch (DisplayMode)
            {
                case DisplayModes.NumberBox:
                    _PageNumberBox.IsEnabled = true;
                    GetTemplateChild<StackPanel>("NumberBoxDisplayPanel").Visibility = Visibility.Visible;
                    break;
                case DisplayModes.ComboBox:
                    _PageComboBox.IsEnabled = true;
                    GetTemplateChild<StackPanel>("ComboBoxDisplayPanel").Visibility = Visibility.Visible;
                    break;
                case DisplayModes.ButtonPanel:
                    GetTemplateChild<StackPanel>("ButtonPanelDisplay").Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void ButtonPanelFirstButtonPrepared(ItemsRepeater sender, ItemsRepeaterElementPreparedEventArgs args)
        {
            ((Button)args.Element).Loaded += AdjustButtonPanelView;
            sender.ElementPrepared -= ButtonPanelFirstButtonPrepared;
        }
    }

    public class PagerTemplateSettings : DependencyObject
    {
        public ObservableCollection<int> Indices { get; private set; }
        
        public PagerTemplateSettings(Pager pager)
        {
            List<int> tempList = new List<int>(Enumerable.Range(1, pager.Maximum));
            Indices = new ObservableCollection<int>(tempList);
        }
    }
}
