using System;
using System.IO;
using System.Security.Cryptography;
using xfilehash.Interface;

namespace xfilehash
{
    public class XSha256Algorithm : IXHashAlgorithm
    {
        public string GetFileHash(string filePath)
        {
            using (var SHA256 = SHA256Managed.Create())
            {
                using (var fileStream = File.OpenRead(filePath))
                    return Convert.ToBase64String(SHA256.ComputeHash(fileStream));
            }
        }
    }
}