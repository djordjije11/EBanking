using EBanking.Console.DataAccessLayer;
using EBanking.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Console.Repositories
{
    internal class AccountRepository : EntityRepository
    {
        public async Task<List<Transaction>> GetTransactionsByAccount(Transaction transaction, Connector connector)
        {
            var transactions = new List<Transaction>();
            connector.StartCommand();
            var command = connector.GetCommand();
            transaction.SetSelectAllByAccountId(command);
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                transactions.Add((Transaction)transaction.GetEntityFromReader(reader));
            }
            await reader.CloseAsync();
            return transactions;
        }
    }
}
