using EBanking.Console.Model;
using EBanking.Console.Validations;
using EBanking.Console.Validations.Exceptions;
using EBanking.Console.Validations.Interfaces;

namespace EBanking.Console.Managers
{
    internal abstract class EntityManager<T> where T : Entity
    {
        protected IValidator<T> validator;

        public EntityManager(IValidator<T> validator)
        {
            this.validator = validator;
        }
        
        protected void ValidateEntity(T entity)
        {
            if (validator != null)
            {
                try
                {
                    validator.Validate(entity);
                }
                catch
                {
                    throw;
                }
            }
        }
        protected int GetIdFromInput(string objectName)
        {
            System.Console.WriteLine($"Унесите ид {objectName}:");
            string input = System.Console.ReadLine() ?? "";
            if (!Int32.TryParse(input, out int id)) throw new ValidationException($"Ид {objectName} мора бити број.");
            if (id < 0) throw new ValidationException($"Ид {objectName} мора бити позитиван број.");
            return id;
        }

        protected T GetEntityFromValidation(Validation<T> validation)
        {
            return (validation.IsValid == true) ? validation.Entity : throw validation.Exception;
        }

        public abstract Task<Validation<T>> ConstructEntityFromInput(int? id);
        public abstract Task<Validation<T>> FindEntityFromInput();
        public abstract Task<Validation<T>> CreateEntityFromInput();
        public abstract Task<Validation<T>> DeleteEntityFromInput();
        public abstract Task<Validation<T>> UpdateEntityFromInput();
        public abstract Task<Validation<T>> GetEntityFromInput();
        public abstract Task<Validation<T>> GetEntitiesFromInput();
    }
}
