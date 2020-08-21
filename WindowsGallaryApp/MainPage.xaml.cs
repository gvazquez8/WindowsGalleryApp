using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WindowsGallaryApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            navBar.SelectedItem = navBar.MenuItems.OfType<Microsoft.UI.Xaml.Controls.NavigationViewItem>().First();
            navBar.IsPaneOpen = false;
            GridView.Icon = new BitmapIcon() { UriSource = new Uri("ms-appx:///Assets/NewFolder/GridView.png", UriKind.RelativeOrAbsolute), ShowAsMonochrome = false };
            ListView.Icon = new BitmapIcon() { UriSource = new Uri("ms-appx:///Assets/NewFolder/ListView.png", UriKind.RelativeOrAbsolute), ShowAsMonochrome = false };
            ItemsRepeater.Icon = new BitmapIcon() { UriSource = new Uri("ms-appx:///Assets/NewFolder/GridView.png", UriKind.RelativeOrAbsolute), ShowAsMonochrome = false };
            DataGrid.Icon = new BitmapIcon() { UriSource = new Uri("ms-appx:///Assets/NewFolder/ListView.png", UriKind.RelativeOrAbsolute), ShowAsMonochrome = false };
        }

        private void NavigationView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            var selectedItem = (Microsoft.UI.Xaml.Controls.NavigationViewItem)args.SelectedItem;
            if (selectedItem != null)
            {
                string selectedItemTag = ((string)selectedItem.Tag);
                string pageName = "WindowsGallaryApp.Samples." + selectedItemTag;
                Type pageType = Type.GetType(pageName);
                contentFrame.Navigate(pageType);
            }
        }
    }
}
