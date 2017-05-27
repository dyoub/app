// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Collections.Generic;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Commercial.Partners
{
    public static class PartnerQuery
    {
        public static IQueryable<Partner> OrderedByName(this IQueryable<Partner> partners)
        {
            return partners.OrderBy(partner => partner.Name);
        }

        public static IQueryable<Partner> WhereId(this IQueryable<Partner> partners, int id)
        {
            return partners.Where(partner => partner.Id == id);
        }

        public static IQueryable<Partner> WhereIdIn(this IQueryable<Partner> partners, IEnumerable<int> ids)
        {
            return partners.Where(partner => ids.Contains(partner.Id));
        }

        public static IQueryable<Partner> WhereNameContains(this IQueryable<Partner> partners, params string[] words)
        {
            foreach (string word in words)
            {
                partners = partners.Where(partner => partner.Name.Contains(word));
            }

            return partners;
        }
    }
}
