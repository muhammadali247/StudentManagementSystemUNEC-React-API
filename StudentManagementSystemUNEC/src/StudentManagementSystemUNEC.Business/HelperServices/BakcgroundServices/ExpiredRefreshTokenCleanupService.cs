using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentManagementSystemUNEC.DataAccess.Contexts;

namespace StudentManagementSystemUNEC.Business.HelperServices.BakcgroundServices;

public class ExpiredRefreshTokenCleanupService : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ExpiredRefreshTokenCleanupService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async void DoWork(object state)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Define the current date and time as the threshold for immediate expiration
            var currentDateTime = DateTime.Now;

            var expiredTokens = dbContext.refreshTokens
                                        .Where(t => t.Expires <= currentDateTime)
                                        .ToList();

            foreach (var token in expiredTokens)
            {
                // Mark the token as revoked immediately
                token.IsRevoked = true;
            }

            await dbContext.SaveChangesAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}