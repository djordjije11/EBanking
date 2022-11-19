namespace EBanking.Models
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogWarning();
        void LogError();
    }

    public class TextLogger : ILogger
    {
        public void LogInfo(string message)
        {
            Console.WriteLine(message);
        }

        public void LogWarning()
        {

        }

        public void LogError()
        {

        }
    }

    public class UserManager
    {
        public UserManager(ILogger logger)
        {
            Logger = logger;
        }

        public ILogger Logger { get; }
        public string Username { get; private set;  }
        public string Password { get; private set; }
        public bool IsLoggedIn { get; private set; }

        public void LoginUser()
        {
            Username = Guid.NewGuid().ToString();
            Password = "123.";
            IsLoggedIn = true;

            Logger.LogInfo("User logged in");
        }

        public void LogoutUser()
        {
            Username = "";
            Password = "";
            IsLoggedIn = false;

            Logger.LogInfo("User logged out");
        }
    }
}
