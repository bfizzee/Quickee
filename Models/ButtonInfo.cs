namespace Quickee.Models
{

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
