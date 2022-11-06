using ConsoleTableExt;
using EBanking.Console.DataAccessLayer;
using EBanking.Console.Model;
using EBanking.Console.Validations.Exceptions;
using EBanking.Console.Validations.Interfaces;

namespace EBanking.Console.Brokers
{
    internal abstract class EntityBroker<T> : IBroker where T : Entity
    {
        protected IValidator<T>? validator;
        public Connector connector;
        public EntityBroker(Connector connector) { this.connector = connector; }
        public EntityBroker() { connector = new Connector(); }
        public EntityBroker(IValidator<T> validator) : this()
        {
            this.validator = validator;
        }
        public void SetValidator(IValidator<T> validator) { this.validator = validator; }
        protected abstract string GetNameForGetId();
        protected abstract string GetClassNameForScreen();
        protected abstract string GetPluralClassNameForScreen();
        protected abstract string[] GetColumnNames();
        protected abstract T GetNewEntityInstance(int id = -1);
        protected void ValidateEntity(T entity)
        {
            if (validator != null) validator.Validate(entity);
        }
        public int GetIdFromInput()
        {
            System.Console.WriteLine($"Унесите ид {GetNameForGetId()}:");
            string input = System.Console.ReadLine() ?? "";
            if (!Int32.TryParse(input, out int id)) throw new ValidationException($"Ид {GetNameForGetId()} мора бити број.");
            if (id < 0) throw new ValidationException($"Ид {GetNameForGetId()} мора бити позитиван број.");
            return id;
        }
        public abstract Task<T> ConstructEntityFromInput(int? id);
        public async Task<T> FindEntityFromInput()
        {
            int id = GetIdFromInput();
            T? wantedEntity = (T?)(await SqlRepository.GetEntityById(GetNewEntityInstance(id), connector));
            if (wantedEntity == null) throw new ValidationException($"У бази не постоји {GetClassNameForScreen()} са унетим ид бројем.");
            return wantedEntity;
        }
        public virtual async Task CreateEntityFromInput()
        {
            try
            {
                await connector.StartConnection();
                T newEntity = await ConstructEntityFromInput(null);
                await connector.StartTransaction();
                T entity = (T)(await SqlRepository.CreateEntity(newEntity, connector));
                System.Console.WriteLine($"Додат нови {GetClassNameForScreen()} објекат: '{entity.SinglePrint()}'. (притисните било који тастер за наставак)");
                await connector.CommitTransaction();
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
        public virtual async Task DeleteEntityFromInput()
        {
            int id = GetIdFromInput();
            try
            {
                await connector.StartConnection();
                await connector.StartTransaction();
                T entity = (T)(await SqlRepository.DeleteEntity(GetNewEntityInstance(id), connector));
                await connector.CommitTransaction();
                System.Console.WriteLine($"Обрисан {GetClassNameForScreen()} објекат: '{entity.SinglePrint()}'. (притисните било који тастер за наставак)");
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
        public async Task UpdateEntityFromInput()
        {
            try
            {
                await connector.StartConnection();
                T wantedEntity = (T)(await FindEntityFromInput());
                System.Console.WriteLine($"Тражени {GetClassNameForScreen()}: {wantedEntity.SinglePrint()}.\n");
                T newEntity = await ConstructEntityFromInput(wantedEntity.GetIdentificator());
                await connector.StartTransaction();
                T updatedEntity = (T)(await SqlRepository.UpdateEntityById(newEntity, connector));
                await connector.CommitTransaction();
                System.Console.WriteLine($"Ажуриран {GetClassNameForScreen()}: '{updatedEntity.SinglePrint()}'. (притисните било који тастер за наставак)");
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
        public async Task GetEntityFromInput()
        {
            try
            {
                await connector.StartConnection();
                T wantedEntity = await FindEntityFromInput();
                System.Console.WriteLine($"Тражени {GetClassNameForScreen()}: '{wantedEntity.SinglePrint()}'. (притисните било који тастер за наставак)");
            }
            catch
            {
                throw;
            }
            finally
            {
                await connector.EndConnection();
            }
        }
        public async Task GetEntitiesFromInput()
        {
            try
            {
                await connector.StartConnection();
                List<Entity> entities = await SqlRepository.GetAllEntities(GetNewEntityInstance(), connector);
                List<T> specEntities = new List<T>();
                foreach (Entity entity in entities) specEntities.Add((T)entity);
                ConsoleTableBuilder
                    .From(specEntities)
                    .WithTitle(GetPluralClassNameForScreen().ToUpper() + " ", ConsoleColor.Yellow, ConsoleColor.DarkGray)
                    .WithColumn(GetColumnNames())
                    .ExportAndWriteLine();
                System.Console.WriteLine("Притисните било који тастер за наставак...");
            }
            catch
            {
                throw;
            }
            finally
            {
                await connector.EndConnection();
            }
        }
    }
}
