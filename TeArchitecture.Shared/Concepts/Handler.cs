namespace TeArchitecture.Shared
{
    /// <summary>
    /// Handlers can handle message of provided type.
    /// </summary>
    public abstract class Handler<TMessage>
    {
        private readonly Task task = new Task();
        public abstract ITask Process(TMessage message);

        protected ITask Fail(IError error)
        {
            task.Fail(error);
            return task;
        }

        protected ITask Fail(string errorMessage)
        {
            task.Fail(new Error(errorMessage));
            return task;
        }

        protected ITask Finish()
        {
            task.Finish();
            return task;
        }

        protected ITask FinishAsync() => task;
    }
}
