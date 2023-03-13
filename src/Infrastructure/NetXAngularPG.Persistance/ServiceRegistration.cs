using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetXAngularPG.Application.Repositories;
using NetXAngularPG.Persistance.Contexts;
using NetXAngularPG.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetXAngularPG.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services) {

            services.AddDbContext<NetXAngularPGDbContext>(x=>x.UseSqlServer(Configuration.MssqlConnectionString));

            services.AddScoped<ICustomerCommandRepository,CustomerCommandRepository>();
            services.AddScoped<ICustomerQueryRepository,CustomerQueryRepository>();

            services.AddScoped<IOrderCommandRepository,OrderCommandRepository>();
            services.AddScoped<IOrderQueryRepository, OrderQueryRepository>();

            services.AddScoped<IProductCommandRepository,ProductCommandRepository>();
            services.AddScoped<IProductQueryRepository, ProductQueryRepository>();

        }
    }
}
