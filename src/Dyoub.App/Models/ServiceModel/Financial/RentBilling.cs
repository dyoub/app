// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.EntityModel.Financial.RentIncomes;
using System.Collections.Generic;
using System.Linq;

namespace Dyoub.App.Models.ServiceModel.Financial
{
    public class RentBilling : Billing<RentIncome>
    {
        public RentContract RentContract { get; private set; }

        public RentBilling(TenantContext tenant, RentContract rentContract) : base(tenant, rentContract)
        {
            RentContract = rentContract;
        }

        protected override void AddIncomes(IEnumerable<RentIncome> incomes)
        {
            Tenant.RentIncomes.AddRange(incomes);
        }

        protected override RentIncome NewIncome()
        {
            return new RentIncome();
        }

        protected override void RemoveIncomes()
        {
            Tenant.RentIncomes.RemoveRange(RentContract.RentPayments
                .SelectMany(s => s.RentIncomes));
        }
    }
}
