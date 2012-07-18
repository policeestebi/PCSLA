using System;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ExceptionManagement.Exceptions
{
    [Serializable]
    public class CustomAppException : BaseApplicationException
    {
        // private variable for OS Version property
        private String m_OSVersion = Environment.OSVersion.ToString();

        // default constructor
        public CustomAppException()
            : base()
        {
        }

        // constructor with exception message
        public CustomAppException(String message)
            : base(message)
        {
        }

        // constructor with message and inner exception
        public CustomAppException(String message, Exception inner)
            : base(message, inner)
        {
        }

        // protected constructor to de-seralize state data
        protected CustomAppException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Initialize state 
            m_OSVersion = info.GetString("m_OSVersion");
        }

        // override GetObjectData to serialize state data
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize this class' state and then call the base class GetObjectData
            info.AddValue("m_OSVersion", m_OSVersion, typeof(String));
            base.GetObjectData(info, context);
        }

        // ReadOnly OS Version property
        public String OSVersion
        {
            get
            {
                return m_OSVersion;
            }
        }
    }
}
