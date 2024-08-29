using Fitz.Utilities;
using Quickee.Controls;
using Quickee.Models;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using YamlDotNet.Serialization;

namespace Quickee.ViewModels
{
    class ConfigViewModel
    {
        private static readonly string _dataFolder = UtilityPaths.LocalAppData;

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
                        Buttons = new List<Button>()
                        {
                            new Explorer("YAML", _configFile)
                        }
                    },
                    new Tab("Main")
                    {
                        Buttons = new List<Button>()
                        {
                            new Button("Notepad", "notepad.exe"),
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

                foreach(Button button in tab.Buttons)
                    grid.Children.Add(new LaunchButton(button.Name, button.LaunchPath, button.LaunchArgs, button.IconPath));

                tabItem.Content = grid;

                _mainViewModel.Window.MainTabs.Items.Add(tabItem);
            }
        }
    }

    class Configuration
    {
        public IDictionary<string, string> Config { get; set; }
        public IList<Tab> Tabs { get; set; }

        public Configuration()
        {
            Config = new Dictionary<string, string>();
            Tabs   = new List<Tab>();
        }
    }

    class Tab
    {
        public string Name { get; set; }
        public IList<Button> Buttons { get; set; }

        public Tab() : this("") { }

        public Tab(string name)
        {
            Name    = name;
            Buttons = new List<Button>();
        }
    }

    class Explorer : Button
    {
        public Explorer(string name, string filePath, string iconPath="")
            : base(name, "explorer.exe", filePath, iconPath) { }
    }

    class Button
    {
        public string Name { get; set; }
        public string LaunchPath { get; set; }
        public string LaunchArgs { get; set; }
        public string IconPath { get; set; }

        public Button() : this("", "", "", "") { }

        public Button(string name, string launchPath, string launchArgs="", string iconPath="")
        {
            Name       = name;
            LaunchPath = launchPath;
            LaunchArgs = launchArgs;
            IconPath   = iconPath;
        }
    }
}
