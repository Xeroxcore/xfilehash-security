using xfilehash.Interface;

namespace xfilehash
{
    public class FileHash : IFileHash
    {
        public string FileName { get; set; }
        public string Hash { get; set; }
        public string FilePath { get; set; }
    }
}
