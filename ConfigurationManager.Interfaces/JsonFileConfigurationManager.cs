using EBanking.ConfigurationManager.Interfaces;
using Newtonsoft.Json;

namespace EBanking.ConfigurationManager
{
    public class JsonFileConfigurationManager : IConfigurationManager
    {
        public Dictionary<string, string> Config { get; set; } = new Dictionary<string, string>();

        public string GetConfigParam(string key)
        {
            if (Config.ContainsKey(key) == false)
                throw new Exception($"Missing ConfigParam with key '{key}'.");
            return Config[key];
        }

        public void Initialize(string filePath)
        {
            var configText = File.ReadAllText(filePath);

            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(configText);
            if (values == null)
            {
                Config = new Dictionary<string, string>();
            }
            else
            {
                Config = values;
            }
        }
    }
}