﻿using System;

namespace TeArchitecture.Shared.Bus
{
    public interface IBus
    {
        ITask Send<TMessage>(TMessage message, object sender = null);

        // Callbacks
        void Subscribe<TMessage>(Action<TMessage> handler);

        void Unsubscribe<TMessage>(Action<TMessage> handler);

        // Handlers. This should not be here, I think
        void Subscribe<TMessage>(Func<IHandler<TMessage>> handlerFactory);

        void Unsubscribe<TMessage>(Func<IHandler<TMessage>> handlerFactory);
    }

    public static class IBusExtensions
    {
        // This reads better I think, I want to try it.
        public static void On<TMessage>(this IBus bus, Func<IHandler<TMessage>> handlerFactory) => bus.Subscribe<TMessage>(handlerFactory);
    }

    public interface IChannel
    {
        ITask<TResponse> Send<TMessage, TResponse>(TMessage message, object sender = null);
    }
}
