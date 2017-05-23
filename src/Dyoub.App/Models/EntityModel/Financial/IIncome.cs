// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;

public interface IIncome
{
    int PaymentId { get; set; }
    decimal AmountReceived { get; set; }
    DateTime PaymentDate { get; set; }
    DateTime? ReceivedDate { get; set; }
}
