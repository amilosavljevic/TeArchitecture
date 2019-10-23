﻿using System;

namespace TeArchitecture.Shared.MVC
{
    public class Model<TModel, TData>
        where TModel : Model<TModel, TData>, new()
    { 
        private TData data;

        protected virtual void OnInit() {}

        protected virtual void OnDispose() {}

        #region Singleton n stuff

        private static readonly Lazy<TModel> instance = new Lazy<TModel>(() => new TModel());

        public static TData Data
        {
            get { return instance.Value.data; }
            private set { instance.Value.data = value; }
        }

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
    }
}
