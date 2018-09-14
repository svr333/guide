using System;
using System.IO;
using Newtonsoft.Json;

namespace Guide.Json
{
    public class JsonStorage : IJsonStorage
    {
        public JsonStorage()
        {
            ValidateResources();
        }

        private void ValidateResources()
        {
            Directory.CreateDirectory(Constants.JsonDirectory);
        }

        public void Store(object obj, string file, Formatting formatting)
        {
            string json = JsonConvert.SerializeObject(obj, formatting);
            string filePath = Path.Combine(Constants.JsonDirectory, file);
            File.WriteAllText(filePath, json);
        }

        public void Store(object obj, string file, bool useIndentations)
        {
            var formatting = (useIndentations) ? Formatting.Indented : Formatting.None;
            Store(obj, file, formatting);
        }

        public T Get<T>(string file)
        {
            string json = GetOrCreateFileContents(file);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public bool JsonExists(string file)
        {
            string filePath = Path.Combine(Constants.JsonDirectory, file);
            return File.Exists(filePath);
        }

        private string GetOrCreateFileContents(string file)
        {
            string filePath = Path.Combine(Constants.JsonDirectory, file);
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "");
                return "";
            }
            return File.ReadAllText(filePath);
        }
    }
}
