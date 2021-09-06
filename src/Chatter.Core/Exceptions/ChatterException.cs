using System;

namespace Chatter.Core.Exceptions
{
    // Custom exception
    public class ChatterException : Exception
    {
        public string Code { get; }

        protected ChatterException()
        {

        }

        protected ChatterException(string code)
        {
            Code = code;
        }

        protected ChatterException(string message, params object[] args) 
            : this(string.Empty, message, args)
        {

        }

        protected ChatterException(string code, string message, params object[] args) 
            : this(null, code, message, args)
        {
        }

        protected ChatterException(Exception innerException, string message, params object[] args)
           : this(innerException, string.Empty, message, args)
        {
        }

        protected ChatterException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
