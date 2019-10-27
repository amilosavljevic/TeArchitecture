using System;
using System.Collections.Generic;

namespace TeArchitecture.Shared.Mock
{
    public class BusMock : IBus
    {
        public readonly List<object> SentMessages = new List<object>();

        public ITask Send<TMessage>(TMessage message, object sender = null)
        {
            SentMessages.Add(message);
            return null;
        }

        public void Subscribe<TMessage>(Action<TMessage> handler)
        {
            throw new NotImplementedException();
        }

        public void Subscribe<TMessage>(Func<IHandler<TMessage>> handlerFactory)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<TMessage>(Action<TMessage> handler)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<TMessage>(Func<IHandler<TMessage>> handlerFactory)
        {
            throw new NotImplementedException();
        }
    }
}
