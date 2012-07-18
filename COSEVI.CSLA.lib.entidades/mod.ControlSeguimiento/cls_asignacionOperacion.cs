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
// Clase que asocia la información de la asignación de las operaciones
// a los funcionarios del COSEVI.
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
    public class cls_asignacionOperacion
    {
        #region Contructor

        public cls_asignacionOperacion()
        {
        }

        #endregion

        #region Propiedades

        public string pFK_Usuario
        {
            get { return FK_usuario; }
            set { FK_usuario = value; }
        }

        public string pComentario
        {
            get { return comentario; }
            set { comentario = value; }
        }

        public cls_operacion pFK_Operacion
        {
            get { return FK_operacion; }
            set { FK_operacion = value; }
        }

        public bool pIsActivo
        {
            get { return isActivo; }
            set { isActivo = value; }
        }

        #endregion

        #region Atributos

        private string FK_usuario;

        private string comentario;

        private cls_operacion FK_operacion;

        private bool isActivo;

        #endregion

    }
}
