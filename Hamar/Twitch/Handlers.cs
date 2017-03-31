using Hamar.API.Utils;
using System.Text.RegularExpressions;

namespace Hamar.Twitch
{
    public static class Handlers
    {
        public static void NotImplemented(Match m, IClient c)
        {

        }

        public static void HandleMessage(Match m, IClient c)
        {
            MessageInfo info = new MessageInfo()
            {
                username = m.Groups["name"].Value,
                channel = m.Groups["channel"].Value,
                message = m.Groups["message"].Value
            };

            foreach (var p in Global.PluginManager.chatscripts)
            {
                p.Value.OnChannelMessage(info, c);
            }
        }

        public static void HandlePing(Match m, IClient c)
        {

        }
    }
}
