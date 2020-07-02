﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsGallaryApp.Samples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ItemsRepeaterSamplePage : Page
    {
        private List<CustomImage> imageList = CustomImage.GetImages(120);
        private int imagesPerPage = 12;

        public ItemsRepeaterSamplePage()
        {
            this.InitializeComponent();
            ImageRepeater.ItemsSource = new ObservableCollection<CustomImage>();
            UpdateGrid();
        }

        private void UpdateGrid(int pageIndex = 0)
        {
            List<CustomImage> result = GetPageImages(pageIndex);

            ((ObservableCollection<CustomImage>)ImageRepeater.ItemsSource)?.Clear();
            
            foreach(CustomImage c in result)
            {
                ((ObservableCollection<CustomImage>)ImageRepeater.ItemsSource)?.Add(c);
            }
        }

        private List<CustomImage> GetPageImages(int pageIndex)
        {
            return imageList.GetRange(pageIndex * imagesPerPage, imagesPerPage);
        }

        private void Pager_IndexChanged(object sender, PagerRoutedEventArgs args)
        {
            UpdateGrid(args.NewIndex);
        }
    }
}