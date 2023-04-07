using Microsoft.Extensions.DependencyInjection;
using NetXAngularPG.Application.Abstractions.Storage;
using NetXAngularPG.Infrastructure.Enums;
using NetXAngularPG.Infrastructure.Services;
using NetXAngularPG.Infrastructure.Services.Storage;
using NetXAngularPG.Infrastructure.Services.Storage.Local;

namespace NetXAngularPG.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();



        }

        public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
        {
            services.AddScoped<IStorage, T>();
        }

        public static void AddStorage<T>(this IServiceCollection services, StorageType storageType) where T : class, IStorage
        {
            switch (storageType)
            {
                case StorageType.Local:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:

                    break;
                case StorageType.AWS:
                    break;

                default:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
