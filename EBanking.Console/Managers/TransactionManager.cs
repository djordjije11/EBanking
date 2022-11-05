using EBanking.Console.DataAccessLayer;
using EBanking.Console.Models;
using EBanking.Console.Validations.Exceptions;
using EBanking.Console.Validations.Interfaces;

namespace EBanking.Console.Managers
{
    internal class TransactionManager : EntityManager<Transaction>
    {
        public TransactionManager(IValidator<Transaction> validator) : base(validator)
        {
        }
        public override async Task<Transaction> ConstructEntityFromInput(int? id)
        {
            System.Console.WriteLine("Унесите тражене податке за рачун даваоца.");
            int fromAccountId = new AccountManager(connector).GetIdFromInput();
            System.Console.WriteLine("Унесите тражене податке за рачун примаоца.");
            int toAccountId = new AccountManager(connector).GetIdFromInput();
            Account? fromAccount;
            Account? toAccount;
            fromAccount = (Account?)(await SqlRepository.GetEntityById(GetNewEntityInstance(fromAccountId), connector));
            if (fromAccount == null) throw new ValidationException($"У бази не постоји {GetClassNameForScreen()} са унетим ид бројем.");
            toAccount = (Account?)(await SqlRepository.GetEntityById(GetNewEntityInstance(toAccountId), connector));
            if (toAccount == null) throw new ValidationException($"У бази не постоји {GetClassNameForScreen()} са унетим ид бројем.");
            System.Console.WriteLine("Унесите износ новца која је пренета.");
            if (!Decimal.TryParse(System.Console.ReadLine() ?? "", out decimal amount) || amount <= 0)
                throw new ValidationException("Износ новца мора бити позитиван број.");
            System.Console.WriteLine("Унесите датум трансакције.");
            //UKOLIKO ZELIMO DA SE DATUM RACUNA KAO TRENUTNI
            DateTime dateTime = DateTime.Now;
            //UKOLIKO ZELIMO DA SE DATUM UNOSI
            /*
            DateTime dateTime;
            while (true)
            {
                System.Console.WriteLine("Унесите број године: ");
                string yearInput = System.Console.ReadLine() ?? "";
                if (!Int32.TryParse(yearInput, out int year))
                {
                    System.Console.WriteLine("Година неадекватно унета. Покушајте опет.");
                    continue;
                }
                System.Console.WriteLine("Унесите број месеца (Јануар - 1, ..., Децембар - 12): ");
                string monthInput = System.Console.ReadLine() ?? "";
                if (!Int32.TryParse(monthInput, out int month) || month < 1 || month > 12)
                {
                    System.Console.WriteLine("Месец неадекватно унет. Покушајте опет.");
                    continue;
                }
                System.Console.WriteLine("Унесите број тог дана у датуму: ");
                string dayInput = System.Console.ReadLine() ?? "";
                if (!Int32.TryParse(dayInput, out int day) || day < 1 || day > 31)
                {
                    System.Console.WriteLine("Дан неадекватно унет. Покушајте опет.");
                    continue;
                }
                System.Console.WriteLine("Унесите број сата: ");
                string hourInput = System.Console.ReadLine() ?? "";
                if (!Int32.TryParse(hourInput, out int hour) || hour < 0 || hour > 23)
                {
                    System.Console.WriteLine("Сат неадекватно унет. Покушајте опет.");
                    continue;
                }
                System.Console.WriteLine("Унесите број минута: ");
                string minuteInput = System.Console.ReadLine() ?? "";
                if (!Int32.TryParse(minuteInput, out int minute) || minute < 0 || minute > 59)
                {
                    System.Console.WriteLine("Минут неадекватно унет. Покушајте опет.");
                    continue;
                }
                System.Console.WriteLine("Унесите број секунде: ");
                string secondInput = System.Console.ReadLine() ?? "";
                if (!Int32.TryParse(secondInput, out int second) || second < 0 || second > 59)
                {
                    System.Console.WriteLine("Секунда неадекватно унет. Покушајте опет.");
                    continue;
                }
                dateTime = new DateTime(year, month, day, hour, minute, second);
                break;
            }
            */
            Transaction newTransaction = new Transaction()
            {
                Amount = amount,
                Date = dateTime,
                FromAccount = fromAccount,
                ToAccount = toAccount
            };
            if(id.HasValue) newTransaction.SetIdentificator(id.Value);
            ValidateEntity(newTransaction);
            return newTransaction;
        }
        public override async Task CreateEntityFromInput()
        {
            try
            {
                await connector.StartConnection();
                Transaction newTransaction = await ConstructEntityFromInput(null);
                decimal fromBalance = newTransaction.FromAccount.Balance;
                decimal toBalance = newTransaction.ToAccount.Balance;
                fromBalance -= newTransaction.Amount;
                toBalance += newTransaction.Amount;
                Account fromAccount = newTransaction.FromAccount;
                fromAccount.Balance = fromBalance;
                Account toAccount = newTransaction.ToAccount;
                toAccount.Balance = toBalance;
                await connector.StartTransaction();
                await SqlRepository.UpdateEntityById(fromAccount, connector);
                await SqlRepository.UpdateEntityById(toAccount, connector);
                Transaction transaction = (Transaction)(await SqlRepository.CreateEntity(newTransaction, connector));
                await connector.CommitTransaction();
                System.Console.WriteLine($"Додат нови {GetClassNameForScreen()} објекат: '{transaction}'. (притисните било који тастер за наставак)");
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
        protected override string GetClassNameForScreen()
        {
            return "трансакција";
        }
        protected override string[] GetColumnNames()
        {
            return new string[] {"ИД", "Износ", "Датум", "Од корисника", "Ка кориснику"};
        }
        protected override string GetNameForGetId()
        {
            return "трансакције";
        }
        protected override Transaction GetNewEntityInstance(int id = -1)
        {
            return new Transaction()
            {
                Id = id
            };
        }
        protected override string GetPluralClassNameForScreen()
        {
            return "ТРАНСАКЦИЈЕ";
        }
    }
}
