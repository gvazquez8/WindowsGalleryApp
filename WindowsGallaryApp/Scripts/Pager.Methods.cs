using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Automation.Provider;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

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
        private void OnNextButton_Click(object sender, RoutedEventArgs args)
        {

            if (DisplayMode == DisplayModes.ButtonPanel)
            {
                Button panelBtn = (Button)_ButtonPanelItems.TryGetElement(CurrentIndex + 1);
                ButtonAutomationPeer peer = new ButtonAutomationPeer(panelBtn);
                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();
                panelBtn.Focus(FocusState.Programmatic);
            } else
            {
                CurrentIndex += 1;
            }
        }

        private void OnPreviousButton_Click(object sender, RoutedEventArgs args)
        {

            if (DisplayMode == DisplayModes.ButtonPanel)
            {
                Button panelBtn = (Button)_ButtonPanelItems.TryGetElement(CurrentIndex - 1);
                ButtonAutomationPeer peer = new ButtonAutomationPeer(panelBtn);
                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();
                panelBtn.Focus(FocusState.Programmatic);
            } else
            {
                CurrentIndex -= 1;
            }

            
        }

        private void OnButtonPanelButtonClick(object sender, RoutedEventArgs e)
        {
            int direction = -1;
            int prevIndex = CurrentIndex;
            
            CurrentIndex = _ButtonPanelItems.GetElementIndex((UIElement)sender);

            if (CurrentIndex > prevIndex) {
                direction = 1;
            }

            TryDisablePageButtons();

            double middleOffset = CurrentIndex * _ButtonPanel_ButtonWidth * direction;
            _ButtonPanelView.ChangeView(middleOffset, 0, 1);
        }

        private void OnNumberBoxValueChanged(Microsoft.UI.Xaml.Controls.NumberBox sender, Microsoft.UI.Xaml.Controls.NumberBoxValueChangedEventArgs args)
        {
            TryDisablePageButtons();
        }

        private void OnComboBoxSelectionChanged(object sender, object arg)
        {
            TryDisablePageButtons();
        }

        private void TryDisablePageButtons()
        {
            _PreviousPageButton.IsEnabled = CurrentIndex + 1 != Minimum;
            _NextPageButton.IsEnabled = CurrentIndex + 1 != Maximum;
        }

        private void SetButtonPanelView(object sender, RoutedEventArgs args)
        {
            _ButtonPanel_ButtonWidth = ((Button)sender).ActualWidth + ((StackLayout)_ButtonPanelItems.Layout).Spacing;
            _ButtonPanelView.MaxWidth = EllipseMaxBefore * _ButtonPanel_ButtonWidth;
        }

        private void OnButtonPanelButtonGotFocus(object sender, RoutedEventArgs args)
        {
            ((Button)sender).Background = new SolidColorBrush(Colors.LightYellow);
        }

        private void OnButtonPanelButtonLostFocus(object sender, RoutedEventArgs args)
        {
            ((Button)sender).Background = new SolidColorBrush(Colors.White);
        }
    }
}
