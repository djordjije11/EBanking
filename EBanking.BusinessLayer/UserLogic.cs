using EBanking.BusinessLayer.Interfaces;
using EBanking.DataAccessLayer.Interfaces;
using EBanking.Models;

namespace EBanking.BusinessLayer
{
    public class UserLogic : IUserLogic
    {
        IBroker Broker { get; }
        public UserLogic(IBroker broker)
        {
            Broker = broker;
        }
        public async Task<List<Account>> GetAccountsByUser(int userId)
        {
            try
            {
                await Broker.StartConnectionAsync();
                var user = await Broker.GetUserByIdAsync(new User() { Id = userId });
                if (user == null)
                    throw new Exception($"Корисник са идентификатором: '{userId}' није пронађен.");
                var accounts = await Broker.GetAccountsByUserAsync(user);
                return accounts;
            }
            finally
            {
                await Broker.EndConnectionAsync();
            }
        }
    }
}
