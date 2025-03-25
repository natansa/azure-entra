using Domain.Models;
using Infrastructure.Auth.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.CronJobs;

public class UserSyncCronJob : BackgroundService
{
    private readonly ILogger<UserSyncCronJob> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly TimeSpan _executionTime = new(13, 57, 0);

    public UserSyncCronJob(ILogger<UserSyncCronJob> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var nextRun = GetNextRunTime(now);

            _logger.LogInformation($"Next scheduled run for: {nextRun}", nextRun);

            var delay = nextRun - now;
            if (delay > TimeSpan.Zero)
            {
                await Task.Delay(delay, stoppingToken);
            }

            if (!stoppingToken.IsCancellationRequested)
            {
                await ExecuteJob();
            }
        }
    }

    private DateTime GetNextRunTime(DateTime now)
    {
        var todayRunTime = now.Date + _executionTime;
        return now < todayRunTime ? todayRunTime : todayRunTime.AddDays(1);
    }

    private async Task ExecuteJob()
    {
        try
        {
            _logger.LogInformation($"Executando job às {DateTime.Now:HH:mm:ss}");

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var authUserService = scope.ServiceProvider.GetRequiredService<IAuthUserService>();

                await authUserService.CreateUserAsync(new UserSyncModel
                (
                    "Natanael",
                    "Sa Rodrigues 2",
                    "nrodrigues+teste01@domain"
                ));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro na execução do job: {ex.Message}");
        }
    }

    
}