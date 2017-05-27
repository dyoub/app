// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial;
using System;
using System.Collections.Generic;

namespace Dyoub.App.Models.EntityModel.Financial
{
    public interface IInvoice
    {
        DateTime? ConfirmationDate { get; set; }
        decimal Total { get; set; }

        IEnumerable<IPayment> Payments { get; }
    }
}
