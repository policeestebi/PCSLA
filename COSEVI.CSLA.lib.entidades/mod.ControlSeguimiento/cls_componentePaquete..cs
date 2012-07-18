using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



//======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_componente_paquete.cs
//
// Clase que contiene la información relacionada con los paquetes pertenecientes a los componentes 
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
	public class cls_componentePaquete
    {
        /// <summary>
        /// Clase que contiene la información relacionada con los paquetes pertenecientes a los componentes
        /// del Consejo de Seguridad Vial.
        /// </summary>
        /// 

        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_componente_paquete.
        /// </summary>
        public cls_componentePaquete()
        {
            this.proyecto = new cls_proyecto();
            this.entregable = new cls_entregable();
            this.componente = new cls_componente();
            this.paquete = new cls_paquete();
            this.paqueteActividad = new cls_paqueteActividad();

            this.paqueteList = new List<cls_paquete>();
            this.paqueteActividadList = new List<cls_paqueteActividad>();

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

        public int pPK_Paquete
        {
            get { return paquete.pPK_Paquete; }
            set { this.paquete.pPK_Paquete= value; }
        }

        public string pNombrePaquete
        {
            get { return paquete.pNombre; }
            set { this.paquete.pNombre = value; }
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

        public cls_paquete pPaquete
        {
            get { return paquete; }
            set { this.paquete = value; }
        }

        public cls_paqueteActividad pPaqueteActividad
        {
            get { return paqueteActividad; }
            set { this.paqueteActividad = value; }
        }

        public List<cls_paquete> pPaqueteList
        {
            get { return paqueteList; }
            set { this.paqueteList = value; }
        }

        public List<cls_paqueteActividad> pPaqueteActividadList
        {
            get { return paqueteActividadList; }
            set { this.paqueteActividadList = value; }
        }

        #endregion

		#region Atributos

        private cls_proyecto proyecto;

        private cls_entregable entregable;

        private cls_componente componente;

        private cls_paquete paquete;

        private cls_paqueteActividad paqueteActividad;

        private List<cls_paquete> paqueteList;

        private List<cls_paqueteActividad> paqueteActividadList;
 
        #endregion

        #region Metodos

        public bool ActividadEncontrada(cls_actividad po_actividad)
        {
            bool encontrado = false;

            if (paqueteActividadList.Where(po => po.pPK_Actividad == po_actividad.pPK_Actividad).Count() > 0)
            {
                encontrado = true;
            }

            return encontrado;
        }

        public bool ActividadesAsignadas()
        {
            bool encontrado = false;

            if (paqueteActividadList.Count > 0)
            {
                encontrado = true;
            }

            return encontrado;
        }

        public void RemoverActividadEncontrada(cls_actividad po_actividad)
        {
            //bool encontrado = false;

            paqueteActividadList.RemoveAll(po => po.pPK_Actividad == po_actividad.pPK_Actividad);

            //return encontrado;
        }

        #endregion Metodos
     
	}

}

