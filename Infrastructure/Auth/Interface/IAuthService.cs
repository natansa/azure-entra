using Domain.Models;

namespace Infrastructure.Auth.Interface;

public interface IAuthService
{
    Task<ProfileModel> GetProfileAsync();
}