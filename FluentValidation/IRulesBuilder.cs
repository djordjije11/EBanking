namespace FluentValidation
{
    public interface IRuleBuilder<TProperty>
    {
        IRuleBuilder<TProperty> WithMaxLength(int maxLength);
        IRuleBuilder<TProperty> WithMinLength(int minLength);
        IRuleBuilder<TProperty> NotEmpty();
        IRuleBuilder<TProperty> MatchesRegex(string regex);
        IRuleBuilder<TProperty> IsEmail();
        IRuleBuilder<TProperty> WithMessage(string message);
    }
}
