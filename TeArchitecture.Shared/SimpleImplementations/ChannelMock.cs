using System;
using System.Collections.Generic;

namespace TeArchitecture.Shared.Mock
{
    public class ChannelMock : IChannel
    {
        private readonly Dictionary<Type, Delegate> generators = new Dictionary<Type, Delegate>();

        public void Add<TMessage, TResponse>(Func<TMessage, TResponse> responseGenerator)
        {
            generators[typeof(Func<TMessage, TResponse>)] = responseGenerator;
        }

        public ITask<TResponse> Send<TMessage, TResponse>(TMessage message, object sender = null)
        {
            // TODO: implement
            return null;
        }
    }
}
