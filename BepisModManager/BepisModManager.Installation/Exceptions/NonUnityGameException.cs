using System;

namespace BepisModManager.Installation.Exceptions
{
    class NonUnityGameException : Exception
    {
        public NonUnityGameException()
        {
        }

        public NonUnityGameException(string message) : base(message)
        {
        }

        public NonUnityGameException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}