namespace TeArchitecture.Shared
{
    /// <summary>
    /// Represent simple communication channel. You can send message and receive response.
    /// </summary>
    public interface IChannel
    {
        ITask<TResponse> Send<TMessage, TResponse>(TMessage message, object sender = null);
    }
}
