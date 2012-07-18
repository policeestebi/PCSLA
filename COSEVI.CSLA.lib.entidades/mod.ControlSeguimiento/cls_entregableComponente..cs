using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



//======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_entregable_componente.cs
//
// Clase que asocia la información de los entregables y componentes
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
	public class cls_entregableComponente
    {

        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_entregable_componente.
        /// </summary>
        public cls_entregableComponente()
        {
            this.proyecto = new cls_proyecto();
            this.entregable = new cls_entregable();
            this.componente = new cls_componente();
            this.componentePaquete = new cls_componentePaquete();

            this.componenteList = new List<cls_componente>();
            this.componentePaqueteList = new List<cls_componentePaquete>();

        }

        #endregion

        #region Propiedades

        public int pPK_Proyecto
        {
            get { return proyecto.pPK_proyecto; }
            set { this.proyecto.pPK_proyecto = value; }
        }

        public int pPK_Entregable
        {
            get { return entregable.pPK_entregable; }
            set { this.entregable.pPK_entregable = value; }
        }

        public int pPK_Componente
        {
            get { return componente.pPK_componente; }
            set { this.componente.pPK_componente = value; }
        }

        public string pNombreComponente
        {
            get { return componente.pNombre; }
            set { this.componente.pNombre = value; }
        }

        public cls_proyecto pProyecto
        {
            get { return proyecto; }
            set { this.proyecto = value; }
        }

        public cls_entregable pEntregable
        {
            get { return entregable; }
            set { this.entregable = value; }
        }

        public cls_componente pComponente
        {
            get { return componente; }
            set { this.componente = value; }
        }

        public cls_componentePaquete pComponentePaquete
        {
            get { return componentePaquete; }
            set { this.componentePaquete = value; }
        }

        public List<cls_componente> pComponenteList
        {
            get { return componenteList; }
            set { this.componenteList = value; }
        }

        public List<cls_componentePaquete> pComponentePaqueteList
        {
            get { return componentePaqueteList; }
            set { this.componentePaqueteList = value; }
        }


        #endregion

        #region Atributos

        private cls_proyecto proyecto;

        private cls_entregable entregable;

        private cls_componente componente;

        private cls_componentePaquete componentePaquete;

        private List<cls_componente> componenteList;

        private List<cls_componentePaquete> componentePaqueteList;

        #endregion

        #region Metodos

        public bool PaqueteEncontrado(cls_paquete po_paquete)
        {
            bool encontrado = false;

            if (componentePaqueteList.Where(po => po.pPK_Paquete == po_paquete.pPK_Paquete).Count() > 0)
            {
                encontrado = true;
            }

            return encontrado;
        }

        public bool PaquetesAsignados()
        {
            bool encontrado = false;

            if (componentePaqueteList.Count > 0)
            {
                encontrado = true;
            }

            return encontrado;
        }

        public void RemoverPaqueteEncontrado(cls_paquete po_paquete)
        {
            //bool encontrado = false;

            componentePaqueteList.RemoveAll(po => po.pPK_Paquete == po_paquete.pPK_Paquete);

            //return encontrado;
        }

        #endregion Metodos

	}

}

