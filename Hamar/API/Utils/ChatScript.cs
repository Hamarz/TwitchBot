using Hamar.Core;

namespace Hamar.API.Utils
{
    public abstract class ChatScript
    {
        /// <summary>
        /// Same as constructing, can't use own parameters for now.
        /// Must have own flag set in script to use this feature
        /// </summary>
        public virtual void OnInitialized(IClient client) { }
        public virtual void OnInitialized(MysqlDatabase database) { }
        public virtual void OnInitialized(IClient client, MysqlDatabase database) { }
        public virtual void OnChannelMessage(IMessageInfo message, IClient client) { }
        public virtual void OnPrivateMessage(IMessageInfo message, IClient client) { }
    }
}
