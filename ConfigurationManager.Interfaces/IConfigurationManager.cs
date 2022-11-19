namespace EBanking.ConfigurationManager.Interfaces
{
    public interface IConfigurationManager
    {
        string GetConfigParam(string key);
        void Initialize(string filePath);
    }
}