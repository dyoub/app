// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Threading;
using System.Web.Hosting;

namespace Dyoub.App.Infrastructure
{
    public class BackgroundTask
    {
        public virtual void Execute(Action<CancellationToken> action)
        {
            HostingEnvironment.QueueBackgroundWorkItem(cancellationToken => action(cancellationToken));
        }
    }
}
