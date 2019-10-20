using System;

namespace TeArchitecture.Shared.MVC
{
    public class Model<T> : IDisposable
    {
        public T Data { get; set; }
        protected IBus Bus { get; private set; }

        public Model(T data, IBus bus)
        {
            Data = data;
            Bus = bus;

            OnInit();
        }

        protected virtual void OnInit () {}
        public virtual void Dispose () {}
    }
}
