using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



//======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_entregable.cs
//
// Clase que asocia la información de los entregables
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
	public class cls_entregable
    {

        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_entregable.
        /// </summary>
        public cls_entregable()
        {
        }

        #endregion

        #region Propiedades

        public int pPK_entregable
        {
            get { return PK_entregable; }
            set { this.PK_entregable = value; }
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
        /// Código del entregable autogenerado
        /// </summary>
 	    private int PK_entregable;

        /// <summary>
        /// Código del entregable
        /// </summary>
	    private string codigo;

        /// <summary>
        /// Nombre del entregable
        /// </summary>
	    private string nombre;

        /// <summary>
        /// Descripción del entregable
        /// </summary>
	    private string descripcion;

        #endregion

    }

}

