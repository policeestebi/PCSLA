using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



//======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_componente.cs
//
// Clase que contiene la información relacionada con los componentes de un entregable
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
	public class cls_componente
    {
        /// <summary>
        /// Clase que contiene la información relacionada con los componentes de los entregables
        /// del Consejo de Seguridad Vial.
        /// </summary>
        
        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_componente.
        /// </summary>
        public cls_componente()
        {
        }

        #endregion

        #region Propiedades

        public int pPK_componente
        {
            get { return PK_componente; }
            set { this.PK_componente = value; }
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
        /// Código del componente autogenerado
        /// </summary>
 	    private int PK_componente;

        /// <summary>
        /// Código del componente
        /// </summary>
	    private string codigo;

        /// <summary>
        /// Nombre del componente
        /// </summary>
	    private string nombre;

        /// <summary>
        /// Descripción del componente
        /// </summary>
	    private string descripcion;

        #endregion

        }
}

