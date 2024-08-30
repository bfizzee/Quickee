using Quickee.Models;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Quickee.Controls
{
    internal class LaunchButton : Border
    {
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(LaunchButton), new PropertyMetadata(Brushes.Black));

        public Brush Foreground
        {
            get => (Brush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }

        private readonly ButtonInfo _buttonInfo;
        private readonly ICommand _command;

        public LaunchButton(ButtonInfo buttonInfo)
        {
            _buttonInfo = buttonInfo;

            _command = new RelayCommand(Launch, CanLaunch);

            Style = (Style)Application.Current.MainWindow.TryFindResource("DarkLaunchButton");
            Child = GetButton();
        }

        private Button GetButton()
        {
            Button button = new Button()
            {
                Command = _command,

                Content = new StackPanel
                {
                    Children =
                    {
                        new TextBlock
                        {
                            Text = _buttonInfo.Name,
                            Margin = new Thickness(5),
                            FontSize = 16,
                            Foreground = Foreground
                        },
                        GetIcon()
                    }
                },

                Background = Background
            };

            return button;
        }

        private Image GetIcon()
        {
            System.Drawing.Icon icon;

            if (!string.IsNullOrEmpty(_buttonInfo.IconPath))
                icon = new System.Drawing.Icon(_buttonInfo.IconPath);
            else
            {
                try { icon = System.Drawing.Icon.ExtractAssociatedIcon(_buttonInfo.IconPath) ?? System.Drawing.SystemIcons.Application; }
                catch { icon = System.Drawing.SystemIcons.Application; }
            }

            // Convert icon to bitmap source
            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            // Create image control and set source
            Image imageControl = new Image
            {
                Source = bitmapSource,
                Width = 32,
                Height = 32
            };

            return imageControl;
        }

        private void Launch()
            => Process.Start(_buttonInfo.LaunchPath, _buttonInfo.LaunchArgs);

        private bool CanLaunch()
            => true;
    }

    public class ButtonInfo
    {
        public string Name { get; set; }
        public string LaunchPath { get; set; }
        public string LaunchArgs { get; set; }
        public string IconPath { get; set; }

        public ButtonInfo() : this("", "", "", "") { }

        public ButtonInfo(string name, string launchPath, string launchArgs = "", string iconPath = "")
        {
            Name = name;
            LaunchPath = launchPath;
            LaunchArgs = launchArgs;
            IconPath = iconPath;
        }

        public override string ToString()
        {
            return $"Button {Name} | \"{LaunchPath}\" \"{LaunchArgs}\" | Icon: {IconPath}";
        }
    }
}
