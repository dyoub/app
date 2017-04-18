// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Users;
using Dyoub.App.Models.EntityModel.Catalog.ProductPrices;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Catalog.ServicePrices;
using Dyoub.App.Models.EntityModel.Catalog.Services;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.App.Models.EntityModel.Manage.TeamMembers;
using Dyoub.App.Models.EntityModel.Manage.TeamRules;
using Dyoub.App.Models.EntityModel.Manage.Teams;
using Dyoub.App.Models.EntityModel.Commercial.Customers;
using System;
using System.Collections.Generic;

namespace Dyoub.App.Models.EntityModel.Account.Tenants
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeactivatedAt { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductPrice> ProductPrices { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<ServicePrice> ServicePrices { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
        public virtual ICollection<TeamRule> TeamRules { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
