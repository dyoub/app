// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethodFees;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dyoub.App.Models.ViewModel.Commercial.PaymentMethods
{
    public class CreatePaymentMethodViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }

        [Required, Range(1, 24)]
        public int? InstallmentLimit { get; set; }

        [Range(0, 31)]
        public int? DaysToReceive { get; set; }

        public bool EarlyReceipt { get; set; }

        public bool Active { get; set; }

        public List<PaymentMethodFeeViewModel> PaymentFees { get; private set; }

        [ValidIf]
        public bool HasNoDuplicateInstallmentCondition
        {
            get
            {
                return !PaymentFees.HasDuplicate(paymentFee => paymentFee.MinimumInstallment);
            }
        }

        public CreatePaymentMethodViewModel()
        {
            PaymentFees = new List<PaymentMethodFeeViewModel>();
        }

        public PaymentMethod MapTo(PaymentMethod paymentMethod)
        {
            paymentMethod.Name = Name;
            paymentMethod.InstallmentLimit = InstallmentLimit.Value;
            paymentMethod.DaysToReceive = DaysToReceive;
            paymentMethod.EarlyReceipt = EarlyReceipt;
            paymentMethod.Active = Active;
            paymentMethod.PaymentMethodFees = PaymentFees.Select(viewModel =>
                viewModel.MapTo(new PaymentMethodFee { PaymentMethod = paymentMethod })).ToList();

            return paymentMethod;
        }
    }
}
