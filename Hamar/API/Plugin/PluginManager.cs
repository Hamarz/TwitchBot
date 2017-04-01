using Hamar.API.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Hamar.API.Plugin
{
    public class PluginManager
    {
        public Dictionary<string, ChatScript> chatscripts;

        public PluginManager()
        {
            chatscripts = new Dictionary<string, ChatScript>();
        }

        public void Initialize()
        {
            Global.Logger.Log("Loading plugins...");

            if (!Directory.Exists("Plugins"))
            {
                Global.Logger.Log("No plugins folder was found, creating one now.");
                Directory.CreateDirectory("Plugins");
            }

            var directories = Directory.GetDirectories("Plugins");
            var files = new List<string>();

            foreach(var dir in directories)
            {
                Task.Run(() => CompileFromDirectory(dir));
            }

            Global.Logger.Log("Finished loading plugins.");
        }

        private void CompileFromDirectory(string dir)
        {
            var files = new List<string>();
            GetFilesFromDirectory(dir, ref files);

            var pluginAssembly = new CodeCompiler(files.ToArray(), new string[] { "Hamar.dll" }).Compile();
            var types = pluginAssembly.GetTypes();


            int count = 0;
            foreach(var type in types)
            {
                if(type.IsClass && type.IsDefined(typeof(PluginAttribute), true))
                {
                    var attribute = Attribute.GetCustomAttribute(type, typeof(PluginAttribute), true) as PluginAttribute;

                    var plugin = (ChatScript)Activator.CreateInstance(type);

                    chatscripts.Add(attribute.Name, plugin);
                    count++;

                    if (attribute.Initialize)
                    {
                        Console.WriteLine(attribute.SetParameters.ToString());
                        if (!attribute.SetParameters.HasFlag(ConstructType.RequireNothing))
                        {
                            if (attribute.SetParameters.HasFlag(ConstructType.RequireIrcControl))
                            {
                                if (!attribute.SetParameters.HasFlag(ConstructType.RequireDatabaseControl))
                                {
                                    Task.Run(() => plugin.OnInitialized(Global.Twitch.GetClient()));
                                    continue;
                                }

                                Task.Run(() => plugin.OnInitialized(Global.Twitch.GetClient(), Global.Database) );
                                continue;
                            }

                            Task.Run(() => plugin.OnInitialized(Global.Database));
                        }
                    }
                }
            }

            Global.Logger.Log($"{count} plugins were found and initialized..");
        }

        private void GetFilesFromDirectory(string directory, ref List<string> files)
        {
            List<string> filestoCompile = new List<string>();

            foreach(var subdir in Directory.GetDirectories(directory))
            {
                GetFilesFromDirectory(subdir, ref files);
            }

            var dirFiles = Directory.GetFiles(directory, "*.cs");
            foreach (var dFile in dirFiles)
                files.Add(dFile);
        }
    }
}
