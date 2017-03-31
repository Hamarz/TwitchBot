namespace Hamar.API.Utils
{
    public interface IClient
    {
        void Send(string input);
        void SendChannelMessage(string channel, string message);
        void JoinChannel(string channel);
        IClient GetClient();
    }
}
