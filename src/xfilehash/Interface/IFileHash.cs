namespace xfilehash.Interface
{
    public interface IFileHash
    {
        string FileName { get; set; }
        string Hash { get; set; }
        string FilePath { get; set; }
    }
}