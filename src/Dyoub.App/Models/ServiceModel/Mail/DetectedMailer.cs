// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Web.Configuration;

namespace Dyoub.App.Models.ServiceModel.Mail
{
    public class DetectedMailer : Mailer
    {
        private Mailer mailer;
        
        public DetectedMailer()
        {
            switch (WebConfigurationManager.AppSettings["Mailer"])
            {
                case "Local": mailer = new LocalMailer(); break;
                case "Google": mailer = new GoogleMailer(); break;
                default: throw new ArgumentOutOfRangeException("Unkown mailer. Please check your 'Settings/Dyoub.config'.");
            }
        }

        public override void Send()
        {
            mailer.From = From;
            mailer.Recipients = Recipients;
            mailer.Subject = Subject;
            mailer.Content = Content;
            mailer.Send();
        }
    }
}
