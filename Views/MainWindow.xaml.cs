using Quickee.ViewModels;
using System.Windows;

namespace Quickee.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
            => Application.Current.Shutdown();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext == null)
                return;

            ((MainViewModel)DataContext).Window = this;
        }
    }
}