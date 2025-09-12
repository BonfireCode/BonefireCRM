using LanguageExt.ClassInstances.Pred;

namespace BonefireCRM.Domain.Exceptions
{
    internal class AddEntityException<T> : Exception
    {
        public AddEntityException()
            : base($"An error occurred on add {typeof(T).Name}.")
        {
        }
    }
}
