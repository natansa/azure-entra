using Domain.Models;
using Infrastructure.Auth.Interface;
using Microsoft.Graph.Models;
using GraphServiceClient = Microsoft.Graph.GraphServiceClient;

namespace Infrastructure.Auth;

public class AuthService : IAuthService
{
    private readonly GraphServiceClient _graphServiceClient;


    public AuthService(GraphServiceClient graphServiceClient) => _graphServiceClient = graphServiceClient;

    public async Task<ProfileModel> GetProfileAsync()
    {
        var userResult = await _graphServiceClient.Me.GetAsync();
        var organizationsResult = await _graphServiceClient.Organization.GetAsync();
        var tenantResult = organizationsResult?.Value?.FirstOrDefault();
        var usersResult = await _graphServiceClient.Users.GetAsync();
        var groupsResult = await _graphServiceClient.Groups.GetAsync();

        var currentUser = new UserModel(userResult.Id, userResult.DisplayName, userResult.UserPrincipalName);
        var users = GetUsers(usersResult);
        var organizations = GetOrganizations(organizationsResult);
        var tenant = new TenantModel(tenantResult.Id, tenantResult.DisplayName);
        var groups = GetGroups(groupsResult);

        return new ProfileModel(currentUser, users, organizations, tenant, groups);
    }

    private static List<GroupModel> GetGroups(GroupCollectionResponse? groupsResult)
    {
        var groups = new List<GroupModel>();

        foreach (var group in groupsResult.Value)
        {
            groups.Add(new GroupModel(group.Id, group.DisplayName, group.Mail));
        }

        return groups;
    }

    private static List<OrganizationModel> GetOrganizations(OrganizationCollectionResponse? organizationsResult)
    {
        var organizations = new List<OrganizationModel>();
        foreach (var organization in organizationsResult.Value)
        {
            organizations.Add(new OrganizationModel(organization.Id, organization.DisplayName));
        }

        return organizations;
    }

    private static List<UserModel> GetUsers(UserCollectionResponse? usersResult)
    {
        var users = new List<UserModel>();

        foreach (var userResult in usersResult.Value)
        {
            users.Add(new UserModel(userResult.Id, userResult.DisplayName, userResult.UserPrincipalName));
        }

        return users;
    }
}