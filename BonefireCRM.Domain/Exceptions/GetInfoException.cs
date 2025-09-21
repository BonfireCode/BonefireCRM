namespace BonefireCRM.Domain.Exceptions
{
    public class GetInfoException : Exception
    {
        public string Code { get; init; }

        public string Text { get; init; }

        public GetInfoException(string code, string text)
            : base($"{code}: {text}")
        {
            Code = code;
            Text = text;
        }
    }
}
