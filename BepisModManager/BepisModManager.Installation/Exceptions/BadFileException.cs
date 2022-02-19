using System;

namespace BepisModManager.Installation.Exceptions
{
    class BadFileException : Exception
    {
        public BadFileException()
        { 
        }

        public BadFileException(string message) : base(message)
        {
        }

        public BadFileException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}