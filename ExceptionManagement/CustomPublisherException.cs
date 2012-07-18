﻿using System;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;

using ExceptionManagement.SectionHandler.Enums;

namespace ExceptionManagement
{
    /// <summary>
    /// Summary description for ExceptionManagerExceptions.
    /// </summary>
    [Serializable]
    public class CustomPublisherException : BaseApplicationException
    {
        #region Constructors
        /// <summary>
        /// Constructor with no params.
        /// </summary>
        public CustomPublisherException()
            : base()
        {
        }
        /// <summary>
        /// Constructor allowing the Message property to be set.
        /// </summary>
        /// <param name="message">String setting the message of the exception.</param>
        public CustomPublisherException(String message)
            : base(message)
        {
        }
        /// <summary>
        /// Constructor allowing the Message and InnerException property to be set.
        /// </summary>
        /// <param name="message">String setting the message of the exception.</param>
        /// <param name="inner">Sets a reference to the InnerException.</param>
        public CustomPublisherException(String message, Exception inner)
            : base(message, inner)
        {
        }
        /// <summary>
        /// Constructor allowing the message, assembly name, type name, and publisher format to be set.
        /// </summary>
        /// <param name="message">String setting the message of the exception.</param>
        /// <param name="assemblyName">String setting the assembly name of the exception.</param>
        /// <param name="typeName">String setting the type name of the exception.</param>
        /// <param name="publisherFormat">String setting the publisher format of the exception.</param>
        public CustomPublisherException(String message, String assemblyName, String typeName, PublisherFormat publisherFormat)
            : base(message)
        {
            this.assemblyName = assemblyName;
            this.typeName = typeName;
            this.publisherFormat = publisherFormat;
        }
        /// <summary>
        /// Constructor allowing the Message and InnerException property to be set.
        /// </summary>
        /// <param name="message">String setting the message of the exception.</param>
        /// <param name="assemblyName">String setting the assembly name of the exception.</param>
        /// <param name="typeName">String setting the type name of the exception.</param>
        /// <param name="publisherFormat">String setting the publisher format of the exception.</param>
        /// <param name="inner">Sets a reference to the InnerException.</param>
        public CustomPublisherException(String message, String assemblyName, String typeName, PublisherFormat publisherFormat, Exception inner)
            : base(message, inner)
        {
            this.assemblyName = assemblyName;
            this.typeName = typeName;
            this.publisherFormat = publisherFormat;
        }
        /// <summary>
        /// Constructor used for deserialization of the exception class.
        /// </summary>
        /// <param name="info">Represents the SerializationInfo of the exception.</param>
        /// <param name="context">Represents the context information of the exception.</param>
        protected CustomPublisherException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            assemblyName = info.GetString("assemblyName");
            typeName = info.GetString("typeName");
            publisherFormat = (PublisherFormat)info.GetValue("publisherFormat", typeof(PublisherFormat));
        }
        #endregion

        // Member variable declarations
        private String assemblyName;
        private String typeName;
        private PublisherFormat publisherFormat;

        /// <summary>
        /// Override the GetObjectData method to serialize custom values.
        /// </summary>
        /// <param name="info">Represents the SerializationInfo of the exception.</param>
        /// <param name="context">Represents the context information of the exception.</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("assemblyName", assemblyName, typeof(String));
            info.AddValue("typeName", typeName, typeof(String));
            info.AddValue("publisherFormat", publisherFormat, typeof(PublisherFormat));
            base.GetObjectData(info, context);
        }

        #region Public Properties
        /// <summary>
        /// The exception format configured for the publisher that threw an exception.
        /// </summary>
        public PublisherFormat PublisherFormat
        {
            get
            {
                return publisherFormat;
            }
        }

        /// <summary>
        /// The Assembly name of the publisher that threw an exception.
        /// </summary>
        public String PublisherAssemblyName
        {
            get
            {
                return assemblyName;
            }
        }

        /// <summary>
        /// The Type name of the publisher that threw an exception.
        /// </summary>
        public String PublisherTypeName
        {
            get
            {
                return typeName;
            }
        }
        #endregion
    }
}
