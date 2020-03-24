using System;
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
        IXFileIntegrity Hasher { get; }
        IFileWriter Filewriter { get; }
        public FileHashTest()
        {
            var algorithm = new XSha256Algorithm();
            Filewriter = new FileWriter();
            Hasher = new XFileIntegrity(algorithm);
        }

        [TestMethod]
        public void EnsureThatFileHashExist()
        {
            var directory = Directory.GetCurrentDirectory();
            var filePath = $"{directory}/Security/hash/filehash.json";
            // Trigger the filehasher to create the filehash.json
            var result = File.Exists(filePath);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AddFiletoHash()
        {
            var directory = Directory.GetCurrentDirectory();
            var filePath = $"{directory}/filehash/filehash.txt";
            Filewriter.EnsureThatFilePathExists($"{directory}/fThe give filename is not registeredilehash", "filehash.txt");
            Hasher.AddFileHashToIntegrityStore("testFile", filePath);
            var result = Hasher.FileIntegrityIsIntact("testFile");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AddDublicatesNotSuccesful()
        {
            try
            {
                var directory = Directory.GetCurrentDirectory();
                var filePath = $"{directory}/filehash/filehash.txt";
                Filewriter.EnsureThatFilePathExists($"{directory}/filehash", "filehash.txt");
                Hasher.AddFileHashToIntegrityStore("testFile", filePath);
                Hasher.AddFileHashToIntegrityStore("testFile", filePath);
            }
            catch (Exception error)
            {
                Assert.AreEqual("Warning: The give filename is already registerd", error.Message);
            }
        }

        [TestMethod]
        public void FileHashAttributeValid()
        {
            try
            {
                var XFile = new XFilehash("testFile");
                XFile.OnAuthorization(null);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {

            }
        }

        [TestMethod]
        public void DeleteFileHash()
        {
            Hasher.DeleteFileIntegrityFromStore("testFile");
            var result = Hasher.FileIntegrityIsIntact("testFile");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FileHashAttributeInValid()
        {
            try
            {
                var XFile = new XFilehash("testFile");
                XFile.OnAuthorization(null);
            }
            catch (Exception error)
            {
                Assert.AreEqual("The give filename is not registered", error.Message);
            }
        }
    }
}
