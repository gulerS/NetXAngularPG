using NetXAngularPG.Application.Repositories;
using NetXAngularPG.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetXAngularPG.Persistance.Repositories
{
    public class FileCommandRepository : CommandRepository<Domain.Entities.File>, IFileCommandRepository
    {
        public FileCommandRepository(NetXAngularPGDbContext context) : base(context)
        {
        }
    }
}
