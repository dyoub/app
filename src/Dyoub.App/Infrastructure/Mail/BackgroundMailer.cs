// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Web.Configuration;
using System.Web.Hosting;

namespace Dyoub.App.Infrastructure.Mail
{
    public class BackgroundMailer : Mailer
    {
        private Mailer mailer;
        
        public BackgroundMailer()
        {
            switch (WebConfigurationManager.AppSettings["Mailer"])
            {
                case "Local": mailer = new LocalMailer(); break;
                case "Google": mailer = new GoogleMailer(); break;
                default: throw new ArgumentOutOfRangeException("Unkown mailer. Please check your settings.");
            }
        }

        public override void Send()
        {
            mailer.From = From;
            mailer.Recipients = Recipients;
            mailer.Subject = Subject;
            mailer.Content = Content;

            HostingEnvironment.QueueBackgroundWorkItem(cancellationToken => mailer.Send());
        }
    }
}
