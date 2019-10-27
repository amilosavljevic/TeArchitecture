using System;

namespace TeArchitecture.Shared
{
    /// <summary>
    /// A bus is abstraction similar to message queue, you Send message trough some channel
    /// and every interested party could react to it. Similar to event system that we currently use,
    /// but message type is used instead of EventType that we used previously.
    /// </summary>
    public interface IBus
    {
        ITask Send<TMessage>(TMessage message, object sender = null);

        // Callbacks
        void Subscribe<TMessage>(Action<TMessage> handler);

        void Unsubscribe<TMessage>(Action<TMessage> handler);       
    }
}
