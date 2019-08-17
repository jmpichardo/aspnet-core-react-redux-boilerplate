using System;

namespace Renetdux.Infrastructure.Commands
{
    public class CommandResult<T> : ICommandResult<T>
    {
        public T Result { get; private set; }
        public bool IsSuccessful => Error == null;
        public (string Code, string Message)? Error { get; private set; }

        public CommandResult(T terms)
        {
            Result = terms;
        }

        public CommandResult(string errorCode, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new Exception("If command failed you must give an error message");
            }

            Error = (errorCode, errorMessage);
        }
    }
}
