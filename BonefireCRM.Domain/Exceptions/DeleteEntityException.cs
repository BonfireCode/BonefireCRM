namespace BonefireCRM.Domain.Exceptions
{
    public class DeleteEntityException<T> : Exception
    {
        public DeleteEntityException()
            : base($"An error occurred on delete {typeof(T).Name}.")
        {
        }
    }
}
