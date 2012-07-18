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
// Clase que contiene la información relacionada con los roles del 
// sistema.
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
//El campo visible, debería ser un Bool?
//El nombre del rol debe ser un unique index.
//
//Post Notas:
//Se pasa el campo a un bool, el nombre a Unique
//=======================================================================

namespace COSEVI.CSLA.lib.entidades.mod.Administracion
{
    /// <summary>
    /// Clase que contiene la información relacionada con los roles del sistema.
    /// </summary>
    public class cls_rol
    {
        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_rol.
        /// </summary>
        public cls_rol()
        {
            this.paginas = new List<cls_pagina>();
        }

        #endregion

        #region Propiedades

        public int pPK_rol
        {
            get { return PK_rol; }
            set { this.PK_rol = value; }
        }


        public string pDescripcion
        {
            get { return descripcion; }
            set { this.descripcion = value; }
        }


        public string pNombre
        {
            get { return nombre; }
            set { this.nombre = value; }
        }


        public bool pVisible
        {
            get { return visible; }
            set { this.visible = value; }
        }

        public List<cls_pagina> Paginas
        {
            get { return paginas; }
            set { paginas = value; }
        }


        #endregion

        #region Atributos

        /// <summary>
        /// Código del rol.
        /// </summary>
        private int PK_rol;

        /// <summary>
        /// Descripción del rol.
        /// </summary>
        private string descripcion;

        /// <summary>
        /// Nombre asociado al rol.
        /// </summary>
        private string nombre;

        /// <summary>
        /// Indica si el rol es visible o no.
        /// </summary>
        private bool visible;

        /// <summary>
        /// Lista de páginas asociadas al rol
        /// las mismas con sus respectivos permisos.
        /// </summary>
        private List<cls_pagina> paginas;

        #endregion

    }
}
