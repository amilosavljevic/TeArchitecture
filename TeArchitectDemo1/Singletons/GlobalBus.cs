using System;
using TeArchitecture.Shared.Bus;

namespace TeArchitecture.Demo1
{
    public static class GlobalBus
    {
        private static readonly Lazy<IBus> instance = new Lazy<IBus>(()=> throw new NotImplementedException());
        public static IBus Instance => instance.Value;
    }
}
