using System.Configuration;

namespace UtilityResolution.Utility
{
    public class ConfigHelper
    {
        public static string GetAppConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static void SetAppConfig(string key, string value)
        {
           System.Configuration.Configuration   config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            System.IO.File.SetAttributes(config.FilePath, System.IO.FileAttributes.Normal);
            if (config.AppSettings.Settings[key] == null)
            {
                config.AppSettings.Settings.Add(key, value);
            }
            else
            {
                config.AppSettings.Settings[key].Value = value;
            }
            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }
    }

}
