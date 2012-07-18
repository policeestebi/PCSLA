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
// Clase que contiene la información de las páginas del sistema.
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
//Se debe poner un constraint UNIQUE para el url de la página
//y no el url si no el nombre de la misma.
//
//Post Nota:
//No queda del todo claro, se asignan tanto el nombre como el rul como Uniques
//=======================================================================
namespace COSEVI.CSLA.lib.entidades.mod.Administracion
{
    /// <summary>
    /// Clase que contiene la información de las páginas del sistema.
    /// </summary>
    public class cls_pagina
    {
        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_pagina.
        /// </summary>
        public cls_pagina()
        {
            this.permisos = new List<cls_permiso>();
            this.menu = new cls_menu();
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Obtiene un permiso específico.
        /// </summary>
        /// <param name="psPermiso">Nombre del permiso.</param>
        /// <returns>cls_permiso permiso</returns>
        public cls_permiso BuscarPermiso(string psPermiso) 
        {

            cls_permiso loPermiso = null;

            try
            {
                if(this.Permisos != null && this.Permisos.Count > 0)
                    loPermiso = this.Permisos.Find(c => c.pNombre == psPermiso);

            }
            catch (Exception e) 
            {
                loPermiso = null;
            }

            return loPermiso;

        }


        #endregion

        #region Propiedades

        public int pPK_pagina
        {
            get { return PK_pagina; }
            set { PK_pagina = value; }
        }

        public int FK_menu
        {
            get
            {
                return this.menu.pPK_menu;
            }
            set
            {
                this.menu.pPK_menu = value;
            }
        }

        public string pNombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string pUrl
        {
            get { return url; }
            set { url = value; }
        }

        public string pHeight
        {
            get { return height; }
            set { height = value; }
        }

        public cls_menu pMenu
        {
            get { return menu; }
            set { menu = value; }
        }


        public List<cls_permiso> Permisos
        {
            get { return permisos; }
            set { permisos = value; }
        }

        public cls_permiso this[string psPermiso] 
        {
            get 
            {
                return this.BuscarPermiso(psPermiso);
            }
        }

        #endregion

        #region Atributos

        /// <summary>
        /// Código de la página.
        /// </summary>
        private int PK_pagina;

        /// <summary>
        /// Menú asociado a la página.
        /// </summary>
        private cls_menu menu;

        /// <summary>
        /// Nombre de la página.
        /// </summary>
        private string nombre;

        /// <summary>
        /// Url de la página.
        /// </summary>
        private string url;

        /// <summary>
        /// Height de la página.
        /// </summary>
        private string height;

        /// <summary>
        /// Lista de permisos asociados a la página.
        /// </summary>
        private List<cls_permiso> permisos;

        #endregion

    }
}
