using Application.UseCase.Interface;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAzureEntra.Controllers;

public class UserController : Controller
{
    private readonly IUserSyncUseCase _useCase;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserSyncUseCase useCase, ILogger<UserController> logger)
    {
        _useCase = useCase;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] UserSyncModel user)
    {
        try
        {
            _logger.LogInformation("Creating user {user}", JsonSerializer.Serialize(user));
            var id = await _useCase.CreateUserAsync(user);
            return Created(string.Empty, id);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error on create user {user}", JsonSerializer.Serialize(user));
            return Problem();
        }
    }
}