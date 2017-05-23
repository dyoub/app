// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Financial.SaleIncomes;
using System.Collections.Generic;
using System.Linq;

namespace Dyoub.App.Models.ServiceModel.Financial
{
    public class SaleBilling : Billing<SaleIncome>
    {
        public SaleOrder SaleOrder { get; private set; }

        public SaleBilling(TenantContext tenant, SaleOrder saleOrder) : base(tenant, saleOrder)
        {
            SaleOrder = saleOrder;
        }

        protected override void AddIncomes(IEnumerable<SaleIncome> incomes)
        {
            Tenant.SaleIncomes.AddRange(incomes);
        }

        protected override SaleIncome NewIncome()
        {
            return new SaleIncome();
        }

        protected override void RemoveIncomes()
        {
            Tenant.SaleIncomes.RemoveRange(SaleOrder.SalePayments
                .SelectMany(s => s.SaleIncomes));
        }
    }
}
