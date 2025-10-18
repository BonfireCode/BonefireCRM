using BonefireCRM.Domain.Infrastructure.Persistance;
using LanguageExt;
using System.Transactions;

namespace BonefireCRM.Infrastructure.Persistance
{
    internal class TransactionManager: ITransactionManager
    {
        public async Task<Fin<U>> Execute<U>(Func<Task<Fin<U>>> multipleOperations)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await multipleOperations();
                if (result.IsSucc)
                {
                    scope.Complete();
                }

                return result;
            }
        }
    }
}
