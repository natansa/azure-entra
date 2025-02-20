using Domain.Models;

namespace Application.UseCase.Interface;

public interface IUserSyncUseCase
{
    Task<string> CreateUserAsync(UserSyncModel user);
}