using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace xfilehash
{
    public sealed class XFilehash : Attribute, IAuthorizationFilter
    {
        private string[] FileNames { get; }
        public XFilehash(params string[] fileNames)
        {
            if (Validation.ObjectIsNull(fileNames))
                throw new NullReferenceException("fileNames is null. Please check your in parameterXFilehash");
            FileNames = fileNames;
        }

        private void IntegrityIsIntact(string fileName)
        {
            var hasher = new XFileIntegrity(new XSha256Algorithm());
            if (!hasher.FileIntegrityIsIntact(fileName))
                throw new Exception($"Error: File integiry check failed for {fileName}");
        }

        private void ValidateFile()
        {
            foreach (var fileName in FileNames)
            {
                if (!XFileHashList._Instance.FileNameExists(fileName))
                    throw new Exception("The give filename is not registered");
                IntegrityIsIntact(fileName);

            }
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            ValidateFile();
        }
    }
}