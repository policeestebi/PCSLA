using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



//======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_proyecto_entregable.cs
//
// Clase que asocia la información de los departamentos, proyectos y entregables
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
    /// <summary>
    /// Clase que asocia la información de los departamentos, proyectos y entregables
    // del Consejo de Seguridad Vial.
    /// </summary>
	public class cls_proyectoEntregable
    {

        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_proyecto_entregable.
        /// </summary>
        public cls_proyectoEntregable()
        {
            this.entregable = new cls_entregable();
            this.proyecto = new cls_proyecto();
            this.entregableComponente = new cls_entregableComponente();

            this.entregableList = new List<cls_entregable>();
            this.entregableComponenteList = new List<cls_entregableComponente>();

        }

        #endregion

        #region Propiedades

        public int pPK_Entregable
        {
            get { return entregable.pPK_entregable; }
            set { this.entregable.pPK_entregable = value; }
        }

        public string pNombreEntregable
        {
            get { return entregable.pNombre; }
            set { this.entregable.pNombre = value; }
        }

        public int pPK_Proyecto
        {
            get { return proyecto.pPK_proyecto; }
            set { this.proyecto.pPK_proyecto = value; }
        }

        public cls_entregableComponente pEntregableComponente
        {
            get { return entregableComponente; }
            set { this.entregableComponente = value; }
        } 

        public cls_entregable pEntregable
        {
            get { return entregable; }
            set { this.entregable = value; }
        }

        public cls_proyecto pProyecto
        {
            get { return proyecto; }
            set { this.proyecto = value; }
        }

        public List<cls_entregable> pEntregableList
        {
            get { return entregableList; }
            set { this.entregableList = value; }
        }

        public List<cls_entregableComponente> pEntregableComponenteList
        {
            get { return entregableComponenteList; }
            set { this.entregableComponenteList = value; }
        }

        #endregion

        #region Atributos

        private cls_proyecto proyecto;

	    private cls_entregable entregable;

        private cls_entregableComponente entregableComponente;

        private List<cls_entregable> entregableList;

        private List<cls_entregableComponente> entregableComponenteList;

        #endregion

        #region Metodos

        public bool ComponenteEncontrado(cls_componente po_componente)
        {
            bool encontrado = false;

            if (entregableComponenteList.Where(po => po.pPK_Componente == po_componente.pPK_componente).Count() > 0)
            {
                encontrado = true;
            }

            return encontrado;
        }

        public bool ComponentesAsignados()
        {
            bool encontrado = false;

            if (entregableComponenteList.Count > 0)
            {
                encontrado = true;
            }

            return encontrado;
        }

        public void RemoverComponenteEncontrado(cls_componente po_componente)
        {
            //bool encontrado = false;

            entregableComponenteList.RemoveAll(po => po.pPK_Componente == po_componente.pPK_componente);

            //return encontrado;
        }

        #endregion Metodos
   
	}

}
