// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Effort;

namespace Dyoub.Test.Contexts
{
    public class InMemoryContext : ApplicationContext
    {
        public InMemoryContext() : base(DbConnectionFactory.CreateTransient()) { }
    }
}
