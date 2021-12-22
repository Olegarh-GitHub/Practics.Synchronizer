using System.Collections.Generic;

namespace Practics.Synchronizer.Core.Extensions
{
    public static class DictionaryConverterExtensions
    {
        public static int? Convert(this Dictionary<string, int> dictionary, string key)
        {
            if (key == default)
            {
                return default;
            }

            var isValueExists = dictionary.TryGetValue(key, out var value);

            if (!isValueExists)
            {
                return default;
            }
            
            return value;
        }

        public static string Convert(this Dictionary<int, string> dictionary, int key)
        {
            if (key == default)
            {
                return default;
            }

            var isValueExists = dictionary.TryGetValue(key, out var value);

            if (!isValueExists)
            {
                return default;
            }
            
            return value;
        }
    }
}