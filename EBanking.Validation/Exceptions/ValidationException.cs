namespace EBanking.Console.Validations.Exceptions
{
    internal class ValidationException : Exception
    {
        public ValidationException(string message) : base("Унети податак није валидан!\n" + message) 
        {
        }
    }
}
