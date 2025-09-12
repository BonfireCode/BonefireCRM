namespace BonefireCRM.Domain.Exceptions
{
    internal class UpdateEntityException<T> : Exception
    {
        public UpdateEntityException()
            : base($"An error occurred on update {typeof(T).Name}.")
        {
        }
    }
}
