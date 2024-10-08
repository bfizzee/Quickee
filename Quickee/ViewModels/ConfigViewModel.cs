﻿using Quickee.Controls;
using Quickee.Models;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using YamlDotNet.Serialization;

namespace Quickee.ViewModels
{
    public static class KnownPaths
    {
        public static string Explorer => @"C:\Windows\explorer.exe";
    }

    class ConfigViewModel
    {
        private static readonly string _dataFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\bfizzee\\Quickee";

        private static readonly string _configFile = $"{_dataFolder}\\config.yaml";
        private static readonly string _iconsFolder = $"{_dataFolder}\\Icons";

        private Configuration _config;

        private readonly MainViewModel _mainViewModel;

        public ConfigViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            _config = InitDefaultConfig();

            LoadConfig();

            PopulateControls();
        }

        private Configuration InitDefaultConfig()
        {
            if (File.Exists(_configFile))
                return new Configuration();

            Configuration config = new Configuration()
            {
                Config = new Dictionary<string, string>(),
                Tabs = new List<Tab>()
                {
                    new Tab("Config")
                    {
                        Buttons = new List<ButtonInfo>()
                        {
                            new ButtonInfo("YAML", "explorer.exe", _configFile)
                        }
                    },
                    new Tab("Main")
                    {
                        Buttons = new List<ButtonInfo>()
                        {
                            new ButtonInfo("Notepad", "notepad.exe"),
                        },
                    }
                }
            };

            SaveConfig(config);

            return config;
        }

        private void LoadConfig()
        {
            var deserializer = new DeserializerBuilder().Build();
            var yaml = File.ReadAllText(_configFile);

            _config = deserializer.Deserialize<Configuration>(yaml);
        }

        public void SaveConfig(Configuration newConfig)
        {
            var serializer = new SerializerBuilder().Build();
            var yaml = serializer.Serialize(newConfig);

            Directory.CreateDirectory(_dataFolder);
            Directory.CreateDirectory(_iconsFolder);

            File.WriteAllText(_configFile, yaml);

            LoadConfig();
        }

        public void PopulateControls()
        {
            foreach (Tab tab in _config.Tabs)
            {
                TabItem tabItem = new TabItem()
                {
                    Header = tab.Name
                };

                int count = (int)Math.Ceiling(Math.Sqrt(tab.Buttons.Count));

                UniformGrid grid = new UniformGrid()
                {
                    Rows = count,
                    Columns = count
                };

                foreach(ButtonInfo button in tab.Buttons)
                    grid.Children.Add(new LaunchButton(button));

                tabItem.Content = grid;

                _mainViewModel.Window.MainTabs.Items.Add(tabItem);
            }
        }
    }
}
