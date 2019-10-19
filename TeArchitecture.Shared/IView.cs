namespace TeArchitecture.Shared
{    
    public interface IView { }   

    public interface IDataView<T> : IView
    {
        void SetData(T data);
    }
}
