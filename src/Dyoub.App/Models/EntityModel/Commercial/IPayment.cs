// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;
using System;

namespace Dyoub.App.Models.EntityModel.Commercial
{
    public interface IPayment
    {
        int Id { get; set; }
        int NumberOfInstallments { get; }
        PaymentMethod PaymentMethod { get; }
        decimal InstallmentBilling { get; set; }
        decimal Total { get; set; }
        decimal? FeePercentage { get; set; }
        decimal? FeeFixedValue { get; set; }
        decimal BilledAmount { get; set; }
        DateTime Date { get; set; }
    }
}
