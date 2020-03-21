using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xfilehash;
using xFilewriter;
using xFilewriter.Interface;

namespace xFilehash.test
{
    [TestClass]
    public class FileHashTest
    {
        IXFileHasher Hasher { get; }
        IFileWriter Filewriter { get; }
        public FileHashTest()
        {
            Filewriter = new FileWriter();
            Hasher = new XFileHasher(Filewriter);
        }

        [TestMethod]
        public void CreateFileHash()
        {
            var directory = Directory.GetCurrentDirectory();
            var filePath = $"{directory}/filehash/filehash.txt";
            Filewriter.EnsureThatFilePathExists($"{directory}/filehash", "filehash.txt");
            Filewriter.AppendTextToFile("Hello world", filePath, FileMode.Open);
            Hasher.AddFileHashToIntegrityStore("testfile", filePath);
            var result = Hasher.FileIntegrityIsIntact("testfile");
            Assert.IsTrue(result);
        }
    }
}
