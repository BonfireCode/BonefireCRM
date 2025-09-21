namespace BonefireCRM.Domain.Exceptions
{
    public class UpdateEntityException<T> : Exception
    {
        public UpdateEntityException()
            : base($"An error occurred on update {typeof(T).Name}.")
        {
        }
    }
}
