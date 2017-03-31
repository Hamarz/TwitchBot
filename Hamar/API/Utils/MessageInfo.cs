namespace Hamar.API.Utils
{
    public interface IMessageInfo
    {
        string Username { get; }
        string Channel { get; }
        string Message { get; }
    }

    public struct MessageInfo : IMessageInfo
    {
        public string username;
        public string channel;
        public string message;

        string IMessageInfo.Username => username;

        string IMessageInfo.Channel => channel;

        string IMessageInfo.Message => message;
    }
}
