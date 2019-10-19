using System;

namespace TeArchitecture.Shared
{
    public interface IResult<T>
    {
    }

    public static class IResultExtensions
    {
        public static IResult<T> OnSuccess<T>(this IResult<T> result)
        {
            throw new NotImplementedException();
        }

        public static IResult<T> OnFail<T>(this IResult<T> result)
        {
            throw new NotImplementedException();
        }
    }
}
