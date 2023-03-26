﻿using NetXAngularPG.Application.Repositories;
using NetXAngularPG.Domain.Entities;
using NetXAngularPG.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetXAngularPG.Persistance.Repositories
{
    public class InvoiceFileCommandRepository : CommandRepository<InvoiceFile>, IInvoiceFileCommandRepository
    {
        public InvoiceFileCommandRepository(NetXAngularPGDbContext context) : base(context)
        {
        }
    }
}