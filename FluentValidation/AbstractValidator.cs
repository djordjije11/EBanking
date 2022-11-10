namespace FluentValidation
{
    public abstract class AbstractValidator
    {
        internal List<string> Errors { get; set; } = new List<string>();
        public List<Rule> Rules { get; set; } = new List<Rule>();

        public IRuleBuilder<TProperty> RuleFor<TProperty>(TProperty property)
        {
            return new RuleBuilder<TProperty>(this, property);
        }

        public ResultInfo Validate()
        {
            foreach (var rule in Rules)
            {
                var error = rule.Validate();
                if (error != null)
                {
                    Errors.Add(error);
                }
            }

            var result = new ResultInfo();

            if(Errors.Count > 0)
            {
                result.IsValid = false;
                result.Errors = Errors;
            }
            else
            {
                result.IsValid = true;
            }

            return result;
        }
    }

    public class ResultInfo
    {
        public bool IsValid { get; set; }
        public List<string>? Errors { get; set; }
        public string GetErrorsString()
        {
            string errorMessage = String.Empty;
            foreach(var error in Errors)
            {
                errorMessage += $"[ERROR]> {error}\n";
            }
            return errorMessage;
        }
    }
}
