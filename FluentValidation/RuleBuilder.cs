
using System.Text.RegularExpressions;

namespace FluentValidation
{
    public class RuleBuilder<TProperty> : IRuleBuilder<TProperty>
    {
        public TProperty PropertyValue { get; set; }
        public Rule LastRule { get; set; }
        public AbstractValidator AbstractValidator { get; set; }

        public RuleBuilder(AbstractValidator abstractValidator, TProperty property)
        {
            AbstractValidator = abstractValidator;
            PropertyValue = property;
        }

        IRuleBuilder<TProperty> IRuleBuilder<TProperty>.WithMaxLength(int maxLength)
        {
            Rule rule = new MaxLengthRule<TProperty>(maxLength, PropertyValue);
            AbstractValidator.Rules.Add(rule);
            LastRule = rule;
            return this;
        }

        IRuleBuilder<TProperty> IRuleBuilder<TProperty>.WithMinLength(int minLength)
        {
            Rule rule = new MinLengthRule<TProperty>(minLength, PropertyValue);
            AbstractValidator.Rules.Add(rule);
            LastRule = rule;
            return this;
        }

        public IRuleBuilder<TProperty> WithMessage(string message)
        {
            if (LastRule != null)
                LastRule.ErrorMessage = message;

            return this;
        }

        public IRuleBuilder<TProperty> NotEmpty()
        {
            Rule rule = new NotEmptyRule<TProperty>(PropertyValue);
            AbstractValidator.Rules.Add(rule);
            LastRule = rule;
            return this;
        }

        public IRuleBuilder<TProperty> MatchesRegex(string regex)
        {
            Rule rule = new MatchRegex<TProperty>(PropertyValue, regex);
            AbstractValidator.Rules.Add(rule);
            LastRule = rule;
            return this;
        }

        public IRuleBuilder<TProperty> IsEmail()
        {
            Rule rule = new IsEmail<TProperty>(PropertyValue);
            AbstractValidator.Rules.Add(rule);
            LastRule = rule;
            return this;
        }
    }


}
