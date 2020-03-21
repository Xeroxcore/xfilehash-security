using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Newtonsoft.Json;
using xFilewriter.Interface;

namespace xfilehash
{
    public class XFileHasher : IXFileHasher
    {
        public IList<FileHash> FileHashList { get; }
        private IFileWriter FileWriter { get; }
        private readonly string DirectoryPath =
            $"{Directory.GetCurrentDirectory()}/Security/hash/";

        public XFileHasher(IFileWriter fileWriter)
        {
            FileWriter = fileWriter;
            FileHashList = LoadFilesHashesFromFile();
        }

        private List<FileHash> LoadFilesHashesFromFile()
        {
            FileWriter.EnsureThatFilePathExists(DirectoryPath, "filehash.json");
            var list = new List<FileHash>();
            var text = FileWriter.ReadTextFromFile($"{DirectoryPath}filehash.json");
            var newList = JsonConvert.DeserializeObject<List<FileHash>>(text);
            if (!Validation.ObjectIsNull(newList))
                list = newList;
            return list;
        }

        private string GetSHA256FileHash(string filePath)
        {
            using (var SHA256 = SHA256Managed.Create())
            {
                using (var fileStream = File.OpenRead(filePath))
                    return Convert.ToBase64String(SHA256.ComputeHash(fileStream));
            }
        }

        private FileHash GetFileHashFromList(string fileName)
            => FileHashList.FirstOrDefault(x => x.FileName == fileName);

        public bool FileIntegrityIsIntact(string fileName)
        {
            var fileHash = GetFileHashFromList(fileName);
            if (!Validation.ObjectIsNull(fileHash))
            {
                var hash = GetSHA256FileHash(fileHash.FilePath);
                return Validation.StringsAreEqual(hash, fileHash.Hash);
            }
            return false;
        }

        private FileHash CreateFileHash(string filenName, string filePath)
            => new FileHash()
            {
                FileName = filenName,
                FilePath = filePath,
                Hash = GetSHA256FileHash(filePath)
            };

        private void saveFileHashListToFile()
            => FileWriter.AppendTextToFile(
                JsonConvert.SerializeObject(FileHashList), $"{DirectoryPath}/filehash.json"
                , FileMode.Open);

        public void AddFileHashToIntegrityStore(string fileName, string filePath)
        {
            FileHashList.Add(CreateFileHash(fileName, filePath));
            saveFileHashListToFile();
        }

        public void DeleteFileIntegrityFromStore(string fileName)
        {
            FileHashList.Remove(GetFileHashFromList(fileName));
            saveFileHashListToFile();
        }
    }
}
