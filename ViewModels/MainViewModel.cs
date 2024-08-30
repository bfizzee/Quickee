using Quickee.Models;
using Quickee.Views;
using System.Windows;
using System.Windows.Input;

namespace Quickee.ViewModels
{
    class MainViewModel
    {
        public ICommand CloseWindowCommand { get; }

        private MainWindow _mainWindow;
        public MainWindow Window
        {
            get => _mainWindow;
            set
            {
                _mainWindow = value;
                Config = new ConfigViewModel(this);
            }
        }

        public ConfigViewModel Config { get; private set; }

        public static bool CloseAfterLaunch { get; set; } = true;

        public MainViewModel()
        {
            CloseWindowCommand = new RelayCommand(() => Application.Current.Shutdown());
        }
    }
}
