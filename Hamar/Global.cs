using Hamar.API.Plugin;
using Hamar.Core;
using System.Threading;

namespace Hamar
{
    public static class Global
    {
        public static ConfigManager Config { get; private set; }
        public static MysqlDatabase Database { get; private set; }
        public static RegexEngine Regex { get; private set; }
        public static Twitch.TwitchClient Twitch { get; private set; }
        public static PluginManager PluginManager { get; private set; }


        public static void Initialize()
        {
            Config = new ConfigManager();
            Database = new MysqlDatabase(Config.GetString("Database.Hostname"), 
                                         Config.GetString("Database.Username"),
                                         Config.GetString("Database.Password"), Config.GetString("Database.Database"),
                                         Config.GetUInt("Database.Port"));
            Regex = new RegexEngine();
            Twitch = new Hamar.Twitch.TwitchClient();
            PluginManager = new PluginManager();
            if(Twitch.Connect(Config.GetString("Twitch.Username"), Config.GetString("Twitch.Password")))
                PluginManager.Initialize();


            new Thread(Info) { IsBackground = true }.Start();
        }

        private static void Info()
        {
            while(true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
