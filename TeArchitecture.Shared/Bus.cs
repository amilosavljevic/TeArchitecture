namespace TeArchitecture.Shared
{
    public interface IBus
    {
        IResult<T> Send<T>(T message, object sender = null);

        void Subscribe<T, THandler>() where THandler : IHandler<T>;

        void Unsubscribe<T, THandler>() where THandler : IHandler<T>;
    }

    public interface IHandler<T>
    {
        void Process(T message);
    }
}
