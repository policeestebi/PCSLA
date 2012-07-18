using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//=======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_rol.cs
//
//Clase que contiene la definición de la relación existente
//entre un rol y una página y sus permisos.
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
//Sin anotaciones.
//=======================================================================
namespace COSEVI.CSLA.lib.entidades.mod.Administracion
{

    /// <summary>
    /// Clase que contiene la definición de la relación existente
    /// entre un rol y una página y sus permisos.
    /// </summary>
    public class cls_rolPaginaPermiso
    {

        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_rolPaginaPermiso.
        /// </summary>
        public cls_rolPaginaPermiso() 
        {
            this.rol = new cls_rol();
            this.pagina = new cls_pagina();
            this.permiso = new cls_permiso();
        }

        #endregion

        #region Propiedades

        public cls_rol pRol
        {
            get { return rol; }
            set { rol = value; }
        }

        public cls_pagina pPagina
        {
            get { return pagina; }
            set { pagina = value; }
        }

        public cls_permiso pPermiso
        {
            get { return permiso; }
            set { permiso = value; }
        }

        public int pPK_rol
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

        public int pPK_pagina
        {
            get
            {
                return this.pagina.pPK_pagina;
            }

            set
            {
                this.pagina.pPK_pagina = value;
            }
        }

        public int pPK_permiso
        {
            get
            {
                return this.permiso.pPK_permiso;
            }

            set
            {
                this.permiso.pPK_permiso = value;
            }
        }

        #endregion

        #region Atributos

        /// <summary>
        /// Rol asociado.
        /// </summary>
        private cls_rol rol;

        /// <summary>
        /// Página asociada.
        /// </summary>
        private cls_pagina pagina;

        /// <summary>
        /// Permisos asociada.
        /// </summary>
        private cls_permiso permiso;

        #endregion

    }
}
