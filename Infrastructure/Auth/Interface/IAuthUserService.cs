using Domain.Models;

namespace Infrastructure.Auth.Interface;

public interface IAuthUserService
{
    Task<string> CreateUserAsync(UserSyncModel userSyncAdb2CModel);
}