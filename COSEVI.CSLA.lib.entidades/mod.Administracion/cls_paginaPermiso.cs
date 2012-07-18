using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//=======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_paginaPermiso.cs
//
//Clase que contiene la información que almacena la relación que existe entre
//una página y sus permisos.
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

namespace COSEVI.CSLA.lib.entidades.mod.Administracion
{
    /// <summary>
    /// Clase que contiene la información que almacena la relación que existe entre
    /// una página y sus permisos.
    /// </summary>
    public class cls_paginaPermiso
    {
        #region Constructor

        /// <summary>
        /// Constructor de clase cls_paginaPermiso
        /// </summary>
        public cls_paginaPermiso()
        {
            this.permiso = new cls_permiso();
            this.pagina = new cls_pagina();
        }

        #endregion

        #region Propiedades

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
        /// Página asociada a la relación.
        /// </summary>
        private cls_pagina pagina;

        /// <summary>
        /// Permiso asociado a la relación.
        /// </summary>
        private cls_permiso permiso;

        #endregion
    }
}
