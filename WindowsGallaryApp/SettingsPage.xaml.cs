using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WindowsGallaryApp.Scripts;
using WindowsGallaryApp.Samples;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsGallaryApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        private void NumberBoxRdBtn_Checked(object sender, RoutedEventArgs e)
        {
            SettingsPager.DisplayMode = Pager.DisplayModes.NumberBox;
        }

        private void ComboBoxRdBtn_Checked(object sender, RoutedEventArgs e)
        {
            SettingsPager.DisplayMode = Pager.DisplayModes.ComboBox;
        }

        private void ButtonPanelRdBtn_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
