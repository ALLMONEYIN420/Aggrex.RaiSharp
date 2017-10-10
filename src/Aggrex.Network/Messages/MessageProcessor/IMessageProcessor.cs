namespace Aggrex.Network.Messages.MessageProcessor
{
    /// <summary>
    /// Message processers are used to process specific types of messages that are passed on the 
    /// network between nodes.
    /// </summary>
    /// <typeparam name="T">Type of message</typeparam>
    public interface IMessageProcessor<T> where T : BaseMessage
    {
        void ProcessMessage(T message, IRemoteNode remoteNode);
    }
}