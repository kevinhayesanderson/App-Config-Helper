using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Entities;
using Support;

namespace File_Access
{
    public class FileDataService : IFileDataService
    {
        private Dictionary<string, XmlNodeList> _fileTagsDetails;

        private Dictionary<string, List<string>> FileTags { get; set; } = new Dictionary<string, List<string>>();

        public void InitializeFileTags(string path, string configType)
        {
            _fileTagsDetails = new Dictionary<string, XmlNodeList>();
            GetAllConfigFiles(path, configType)
                .Result
                .ForEach(x => _fileTagsDetails.Add(x, LoadXmlAsync(x).GetAwaiter().GetResult()));
            FileTags.Clear();
            FileTags = GetFileTags();
        }

        public bool TryGetFileTags(string path, out Dictionary<string, List<string>> fileTag)
        {
            var fileTags = GetFileTagByPath(path);
            if (!fileTags?.Any() ?? false)
            {
                fileTag = new Dictionary<string, List<string>>
                {
                    { path, fileTags }
                };
                return true;
            }
            fileTag = new Dictionary<string, List<string>>();
            return false;
        }

        private Dictionary<string, List<string>> GetFileTags()
        {
            var fileTags = new Dictionary<string, List<string>>();
            _fileTagsDetails.ForEach(fileTag =>
            {
                var nodeList = new List<string>();
                _fileTagsDetails.TryGetValue(fileTag.Key, out var nodes);
                nodes.ForEachXml<XmlNode>(xmlNode => nodeList.Add(xmlNode.Name.ToString()));
                fileTags.Add(fileTag.Key, nodeList);
            });
            return fileTags;
        }

        private List<string> GetFileTagByPath(string path) => FileTags.SingleOrDefault(x => x.Key == path).Value;

        private static async Task<List<string>> GetAllConfigFiles(string path, string configType)
        {
            return await Task.Run(() =>
                Directory
                .EnumerateFiles(path, $"*.{Constants.FileExtention}", SearchOption.AllDirectories)
                .Where(s => FilterConfigTypes(configType).Contains(Path.GetFileNameWithoutExtension(s))).ToList());
        }

        private static async Task<XmlNodeList> LoadXmlAsync(string path)
        {
            var mDocument = new XmlDocument();
            mDocument.Load(path);
            return await Task.Run(() => mDocument.DocumentElement?.SelectNodes("*"));
        }

        private static List<string> FilterConfigTypes(string configType)
        {
            var filterTagTypes = new List<string>();
            if (Constants.ConfigFileTypes.Contains(configType))
            {
                Constants.ConfigFileTypes.ForEach(x =>
                {
                    if (string.Equals(x, configType, StringComparison.InvariantCultureIgnoreCase))
                        filterTagTypes.Add(x);
                });
            }
            return filterTagTypes;
        }
    }
}