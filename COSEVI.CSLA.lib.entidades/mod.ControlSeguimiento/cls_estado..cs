using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



//======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_estado.cs
//
/// Clase que asocia la información de las actividades de los proyectos
// del Consejo de Seguridad Vial.
// =====================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// Cristian Arce            05  - 15    2011    Se crea la clase.
//							
// 
//								
//								
//
//======================================================================

namespace COSEVI.CSLA.lib.entidades.mod.ControlSeguimiento
{
    /// <summary>
    /// Clase que asocia los estados a los proyectos y actividades
    /// del Consejo de Seguridad Vial.
    /// </summary>
	public class cls_estado
    {

        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_estado.
        /// </summary>
        public cls_estado()
        {
        }

        #endregion

        #region Propiedades

        public int pPK_estado
        {
            get { return PK_estado; }
            set { this.PK_estado = value; }
        }

        public string pDescripcion
        {
            get { return descripcion; }
            set { this.descripcion = value; }
        }

        #endregion

		#region Atributos

        /// <summary>
        /// Código del estado
        /// </summary>
 	    private int PK_estado;

        /// <summary>
        /// Decripción del estado
        /// </summary>
	    private string descripcion;

        #endregion

    }

}

