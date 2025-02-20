using Application.UseCase.Interface;
using Domain.Models;
using Infrastructure.Auth.Interface;
using Microsoft.Extensions.Logging;

namespace Application.UseCase;

public class UserSyncUseCase : IUserSyncUseCase
{
    private readonly ILogger<UserSyncUseCase> _logger;
    private readonly IAuthUserService _authUserService;

    public UserSyncUseCase(ILogger<UserSyncUseCase> logger, IAuthUserService authUserService)
    {
        _logger = logger;
        _authUserService = authUserService;
    }

    public async Task<string> CreateUserAsync(UserSyncModel user) => await _authUserService.CreateUserAsync(user);
}