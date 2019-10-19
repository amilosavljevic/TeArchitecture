namespace TeArchitecture.Shared
{
    public interface IBus
    {
        IResult Send<TMessage>(TMessage message, object sender = null);       

        void Subscribe<TMessage>(IHandler<TMessage> handler);

        void Unsubscribe<TMessage>(IHandler<TMessage> handler);


        // Aditionally we could have message that needs to return response/result.

        IResult<TResponse> Send<TMessage, TResponse>(TMessage message, object sender = null);

        void Subscribe<TMessage, TResponse>(IHandler<TMessage, TResponse> handler);

        void Unsubscribe<TMessage, TResponse>(IHandler<TMessage, TResponse> handler);
    }

    public interface IHandler<TMessage>
    {
        IResult Process(TMessage message, IResultBuilder result);
    }

    public interface IHandler<TMessage, TResponse>
    {
        IResult<TResponse> Process(TMessage message, IResultBuilder<TResponse> result);
    }

    public interface IResultBuilder
    {
        IResult Fail(string message);
        IResult Finish();
        IResult Wait();
    }

    // This maybe should inherit from ITask<T>, so we can do all kind of async shenanigans.
    public interface IResultBuilder<T>
    {
        IResult<T> Fail(string message);
        IResult<T> Finish(T result);
    }
}
