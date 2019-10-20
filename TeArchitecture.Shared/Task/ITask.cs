﻿using System;

namespace TeArchitecture.Shared
{
    // This maybe should inherit from IAsyncResult, so we can do all kind of async shenanigans.
    public interface ITask
    {
        void Fail(string errorMessage);

        void Fail(IError error);

        void Finish();

        IError Error { get; }
    }

    // This maybe should inherit from IAsyncResult<T>, so we can do all kind of async shenanigans.
    public interface ITask<T>
    {
        void Fail(string errorMessage);

        void Fail(IError error);
        void Finish(T result);

        T Value { get; }
        IError Error { get; }
    }

    public static partial class ITaskExtensions
    {
        public static ITask OnSuccess(this ITask result, Action onSuccess)
        {
            throw new NotImplementedException();
        }

        public static ITask OnFail(this ITask result, Action<ITask> onFail)
        {
            throw new NotImplementedException();
        }

        public static ITask OnFinish(this ITask result, Action<ITask> onFinish)
        {
            throw new NotImplementedException();
        }

        public static ITask<T> OnSuccess<T>(this ITask<T> result, Action<T> onSuccess)
        {
            throw new NotImplementedException();
        }

        public static ITask<T> OnFail<T>(this ITask<T> result, Action<ITask<T>> onFail)
        {
            throw new NotImplementedException();
        }

        public static ITask OnFinish<T>(this ITask result, Action<ITask<T>> onFinish)
        {
            throw new NotImplementedException();
        }
    }
}
