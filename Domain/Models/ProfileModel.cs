namespace Domain.Models;

public record ProfileModel(
    UserModel CurrentUser, 
    IEnumerable<UserModel> Users,
    IEnumerable<OrganizationModel> Organizations,
    TenantModel Tenant,
    IEnumerable<GroupModel> Groups
);