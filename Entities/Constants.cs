namespace Entities
{
    public static class Constants
    {
        public enum ConfigType
        {
            App,
            Web
        }
        public static readonly string[] configFileTypes = { "App", "app", "Web", "web" };
        public const string fileExtention = "config";
    }
}
