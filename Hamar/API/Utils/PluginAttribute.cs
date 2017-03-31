using System;

namespace Hamar.API.Utils
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginAttribute : Attribute
    {
        public string Name { get; private set; }
        public bool Initialize { get; private set; }
        public ConstructType SetParameters { get; private set; }

        public PluginAttribute(string name, bool init = false, ConstructType type = ConstructType.RequireNothing)
        {
            Name = name;
            Initialize = init;
            SetParameters = type;
        }
    }
}
