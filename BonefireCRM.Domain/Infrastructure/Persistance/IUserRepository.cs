using BonefireCRM.Domain.Entities;

namespace BonefireCRM.Domain.Infrastructure.Persistance
{
    public interface IUserRepository: IBaseRepository<User>
    {
        Task<Guid> GetUserIdAsync(Guid registerId, CancellationToken ct);
    }
}
