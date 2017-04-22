// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;

namespace Dyoub.App.Models.EntityModel.Financial
{
    public class Money
    {
        public decimal Amount { get; private set; }

        public Money(decimal amount = 0)
        {
            Amount = decimal.Round(amount, 2, MidpointRounding.ToEven);
        }

        public static implicit operator Money(decimal amount)
        {
            return new Money(amount);
        }

        public static implicit operator decimal(Money money)
        {
            return money.Amount;
        }

        public Money Add(decimal value)
        {
            return Amount + value;
        }

        public Money AddPercentage(decimal percentValue)
        {
            return Amount + Percentage(percentValue);
        }

        public Money Divide(decimal value)
        {
            return Amount / value;
        }

        public Money Multiply(decimal value)
        {
            return Amount * value;
        }

        public decimal Percentage(decimal percentValue)
        {
            return Amount * percentValue / 100;
        }

        public Money Subtract(decimal value)
        {
            return Amount - value;
        }

        public Money SubtractPercentage(decimal percentValue)
        {
            return Amount - Percentage(percentValue);
        }
    }
}
