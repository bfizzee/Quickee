using Quickee.Models;
using System.Windows;
using System.Windows.Input;

namespace Quickee.ViewModels
{
    class MainViewModel
    {
        public ICommand CloseWindowCommand { get; }

        public MainViewModel()
        {
            CloseWindowCommand = new RelayCommand(() => Application.Current.Shutdown());
        }
    }
}
