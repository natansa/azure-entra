using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CronJobs;

public static class ConfigCronJob
{
    public static void ConfigureCronJob(this IServiceCollection services) => 
        services.AddHostedService<UserSyncCronJob>();
}