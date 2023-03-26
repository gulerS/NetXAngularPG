using NetXAngularPG.Application.Repositories;
using NetXAngularPG.Domain.Entities;
using NetXAngularPG.Persistance.Contexts;
using NetXAngularPG.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetXAngularPG.Persistance.Repositories
{
    public class InvoiceFileQueryRepository : QueryRepository<InvoiceFile>, IInvoiceFileQueryRepository
    {
        public InvoiceFileQueryRepository(NetXAngularPGDbContext context) : base(context)
        {
        }
    }
}
