using System.Collections.Generic;

namespace UnityBlocks.FSM
{
    public class SharedContext
    {
        private readonly Dictionary<string, object> _data = new();

        public void Set<T>(string key, T value)
        {
            _data[key] = value;
        }

        public T Get<T>(string key)
        {
            return _data.TryGetValue(key, out var value) ? (T) value : default;
        }

        public bool Has(string key) => _data.ContainsKey(key);
    }
}