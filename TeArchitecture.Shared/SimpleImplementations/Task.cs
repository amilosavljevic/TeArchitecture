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
        public event Action<ITask> Done;

        private bool isDone = false;

        public TaskState State => !isDone ? TaskState.Failed : (Error != null ? TaskState.Failed : TaskState.Successful);

        public IError Error { get; private set; }

        public void Fail(IError error)
        {
            isDone = true;
            Error = error;
            ExecuteCallbacks();
        }       

        public void Finish()
        {
            isDone = true;
            ExecuteCallbacks();
        }

        private void ExecuteCallbacks() => Done?.Invoke(this);
    }
}
