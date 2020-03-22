using System;

namespace xfilehash
{
    public sealed class XFilehash : Attribute
    {
        private string[] FileNames { get; }
        public XFilehash(params string[] fileNames)
        {
            FileNames = fileNames;
            ValidateFile();
        }

        private void ValidateFile()
        {
            foreach (var fileName in FileNames)
                if (!XFileHashList._Instance.FileNameExists(fileName))
                    throw new Exception("The give file does not exist or has been compromised");
        }
    }
}