using System;
using TeArchitecture.Shared;

namespace TeArchitecture.Demo1
{
    public class Model<TModel, TData>
        where TModel : Model<TModel, TData>, new()
    {
        private TData data;

        protected virtual void OnInit() {}

        protected virtual void OnDispose() {}

        #region Singleton n stuff

        private static readonly Lazy<TModel> instance = new Lazy<TModel>(() => new TModel());

        public static TData Data => instance.Value.data;

		public static void Init(TData data)
        {
            instance.Value.data = data;
            instance.Value.OnInit();
        }

        public static void Clear()
        {
            instance.Value.OnDispose();
        }

        #endregion

        #region Handler registration        
        
        protected void On<TMessage>(Func<IHandler<TMessage>> handlerFactory)
        {
            // TODO: un-subscribe.
            GlobalBus.Instance.Subscribe<TMessage>(
               (message) =>
               {
                   var handler = handlerFactory();
                   handler.Process(message);
               });
        }

        #endregion
    }
}
