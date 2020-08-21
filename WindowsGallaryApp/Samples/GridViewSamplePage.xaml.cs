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
        private List<CustomImage> imageList = new List<CustomImage>();
        private int imagesPerPage = 48;

        public GridViewSamplePage()
        {
            this.InitializeComponent();
            imagesCVS.Source = new ObservableCollection<CustomImage>();
            Loaded += ListViewSamplePage_Loaded;
        }

        private async void ListViewSamplePage_Loaded(object sender, RoutedEventArgs e)
        {
            imageList = await CustomImage.GenerateImages();
            MyPager.NumberOfPages = (int)Math.Ceiling((double)imageList.Count / imagesPerPage);
            UpdateCollection();
        }

        private void UpdateCollection(int pageIndex = 0)
        {
            List<CustomImage> images = GetPageImages(pageIndex);
            ((ObservableCollection<CustomImage>)imagesCVS.Source)?.Clear();
            foreach (CustomImage c in images)
            {
                ((ObservableCollection<CustomImage>)imagesCVS.Source)?.Add(c);
            };
        }

        private List<CustomImage> GetPageImages(int pageIndex)
        {
            if ((pageIndex + 1) * imagesPerPage > imageList.Count)
            {
                return imageList.GetRange(pageIndex * imagesPerPage, imageList.Count - (pageIndex * imagesPerPage));
            }

            return imageList.GetRange(pageIndex * imagesPerPage, imagesPerPage);
        }

        private void Pager_PageChanged(Pager sender, PageChangedEventArgs args)
        {
            if (imageList.Count == 0)
            {
                return;
            }

            UpdateCollection(args.CurrentPage);
        }

        private void AddMoreButton_Click(object sender, RoutedEventArgs e)
        {
            imageList.AddRange(imageList);

            MyPager.NumberOfPages = (int)Math.Ceiling((double)imageList.Count / imagesPerPage);
        }
    }
}
