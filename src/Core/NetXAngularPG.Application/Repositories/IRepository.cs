﻿using Microsoft.EntityFrameworkCore;
using NetXAngularPG.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetXAngularPG.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get;  }
    }
}
