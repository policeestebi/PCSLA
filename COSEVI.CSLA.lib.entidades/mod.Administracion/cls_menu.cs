using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//=======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_menu.cs
//
//Clase que contiene la información de una opción de menú del sistema.
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
//Se agrega el campo descripción, que quita el campo URL.
//
//Post Nota:
//Se modifica en la bd, 15052011
//=======================================================================

namespace COSEVI.CSLA.lib.entidades.mod.Administracion
{
    /// <summary>
    /// Clase que contiene la información de una opción de menú del sistema.
    /// </summary>
    public class cls_menu
    {

        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_menu.
        /// </summary>
        public cls_menu()
        {
            //this.menuPadre = new cls_menu();
        }

        #endregion

        #region Propiedades

        public int pPK_menu
        {
            get { return PK_menu; }
            set { PK_menu = value; }
        }

        public int pFK_menu
        {
            get
            {
                return this.menuPadre;
            }
            set
            {
                this.menuPadre = value;
            }
        }

        public string pImagen
        {
            get { return imagen; }
            set { imagen = value; }
        }

        public string pTitulo
        {
            get { return titulo; }
            set { titulo = value; }
        }

        public string pDescripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public int pMenuPadre
        {
            get { return menuPadre; }
            set { menuPadre = value; }
        }

        #endregion

        #region Atributos

        /// <summary>
        /// Código del menú.
        /// </summary>
        private int PK_menu;

        /// <summary>
        /// Opción de menú Padre.
        /// </summary>
        private int menuPadre;

        /// <summary>
        /// Imagen asociada a la opción de menú.
        /// Esta es una dirección.
        /// </summary>
        private string imagen;

        /// <summary>
        /// Título desplegado por la opción de menú.
        /// </summary>
        private string titulo;

        /// <summary>
        /// Descripción de la opción de menú.
        /// </summary>
        private string descripcion;

        #endregion

    }
}
