using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VeeArc.Infrastructure.Common.Interfaces;

namespace VeeArc.Infrastructure.Common.HostedServices;

public class DatabaseInitService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var applicationDbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

        if (applicationDbContext is DbContext dbContext)
        {
            await dbContext.Database.MigrateAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}