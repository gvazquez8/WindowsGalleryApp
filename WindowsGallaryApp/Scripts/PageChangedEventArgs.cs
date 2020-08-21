using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace WindowsGallaryApp.Scripts
{
    public sealed class PageChangedEventArgs : EventArgs
    {
        public int CurrentPage { get; private set; }
        public int PreviousPage { get; private set; }

        public PageChangedEventArgs(int OldIndex, int NewIndex)
        {
            PreviousPage = OldIndex;
            CurrentPage = NewIndex;
        }
    }
}
