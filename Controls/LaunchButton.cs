using Quickee.Models;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Quickee.Controls
{
    internal class LaunchButton : Button
    {
        public LaunchButton(string name, string launchPath, string launchArgs="", string iconPath="")
        {
            Grid grid = new Grid();

            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            grid.Children.Add(new Label() { Content = name });
            grid.Children.Add(new Image() { Source= new BitmapImage(new Uri(iconPath, UriKind.RelativeOrAbsolute)) });

            Content = grid;
            Command = new RelayCommand(() => Process.Start(launchPath, launchArgs));

        }
    }
}
