using System.Collections.Generic;

namespace TeArchitecture.Shared
{
    public static class Extensions
    {
        public static void Swap<T>(this IList<T> list, int index1, int index2)
        {
            var temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
    }
}
