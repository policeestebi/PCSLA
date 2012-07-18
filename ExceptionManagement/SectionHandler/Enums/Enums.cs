using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExceptionManagement.SectionHandler.Enums
{
    /// <summary>
    /// Enum containing the mode options for the exceptionManagement tag.
    /// </summary>
    public enum ExceptionManagementMode
    {
        /// <summary>The ExceptionManager should not process exceptions.</summary>
        Off,
        /// <summary>The ExceptionManager should process exceptions. This is the default.</summary>
        On
    }

    /// <summary>
    /// Enum containing the mode options for the publisher tag.
    /// </summary>
    public enum PublisherMode
    {
        /// <summary>The ExceptionManager should not call the publisher.</summary>
        Off,
        /// <summary>The ExceptionManager should call the publisher. This is the default.</summary>
        On
    }

    /// <summary>
    /// Enum containing the format options for the publisher tag.
    /// </summary>
    public enum PublisherFormat
    {
        /// <summary>The ExceptionManager should call the IExceptionPublisher interface of the publisher. 
        /// This is the default.</summary>
        Exception,
        /// <summary>The ExceptionManager should call the IExceptionXmlPublisher interface of the publisher.</summary>
        Xml
    }
}
