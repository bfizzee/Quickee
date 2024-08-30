namespace Quickee.Models
{

    public class Configuration
    {
        public IDictionary<string, string> Config { get; set; }
        public IList<Tab> Tabs { get; set; }

        public Configuration()
        {
            Config = new Dictionary<string, string>();
            Tabs = new List<Tab>();
        }
    }

    public class Tab
    {
        public string Name { get; set; }
        public IList<ButtonInfo> Buttons { get; set; }

        public Tab() : this("") { }

        public Tab(string name)
        {
            Name = name;
            Buttons = new List<ButtonInfo>();
        }
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
