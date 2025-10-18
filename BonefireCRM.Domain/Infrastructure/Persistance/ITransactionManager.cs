using LanguageExt;

namespace BonefireCRM.Domain.Infrastructure.Persistance
{
    public interface ITransactionManager
    {
        Task<Fin<U>> Execute<U>(Func<Task<Fin<U>>> multipleOperations);
    }
}
