using BonefireCRM.Domain.DTOs.User;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Exceptions;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Mappers;
using BonefireCRM.SourceGenerator;
using LanguageExt;

namespace BonefireCRM.Domain.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly SecurityService _securityService;

        public UserService(IUserRepository userRepository, SecurityService securityService)
        {
            _userRepository = userRepository;
            _securityService = securityService;
        }

        public async Task<Option<GetUserDTO>> GetUserAsync(Guid id, CancellationToken ct)
        {
            var user = await _userRepository.GetAsync(id, ct);
            if (user is null)
            {
                return Option<GetUserDTO>.None;
            }

            return user.MapToGetDto();
        }

        public IEnumerable<GetUserDTO> GetAllUsers(GetAllUsersDTO getAllUsersDTO, CancellationToken ct)
        {
            var filterExpression = UserQueryExpressions.Filter(getAllUsersDTO);

            var sortExpression = UserQueryExpressions.Sort(getAllUsersDTO.SortBy);

            var skip = (getAllUsersDTO.PageNumber - 1) * getAllUsersDTO.PageSize;
            var take = getAllUsersDTO.PageSize;

            var users = _userRepository.GetAll(filterExpression, sortExpression, getAllUsersDTO.SortDirection, skip, take, ct);

            var getUsersResultDTO = users.Select(u => u.MapToGetDto());

            return getUsersResultDTO;
        }

        public async Task<Fin<bool>> DeleteUserAsync(Guid id, Guid registerId, CancellationToken ct)
        {
            //Domain validations if needed

            var result = await _securityService.DeleteUserAsync(registerId, ct);
            if (result.IsFail)
            {
                return Fin<bool>.Fail(new DeleteUserException());
            }

            var user = new User { Id = id, RegisterId = registerId };
            var isDeleted = await _userRepository.DeleteAsync(user, ct);
            if (!isDeleted)
            {
                return Fin<bool>.Fail(new DeleteEntityException<User>());
            }

            return isDeleted;
        }

        public async Task<Fin<UpdatedUserDTO>> UpdateUserAsync(UpdateUserDTO updateUserDTO, CancellationToken ct)
        {
            //Domain validations if needed

            var user = updateUserDTO.MapToUser();

            var updatedUser = await _userRepository.UpdateAsync(user, ct);
            if (updatedUser is null)
            {
                return Fin<UpdatedUserDTO>.Fail(new UpdateEntityException<User>());
            }

            return updatedUser.MapToUpdatedDto();
        }

        public async Task<Guid> GetUserIdAsync(Guid registerId, CancellationToken ct)
        {
            var userId = await _userRepository.GetUserIdAsync(registerId, ct);
            return userId;
        }
    }
}
