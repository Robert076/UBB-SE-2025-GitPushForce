using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using src.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace src.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ZodiacView : Page
    {
        public ZodiacView()
        {
            this.InitializeComponent();
        }

        private void OnUpdateAttributeClick(object sender, RoutedEventArgs e)
        {
            
            if (this.DataContext is ZodiacViewModel viewModel)
            {
                
                if (viewModel.UpdateCreditScoreBasedOnAttributeCommand.CanExecute(null))
                {
                    viewModel.UpdateCreditScoreBasedOnAttributeCommand.Execute(null);
                }
            }
        }
    }
}
