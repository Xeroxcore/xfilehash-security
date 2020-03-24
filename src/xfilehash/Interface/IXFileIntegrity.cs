namespace xfilehash
{
    public interface IXFileIntegrity
    {
        bool FileIntegrityIsIntact(string fileName);
        void AddFileHashToIntegrityStore(string fileName, string filePath);
        void DeleteFileIntegrityFromStore(string fileName);
    }
}