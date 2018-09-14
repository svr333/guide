using System.Collections.Generic;
using System.Linq;
using Guide.Json;

namespace Guide.Language
{
    public class JsonLanguage : ILanguage
    {
        private readonly IJsonStorage jsonStorage;
        private Dictionary<string, string[]> pools;

        public JsonLanguage(IJsonStorage jsonStorage)
        {
            this.jsonStorage = jsonStorage;
            RestorePools();
        }

        public string GetPhrase(string key)
        {
            if(!pools.Keys.Contains(key))
            {
                return string.Empty;
            }

            return pools[key].GetRandomElement();
        }

        private void RestorePools()
        {
            pools = jsonStorage.GetConcrete<Dictionary<string, string[]>>(Constants.LanguageFile);
        }
    }
}
