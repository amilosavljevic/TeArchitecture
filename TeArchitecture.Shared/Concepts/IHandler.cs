namespace TeArchitecture.Shared
{
    /// <summary>
    /// Handlers can handle message of provided type.
    /// </summary>
    public interface IHandler<TMessage>
    {
        void Process(TMessage message, ITask task);
    }  
}
