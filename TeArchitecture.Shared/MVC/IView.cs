namespace TeArchitecture.Shared.MVC
{
    public interface IView { }   

    public interface IDataView<T> : IView
    {
        void SetData(T data);
    }

    public class Toaster
    {
        public static void Show(string message) => System.Console.WriteLine(message);
    }
}
