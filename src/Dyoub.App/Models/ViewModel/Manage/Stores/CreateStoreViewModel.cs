// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Manage.Stores;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Manage.Stores
{
    public class CreateStoreViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }

        public bool Active { get; set; }

        public Store MapTo(Store store)
        {
            store.Name = Name;
            store.Active = Active;

            return store;
        }
    }
}
