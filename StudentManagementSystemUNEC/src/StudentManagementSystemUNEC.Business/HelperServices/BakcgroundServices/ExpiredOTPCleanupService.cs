using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentManagementSystemUNEC.DataAccess.Contexts;

namespace StudentManagementSystemUNEC.Business.HelperServices.BakcgroundServices;

public class ExpiredOTPCleanupService : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly IServiceScopeFactory _serviceScopeFactory;


    public ExpiredOTPCleanupService(IServiceScopeFactory serviceScopeFactory)
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

            var expiredUsers = dbContext.Users
                                        .Where(u => u.OTPExpiryDate <= DateTime.Now)
                                        .ToList();

            foreach (var user in expiredUsers)
            {
                user.OTP = null;
                user.OTPExpiryDate = null;
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