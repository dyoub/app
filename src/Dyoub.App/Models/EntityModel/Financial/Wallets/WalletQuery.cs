// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Linq;

namespace Dyoub.App.Models.EntityModel.Financial.Wallets
{
    public static class WalletQuery
    {
        public static IQueryable<Wallet> OrderByName(this IQueryable<Wallet> wallets)
        {
            return wallets.OrderBy(wallet => wallet.Name);
        }
        
        public static IQueryable<Wallet> WhereNameContains(this IQueryable<Wallet> wallets, params string[] words)
        {
            foreach (string word in words)
            {
                wallets = wallets.Where(wallet => wallet.Name.Contains(word));
            }

            return wallets;
        }
        
        public static IQueryable<Wallet> WhereId(this IQueryable<Wallet> wallets, int id)
        {
            return wallets.Where(wallet => wallet.Id == id);
        }
    }
}
