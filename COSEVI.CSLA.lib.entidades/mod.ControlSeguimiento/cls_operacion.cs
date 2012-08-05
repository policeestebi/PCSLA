using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_actividad.cs
//
// Clase que asocia la información de las operaciones por realizar por lo empleados
// del Consejo de Seguridad Vial.
// =====================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// Esteban Ramírez          04  - 08    2012    Se crea la clase.
//							
// 
//								
//								
//
//======================================================================

namespace COSEVI.CSLA.lib.entidades.mod.ControlSeguimiento
{
    public class cls_operacion
    {
        #region Constructor

        public cls_operacion()
        {
            this.listaAsignaciones = new List<cls_asignacionOperacion>();
        }

        #endregion

        #region Propiedades

        public bool pActivo
        {
            get { return activo; }
            set { activo = value; }
        }

        public string pTipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public string pDescTipo
        {
            get
            {
                return this.pTipo == "O" ? "Operación" : "Imprevisto";
            }
        }

        public int pFK_Proyecto
        {
            get { return FK_proyecto; }
            set { FK_proyecto = value; }
        }

        public String pPK_Codigo
        {
            get { return PK_codigo; }
            set { PK_codigo = value; }
        }

        public string pDescripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }


        public List<cls_asignacionOperacion> ListaAsignaciones
        {
            get { return listaAsignaciones; }
            set { listaAsignaciones = value; }
        }

        #endregion

        #region Atributos

        private string tipo;

        private int FK_proyecto;

        private String PK_codigo;

        private string descripcion;

        private bool activo;

        private List<cls_asignacionOperacion> listaAsignaciones;

        #endregion
    }
}
