using System.Collections.Generic;

namespace File_Access
{
    public interface IFileDataService
    {
        void InitializeFileTags(string path, string configType);

        bool TryGetFileTags(string path, out Dictionary<string, List<string>> fileTag);
    }
}