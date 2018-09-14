using Newtonsoft.Json;

namespace Guide.Json
{
    public interface IJsonStorage
    {
        void Store(object obj, string file, Formatting formatting);
        void Store(object obj, string file, bool useIndentations);
        T Get<T>(string file);
        bool JsonExists(string file);
    }
}
