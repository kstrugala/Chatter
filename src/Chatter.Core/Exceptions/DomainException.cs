using System;

namespace Chatter.Core.Exceptions
{
    public class DomainException : ChatterException
    {
        public DomainException()
        {
        }

        public DomainException(string code) 
            : base(code)
        {
        }

        public DomainException(string message, params object[] args) 
            : base(message, args)
        {
        }

        public DomainException(string code, string message, params object[] args) 
            : base(code, message, args)
        {
        }

        public DomainException(Exception innerException, string message, params object[] args) 
            : base(innerException, message, args)
        {
        }

        public DomainException(Exception innerException, string code, string message, params object[] args) 
            : base(innerException, code, message, args)
        {
        }
    }
}
