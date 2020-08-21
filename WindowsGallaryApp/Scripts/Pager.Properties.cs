﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WindowsGallaryApp.Scripts
{
    public sealed partial class Pager : Control
    {
        public object PagerElementFactory
        {
            get { return (object)GetValue(PagerElementFactoryProperty); }
        }

        private static readonly DependencyProperty PagerElementFactoryProperty =
            DependencyProperty.Register("PagerElementFactory", typeof(object), typeof(Pager), new PropertyMetadata(new PagerElementFactory()));

        public PagerDisplayModes PagerDisplayMode
        {
            get { return (PagerDisplayModes)GetValue(PagerDisplayModeProperty); }
            set { SetValue(PagerDisplayModeProperty, value); }
        }
        public static readonly DependencyProperty PagerDisplayModeProperty =
            DependencyProperty.Register("PagerDisplayMode", typeof(PagerDisplayModes), typeof(Pager), new PropertyMetadata(PagerDisplayModes.Auto));

        /*  Universal Pager Properties
         * 
         */
        public PagerTemplateSettings TemplateSettings
        {
            get { return (PagerTemplateSettings)GetValue(TemplateSettingsProperty); }
        }
        private static readonly DependencyProperty TemplateSettingsProperty =
            DependencyProperty.Register("TemplateSettings", typeof(PagerTemplateSettings), typeof(Pager), new PropertyMetadata(default(PagerTemplateSettings)));

        public int NumberOfPages
        {
            get { return (int)GetValue(NumberOfPagesProperty); }
            set { SetValue(NumberOfPagesProperty, value); }
        }
        public static readonly DependencyProperty NumberOfPagesProperty =
            DependencyProperty.Register("NumberOfPages", typeof(int), typeof(Pager), new PropertyMetadata(5));

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(Pager), new PropertyMetadata(1, OnSelectedIndexChanged));

        /* NumberBox & ComboBox Text Properties
         * 
         */
        public string PrefixText
        {
            get { return (string)GetValue(PrefixTextProperty); }
            set { SetValue(PrefixTextProperty, value); }
        }
        public static readonly DependencyProperty PrefixTextProperty =
            DependencyProperty.Register("PrefixText", typeof(string), typeof(Pager), new PropertyMetadata("Page"));

        public string SuffixText
        {
            get { return (string)GetValue(SuffixTextProperty); }
            set { SetValue(SuffixTextProperty, value); }
        }
        public static readonly DependencyProperty SuffixTextProperty =
            DependencyProperty.Register("SuffixText", typeof(string), typeof(Pager), new PropertyMetadata("/"));

        /* Chevron Buttons Text Properties
         * 
         */
        public string FirstPageButtonText
        {
            get { return (string)GetValue(FirstPageButtonTextProperty); }
            set { SetValue(FirstPageButtonTextProperty, value); }
        }
        public static readonly DependencyProperty FirstPageButtonTextProperty =
            DependencyProperty.Register("FirstPageButtonText", typeof(string), typeof(Pager), new PropertyMetadata(""));
        public string PreviousPageButtonText
        {
            get { return (string)GetValue(PreviousPageButtonTextProperty); }
            set { SetValue(PreviousPageButtonTextProperty, value); }
        }
        public static readonly DependencyProperty PreviousPageButtonTextProperty =
            DependencyProperty.Register("PreviousPageButtonText", typeof(string), typeof(Pager), new PropertyMetadata(""));
        public string NextPageButtonText
        {
            get { return (string)GetValue(NextPageButtonTextProperty); }
            set { SetValue(NextPageButtonTextProperty, value); }
        }
        public static readonly DependencyProperty NextPageButtonTextProperty =
            DependencyProperty.Register("NextPageButtonText", typeof(string), typeof(Pager), new PropertyMetadata(""));
        public string LastPageButtonText
        {
            get { return (string)GetValue(LastPageButtonTextProperty); }
            set { SetValue(LastPageButtonTextProperty, value); }
        }
        public static readonly DependencyProperty LastPageButtonTextProperty =
            DependencyProperty.Register("LastPageButtonText", typeof(string), typeof(Pager), new PropertyMetadata(""));

        /* Chevron Buttons Visibility Properties
         * 
         */

        public ButtonVisibilityMode FirstPageButtonVisibility
        {
            get { return (ButtonVisibilityMode)GetValue(FirstPageButtonVisibilityProperty); }
            set { SetValue(FirstPageButtonVisibilityProperty, value); }
        }
        public static readonly DependencyProperty FirstPageButtonVisibilityProperty =
            DependencyProperty.Register("FirstPageButtonVisibility", typeof(ButtonVisibilityMode), typeof(Pager), new PropertyMetadata(ButtonVisibilityMode.Auto));

        public ButtonVisibilityMode PreviousPageButtonVisibility
        {
            get { return (ButtonVisibilityMode)GetValue(PreviousPageButtonVisibilityProperty); }
            set { SetValue(PreviousPageButtonVisibilityProperty, value); }
        }
        public static readonly DependencyProperty PreviousPageButtonVisibilityProperty =
            DependencyProperty.Register("PreviousPageButtonVisibility", typeof(ButtonVisibilityMode), typeof(Pager), new PropertyMetadata(ButtonVisibilityMode.Auto));

        public ButtonVisibilityMode NextPageButtonVisibility
        {
            get { return (ButtonVisibilityMode)GetValue(NextPageButtonVisibilityProperty); }
            set { SetValue(NextPageButtonVisibilityProperty, value); }
        }
        public static readonly DependencyProperty NextPageButtonVisibilityProperty =
            DependencyProperty.Register("NextPageButtonVisibility", typeof(ButtonVisibilityMode), typeof(Pager), new PropertyMetadata(ButtonVisibilityMode.Auto));

        public ButtonVisibilityMode LastPageButtonVisibility
        {
            get { return (ButtonVisibilityMode)GetValue(LastPageButtonVisibilityProperty); }
            set { SetValue(LastPageButtonVisibilityProperty, value); }
        }
        public static readonly DependencyProperty LastPageButtonVisibilityProperty =
            DependencyProperty.Register("LastPageButtonVisibility", typeof(ButtonVisibilityMode), typeof(Pager), new PropertyMetadata(ButtonVisibilityMode.Auto));

        /* NumberPanel Properties
         * 
         */
        public int NumberOfIndicesShowing
        {
            get { return (int)GetValue(NumberOfIndicesShowingProperty); }
            set { SetValue(NumberOfIndicesShowingProperty, value); }
        }
        public static readonly DependencyProperty NumberOfIndicesShowingProperty =
            DependencyProperty.Register("NumberOfIndicesShowing", typeof(int), typeof(Pager), new PropertyMetadata(5));

        public bool EllipsisEnabled
        {
            get { return (bool)GetValue(EllipsisEnabledProperty); }
            set { SetValue(EllipsisEnabledProperty, value); }
        }
        public static readonly DependencyProperty EllipsisEnabledProperty =
            DependencyProperty.Register("EllipsisEnabled", typeof(bool), typeof(Pager), new PropertyMetadata(true));

        public bool EllipsisShowFirstAndLast
        {
            get { return (bool)GetValue(EllipsisShowFirstAndLastProperty); }
            set { SetValue(EllipsisShowFirstAndLastProperty, value); }
        }
        public static readonly DependencyProperty EllipsisShowFirstAndLastProperty =
            DependencyProperty.Register("EllipsisShowFirstAndLast", typeof(bool), typeof(Pager), new PropertyMetadata(true));

        public int EllipsisMaxBefore
        {
            get { return (int)GetValue(EllipsisMaxBeforeProperty); }
            set { SetValue(EllipsisMaxBeforeProperty, value); }
        }
        public static readonly DependencyProperty EllipsisMaxBeforeProperty =
            DependencyProperty.Register("EllipsisMaxBefore", typeof(int), typeof(Pager), new PropertyMetadata(0));

        public int EllipsisMaxAfter
        {
            get { return (int)GetValue(EllipsisMaxAfterProperty); }
            set { SetValue(EllipsisMaxAfterProperty, value); }
        }
        public static readonly DependencyProperty EllipsisMaxAfterProperty =
            DependencyProperty.Register("EllipsisMaxAfter", typeof(int), typeof(Pager), new PropertyMetadata(0));
    }
}
