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
using WindowsGallaryApp.Scripts;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WindowsGallaryApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class MainPage : Page
    {
        private List<CustomImage> imageList;
        private int pageIndex;
        private int imagesPerPage = 12;

        public MainPage()
        {
            this.InitializeComponent();
            imageList = CustomImage.GetImages(120);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            pageIndex = 0;
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            ImagesCVS.Source = new ObservableCollection<CustomImage>(GetPageImages());
        }

        private List<CustomImage> GetPageImages()
        {
            return imageList.GetRange(pageIndex * imagesPerPage, imagesPerPage);
        }

        private void PagerBackButton_Click(object sender, RoutedEventArgs e)
        {
            pageIndex = Math.Clamp(--pageIndex, 0, 9);
            if (pageIndex == 0)
            {
                PagerBackButton.IsEnabled = false;
            }

            PagerForwardButton.IsEnabled = true;

            UpdateGrid();
        }

        private void PagerForwardButton_Click(object sender, RoutedEventArgs e)
        {
            pageIndex = Math.Clamp(++pageIndex, 0, 9);
            if (pageIndex == 9)
            {
                PagerForwardButton.IsEnabled = false;
            }
            PagerBackButton.IsEnabled = true;
            UpdateGrid();
        }
    }
}
