using EBanking.Console.DataAccessLayer;
using EBanking.Console.Model;
using EBanking.Console.Models;
using EBanking.Console.Repositories;
using EBanking.Console.Validations.Exceptions;
using EBanking.Console.Validations.Interfaces;

namespace EBanking.Console.Brokers
{
    internal class AccountBroker : EntityBroker<Account>
    {
        private AccountRepository accountRepository;
        public AccountBroker() : base(new AccountRepository())
        {
            accountRepository = (AccountRepository)repository;
        }
        public AccountBroker(Connector connector) : base(new AccountRepository(), connector)
        {
            accountRepository = (AccountRepository)repository;
        }
        public AccountBroker(IValidator<Account> validator) : base(new AccountRepository(), validator)
        {
            accountRepository = (AccountRepository)repository;
        }
        public AccountBroker(Connector connector, IValidator<Account> validator) : base(new AccountRepository(), connector, validator)
        {
            accountRepository = (AccountRepository)repository;
        }
        protected override string GetNameForGetId()
        {
            return "рачуна";
        }
        protected override string GetClassNameForScreen()
        {
            return "рачун";
        }
        protected override string GetPluralClassNameForScreen()
        {
            return "рачуни";
        }
        protected override string[] GetColumnNames()
        {
            return new string[] { "ИД", "Стање", "Статус", "Број", "Корисник", "Валута" };
        }
        protected override Account GetNewEntityInstance(int id = -1)
        {
            return new Account() { Id = id };
        }
        public override async Task<Account> ConstructEntityFromInput(int? id)
        {
            User wantedUser = await(new UserBroker(connector).FindEntityFromInput());;
            Currency wantedCurrency = await(new CurrencyBroker(connector).FindEntityFromInput());
            System.Console.WriteLine("Унесите стање рачуна:");
            if (!Decimal.TryParse(System.Console.ReadLine() ?? "", out decimal balance)) throw new ValidationException("Стање на рачуну мора бити број.");
            System.Console.WriteLine($"Одаберите статус рачуна:\n{(int)Status.ACTIVE}. Активан {(int)Status.INACTIVE}. Неактиван");
            string statusOption = System.Console.ReadLine() ?? "";
            while (!statusOption.Trim().Equals("1") && !statusOption.Trim().Equals("2"))
            {
                System.Console.WriteLine("Непозната опција. Покушајте опет.");
                System.Console.Clear();
                System.Console.WriteLine($"Одаберите статус рачуна:\n{(int)Status.ACTIVE}. Активан {(int)Status.INACTIVE}. Неактиван");
                statusOption = System.Console.ReadLine() ?? "";
            }
            Status status = (Status)Int32.Parse(statusOption);
            System.Console.WriteLine("Унесите број рачуна:");
            string accountNumber = System.Console.ReadLine() ?? "";
            var newAccount = new Account()
            {
                Balance = balance,
                Status = status,
                Number = accountNumber,
                User = wantedUser,
                Currency = wantedCurrency
            };
            if (id.HasValue) newAccount.SetIdentificator(id.Value);
            ValidateEntity(newAccount);
            return newAccount;
        }
        public override async Task DeleteEntityFromInput()
        {
            try
            {
                await connector.StartConnection();
                Account account = await FindEntityFromInput();
                List<Transaction> transactions = await FindTransactionsFromAccount(account);
                bool isTransactionDeleted = false;
                await connector.StartTransaction();
                if (transactions.Count > 0)
                {
                    account.Status = Status.INACTIVE;
                    await repository.UpdateEntityById(account, connector);
                }
                else
                {
                    Account deletedAccount = (Account)(await repository.DeleteEntity(account, connector));
                    isTransactionDeleted = true;
                    System.Console.WriteLine($"Обрисан {GetClassNameForScreen()} објекат: '{deletedAccount.SinglePrint()}'. (притисните било који тастер за наставак)");
                }
                await connector.CommitTransaction();
                if(isTransactionDeleted == false)
                    throw new ValidationException("Не можете обрисати рачуне који су већ вршили трансакције. Рачун је деактивиран. (притисните било који тастер за наставак)");
            }
            catch
            {
                await connector.RollbackTransaction();
                throw;
            }
            finally
            {
                await connector.EndConnection();
            }
        }
        public async Task<List<Transaction>> FindTransactionsFromAccount(Account account)
        {
            Transaction transaction = new() { FromAccount = account, ToAccount = account };
            return await accountRepository.GetTransactionsByAccount(transaction, connector);
        }
        public async Task GetTransactionsFromAccountFromInput()
        {
            List<Transaction> transactions;
            try
            {
                await connector.StartConnection();
                Account account = await FindEntityFromInput();
                System.Console.WriteLine($"Тражени {GetClassNameForScreen()}: '{account.SinglePrint()}'. (притисните било који тастер за наставак)");
                transactions = await FindTransactionsFromAccount(account);
            }
            catch
            {
                throw;
            }
            finally
            {
                await connector.EndConnection();
            }
            new TransactionBroker().WriteEntitiesTable(transactions);
        }
    }
}