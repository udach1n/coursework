using System;

namespace Atheneum.Exceptions
{
    public class IncorrectLoginException : ArgumentException
    {
        public IncorrectLoginException() :
            base()
        { }
        public IncorrectLoginException(string message) :
            base(message)
        { }
    }
}