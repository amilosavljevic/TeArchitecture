using System;
using System.Collections.Generic;

namespace TeArchitecture.Shared.Mock
{
    public class ChannelMock : IChannel
    {
        private readonly Dictionary<Type, Delegate> generators = new Dictionary<Type, Delegate>();

        public void Mock<TMessage, TResponse>(Func<TMessage, TResponse> responseGenerator)
        {
            generators[typeof(TMessage)] = responseGenerator;
        }

        public ITask<TResponse> Send<TMessage, TResponse>(TMessage message)
        {            
            var responseGenerator = (Func<TMessage, TResponse>)generators[typeof(TMessage)];
            return Task<TResponse>.FinishedTask(responseGenerator(message));
        }
    }
}
