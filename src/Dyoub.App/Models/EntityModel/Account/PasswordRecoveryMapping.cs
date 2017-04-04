// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Account
{
    public class PasswordRecoveryMapping : EntityTypeConfiguration<PasswordRecovery>
    {
        public PasswordRecoveryMapping()
        {
            HasKey(p => p.Token);
        }
    }
}
