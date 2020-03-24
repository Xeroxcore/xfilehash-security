using System;

namespace xfilehash
{
    public sealed class XFilehash : Attribute
    {
        private string[] FileNames { get; }
        public XFilehash(params string[] fileNames)
        {
            if (Validation.ObjectIsNull(fileNames))
                throw new NullReferenceException("fileNames is null. Please check your in parameterXFilehash");
            FileNames = fileNames;
            ValidateFile();
        }

        private void IntegrityIsIntact(string fileName)
        {
            var hasher = new XFileHasher(new XSha256Algorithm());
            if (!hasher.FileIntegrityIsIntact(fileName))
                throw new Exception($"Error: Failintegiry check failed for {fileName}");
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
    }
}