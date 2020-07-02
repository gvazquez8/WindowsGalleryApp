using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace WindowsGallaryApp.Samples
{
    public sealed partial class GridViewSamplePage : Page
    {
        private List<CustomImage> imageList = CustomImage.GetImages(120);
        private int imagesPerPage = 12;

        public GridViewSamplePage()
        {
            this.InitializeComponent();
            imagesCVS.Source = new ObservableCollection<CustomImage>();
            UpdateGrid();
        }

        private void UpdateGrid(int pageIndex = 0)
        {
            List<CustomImage> images = GetPageImages(pageIndex);
            ((ObservableCollection<CustomImage>)imagesCVS.Source)?.Clear();
            foreach (CustomImage c in images)
            {
                ((ObservableCollection<CustomImage>)imagesCVS.Source).Add(c);
            };
        }

        private List<CustomImage> GetPageImages(int pageIndex)
        {
            return imageList.GetRange(pageIndex * imagesPerPage, imagesPerPage);
        }

        private void GridPager_IndexChanged(object sender, PagerRoutedEventArgs args)
        {
            UpdateGrid(args.NewIndex);
        }
    }
}
