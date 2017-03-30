// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Web.Configuration;
using System.Web.Hosting;

namespace Dyoub.App.Infrastructure.Logging
{
    public class BackgroundLogger : Logger
    {
        private Logger logger;

        public BackgroundLogger()
        {
            switch (WebConfigurationManager.AppSettings["Logger"])
            {
                case "Local": logger = new LocalLogger(); break;
                case "Sentry": logger = new SentryLogger(); break;
                default: throw new ArgumentOutOfRangeException("Unkown logger. Please check your settings.");
            }
        }

        public override void Register(Exception ex)
        {
            HostingEnvironment.QueueBackgroundWorkItem(cancellationToken => logger.Register(ex));
        }
    }
}
