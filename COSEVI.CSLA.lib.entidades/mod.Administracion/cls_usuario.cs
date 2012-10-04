using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//=======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_usuario.cs
//
// Clase que contiene la información de un usuario del sistema.
// Sólo información para la sesión de usuario.
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

//=======================================================================
//NOTAS:
//Se clase se modifica a como esta en la base de datos, revisar
//si de esta forma se comporta de manera correcta.
//
//Post Notas:
//Se deja el campo de Activo
//=======================================================================
namespace COSEVI.CSLA.lib.entidades.mod.Administracion
{
    /// <summary>
    /// Clase que contiene la información de un usuario del sistema.
    /// Sólo información para la sesión de usuario.
    /// </summary>
    public class cls_usuario
    {

        #region Constructor

        /// <summary>
        /// Constructor de clase cls_usuario.
        /// </summary>
        public cls_usuario()
        {
            this.rol = new cls_rol();
            this.departamento = new cls_departamento();
        }

        #endregion

        #region Propiedades

        public string pPK_usuario
        {
            get { return PK_usuario; }
            set { PK_usuario = value; }
        }

        public int pFK_rol
        {
            get
            {
                return this.rol.pPK_rol;
            }
            set
            {
                this.rol.pPK_rol = value;
            }
        }

        public string pNombreRol
        {
            get { return this.rol.pNombre; }
            set { this.rol.pNombre = value; }
        }

        public string pContrasena
        {
            get { return contrasena; }
            set { contrasena = value; }
        }

        public bool pActivo
        {
            get { return activo; }
            set { activo = value; }
        }

        public string pNombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string pApellido1
        {
            get { return apellido1; }
            set { apellido1 = value; }
        }

        public string pApellido2
        {
            get { return apellido2; }
            set { apellido2 = value; }
        }

        public string pPuesto
        {
            get { return puesto; }
            set { puesto = value; }
        }

        public string pEmail
        {
            get { return email; }
            set { email = value; }
        }

        public int pFK_departamento
        {
            get
            {
                return this.departamento.pPK_departamento;
            }
            set
            {
                this.departamento.pPK_departamento = value;
            }
        }

        public string pNombreDepartamento
        {
            get { return this.departamento.pNombre; }
            set { this.departamento.pNombre = value; }
        }

        /// <summary>
        /// Nombre completo del usuario
        /// </summary>
        /// <returns></returns>
        public string pNombreCompleto
        {
            get
            {
                return String.Format("{0} {1} {2}", this.pNombre, this.apellido1, this.apellido2);
            }
        }

        #endregion

        #region Atributos

        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        private string PK_usuario;

        /// <summary>
        /// Atributo de clase rol
        /// </summary>
        private cls_rol rol;

        /// <summary>
        /// Constraseña del usuario
        /// Encriptada.
        /// </summary>
        private string contrasena;

        /// <summary>
        /// Depermina si el usuario
        /// esta activo.
        /// </summary>
        private bool activo;

        /// <summary>
        /// Nombre el funcionario.
        /// </summary>
        private string nombre;

        /// <summary>
        /// Apellido 1 del funcionario.
        /// </summary>
        private string apellido1;

        /// <summary>
        /// Apellido 2 del funcionario.
        /// </summary>
        private string apellido2;

        /// <summary>
        /// Puesto que desempeña el funcionario.
        /// </summary>
        private string puesto;

        /// <summary>
        /// Email del funcionario.
        /// </summary>
        private string email;

        /// <summary>
        /// Atributo de clase departamento
        /// </summary>
        private cls_departamento departamento;

        private string usuarioTransaccion;

        public string pUsuarioTransaccion
        {
            get { return usuarioTransaccion; }
            set { usuarioTransaccion = value; }
        }

        #endregion

    }
}
