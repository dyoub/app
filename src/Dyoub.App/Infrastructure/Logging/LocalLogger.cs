// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Infrastructure.IO;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Dyoub.App.Infrastructure.Logging
{
    public class LocalLogger : Logger
    {
        private string HtmlTemplate()
        {
            EmbeddedResource html = new EmbeddedResource(
                assembly: GetType().Assembly,
                resourceName: string.Format("{0}.ErrorLogTemplate.html", GetType().Namespace)
            );

            return html.ReadAllText();
        }

        private string HtmlLineBreak(string content)
        {
            return Regex.Replace(content, "\\r?\\n", "<br />");
        }

        public override void Register(Exception ex)
        {
            string logFolder = string.Format("{0}temp\\logs",
                AppDomain.CurrentDomain.BaseDirectory);

            string htmlLogFile = string.Format("{0}\\{1}.html", logFolder, Guid.NewGuid());

            string error = HtmlTemplate()
                .Replace("{{Message}}", HtmlLineBreak(ex.Message))
                .Replace("{{StackTrace}}", HtmlLineBreak(ex.StackTrace));

            Directory.CreateDirectory(logFolder);
            File.WriteAllText(htmlLogFile, error);
            Process.Start(htmlLogFile);
        }
    }
}
