using System;
using System.Collections.Generic;

namespace TeArchitecture.Shared
{
    public interface IError
    {
        string ErrorMessage{ get; }
    }

    public class Error : IError, IEquatable<Error>
    {
        public Error(string errorMessage) => ErrorMessage = errorMessage;

        public override bool Equals(object obj) => obj is Error other && Equals(other);

        public bool Equals (Error other) => ErrorMessage == other.ErrorMessage;

        public string ErrorMessage { get; private set; }

        public override int GetHashCode() => -378322542 + EqualityComparer<string>.Default.GetHashCode(ErrorMessage);

       
    }
}
