// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Infrastructure.Mail;
using System;

namespace Dyoub.Test.Fakes
{
    public class MailerFake : Mailer
    {
        public bool EmailSent { get; private set; }
        public bool EmailNotSent { get { return !EmailSent; } }

        public override void Send()
        {
            EmailSent = true;
        }
    }
}
