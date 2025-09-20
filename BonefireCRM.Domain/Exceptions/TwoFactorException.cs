namespace BonefireCRM.Domain.Exceptions
{
    public class TwoFactorException : Exception
    {
        public string Code { get; init; }

        public string Text { get; init; }

        public TwoFactorException(string code, string text)
            : base($"{code}: {text}")
        {
            Code = code;
            Text = text;
        }
    }
}
