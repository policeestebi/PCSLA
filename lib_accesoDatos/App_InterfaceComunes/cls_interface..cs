using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using COSEVI.CSLA.lib.accesoDatos;
using COSEVI.CSLA.lib.entidades.mod.Administracion;
using COSEVI.CSLA.lib.entidades.mod.ControlSeguimiento;
using COSEVI.CSLA.lib.accesoDatos.App_Database;
using COSEVI.CSLA.lib.accesoDatos.mod.Administracion;
using COSEVI.CSLA.lib.accesoDatos.App_Constantes;


//=======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_gestorBitacora.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// GENERADOR			 	16    05    2011    Se crea la clase
// Cristian Arce            22    05    2011    Se modifica los gestores producidos por el generador
//								
//								
//
//======================================================================

namespace COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes
{
    public class cls_interface
    {

        #region Variables Estáticas
        
        public static string vs_usuarioActual;
        public static string vs_direccionIP;
        public static string vs_nombreHost;

        //Esta variable se encarga de mantener la llave del proyecto al cual se le va a realizar la 
        //asignación de Entregables, Componentes, Paquetes y Actividades
        public static int vs_llaveProyecto;

        public static List<cls_registroPermanete> vl_registrosPermantentes = new List<cls_registroPermanete>();

        #endregion Variables Estáticas

        #region Metodos Estáticos

        /// <summary>
        /// Método encargado de crear el objeto bitácora y enviarlo a insertar
        /// </summary>
        /// <param name="psUsuario"></param>
        /// <param name="psAccion"></param>
        /// <param name="psTabla"></param>
        public static void insertarTransacccionBitacora(string psAccion, string psTabla)
        {

                   cls_bitacora poBitacora = new cls_bitacora();

                   poBitacora.pFK_departamento = 1;

                   poBitacora.pFK_usuario = vs_usuarioActual;

                   poBitacora.pAccion = psAccion;

                   poBitacora.pFechaAccion = System.DateTime.Today;

                   poBitacora.pNumeroRegistro = "0";

                   poBitacora.pTabla = psTabla;

                   poBitacora.pMaquina = vs_nombreHost;

                   cls_gestorBitacora.insertBitacora(poBitacora);

        }

        /// <summary>
        /// Método encargado de crear el objeto bitácora y enviarlo a insertar
        /// </summary>
        /// <param name="psUsuario"></param>
        /// <param name="psAccion"></param>
        /// <param name="psTabla"></param>
        public static void insertarTransacccionBitacora(string psAccion, string psTabla, string psNumeroRegistro)
        {

            cls_bitacora poBitacora = new cls_bitacora();

            poBitacora.pFK_departamento = 1;

            poBitacora.pFK_usuario = vs_usuarioActual;

            poBitacora.pAccion = psAccion;

            poBitacora.pFechaAccion = System.DateTime.Now;
            
            poBitacora.pNumeroRegistro = psNumeroRegistro;

            poBitacora.pTabla = psTabla;

            poBitacora.pMaquina = vs_nombreHost;

            cls_gestorBitacora.insertBitacora(poBitacora);

        }

        /// <summary>
        /// Hace un lista de registros permanentes que no pueden ser eliminados de la aplicación.
        /// </summary>
        public static void cargarRegistrosPermanentes()
        {
            cls_registroPermanete voRegistroPermanente = null;
            try
            {
                DataSet vu_dataSet = cls_gestorUtil.selectRegistroPermanente();

                for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
                {
                    voRegistroPermanente = new cls_registroPermanete();

                    voRegistroPermanente.pTabla = vu_dataSet.Tables[0].Rows[i]["tabla"].ToString();

                    voRegistroPermanente.pRegistro = vu_dataSet.Tables[0].Rows[i]["registro"].ToString();

                    vl_registrosPermantentes.Add(voRegistroPermanente);
                }

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los registros permanentes.", po_exception);
            }
        }

        /// <summary>
        /// Método encargado de verificar si el registro a eliminar se encuentra en la tabla de registros permanentes.
        /// </summary>
        public static Boolean verificarRegistrosPermanentes(string psTabla, string psRegistro)
        {
            Boolean vb_registroPermanente = false;

            if (vl_registrosPermantentes != null)
            {
                //Se recorre la lista de registros permanentes, de encontrarse el registro, se devuelve un valor que indica que no se puede eliminar
                foreach (cls_registroPermanete vo_registro in vl_registrosPermantentes)
                {
                    if ((vo_registro.pTabla == psTabla) && (vo_registro.pRegistro == psRegistro))
                    {
                        vb_registroPermanente = true;
                        break;
                    }
                }
            }

            return vb_registroPermanente;

        }

        #endregion Metodos Estáticos

    }
}
