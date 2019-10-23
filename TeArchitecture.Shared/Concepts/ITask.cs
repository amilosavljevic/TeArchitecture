using System;

namespace TeArchitecture.Shared
{
    /// <summary>
    /// Abstraction for asynchronous task.
    /// </summary>
    public interface ITask
    {
        event Action<ITask> Done;
        
        void Fail(IError error);

        void Finish();

        IError Error { get; }
    }

    public interface ITask<T>
    {
        event Action<ITask<T>> Done;        

        void Fail(IError error);
        void Finish(T result);

        T Value { get; }
        IError Error { get; }
    }

    public static partial class ITaskExtensions
    {
        public static void Fail(this ITask task, string errorMessage)
        {
            task.Fail(new Error(errorMessage));
        }

        public static void Fail<T>(this ITask<T> task, string errorMessage)
        {
            task.Fail(new Error(errorMessage));
        }

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
