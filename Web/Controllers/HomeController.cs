using System.Diagnostics;
using System.Threading.Tasks;
using WebAzureEntra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Application.UseCase.Interface;
using Domain.Models;
using Infrastructure.Auth.Interface;

namespace WebAzureEntra.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IProfileUseCase _useCase;
    private readonly IAuthUserService _authUserService;

    public HomeController(IProfileUseCase useCase, IAuthUserService authUserService)
    {
        _useCase = useCase;
        _authUserService = authUserService;
    }

    [AllowAnonymous]
    public IActionResult Index() => View();

    [AuthorizeForScopes(ScopeKeySection = "DownstreamApi:Scopes")]
    public async Task<IActionResult> Profile()
    {
        var profile = await _useCase.GetAsync();
        return View(profile);
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}