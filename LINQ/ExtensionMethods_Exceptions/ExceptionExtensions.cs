using System;

namespace ExtensionMethods_Exceptions
{
    public static class ExceptionExtensions
    {
        public static Exception GetInnerstException(this Exception exception)
        {
            var currentException = exception;
            while (currentException?.InnerException != null) currentException = currentException.InnerException;
            return currentException;
        }
        public static void ForEachException(this Exception exception, Action<Exception> action)
        {
            var currentException = exception;
            while (currentException != null)
            {
                action(currentException);
                currentException = currentException.InnerException;
            }
        }
    }
}
