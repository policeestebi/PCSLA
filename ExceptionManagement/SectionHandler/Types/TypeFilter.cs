﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExceptionManagement.SectionHandler.Types
{
    /// <summary>
    /// TypeFilter class stores contents of the Include and Exclude filters provided in the
    /// configuration file
    /// </summary>
    public class TypeFilter
    {
        private Boolean acceptAllTypes = false;
        private ArrayList types = new ArrayList();

        /// <summary>
        /// Indicates if all types should be accepted for a filter
        /// </summary>
        public Boolean AcceptAllTypes
        {
            get
            {
                return acceptAllTypes;
            }

            set
            {
                acceptAllTypes = value;
            }
        }

        /// <summary>
        /// Collection of types for the filter
        /// </summary>
        public ArrayList Types
        {
            get
            {
                return types;
            }
        }
    }
}
