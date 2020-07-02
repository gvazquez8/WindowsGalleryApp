using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace WindowsGallaryApp.Scripts
{
    public sealed class PagerRoutedEventArgs : RoutedEventArgs
    {
        public int NewIndex { get; private set; }

        public PagerRoutedEventArgs(int NewIndex)
        {
            this.NewIndex = NewIndex - 1;
        }
    }
}
