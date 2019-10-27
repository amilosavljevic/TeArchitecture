using System;

namespace TeArchitecture.Shared
{
    /// <summary>
    /// Abstraction for asynchronous task. Used to hide complexity of task that could be executed now or somewhere in future.
    /// </summary>
    public interface ITask
    {
        event Action<ITask> Done;
        
        void Fail(IError error);

        void Finish();

        TaskState State { get; }

        IError Error { get; }
    }

    /// <summary>
    /// Abstraction for asynchronous task that should return result. Used to hide complexity of task that could be executed now or somewhere in future.
    /// </summary>
    public interface ITask<T>
    {
        event Action<ITask<T>> Done;        

        void Fail(IError error);
        void Finish(T result);

        TaskState State { get; }
        T Result { get; }
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

        public static ITask OnSuccess(this ITask task, Action onSuccess)
        {
            if (onSuccess == null) return task;

            task.Done += (t) =>
            {
                if (t.State == TaskState.Successful) onSuccess();
            };

            return task;
        }

        public static ITask OnFail(this ITask task, Action<ITask> onFail)
        {
            if (onFail == null) return task;

            task.Done += (t) =>
            {
                if (t.State == TaskState.Failed) onFail(t);
            };

            return task;
        }      

        public static ITask<T> OnSuccess<T>(this ITask<T> task, Action<T> onSuccess)
        {
            if (onSuccess == null) return task;

            task.Done += (t) =>
            {
                if (t.State == TaskState.Successful) onSuccess(t.Result);
            };

            return task;
        }

        public static ITask<T> OnFail<T>(this ITask<T> task, Action<ITask<T>> onFail)
        {
            if (onFail == null) return task;

            task.Done += (t) =>
            {
                if (t.State == TaskState.Failed) onFail(t);
            };

            return task;
        }
    }
}
