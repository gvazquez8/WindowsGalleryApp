﻿
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using WindowsGallaryApp.Scripts;


namespace WindowsGallaryApp.Scripts
{
    public sealed partial class Pager : Control
    {
        public enum PagerDisplayModes { Auto, ComboBox, NumberBox, NumberPanel, }
        public enum ButtonVisibilityMode { Auto, AlwaysVisible, HiddenOnEdge, None, }

        private Button FirstPageButton, PreviousPageButton, NextPageButton, LastPageButton;
        private ComboBox PagerComboBox;
        private NumberBox PagerNumberBox;
        private ItemsRepeater PagerNumberPanel;
        private Rectangle NumberPanelCurrentPageIdentifier;
        private ObservableCollection<object> PagerNumberPanelItems = new ObservableCollection<object>();

        private IconElement LeftEllipse = new SymbolIcon(Symbol.More);
        private IconElement RightEllipse = new SymbolIcon(Symbol.More);

        private static string NumberBoxVisibleVisualState = "NumberBoxVisible";
        private static string ComboBoxVisibleVisualState = "ComboBoxVisible";
        private static string NumberPanelVisibleVisualState = "NumberPanelVisible";

        private static string FirstPageButtonVisibleVisualState = "FirstPageButtonVisible";
        private static string FirstPageButtonNotVisibleVisualState = "FirstPageButtonCollapsed";
        private static string FirstPageButtonEnabledVisualState = "FirstPageButtonEnabled";
        private static string FirstPageButtonDisabledVisualState = "FirstPageButtonDisabled";

        private static string PreviousPageButtonVisibleVisualState = "PreviousPageButtonVisible";
        private static string PreviousPageButtonNotVisibleVisualState = "PreviousPageButtonCollapsed";
        private static string PreviousPageButtonEnabledVisualState = "PreviousPageButtonEnabled";
        private static string PreviousPageButtonDisabledVisualState = "PreviousPageButtonDisabled";

        private static string NextPageButtonVisibleVisualState = "NextPageButtonVisible";
        private static string NextPageButtonNotVisibleVisualState = "NextPageButtonCollapsed";
        private static string NextPageButtonEnabledVisualState = "NextPageButtonEnabled";
        private static string NextPageButtonDisabledVisualState = "NextPageButtonDisabled";

        private static string LastPageButtonVisibleVisualState = "LastPageButtonVisible";
        private static string LastPageButtonNotVisibleVisualState = "LastPageButtonCollapsed";
        private static string LastPageButtonEnabledVisualState = "LastPageButtonEnabled";
        private static string LastPageButtonDisabledVisualState = "LastPageButtonDisabled";

        private int PreviousPageIndex = -1;

        public event TypedEventHandler<Pager, PageChangedEventArgs> PageChanged;

        public Pager()
        {
            this.DefaultStyleKey = typeof(Pager);
            this.Loaded += OnLoad;
        }

        private void OnLoad(object sender, RoutedEventArgs args)
        {
            // Attach Callbacks for property changes
            RegisterPropertyChangedCallback(NumberOfPagesProperty, (s, e) => { OnNumberOfPagesChanged(); });
            RegisterPropertyChangedCallback(SelectedIndexProperty, (s, e) => { OnSelectedIndexChanged(); });
            RegisterPropertyChangedCallback(PagerDisplayModeProperty, (s, e) => { OnPagerDisplayModeChanged(); });
            RegisterPropertyChangedCallback(FirstPageButtonVisibilityProperty, (s, e) => { OnFirstPageButtonVisibilityChanged(); });
            RegisterPropertyChangedCallback(PreviousPageButtonVisibilityProperty, (s, e) => { OnPreviousPageButtonVisibilityChanged(); });
            RegisterPropertyChangedCallback(NextPageButtonVisibilityProperty, (s, e) => { OnNextPageButtonVisibilityChanged(); });
            RegisterPropertyChangedCallback(LastPageButtonVisibilityProperty, (s, e) => { OnLastPageButtonVisibilityChanged(); });
        }
        protected override void OnApplyTemplate()
        {
            SetValue(TemplateSettingsProperty, new PagerTemplateSettings(this));
            // Grab UIElements for later
            FirstPageButton = GetTemplateChild("FirstPageButton") as Button;
            PreviousPageButton = GetTemplateChild("PreviousPageButton") as Button;
            NextPageButton = GetTemplateChild("NextPageButton") as Button;
            LastPageButton = GetTemplateChild("LastPageButton") as Button;
            PagerComboBox = GetTemplateChild("ComboBoxDisplay") as ComboBox;
            PagerNumberBox = GetTemplateChild("NumberBoxDisplay") as NumberBox;
            PagerNumberPanel = GetTemplateChild("NumberPanelItemsRepeater") as ItemsRepeater;
            NumberPanelCurrentPageIdentifier = GetTemplateChild("NumberPanelCurrentPageIdentifier") as Rectangle;

            // Attach click events
            if (FirstPageButton != null)
            {
                FirstPageButton.Click += (s, e) => { SelectedIndex = 1; };
            }
            if (PreviousPageButton != null)
            {
                PreviousPageButton.Click += (s, e) => { SelectedIndex -= 1; };
            }
            if (NextPageButton != null)
            {
                NextPageButton.Click += (s, e) => { SelectedIndex += 1; };
            }
            if (LastPageButton != null)
            {
                LastPageButton.Click += (s, e) => { SelectedIndex = NumberOfPages; };
            }
            if (PagerComboBox != null)
            {
                PagerComboBox.SelectedIndex = SelectedIndex - 1;
                PagerComboBox.SelectionChanged += (s, e) => { OnComboBoxSelectionChanged(); };
            }
            if (PagerNumberPanel != null)
            {
                InitializeNumberPanel();
                PagerNumberPanel.ElementPrepared += OnElementPrepared;
                PagerNumberPanel.ElementClearing += OnElementClearing;
            }

            OnPagerDisplayModeChanged();

            // This is for the initial page being loaded whatever page that might be.
            PageChanged?.Invoke(this, new PageChangedEventArgs(PreviousPageIndex, SelectedIndex - 1));
        }

        private void InitializeNumberPanel()
        {

            PagerNumberPanelItems?.Clear();
            PagerNumberPanel.ItemsSource = PagerNumberPanelItems;

            if (NumberOfPages <= 7)
            {
                foreach (var num in TemplateSettings.Pages.GetRange(0, NumberOfPages))
                {
                    PagerNumberPanelItems.Add(num);
                }
            }
            else
            {
                foreach (var num in TemplateSettings.Pages.GetRange(0, 5))
                {
                    PagerNumberPanelItems.Add(num);
                }
                PagerNumberPanelItems.Add(RightEllipse);
                PagerNumberPanelItems.Add(NumberOfPages);

            }
        }
    }

    public sealed class PagerTemplateSettings : DependencyObject
    {
        public List<object> Pages { get; set; }

        public PagerTemplateSettings(Pager pager)
        {
            Pages = new List<object>(Enumerable.Range(1, pager.NumberOfPages).Cast<object>());
        }
    }
}
