using System;
using TeArchitecture.Shared;

namespace TeArchitecture.Demo1
{
    public static class Session
    {
        private static readonly Lazy<IChannel> instance = new Lazy<IChannel>(() => throw new NotImplementedException());
        public static IChannel Instance => instance.Value;
    }
}
