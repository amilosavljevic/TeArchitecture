namespace TeArchitecture.Shared.Bus
{
    public interface IHandler<TMessage>
    {
        void Process(TMessage message, ITask task);
    }

    public interface IHandler<TMessage, TResponse>
    {
        ITask<TResponse> Process(TMessage message, ITask<TResponse> result);
    }
}
