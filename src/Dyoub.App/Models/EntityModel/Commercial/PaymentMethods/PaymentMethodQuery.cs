// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Data.Entity;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Commercial.PaymentMethods
{
    public static class PaymentMethodQuery
    {
        public static IQueryable<PaymentMethod> Actives(this IQueryable<PaymentMethod> paymentMethods)
        {
            return paymentMethods.Where(paymentMethod => paymentMethod.Active);
        }

        public static IQueryable<PaymentMethod> IncludeFees(this IQueryable<PaymentMethod> paymentMethods)
        {
            return paymentMethods.Include(paymentMethod => paymentMethod.PaymentMethodFees);
        }

        public static IQueryable<PaymentMethod> OrderByName(this IQueryable<PaymentMethod> paymentMethods)
        {
            return paymentMethods.OrderBy(paymentMethod => paymentMethod.Name);
        }

        public static IQueryable<PaymentMethod> WhereId(this IQueryable<PaymentMethod> paymentMethods, int id)
        {
            return paymentMethods.Where(paymentMethod => paymentMethod.Id == id);
        }

        public static IQueryable<PaymentMethod> WhereNameContains(this IQueryable<PaymentMethod> paymentMethods, params string[] words)
        {
            foreach (string word in words)
            {
                paymentMethods = paymentMethods.Where(paymentMethod => paymentMethod.Name.Contains(word));
            }

            return paymentMethods;
        }
    }
}
