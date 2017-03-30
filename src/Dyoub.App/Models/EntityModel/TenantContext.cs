﻿// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Infrastructure.EntityFramework;
using Dyoub.App.Models.EntityModel.Account;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Dyoub.App.Models.EntityModel
{
    public class TenantContext : ApplicationContext
    {
        public int CurrentId { get; private set; }

        public IQueryable<Tenant> Current
        {
            get
            {
                return base.Set<Tenant>().Where(tentant => tentant.Id == CurrentId);
            }
        }

        public TenantContext(DbConnection connection, int tenantId) : base(connection)
        {
            CurrentId = tenantId;
        }

        public override DbSet<TEntity> Set<TEntity>()
        {
            return new FilteredDbSet<TEntity>(base.Set<TEntity>(),
                new PropertyFilterExpression<TEntity>("TenantId", CurrentId));
        }

        public override async Task<int> SaveChangesAsync()
        {
            foreach (DbEntityEntry entry in ChangeTracker.Entries())
            {
                ((ITenantData)entry.Entity).TenantId = CurrentId;
            }

            return await base.SaveChangesAsync();
        }
    }
}
