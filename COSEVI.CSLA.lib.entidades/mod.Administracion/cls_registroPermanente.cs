﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//=======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_registroPermanete.cs
//
//Clase que contiene la información de una opción de menú del sistema.
// =====================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// Cristian Arce            24    01    2011    Se crea la entidad
//							

//=======================================================================

namespace COSEVI.CSLA.lib.entidades.mod.Administracion
{
    /// <summary>
    /// Clase que contiene la información de una opción de menú del sistema.
    /// </summary>
    public class cls_registroPermanete
    {

        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_registroPermanete.
        /// </summary>
        public cls_registroPermanete()
        {
           
        }

        #endregion

        #region Propiedades

        public string pTabla
        {
            get { return tabla; }
            set { tabla = value; }
        }

        public string pRegistro
        {
            get { return registro; }
            set { registro = value; }
        }

        #endregion

        #region Atributos

        /// <summary>
        /// Tabla que mantiene el registro permanente.
        /// </summary>
        private string tabla;

        /// <summary>
        /// Registro de la tabla de registro permanente.
        /// </summary>
        private string registro;

        #endregion

    }
}

