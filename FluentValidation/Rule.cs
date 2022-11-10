using System.Text.RegularExpressions;

namespace FluentValidation
{
    public abstract class Rule
    {
        public string ErrorMessage { get; set; }
        public abstract string? Validate();
    }

    public class MaxLengthRule<TProperty> : Rule
    {
        public TProperty Property { get; set; }
        public int MaxLength { get; set; }
        public MaxLengthRule(int maxLength, TProperty property)
        {
            MaxLength = maxLength;
            Property = property;
        }

        public override string? Validate()
        {
            if (Property is string str)
            {
                if (str.Length > MaxLength)
                {
                    if (string.IsNullOrEmpty(ErrorMessage) == false)
                        return ErrorMessage;
                    else
                        return $"String ne smije biti duzi od {MaxLength}";
                }
            }
            else if (Property is Array array)
            {
                if (array.Length > MaxLength)
                {
                    if (string.IsNullOrEmpty(ErrorMessage) == false)
                        return ErrorMessage;
                    else
                        return $"Niz ne smije biti duzi od {MaxLength}";
                }
            }

            return null;
        }
    }

    public class MinLengthRule<TProperty> : Rule
    {
        public TProperty Property { get; set; }
        public int MinLength { get; set; }
        public MinLengthRule(int minLength, TProperty property)
        {
            MinLength = minLength;
            Property = property;
        }

        public override string? Validate()
        {
            if (Property is string str)
            {
                if (str.Length < MinLength)
                {
                    if (!string.IsNullOrEmpty(ErrorMessage))
                        return ErrorMessage;
                    else
                        return $"String ne smije biti kraci od {MinLength}";
                }
            }
            return null;

        }
    }

    public class NotEmptyRule<TProperty> : Rule
    {
        public TProperty Property { get; set; }
        public NotEmptyRule(TProperty property)
        {
            Property = property;
        }

        public override string? Validate()
        {
            if (Property is null || (Property is string str && string.IsNullOrEmpty(str)))
            {
                if (!string.IsNullOrEmpty(ErrorMessage))
                    return ErrorMessage;
                else
                    return $"String ne smije biti prazan ili null";
            }

            return null;
        }
    }

    public class MatchRegex<TProperty> : Rule
    {
        public Regex RegexExp { get; set; }
        public TProperty Property { get; set; }
        public MatchRegex(TProperty property, string regex)
        {
            Property = property;
            RegexExp = new Regex(regex);
        }

        public override string? Validate()
        {
            if (Property is string str && str != null)
            {
                if (RegexExp.IsMatch(str))
                    return null;

                if (!string.IsNullOrEmpty(ErrorMessage))
                    return ErrorMessage;
                else
                    return $"Not match";
            }

            return null;
        }
    }

    public class IsEmail<TProperty> : Rule
    {
        public TProperty Property { get; set; }
        public IsEmail(TProperty property)
        {
            Property = property;
        }

        public override string? Validate()
        {
            if (Property is string str && str != null)
            {
                if (Regex.IsMatch(str, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                    return null;

                if (!string.IsNullOrEmpty(ErrorMessage))
                    return ErrorMessage;
                else
                    return $"Neispravan email";
            }

            return null;
        }
    }
}
