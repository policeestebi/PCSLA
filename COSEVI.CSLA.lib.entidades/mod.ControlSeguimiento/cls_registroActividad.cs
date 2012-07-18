using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using COSEVI.CSLA.lib.entidades.mod.Administracion;
//======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_actividad.cs
//
// Clase que asocia la información de los proyectos con las actividades por realizar de los mismos
// del Consejo de Seguridad Vial.
// =====================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// Esteban Ramírez          04  -  19   2011    Se crea la clase.
//							
// 
//								
//								
//
//======================================================================
namespace COSEVI.CSLA.lib.entidades.mod.ControlSeguimiento
{
    public class cls_registroActividad
    {

        #region Constructor

        #endregion

        #region Propiedades

        public decimal pRegistro
        {
            get { return registro; }
            set { registro = value; }
        }

        public cls_asignacionActividad pAsignacion
        {
            get { return asignacion; }
            set { asignacion = value; }
        }

        public decimal pHoras
        {
            get { return horas; }
            set { horas = value; }
        }

        public string pComentario
        {
            get { return comentario; }
            set { comentario = value; }
        }

        public DateTime pFecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        #endregion
        
        #region Atributos

        cls_asignacionActividad asignacion;

        private DateTime fecha;

        private string comentario;

        private decimal horas;

        private decimal registro;

    

        #endregion

    }
}
