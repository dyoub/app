// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;

namespace Dyoub.App.Infrastructure.Logging
{
    public abstract class Logger
    {
        public abstract void Register(Exception ex);
    }
}
