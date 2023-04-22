using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VeeArc.Application.Common.Interfaces;
using VeeArc.Infrastructure.BlobStorages;
using VeeArc.Infrastructure.Common.HostedServices;
using VeeArc.Infrastructure.Common.Interfaces;
using VeeArc.Infrastructure.DataBase;
using VeeArc.Infrastructure.DataBase.Repositories;
using VeeArc.Infrastructure.Extensions;

namespace VeeArc.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<DatabaseInitService>();

        services.ConfigureApplicationDbContext(configuration);

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddAzureBlobStorage(configuration);

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RolesRepository>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<IImageStorageRepositry, ImageStorageRepositry>();

        return services;
    }

    public static IServiceCollection AddAzureBlobStorage(this IServiceCollection services, IConfiguration configuration)
    {
        var blobServiceClient = new BlobServiceClient(configuration.GetConnectionString("BlobConnection"));

        services.AddSingleton(blobServiceClient);

        services.AddScoped<IImageStorageRepositry, ImageStorageRepositry>();
        services.AddScoped<IArticleStorageRepository, ArticleStorageRepository>();

        ImageStorageRepositry.Init(blobServiceClient);
        ArticleStorageRepository.Init(blobServiceClient);

        return services;
    }
}
