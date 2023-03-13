using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetXAngularPG.Persistance.Contexts
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<NetXAngularPGDbContext>
    {
        public NetXAngularPGDbContext CreateDbContext(string[] args)
        {

         

            DbContextOptionsBuilder<NetXAngularPGDbContext> optionsBuilder = new();

            optionsBuilder.UseSqlServer(Configuration.MssqlConnectionString);


            return new NetXAngularPGDbContext(optionsBuilder.Options);
        }
    }
}
