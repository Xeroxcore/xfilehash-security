using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using xfilehash.Interface;
using xFilewriter;
using xFilewriter.Interface;

namespace xfilehash
{
    public class XFileHashList : IFileHashList
    {
        private static Lazy<XFileHashList> Instance = new Lazy<XFileHashList>(() => new XFileHashList(new FileWriter()));
        public static XFileHashList _Instance { get { return Instance.Value; } }
        private IFileWriter Filewriter { get; }
        private List<IFileHash> FileHashList { get; }
        private readonly string DirectoryPath =
            $"{Directory.GetCurrentDirectory()}/Security/hash/";
        public XFileHashList(IFileWriter filewriter)
        {
            Filewriter = filewriter;
            FileHashList = LoadFilesHashesFromFile();
        }

        private List<IFileHash> convertList(List<FileHash> data)
        {
            var list = new List<IFileHash>();
            foreach (var obj in data)
            {
                list.Add(obj);
            }
            return list;
        }

        private List<IFileHash> LoadFilesHashesFromFile()
        {
            Filewriter.EnsureThatFilePathExists(DirectoryPath, "filehash.json");
            var text = Filewriter.ReadTextFromFile($"{DirectoryPath}filehash.json");
            var data = JsonConvert.DeserializeObject<List<FileHash>>(text);
            List<IFileHash> newList = new List<IFileHash>();
            if (!Validation.ObjectIsNull(data))
                newList = convertList(data);
            return newList;
        }

        public IFileHash GetFileHashFromList(string fileName)
            => FileHashList.FirstOrDefault(x => x.FileName == fileName);

        public bool FileNameExists(string fileName)
            => FileHashList.Where(x => x.FileName == fileName).Any();

        public void saveFileHashListToFile()
            => Filewriter.AppendTextToFile(
                JsonConvert.SerializeObject(FileHashList), $"{DirectoryPath}/filehash.json"
                , FileMode.Truncate);

        public void AddFileHash(IFileHash fileHash)
        {
            if (FileNameExists(fileHash.FileName))
                throw new Exception("Warning: The give filename is already registerd");
            FileHashList.Add(fileHash);
        }
        public void DeleteFileHash(string fileName)
        {
            var fileHash = GetFileHashFromList(fileName);
            FileHashList.Remove(fileHash);
            saveFileHashListToFile();
        }

    }
}