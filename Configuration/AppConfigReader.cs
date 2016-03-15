using TestEmailService.Interfaces;
using System.Configuration;
using TestEmailService.Settings;

namespace TestEmailService.Configuration
{
    public class AppConfigReader : IConfig
    {
        public string GetName()
        {
            return ConfigurationManager.AppSettings.Get(AppConfigKeys.Name);
        }

        public string GetPassword()
        {
            return ConfigurationManager.AppSettings.Get(AppConfigKeys.Password);
        }

        public string GetUserName()
        {
            return ConfigurationManager.AppSettings.Get(AppConfigKeys.Username);
        }

        public string GetTo()
        {
            return ConfigurationManager.AppSettings.Get(AppConfigKeys.To);
        }

        public string GetToUserName()
        {
            return ConfigurationManager.AppSettings.Get(AppConfigKeys.ToUsername);
        }
    }
}
