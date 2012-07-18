using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



//======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_departamento.cs
//
// Clase que contiene la información relacionada con un departamentos
// del Consejo de Seguridad Vial.
// =====================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// Esteban Ramírez          05  - 14    2011    Se crea la clase.
// Cristian Arce            27  - 10  - 2011    Se modifica la clase							
// 
//								
//								
//
//======================================================================
namespace COSEVI.CSLA.lib.entidades.mod.Administracion
{
    /// <summary>
    /// Clase que contiene la información relacionada con un departamentos
    /// del Consejo de Seguridad Vial.
    /// </summary>
    public class cls_departamento
    {

        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_departamento.
        /// </summary>
        public cls_departamento()
        {
            this.departamentoPadre = new cls_departamento(false);
        }

        public cls_departamento(bool pbPadre)
        {
            
        }

        #endregion

        #region Propiedades

        public int pPK_departamento
        {
            get { return PK_departamento; }
            set { this.PK_departamento = value; }
        }

        public int pFK_departamento
        {
            get { return this.departamentoPadre.PK_departamento; }
            set { this.departamentoPadre.PK_departamento = value; }

        }

        public string pNombre
        {
            get { return nombre; }
            set { this.nombre = value; }
        }

        public string pUbicacion
        {
            get { return ubicacion; }
            set { this.ubicacion = value; }
        }

        public string pAdministrador
        {
            get { return administrador; }
            set { this.administrador = value; }
        }

        public string pConsecutivo
        {
            get { return consecutivo; }
            set { this.consecutivo = value; }
        }

        public cls_departamento pDepartamentoPadre
        {
            get { return departamentoPadre; }
            set { departamentoPadre = value; }
        }

        #endregion

        #region Atributos

        /// <summary>
        /// Código del departamento
        /// </summary>
 	    private int PK_departamento;

        /// <summary>
        /// Nombre del departamento
        /// </summary>
	    private string nombre;

        /// <summary>
        /// Ubicación del departamento.
        /// </summary>
	    private string ubicacion;

        /// <summary>
        /// Administrador del departamento
        /// </summary>
	    private string administrador;

        /// <summary>
        /// Consecutivo del departamento
        /// </summary>
        private string consecutivo;

        private cls_departamento departamentoPadre;

        #endregion

    }
}
