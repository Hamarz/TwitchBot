using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamar.Core
{
    public class ConfigManager
    {
        private Dictionary<string, string> configproperties;

        public ConfigManager()
        {
            configproperties = new Dictionary<string, string>();

            var lines = File.ReadAllLines(@"settings.cfg");

            foreach(var line in lines)
            {
                string key, value;
                key = line.Split('=')[0].Trim(' ');
                value = line.Split('=')[1].Trim(' ');
                configproperties.Add(key, value);
            }
        }

        private string GetValue(string key)
        {
            if (!configproperties.TryGetValue(key, out string value))
                throw new Exception($"There's no property for config {key}");
            return value;
        }

        internal uint GetUInt(string key)
        {
            return uint.Parse(GetValue(key));
        }

        public int GetInt(string key)
        {
            return int.Parse(GetValue(key));
        }
        public bool GetBool(string key)
        {
            return bool.Parse(GetValue(key));
        }
        public string GetString(string key)
        {
            return GetValue(key);
        }
    }
}
