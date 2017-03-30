// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Infrastructure.Logging;
using System;

namespace Dyoub.Test.Fakes
{
    public class LoggerFake : Logger
    {
        public bool ErrorRegistered { get; private set; }

        public override void Register(Exception ex)
        {
            ErrorRegistered = true;
        }
    }
}
