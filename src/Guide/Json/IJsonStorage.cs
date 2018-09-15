using Newtonsoft.Json;

namespace Guide.Json
{
    public interface IJsonStorage
    {
        void Store(object obj, string file, Formatting formatting);
        void Store(object obj, string file, bool useIndentations);
        T Get<T>(string file);
        T GetConcrete<T>(string fullFilePath);
        bool JsonExists(string file);
        void DeleteFile(string file);
    }
}
