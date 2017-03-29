// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Collections.Generic;

namespace Dyoub.App.Models.ServiceModel.Mail
{
    public abstract class Mailer
    {
        public string From { get; set; }
        public ICollection<string> Recipients { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Mailer()
        {
            Recipients = new List<string>();
            Subject = "dyoub";
        }

        public abstract void Send();
    }
}
