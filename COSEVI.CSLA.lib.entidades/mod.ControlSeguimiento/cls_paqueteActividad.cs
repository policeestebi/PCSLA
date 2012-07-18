using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



//======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_paquete_actividad.cs
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
	public class cls_paqueteActividad
    {
        /// <summary>
        /// Clase que contiene la información relacionada con las actividades pertenecientes a los paquetes
        /// del Consejo de Seguridad Vial.
        /// </summary>
        /// 

        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_paqueteActividad.
        /// </summary>
        public cls_paqueteActividad()
        {
            this.proyecto = new cls_proyecto();
            this.entregable = new cls_entregable();
            this.componente = new cls_componente();
            this.paquete = new cls_paquete();
            this.actividad = new cls_actividad();

            this.actividadList = new List<cls_actividad>();

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
            set { this.paquete.pPK_Paquete = value; }
        }
       
        public int pPK_Actividad
        {
            get { return actividad.pPK_Actividad; }
            set { this.actividad.pPK_Actividad = value; }
        }

        public string pNombreActividad
        {
            get { return actividad.pNombre; }
            set { this.actividad.pNombre = value; }
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

        public cls_actividad pActividad
        {
            get { return actividad; }
            set { this.actividad = value; }
        }

        public List<cls_actividad> pActividadList
        {
            get { return actividadList; }
            set { this.actividadList = value; }
        }

        #endregion

		#region Atributos

        private cls_proyecto proyecto;

        private cls_entregable entregable;

        private cls_componente componente;

        private cls_paquete paquete;

        private cls_actividad actividad;

        private List<cls_actividad> actividadList;
 
        #endregion
     
	}

}

