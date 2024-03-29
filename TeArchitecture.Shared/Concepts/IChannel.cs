﻿namespace TeArchitecture.Shared
{
    /// <summary>
    /// Represent simple communication channel. You can send message and receive response asynchronously.
    /// </summary>
    public interface IChannel
    {
        ITask<TResponse> Send<TMessage, TResponse>(TMessage message);
    }
}
