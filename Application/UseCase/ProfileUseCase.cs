using Application.UseCase.Interface;
using Domain.Models;
using Infrastructure.Auth.Interface;

namespace Application.UseCase;

public class ProfileUseCase : IProfileUseCase
{
    private readonly IAuthService _authService;

    public ProfileUseCase(IAuthService authService) => _authService = authService;

    public async Task<ProfileModel> GetAsync() => await _authService.GetProfileAsync();
}