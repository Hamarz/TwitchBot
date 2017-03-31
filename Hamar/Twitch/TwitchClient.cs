using Hamar.API.Utils;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Hamar.Twitch
{
    public class TwitchClient : IClient
    {
        private TcpClient session;
        private StreamWriter writer;
        private StreamReader reader;

        public TwitchClient()
        {

        }

        public bool Connect(string username, string oAuth)
        {
            session = new TcpClient("irc.twitch.tv", 6667);
            writer = new StreamWriter(session.GetStream())
            {
                AutoFlush = true
            };
            reader = new StreamReader(session.GetStream());

            writer.WriteLine($"PASS {oAuth}{Environment.NewLine} NICK {username.ToLower()}{Environment.NewLine}");

            new Thread(Listen) { IsBackground = true }.Start();

            // Plugin onconnected trigger.
            return true;
        }

        private void Listen()
        {
            while(session != null && session.Connected)
            {
                if (session.Available <= 0 && reader.Peek() < 0)
                    continue;

                var message = reader.ReadLine();
                Task.Run(() => HandleData(message));
            }
        }

        private void HandleData(string message)
        {

            // Do the regex engine stuff.

            foreach(var option in Global.Regex.GetRegex())
            {
                var match = option.Value.Match(message);

                if (!match.Success) continue;

                option.Key.Method.Invoke(match, this);
            }
        }

        public void Send(string input)
        {
            if (!session.Connected)
                return;

            writer.WriteLine($"{input}{Environment.NewLine}");
        }

        public void SendChannelMessage(string channel, string message)
        {
            Send($"PRIVMSG #{channel} :{message}");
        }

        public void JoinChannel(string channel)
        {
            Send($"JOIN #{channel}");
        }

        public void SendPing(string ip)
        {
            Send($"PONG {ip}");
        }

        public IClient GetClient()
        {
            return this;
        }
    }
}
