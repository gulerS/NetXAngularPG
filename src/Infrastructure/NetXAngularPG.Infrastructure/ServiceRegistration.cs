using Microsoft.Extensions.DependencyInjection;
using NetXAngularPG.Application.Repositories;
using NetXAngularPG.Application.Services;
using NetXAngularPG.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetXAngularPG.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {

            services.AddScoped<IFileService, FileService>();
            

        }
    }
}
