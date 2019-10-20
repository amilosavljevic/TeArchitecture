using TeArchitecture.Shared.Bus;

namespace TeArchitecture.Shared
{
    public interface IBus
    {
        ITask Send<TMessage>(TMessage message, object sender = null);       

        void Subscribe<TMessage>(IHandler<TMessage> handler);

        void Unsubscribe<TMessage>(IHandler<TMessage> handler);


        // Aditionally we could have message that needs to return response/result.

        ITask<TResponse> Send<TMessage, TResponse>(TMessage message, object sender = null);

        void Subscribe<TMessage, TResponse>(IHandler<TMessage, TResponse> handler);

        void Unsubscribe<TMessage, TResponse>(IHandler<TMessage, TResponse> handler);
    }  
}
