using Microsoft.Extensions.Configuration;

namespace Selenium.Specflow.Automation
{
    public static class Settings
    {
        private static IConfiguration Config;
        static readonly string rootPath = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\net6.0", "");
        static readonly string configPath = rootPath + "appsettings.json";

        static Settings()
        {
            Config = new ConfigurationBuilder().AddJsonFile
                (configPath, true, true)
                .Build();
        }

        public static string TempDownDirectory => Config["TempDownLoc"];

        public static string ClientUrl => Config["ClientUrl"];

        public static string Username => Config["Username"];

        public static string Password => Config["Password"];

    }
}
