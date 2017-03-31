using Hamar.API.Plugin;
using Hamar.Core;
using System.Threading;

namespace Hamar
{
    public static class Global
    {
        public static MysqlDatabase Database { get; private set; }
        public static RegexEngine Regex { get; private set; }
        public static Twitch.TwitchClient Twitch { get; private set; }
        public static PluginManager PluginManager { get; private set; }


        public static void Initialize()
        {
            Database = new MysqlDatabase("localhost", "root", "ascent", "twitchBot", 3306);
            Regex = new RegexEngine();
            Twitch = new Hamar.Twitch.TwitchClient();
            PluginManager = new PluginManager();
            if(Twitch.Connect("xtume", "oauth:dnurocgjxbhpiyozfal66vz876kuev"))
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
