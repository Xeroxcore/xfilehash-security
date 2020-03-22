namespace xfilehash.Interface
{
    public interface IFileHashList
    {
        IFileHash GetFileHashFromList(string fileName);
        bool FileNameExists(string fileName);
        void saveFileHashListToFile();
        void AddFileHash(IFileHash fileHash);
        void DeleteFileHash(string fileName);
    }
}