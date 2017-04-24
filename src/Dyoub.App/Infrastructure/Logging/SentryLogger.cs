// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using SharpRaven;
using SharpRaven.Data;
using System;
using System.Web.Configuration;

namespace Dyoub.App.Infrastructure.Logging
{
    public class SentryLogger : Logger
    {
        public override void Register(Exception ex)
        {
            string dsn = WebConfigurationManager.AppSettings["Logger:Sentry:DSN"];

            RavenClient ravenClient = new RavenClient(dsn);
            ravenClient.Capture(new SentryEvent(ex));
        }
    }
}
