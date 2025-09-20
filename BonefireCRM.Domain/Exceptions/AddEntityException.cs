namespace BonefireCRM.Domain.Exceptions
{
    public class AddEntityException<T> : Exception
    {
        public AddEntityException()
            : base($"An error occurred on add {typeof(T).Name}.")
        {
        }
    }
}
