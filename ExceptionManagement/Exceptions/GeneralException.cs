using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ExceptionManagement.Exceptions
{
    [Serializable]
    public class GeneralException : BaseApplicationException
    {
        // Default constructor
        public GeneralException()
            : base()
        {
        }
        // Constructor with exception message
        public GeneralException(string message)
            : base(message)
        {
        }
        // Constructor with message and inner exception
        public GeneralException(string message, Exception inner)
            : base(message, inner)
        {
        }
        // Protected constructor to de-serialize data
        protected GeneralException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
