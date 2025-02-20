using Domain.Models;

namespace Application.UseCase.Interface;

public interface IProfileUseCase
{
    Task<ProfileModel> GetAsync();
}