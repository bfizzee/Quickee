using Quickee.ViewModels;
using System.Reflection;
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
            Version version = Assembly.GetExecutingAssembly().GetName().Version ?? new Version(0, 0, 0);

            TitleTextBlock.Text += $" v{version.Major}.{version.Minor}.{version.Build}";

            if (DataContext == null)
                return;

            ((MainViewModel)DataContext).Window = this;
        }
    }
}