using System;

namespace TeArchitecture.Shared
{
    public interface IResult
    {        
    }

    // This maybe should inherit from ITask<T>, so we can do all kind of async shenanigans.
    public interface IResult<T>
    {
        T Value { get; }
    }   

    public static partial class IResultExtensions
    {
        public static IResult OnSuccess(this IResult result, Action<IResult> onSuccess)
        {
            throw new NotImplementedException();
        }

        public static IResult OnFail(this IResult result, Action<IResult> onFail)
        {
            throw new NotImplementedException();
        }

        public static IResult<T> OnSuccess<T>(this IResult<T> result, Action<IResult<T>> onSuccess)
        {
            throw new NotImplementedException();
        }

        public static IResult<T> OnFail<T>(this IResult<T> result, Action<IResult<T>> onFail)
        {
            throw new NotImplementedException();
        }
    }
}
