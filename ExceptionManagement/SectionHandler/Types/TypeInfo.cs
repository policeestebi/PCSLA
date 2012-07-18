using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExceptionManagement.SectionHandler.Types
{
    /// <summary>
    /// TypeInfo class contains information about each type within a TypeFilter
    /// </summary>
    public class TypeInfo
    {
        private Type classType;
        private Boolean includeSubClasses = false;

        /// <summary>
        /// Indicates if subclasses are to be included with the type specified in the Include and Exclude filters
        /// </summary>
        public Boolean IncludeSubClasses
        {
            get
            {
                return includeSubClasses;
            }

            set
            {
                includeSubClasses = value;
            }
        }

        /// <summary>
        /// The Type class representing the type specified in the Include and Exclude filters
        /// </summary>
        public Type ClassType
        {
            get
            {
                return classType;
            }

            set
            {
                classType = value;
            }
        }
    }
}
