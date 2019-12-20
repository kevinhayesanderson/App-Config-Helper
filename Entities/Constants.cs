namespace Entities
{
    public static class Constants
    {
        public enum ConfigType
        {
            App,
            Web
        }
        public static readonly string[] ConfigFileTypes = { "App", "app", "Web", "web" };
        public const string FileExtention = "config";
    }
}
