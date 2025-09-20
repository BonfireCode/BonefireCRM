namespace BonefireCRM.Domain.Exceptions
{
    public class RegisterUserException : Exception
    {
        public string Code { get; init; }

        public string Text { get; init; }

        public RegisterUserException(string code, string text)
            : base($"{code}: {text}")
        {
            Code = code;
            Text = text;
        }
    }
}
