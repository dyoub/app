// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Data.Entity;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Commercial.RentContracts
{
    public static class RentContractQuery
    {
        public static IQueryable<RentContract> IncludeCustomer(this IQueryable<RentContract> rentContracts)
        {
            return rentContracts.Include(rentContract => rentContract.Customer);
        }

        public static IQueryable<RentContract> IncludePaymentMethods(this IQueryable<RentContract> rentContracts)
        {
            return rentContracts.Include(rentContract => rentContract.RentPayments
                .Select(rentPayment => rentPayment.PaymentMethod));
        }

        public static IQueryable<RentContract> IncludePaymentMethodsAndFees(this IQueryable<RentContract> rentContracts)
        {
            return rentContracts.Include(rentContract => rentContract
                .RentPayments.Select(rentPayment => rentPayment.PaymentMethod.PaymentMethodFees));
        }

        public static IQueryable<RentContract> IncludeRentedProducts(this IQueryable<RentContract> rentContracts)
        {
            return rentContracts.Include(rentContract => rentContract.RentedProducts);
        }

        public static IQueryable<RentContract> IncludeRentIncomes(this IQueryable<RentContract> rentContracts)
        {
            return rentContracts.Include(rentContract => rentContract.RentPayments
                .Select(rentPayment => rentPayment.RentIncomes));
        }

        public static IQueryable<RentContract> IncludeRentPayments(this IQueryable<RentContract> rentContracts)
        {
            return rentContracts.Include(rentContract => rentContract.RentPayments);
        }

        public static IQueryable<RentContract> IncludeStore(this IQueryable<RentContract> rentContracts)
        {
            return rentContracts.Include(rentContract => rentContract.Store);
        }

        public static IQueryable<RentContract> IncludeWallet(this IQueryable<RentContract> rentContracts)
        {
            return rentContracts.Include(rentContract => rentContract.Wallet);
        }

        public static IQueryable<RentContract> OrderByMostRecent(this IQueryable<RentContract> rentContracts)
        {
            return rentContracts.OrderByDescending(rentContract => rentContract.StartDate);
        }

        public static IQueryable<RentContract> StartedThisMonth(this IQueryable<RentContract> rentContracts)
        {
            DateTime startDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            DateTime endDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month,
                DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month));

            return rentContracts.WhereStartAt(startDate).WhereEndAt(endDate);
        }
        
        public static IQueryable<RentContract> WhereCustomerId(this IQueryable<RentContract> rentContracts, int? customerId)
        {
            if (customerId == null)
            {
                return rentContracts;
            }

            return rentContracts.Where(rentContract => rentContract.CustomerId == customerId);
        }

        public static IQueryable<RentContract> WhereEndAt(this IQueryable<RentContract> rentContracts, DateTime? date)
        {
            if (date == null)
            {
                return rentContracts;
            }

            date = date.Value.Date.AddDays(1);

            return rentContracts.Where(rentContract => rentContract.EndDate < date);
        }

        public static IQueryable<RentContract> WhereId(this IQueryable<RentContract> rentContracts, int id)
        {
            return rentContracts.Where(rentContract => rentContract.Id == id);
        }

        public static IQueryable<RentContract> WhereStartAt(this IQueryable<RentContract> rentContracts, DateTime? date)
        {
            if (date == null)
            {
                return rentContracts;
            }

            date = date.Value.Date;

            return rentContracts.Where(rentContract => rentContract.StartDate >= date);
        }

        public static IQueryable<RentContract> WhereStoreId(this IQueryable<RentContract> rentContracts, int? storeId)
        {
            if (storeId == null)
            {
                return rentContracts;
            }

            return rentContracts.Where(rentContract => rentContract.StoreId == storeId);
        }

        public static IQueryable<RentContract> WhereWalletId(this IQueryable<RentContract> rentContracts, int? walletId)
        {
            if (walletId == null)
            {
                return rentContracts;
            }

            return rentContracts.Where(rentContract => rentContract.WalletId == walletId);
        }
    }
}
