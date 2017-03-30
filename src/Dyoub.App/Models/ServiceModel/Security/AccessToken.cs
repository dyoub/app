// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Linq;

namespace Dyoub.App.Models.ServiceModel.Security
{
    public class AccessToken
    {
        private byte[] time;
        private byte[] key;

        public AccessToken()
        {
            time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            key = Guid.NewGuid().ToByteArray();
        }

        public override string ToString()
        {
            return BitConverter.ToString(key.Concat(time).ToArray()).Replace("-", string.Empty);
        }
    }
}
