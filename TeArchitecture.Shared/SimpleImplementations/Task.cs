using System;

namespace TeArchitecture.Shared
{
    public enum TaskState
    {
        InProgress,
        Successful,
        Failed,
    }

   public class Task : ITask
   {
        private Action<ITask> doneHandlers;

        public event Action<ITask> Done
        {
            add
            {
                if (State == TaskState.InProgress) doneHandlers += value;
                else value?.Invoke(this);
            }
            remove
            {
                doneHandlers -= value;
            }
        }

        public TaskState State { get; private set; }

        public IError Error { get; private set; }

        public void Fail(IError error)
        {
            State = TaskState.Failed;
            Error = error;
            ExecuteCallbacks();
        }       

        public void Finish()
        {
            State = TaskState.Successful;
            ExecuteCallbacks();
        }

        private void ExecuteCallbacks() => doneHandlers?.Invoke(this);

        public static Task FinishedTask()
        {
            var task = new Task();
            task.Finish();
            return task;
        }
    }

    public class Task<TResult> : ITask<TResult>
    {
        private Action<ITask<TResult>> doneHandlers;

        public event Action<ITask<TResult>> Done
        {
            add
            {
                if (State == TaskState.InProgress) doneHandlers += value;
                else value?.Invoke(this);
            }
            remove
            {
                doneHandlers -= value;
            }
        }

        public TaskState State { get; private set; }
        public TResult Result { get; private set; }
        public IError Error { get; private set; }


        public void Fail(IError error)
        {
            State = TaskState.Failed;
            Error = error;
            ExecuteCallbacks();
        }

        public void Finish(TResult result)
        {
            State = TaskState.Successful;
            Result = result;
            ExecuteCallbacks();
        }

        private void ExecuteCallbacks() => doneHandlers?.Invoke(this);

        public static Task<TResult> FinishedTask(TResult result)
        {
            var task = new Task<TResult>();
            task.Finish(result);
            return task;
        }
    }
}
