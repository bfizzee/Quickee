using Quickee.Models;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace Quickee.Controls
{
    internal class LaunchButton : Button
    {
        public LaunchButton(string name, string launchPath, string launchArgs="", string iconPath="")
        {
            Grid grid = new Grid();

            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            ImageSource? iSource = null;

            if (!string.IsNullOrEmpty(iconPath))
                iSource = new BitmapImage(new Uri(iconPath, UriKind.RelativeOrAbsolute));
            else
            {
                Icon? icon = null;
                try
                {
                    icon = Icon.ExtractAssociatedIcon(launchPath);
                }
                catch
                {
                    // TODO (29 AUG 2024): We should default the icon here
                }
                if (icon != null)
                    iSource = Imaging.CreateBitmapSourceFromHIcon(
                        icon.Handle,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
            }

            grid.Children.Add(new Label() { Content = name });
            grid.Children.Add(new Image() { Source = iSource });

            Content = grid;
            Command = new RelayCommand(() => Process.Start(launchPath, launchArgs));
        }
    }
}
