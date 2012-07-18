using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//=======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_bitacora.cs
//
// Clase que contiene la información de una transacción realizada 
// por algún usuario del sistema. Esta clase es para llevar la auditoría
// del sistema.
// =====================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// Esteban Ramírez          05  - 14    2011    Se crea la clase.
//							
// 
//								
//								
//
//======================================================================

//=======================================================================
//NOTAS:
//La llave primaria de la bitácora debería ser sólamente el identity 
//que es el consecutivo que tienen las transacciones realizadas.
//
//
//Post Nota:
//Corregido en 15052011
//Por ser bitáacora, todas las colmunas de la tabla quedan para no permitir nulos
//=======================================================================

namespace COSEVI.CSLA.lib.entidades.mod.Administracion
{
    /// <summary>
    /// Clase que contiene la información de una transacción realizada 
    /// por algún usuario del sistema. Esta clase es para llevar la auditoría
    /// del sistema.
    /// </summary>
    public class cls_bitacora
    {

        #region Constructor

        /// <summary>
        /// Constructo de la clase cls_bitacora.
        /// </summary>
        public cls_bitacora()
        {
            this.departamento = new cls_departamento();
            this.usuario = new cls_usuario();
        }

        #endregion

        #region Propiedades

        public decimal pPK_bitacora
        {
            get { return PK_bitacora; }
            set { PK_bitacora = value; }
        }

        public int pFK_departamento
        {
            get 
            {
                return this.departamento.pPK_departamento;
            }
            set 
            {
                departamento.pPK_departamento = value;
            }
        }

        public string pFK_usuario
        {
            get
            {
                return this.usuario.pPK_usuario;
            }
            set
            {
                usuario.pPK_usuario = value;
            }
        }

        public string pAccion
        {
            get { return accion; }
            set { accion = value; }
        }

        public DateTime pFechaAccion
        {
            get { return fechaAccion; }
            set { fechaAccion = value; }
        }

        public string pNumeroRegistro
        {
            get { return numeroRegistro; }
            set { numeroRegistro = value; }
        }

        public string pTabla
        {
            get { return tabla; }
            set { tabla = value; }
        }

        public string pMaquina
        {
            get { return maquina; }
            set { maquina = value; }
        }

        public cls_usuario pUsuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public cls_departamento pDepartamento
        {
            get { return departamento; }
            set { departamento = value; }
        }
        #endregion

        #region Atributos

        private decimal PK_bitacora;

        private string accion;

        private DateTime fechaAccion;

        private string numeroRegistro;

        private string tabla;

        private string maquina;

        private cls_usuario usuario;

        private cls_departamento departamento;

        #endregion

    }
}
