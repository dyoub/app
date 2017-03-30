// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.IO;
using System.Reflection;

namespace Dyoub.App.Infrastructure.IO
{
    public sealed class EmbeddedResource
    {
        private Assembly assembly;
        private string resourceName;

        public EmbeddedResource(Assembly assembly, string resourceName)
        {
            this.assembly = assembly;
            this.resourceName = resourceName;
        }

        public string ReadAllText()
        {
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
