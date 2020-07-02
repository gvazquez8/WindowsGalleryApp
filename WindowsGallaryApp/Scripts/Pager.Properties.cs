using System;
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
        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }
        public static readonly DependencyProperty SpacingProperty =
            DependencyProperty.Register(nameof(Spacing), typeof(double), typeof(Pager), new PropertyMetadata(0.0));

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(nameof(Maximum), typeof(int), typeof(Pager), new PropertyMetadata(10));

        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(nameof(Minimum), typeof(int), typeof(Pager), new PropertyMetadata(1));

        public int SmallChange
        {
            get { return (int)GetValue(SmallChangeProperty); }
            set { SetValue(SmallChangeProperty, value); }
        }
        public static readonly DependencyProperty SmallChangeProperty =
            DependencyProperty.Register(nameof(SmallChange), typeof(int), typeof(Pager), new PropertyMetadata(1));

        public int LargeChange
        {
            get { return (int)GetValue(LargeChangeProperty); }
            set { SetValue(LargeChangeProperty, value); }
        }
        public static readonly DependencyProperty LargeChangeProperty =
            DependencyProperty.Register(nameof(LargeChange), typeof(int), typeof(Pager), new PropertyMetadata(5.0));

        public int CurrentIndex
        {
            get { return (int)GetValue(CurrentIndexProperty); }
            set { SetValue(CurrentIndexProperty, value); }
        }
        public static readonly DependencyProperty CurrentIndexProperty =
            DependencyProperty.Register("CurrentIndex", typeof(int), typeof(Pager), new PropertyMetadata(0));
        public string PagePrefixText
        {
            get { return (string)GetValue(PagePrefixTextProperty); }
            set { SetValue(PagePrefixTextProperty, value); }
        }
        public static readonly DependencyProperty PagePrefixTextProperty =
            DependencyProperty.Register("PagePrefixText", typeof(string), typeof(Pager), new PropertyMetadata("Page"));
        public PagerTemplateSettings PagerTemplateSettings
        {
            get { return (PagerTemplateSettings)GetValue(PagerTemplateSettingsProperty); }
            private set { SetValue(PagerTemplateSettingsProperty, value); }
        }
        public static readonly DependencyProperty PagerTemplateSettingsProperty =
            DependencyProperty.Register("PagerTemplateSettings", typeof(PagerTemplateSettings), typeof(Pager), new PropertyMetadata(default(PagerTemplateSettings)));
        public DisplayModes DisplayMode
        {
            get { return (DisplayModes)GetValue(DisplayModeProperty); }
            set { SetValue(DisplayModeProperty, value); }
        }
        public static readonly DependencyProperty DisplayModeProperty =
            DependencyProperty.Register("DisplayMode", typeof(DisplayModes), typeof(Pager), new PropertyMetadata(DisplayModes.NumberBox));

        public int NumberOfIndicesShowing
        {
            get { return (int)GetValue(NumberOfIndicesShowingProperty); }
            set { SetValue(NumberOfIndicesShowingProperty, value); }
        }
        public static readonly DependencyProperty NumberOfIndicesShowingProperty =
            DependencyProperty.Register("NumberOfIndicesShowing", typeof(int), typeof(Pager), new PropertyMetadata(0));
        public int EllipseMaxBefore
        {
            get { return (int)GetValue(EllipseMaxBeforeProperty); }
            set { SetValue(EllipseMaxBeforeProperty, value); }
        }
        public static readonly DependencyProperty EllipseMaxBeforeProperty =
            DependencyProperty.Register("EllipseMaxBefore", typeof(int), typeof(Pager), new PropertyMetadata(0));
        public int EllipseMaxAfter
        {
            get { return (int)GetValue(EllipseMaxAfterProperty); }
            set { SetValue(EllipseMaxAfterProperty, value); }
        }
        public static readonly DependencyProperty EllipseMaxAfterProperty =
            DependencyProperty.Register("EllipseMaxAfter", typeof(int), typeof(Pager), new PropertyMetadata(0));








    }
}
