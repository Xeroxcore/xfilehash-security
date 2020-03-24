using System;
using xfilehash.Interface;

namespace xfilehash
{
    public class XFileHasher : IXFileHasher
    {
        private readonly XFileHashList FileHashList = XFileHashList._Instance;
        private IXHashAlgorithm Algorithm { get; }
        public XFileHasher(IXHashAlgorithm algorithm)
        {
            if (Validation.ObjectIsNull(algorithm))
                throw new Exception("Your algorithm is null");
            Algorithm = algorithm;
        }

        public bool FileIntegrityIsIntact(string fileName)
        {
            var fileHash = FileHashList.GetFileHashFromList(fileName);
            if (!Validation.ObjectIsNull(fileHash))
            {
                var hash = Algorithm.GetFileHash(fileHash.FilePath);
                return Validation.StringsAreEqual(hash, fileHash.Hash);
            }
            return false;
        }

        private IFileHash CreateFileHash(string filenName, string filePath)
            => new FileHash()
            {
                FileName = filenName,
                FilePath = filePath,
                Hash = Algorithm.GetFileHash(filePath)
            };

        public void AddFileHashToIntegrityStore(string fileName, string filePath)
        {
            FileHashList.AddFileHash(CreateFileHash(fileName, filePath));
            XFileHashList._Instance.saveFileHashListToFile();
        }

        public void DeleteFileIntegrityFromStore(string fileName)
        {
            FileHashList.DeleteFileHash(fileName);
            FileHashList.saveFileHashListToFile();
        }
    }
}
