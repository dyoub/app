// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Security.Cryptography;
using System.Text;

namespace Dyoub.App.Models.ServiceModel.Security
{
    public class Sha256Hash
    {
        private string plainText;
        private string salt;

        public Sha256Hash(string plainText)
            : this(plainText, string.Empty) { }

        public Sha256Hash(string plainText, string salt)
        {
            this.plainText = plainText;
            this.salt = salt;
        }

        public override string ToString()
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plainText + salt);

            using (SHA256Managed sha = new SHA256Managed())
            {
                byte[] hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}