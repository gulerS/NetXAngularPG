using NetXAngularPG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetXAngularPG.Application.Repositories
{
    public interface IProductCommandRepository : ICommandRepository<Product>
    {
    }
}
