﻿// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Account.Users;
using Dyoub.App.Infrastructure.Security;
using System;

namespace Dyoub.Test.Factories.Account
{
    public class UserFactory
    {
        public static User User()
        {
            User user = new User();
            user.Name = "User Test";
            user.Email = "user@test.com";
            user.Password = "12345678";

            return user;
        }

        public static User User(Tenant tenant)
        {
            User user = User();
            user.Salt = Guid.NewGuid().ToString("N");
            user.Password = new Sha256Hash(user.Password, user.Salt).ToString();
            user.LastChangePassword = DateTime.UtcNow.Date.AddDays(-1);
            user.Tenant = tenant;

            return user;
        }

        public static User AuthenticatedUser(Tenant tenant)
        {
            User user = User(tenant);
            user.Token = new AccessToken().ToString();

            return user;
        }
    }
}
