using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COSEVI.CSLA.lib.entidades.mod.Administracion;



//======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_actividad.cs
//
// Clase que asocia la información de los proyectos con las actividades por realizar de los mismos
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
    /// Clase que asocia la información de los proyectos con las actividades por realizar de los mismos
    // del Consejo de Seguridad Vial.
    /// </summary>
	public class cls_actividad
    {

        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_actividad.
        /// </summary>
        public cls_actividad()
        {

        }

        #endregion
       
        #region Propiedades

        public int pPK_Actividad
        {
            get { return PK_actividad; }
            set { this.PK_actividad = value; }
        }

        public string pCodigo
        {
            get { return codigo; }
            set { this.codigo = value; }
        }

        public string pNombre
        {
            get { return nombre; }
            set { this.nombre = value; }
        }

        public string pDescripcion
        {
            get { return descripcion; }
            set { this.descripcion = value; }
        }

        #endregion 

		#region Atributos

        /// <summary>
        /// Código de actividad
        /// </summary>
 	    private int PK_actividad;

        /// <summary>
        /// Código de la actividad
        /// </summary>
	    private string codigo;

        /// <summary>
        /// Nombre de la actividad
        /// </summary>
	    private string nombre;

        /// <summary>
        /// Descripción de la actividad
        /// </summary>
	    private string descripcion;

        #endregion

    }

}

