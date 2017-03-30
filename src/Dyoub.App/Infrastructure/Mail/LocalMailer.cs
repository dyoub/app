// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Diagnostics;
using System.IO;

namespace Dyoub.App.Infrastructure.Mail
{
    public class LocalMailer : Mailer
    {
        public LocalMailer()
        {
            From = "dev@dyoub.com";
        }

        private string Template
        {
            get
            {
                string resourceName = string.Format("{0}.LocalMail.html", GetType().Namespace);

                using (Stream stream = GetType().Assembly.GetManifestResourceStream(resourceName))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        public override void Send()
        {
            DateTime sendDate = DateTime.Now;
            string mailbox = string.Format("{0}temp\\mailbox", AppDomain.CurrentDomain.BaseDirectory);
            string htmlFilePath = string.Format("{0}/{1:yyyyMMddHHmmss}.html", mailbox, sendDate);

            string mailMessage = Template
                .Replace("{{From}}", From)
                .Replace("{{To}}", string.Join(",", Recipients))
                .Replace("{{Date}}", string.Format("{0} at {1}", sendDate.ToShortDateString(), sendDate.ToShortTimeString()))
                .Replace("{{Subject}}", Subject)
                .Replace("{{Content}}", Content);

            Directory.CreateDirectory(mailbox);
            File.WriteAllText(htmlFilePath, mailMessage);
            Process.Start(htmlFilePath);
        }
    }
}
