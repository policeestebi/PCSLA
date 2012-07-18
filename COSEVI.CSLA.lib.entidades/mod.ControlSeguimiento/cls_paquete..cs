using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



//======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_paquete.cs
//
// Clase que asocia la información de los paquetes que conforman los componentes, en los proyectos
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
    /// Clase que asocia la información de los paquetes que conforman los componentes, en los proyectos
    // del Consejo de Seguridad Vial.
    /// </summary>
	public class cls_paquete
    {
       #region Constructor

        /// <summary>
        /// Constructor de la clase cls_paquete.
        /// </summary>
        public cls_paquete()
        {

        }

        #endregion

       #region Propiedades

        public int pPK_Paquete
        {
            get { return PK_paquete; }
            set { this.PK_paquete = value; }
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
        /// Código del paquete autogenerado
        /// </summary>
 	    private int PK_paquete;

        /// <summary>
        /// Código del paquete
        /// </summary>
	    private string codigo;

        /// <summary>
        /// Nombre del paquete
        /// </summary>
	    private string nombre;

        /// <summary>
        /// Descripción del paquete
        /// </summary>
	    private string descripcion;

       #endregion
        
	}

}

