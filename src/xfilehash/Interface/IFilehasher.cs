namespace xfilehash
{
    public interface IXFileHasher
    {
        bool FileIntegrityIsIntact(string fileName);
        void AddFileHashToIntegrityStore(string fileName, string filePath);
        void DeleteFileIntegrityFromStore(string fileName);
    }
}