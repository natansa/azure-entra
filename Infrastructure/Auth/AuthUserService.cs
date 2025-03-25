using Domain.Models;
using Infrastructure.Auth.Interface;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace Infrastructure.Auth;

public class AuthUserService : IAuthUserService
{
    private readonly GraphServiceClient _graphServiceClient;

    public AuthUserService(GraphServiceClient graphServiceClient) => _graphServiceClient = graphServiceClient;

    public async Task<string> CreateUserAsync(UserSyncModel userSyncAdb2CModel)
    {
        var user = new User
        {
            GivenName = userSyncAdb2CModel.FirstName,
            Surname = userSyncAdb2CModel.LastName,
            DisplayName = $"{userSyncAdb2CModel.FirstName} {userSyncAdb2CModel.LastName}",
            Identities = new List<ObjectIdentity>
            {
                new()
                {
                    SignInType = "emailAddress",
                    Issuer = "domain.onmicrosoft.com",
                    IssuerAssignedId = userSyncAdb2CModel.Email
                }
            },
            PasswordProfile = new PasswordProfile
            {
                Password = "SenhaForte123!",
                ForceChangePasswordNextSignIn = false
            },
            PasswordPolicies = "DisablePasswordExpiration",
            AccountEnabled = true
        };

        var createdUser = await _graphServiceClient.Users.PostAsync(user);
        return createdUser?.Id!;
    }
}
