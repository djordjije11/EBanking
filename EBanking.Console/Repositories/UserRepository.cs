using EBanking.Console.DataAccessLayer;
using EBanking.Console.Model;
using EBanking.Console.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Console.Repositories
{
    internal class UserRepository : EntityRepository
    {
        public async Task<List<Account>> GetAccountsByUser(Account account, Connector connector)
        {
            var accounts = new List<Account>();
            connector.StartCommand();
            var command = connector.GetCommand();
            account.SetSelectAllByUserId(command);
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                accounts.Add((Account)account.GetEntityFromReader(reader));
            }
            await reader.CloseAsync();
            return accounts;
        }
    }
}
