using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//=======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_permiso.cs
//
// Clase que contiene la informaci�n de un permiso del sistema
// por ejemplo Eliminar, Consultar.
// =====================================================================
// Historial
// PERSONA 			        MES - DIA - A�O		DESCRIPCION
// Esteban Ram�rez          05  - 14    2011    Se crea la clase.
//							
// 
//								
//								
//
//======================================================================


namespace COSEVI.CSLA.lib.entidades.mod.Administracion
{
    /// <summary>
    /// Clase que contiene la informaci�n de un permiso del sistema.
    /// </summary>
	public class cls_permiso
    {
        #region Constructor

        public cls_permiso()
        {

        }

        #endregion

        #region Propiedades

        public int pPK_permiso
        {
            get { return PK_permiso; }
            set { this.PK_permiso = value; }
        }

        public string pNombre
        {
            get { return nombre; }
            set { this.nombre = value; }
        }

        #endregion

        #region Atributos

        /// <summary>
        /// C�digo del permiso
        /// </summary>
        private int PK_permiso;

        /// <summary>
        /// Nombre del permiso
        /// </summary>
        private string nombre;

        #endregion
	}

}

