using System;

namespace Atheneum.Exceptions
{
    public class LibraryException : InvalidOperationException
    {
        public LibraryException() :
            base()
        { }
        public LibraryException(string message) :
            base(message)
        { }
    }
}