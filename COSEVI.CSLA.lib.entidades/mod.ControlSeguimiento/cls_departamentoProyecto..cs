using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COSEVI.CSLA.lib.entidades.mod.Administracion;



//======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_departamento_proyecto.cs
//
// Clase que asocia la información de los departamentos y proyectos
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
    /// Clase que asocia la información de los departamentos y proyectos
    /// del Consejo de Seguridad Vial.
    /// </summary>
    /// 
	public class cls_departamentoProyecto
    {

        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_departamento.
        /// </summary>
        public cls_departamentoProyecto()
        {
            this.departamento = new cls_departamento();
            this.proyecto = new cls_proyecto();
            this.proyectoEntregable = new cls_proyectoEntregable();
            this.departamentoList = new List<cls_departamento>();
            this.proyectoEntregablesList = new List<cls_proyectoEntregable>();
        }

        #endregion

        #region Propiedades
        
        public int pPK_departamento
        {
            get { return departamento.pPK_departamento; }
            set { this.departamento.pPK_departamento = value; }
        }

		public int pPK_proyecto
        {
            get { return proyecto.pPK_proyecto; }
            set { this.proyecto.pPK_proyecto = value; }
        }

		public cls_departamento pDepartamento
        {
            get { return departamento; }
            set { this.departamento = value; }
        }

		public cls_proyecto pProyecto
        {
            get { return proyecto; }
            set { this.proyecto = value; }
        }

        public cls_proyectoEntregable pProyectoEntregable
        {
            get { return proyectoEntregable; }
            set { this.proyectoEntregable = value; }
        }

        public List<cls_departamento> pDepartamentoList
        {
            get { return departamentoList; }
            set { this.departamentoList = value; }
        }

        public List<cls_proyectoEntregable> pProyectoEntregableList
        {
            get { return proyectoEntregablesList; }
            set { this.proyectoEntregablesList = value; }
        }

        #endregion

        #region Atributos

        /// <summary>
        /// Código del departamento
        /// </summary>
        private int PK_departamento;

        /// <summary>
        /// Código del proyecto
        /// </summary>
        private int PK_proyecto;

        private cls_departamento departamento;

        private cls_proyecto proyecto;

        private cls_proyectoEntregable proyectoEntregable;

        private List<cls_departamento> departamentoList;
        
        private List<cls_proyectoEntregable> proyectoEntregablesList;

        #endregion


        #region Metodos

        public bool EntregableEncontrado(cls_entregable po_entregable)
        {
            bool encontrado = false;

            if(pProyectoEntregableList.Where(po => po.pPK_Entregable == po_entregable.pPK_entregable).Count() > 0)
            {
                encontrado = true;
            }

            return encontrado;
        }

        public bool EntregablesAsignado()
        {
            bool encontrado = false;

            if (pProyectoEntregableList.Count > 0)
            {
                encontrado = true;
            }

            return encontrado;
        }

        public void RemoverEntregableEncontrado(cls_entregable po_entregable)
        {
            //bool encontrado = false;

            pProyectoEntregableList.RemoveAll(po => po.pPK_Entregable == po_entregable.pPK_entregable);

            //return encontrado;
        }

        #endregion Metodos

    }

}

