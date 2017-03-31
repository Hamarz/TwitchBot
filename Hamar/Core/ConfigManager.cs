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
            if(!File.Exists(@"settings.cfg"))
            {
                var stream = File.Create(@"settings.cfg");
                StreamWriter writer = new StreamWriter(stream);

                writer.WriteLine("## Database Login Information ##");
                writer.WriteLine("Database.Hostname = localhost");
                writer.WriteLine("Database.Username = root");
                writer.WriteLine("Database.Password = ascent");
                writer.WriteLine("Database.Database = twitchBot");
                writer.WriteLine("Database.Port = 3306");

                writer.WriteLine("## Twitch Login Information ##");
                writer.WriteLine("Twitch.Username = put your user name here");
                writer.WriteLine("Twitch.Token = put your token here");

                writer.Flush();
                writer.Close();
                writer.Dispose();

                throw new Exception("Config file has been created but is empty, modify settings.cfg file.");
            }

            var lines = File.ReadAllLines(@"settings.cfg");

            foreach(var line in lines)
            {
                if (line.Contains("#"))
                    continue;

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
