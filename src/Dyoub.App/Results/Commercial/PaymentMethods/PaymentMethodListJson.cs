// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Commercial.PaymentMethods
{
    public class PaymentMethodListJson : JsonResult
    {
        public IEnumerable<PaymentMethod> PaymentMethods { get; private set; }

        public PaymentMethodListJson(IEnumerable<PaymentMethod> paymentMethods)
        {
            PaymentMethods = paymentMethods;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = PaymentMethods.Select(paymentMethod => new
            {
                id = paymentMethod.Id,
                name = paymentMethod.Name,
                active = paymentMethod.Active,
                installmentLimit = paymentMethod.InstallmentLimit
            });

            base.ExecuteResult(context);
        }
    }
}
