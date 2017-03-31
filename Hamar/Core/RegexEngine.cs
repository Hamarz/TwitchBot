using Hamar.API.Utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Hamar.Core
{
    public class RegexEngine
    {
        public delegate void DelegateString(Match m, IClient c);
        public struct CommandStucture
        {
            public string Name;
            public Action<Match, IClient> Method;
        }

        private Dictionary<CommandStucture, Regex> regex; 

        public RegexEngine()
        {
            regex = new Dictionary<CommandStucture, Regex>();
            LoadRegex();
        }

        private void LoadRegex()
        {
            var reader = Global.Database.Query("SELECT * FROM regex_options");

            while(reader.Read())
            {
                var c = new CommandStucture()
                {
                    Name = reader.GetString(1),
                    Method = new Action<Match, IClient>(GetDelegateFromString(reader.GetString(3)))
                };

                var r = new Regex(reader.GetString(2));

                regex.Add(c, r);
            }
        }

        private DelegateString GetDelegateFromString(string methodName)
        {
            DelegateString function = Twitch.Handlers.NotImplemented;

            Type inf = typeof(Twitch.Handlers);
            foreach (var method in inf.GetMethods())
                if (method.Name == methodName)
                    function = (DelegateString)Delegate.CreateDelegate(typeof(DelegateString), method);

            return function;
        }

        public Dictionary<CommandStucture, Regex> GetRegex() { return regex; }
    }
}
