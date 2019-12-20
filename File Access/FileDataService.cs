using Entities;
using Support;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;


namespace File_Access
{
    public class FileDataService : IFileDataService
    {
        private readonly Dictionary<string, XmlNodeList> _fileTagsDetails;

        public Dictionary<string, List<string>> FileTags { get; set; } = new Dictionary<string, List<string>>();

        public void InitializeFileTags(string path, string configType)
        {
            FileTags.Clear();
            _fileTagsDetails.Clear();
            GetAllConfigFiles(path, configType)
                .Result
                .ForEach(x => _fileTagsDetails.Add(x, LoadXMLAsync(x).GetAwaiter().GetResult()));
            GetFileTags();
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
            FileTags.Clear();
            _fileTagsDetails.ForEach(fileTag =>
            {
                var nodeList = new List<string>() { };
                _fileTagsDetails.TryGetValue(fileTag.Key, out XmlNodeList nodes);
                nodes.ForEachXML<XmlNode>(xmlNode => nodeList.Add(xmlNode.Name.ToString()));
                FileTags.Add(fileTag.Key, nodeList);
            });
            return FileTags;
        }

        private List<string> GetFileTagByPath(string path) => FileTags.SingleOrDefault(x => x.Key == path).Value;

        private async Task<List<string>> GetAllConfigFiles(string path, string configType)
        {
            return await Task.Run(() =>
                Directory
                .EnumerateFiles(path, $"*.{Constants.fileExtention}", SearchOption.AllDirectories)
                .Where(s => FilterConfigTypes(configType).Contains(Path.GetFileNameWithoutExtension(s))).ToList());
        }

        private async Task<XmlNodeList> LoadXMLAsync(string path)
        {
            var mDocument = new XmlDocument();
            mDocument.Load(path);
            return await Task.Run(() => mDocument.DocumentElement.SelectNodes("*"));
        }

        private List<string> FilterConfigTypes(string configType)
        {
            var filterTagTypes = new List<string>() { };
            if (Constants.configFileTypes.Contains(configType))
            {
                Constants.configFileTypes.ForEach(x =>
                {
                    if (x.ToLowerInvariant() == configType.ToLowerInvariant())
                        filterTagTypes.Add(x);
                });
            }
            return filterTagTypes;
        }
    }
}