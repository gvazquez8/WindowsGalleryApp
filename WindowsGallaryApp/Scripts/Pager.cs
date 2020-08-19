
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

        private Button      _PreviousPageButton, _NextPageButton;
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

            _PageNumberBox = GetTemplateChild<NumberBox>("PageNumberBox");
            _PageNumberBox.ValueChanged += OnNumberBoxValueChanged;
            _PageNumberBox.ValueChanged += (s, args) => IndexChanged?.Invoke(s, new PagerRoutedEventArgs(CurrentIndex + 1));
        }

        public void ApplyComboBoxTemplate()
        {
            _PageComboBox = GetTemplateChild<ComboBox>("PageComboBox");
            _PageComboBox.SelectionChanged += OnComboBoxSelectionChanged;
        }

        private void ApplyButtonPanelTemplate()
        {
            _ButtonPanelView = GetTemplateChild<Windows.UI.Xaml.Controls.ScrollViewer>("ButtonPanelViewer");
            _ButtonPanelItems = GetTemplateChild<ItemsRepeater>("ButtonPanelItemsRepeater");

            _ButtonPanelItems.ElementPrepared += ButtonPanelFirstButtonPrepared;
            _ButtonPanelItems.ElementPrepared += ButtonPanelSetButtonEvents;
        }

        private void ButtonPanelSetButtonEvents(ItemsRepeater sender, ItemsRepeaterElementPreparedEventArgs args)
        {
            ((Button)args.Element).Click += OnButtonPanelButtonClick;
            ((Button)args.Element).GotFocus += OnButtonPanelButtonGotFocus;
            ((Button)args.Element).LostFocus += OnButtonPanelButtonLostFocus;
            ((Button)args.Element).Click += (s, e) => { IndexChanged?.Invoke(s, new PagerRoutedEventArgs(CurrentIndex + 1)); };
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _PreviousPageButton = GetTemplateChild<Button>("PreviousPageButton");
            _NextPageButton = GetTemplateChild<Button>("NextPageButton");
            _PreviousPageButton.Click += OnPreviousButton_Click;
            _NextPageButton.Click += OnNextButton_Click;

            ApplyNumberBoxTemplate();
            ApplyComboBoxTemplate();
            ApplyButtonPanelTemplate();

            switch (DisplayMode)
            {
                case DisplayModes.NumberBox:
                    _PageNumberBox.IsEnabled = true;
                    GetTemplateChild<StackPanel>("BoxPanels").Visibility = Visibility.Visible;
                    _PageNumberBox.Visibility = Visibility.Visible;
                    break;
                case DisplayModes.ComboBox:
                    _PageComboBox.IsEnabled = true;
                    _PageComboBox.SelectionChanged += (s, args) => IndexChanged?.Invoke(s, new PagerRoutedEventArgs(CurrentIndex + 1));
                    GetTemplateChild<StackPanel>("BoxPanels").Visibility = Visibility.Visible;
                    _PageComboBox.Visibility = Visibility.Visible;
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
            ((Button)args.Element).Loaded += SetButtonPanelView;
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
