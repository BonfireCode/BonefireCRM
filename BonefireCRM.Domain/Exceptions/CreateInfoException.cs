namespace BonefireCRM.Domain.Exceptions
{
    public class CreateInfoException : Exception
    {
        public string Code { get; init; }

        public string Text { get; init; }

        public CreateInfoException(string code, string text)
            : base($"{code}: {text}")
        {
            Code = code;
            Text = text;
        }
    }
}
