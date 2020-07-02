using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WindowsGallaryApp.Scripts
{
    public sealed partial class Pager : Control
    {
        T GetTemplateChild<T>(string name) where T : DependencyObject
        {
            T templateChild = GetTemplateChild(name) as T;
            if (templateChild == null)
            {
                throw new NullReferenceException(name);
            }
            return templateChild;
        }
        private void OnForwardButton_Click(object sender, RoutedEventArgs args)
        {
            CurrentIndex += 1;
        }

        private void OnPreviousButton_Click(object sender, RoutedEventArgs args)
        {
            CurrentIndex -= 1;
        }

        private void OnNumberBoxValueChanged(Microsoft.UI.Xaml.Controls.NumberBox sender, Microsoft.UI.Xaml.Controls.NumberBoxValueChangedEventArgs args)
        {
            _NB_PreviousPageButton.IsEnabled = CurrentIndex != Minimum;
            _NB_ForwardPageButton.IsEnabled  = CurrentIndex != Maximum;
        }

        private void OnComboBoxSelectionChanged(object sender, object arg)
        {
            _CB_PreviousPageButton.IsEnabled = CurrentIndex+1 != Minimum;
            _CB_ForwardPageButton.IsEnabled  = CurrentIndex+1 != Maximum;
        }

        private void OnButtonPanelButtonClick(object sender, RoutedEventArgs e)
        {
            CurrentIndex = _ButtonPanelItems.GetElementIndex((UIElement)sender);
            double middleOffset = (CurrentIndex*_ButtonPanel_ButtonWidth); 
            _ButtonPanelView.ChangeView(middleOffset, 0,1);
        }
        private void AdjustButtonPanelView(object sender, RoutedEventArgs args)
        {
            _ButtonPanel_ButtonWidth = ((Button)sender).ActualWidth + ((StackLayout)_ButtonPanelItems.Layout).Spacing;
            _ButtonPanelView.MaxWidth = EllipseMaxBefore * _ButtonPanel_ButtonWidth;
        }
    }
}
