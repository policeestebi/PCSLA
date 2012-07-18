using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using COSEVI.CSLA.lib.accesoDatos;
using COSEVI.CSLA.lib.entidades.mod.Administracion;
using COSEVI.CSLA.lib.accesoDatos.App_Database;
using COSEVI.CSLA.lib.accesoDatos.App_Constantes;
using COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes;

//=======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_gestorUsuario.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// GENERADOR			 	16    05    2011    Se crea la clase
// Cristian Arce            22    05    2011    Se modifica los gestores producidos por el generador
// Cristian Arce Jiménez  	27    11    2011	Se agrega el manejo de excepciones personalizadas
// Cristian Arce Jiménez  	23    01    2012	Se agrega el manejo de filtros
//
//======================================================================

namespace COSEVI.CSLA.lib.accesoDatos.mod.Administracion
{

    public class cls_gestorUsuario
    {
        /// <summary>
        /// Método que permite insertar 
        /// un nuevo registro en la tabla usuario
        /// </summary>
        /// <param name="poUsuario">Usuario a insertar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
        public static int insertUsuario(cls_usuario poUsuario)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_admi_usuarioInsert";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramPK_usuario", poUsuario.pPK_usuario),
                    new cls_parameter("@paramFK_rol", poUsuario.pFK_rol),
                    new cls_parameter("@paramclave", poUsuario.pContrasena),
                    new cls_parameter("@paramactivo", poUsuario.pActivo ? 1 : 0),
                    new cls_parameter("@paramnombre", poUsuario.pNombre),
                    new cls_parameter("@paramapellido1", poUsuario.pApellido1),
                    new cls_parameter("@paramapellido2", poUsuario.pApellido2),
                    new cls_parameter("@parampuesto", poUsuario.pPuesto),
                    new cls_parameter("@paramemail", poUsuario.pEmail),
                    new cls_parameter("@paramFK_departamento", poUsuario.pFK_departamento)
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.USUARIO, poUsuario.pPK_usuario);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al insertar el usuario.", po_exception);
            }

        }

        /// <summary>
        /// Método que permite actualizar 
        /// un registro en la tabla usuario
        /// </summary>
        /// <param name="poUsuario">Usuario a actualizar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
        public static int updateUsuario(cls_usuario poUsuario)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_admi_usuarioUpdate";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramPK_usuario", poUsuario.pPK_usuario),
                    new cls_parameter("@paramFK_rol", poUsuario.pFK_rol),
                    new cls_parameter("@paramclave", poUsuario.pContrasena),
                    new cls_parameter("@paramactivo", poUsuario.pActivo ? 1 : 0),
                    new cls_parameter("@paramnombre", poUsuario.pNombre),
                    new cls_parameter("@paramapellido1", poUsuario.pApellido1),
                    new cls_parameter("@paramapellido2", poUsuario.pApellido2),
                    new cls_parameter("@parampuesto", poUsuario.pPuesto),
                    new cls_parameter("@paramemail", poUsuario.pEmail),
                    new cls_parameter("@paramFK_departamento", poUsuario.pFK_departamento)
                };


                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.MODIFICAR, cls_constantes.USUARIO, poUsuario.pPK_usuario);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al modificar el usuario.", po_exception);
            }

        }

        /// <summary>
        /// Método que permite eliminar 
        /// un registro en la tabla usuario
        /// </summary>
        /// <param name="poUsuario">Usuario a eliminar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
        public static int deleteUsuario(cls_usuario poUsuario)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_admi_usuarioDelete";
                cls_parameter[] vu_parametros = 
                {
                 		new cls_parameter("@paramPK_usuario", poUsuario.pPK_usuario)  
                };


                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.USUARIO, poUsuario.pPK_usuario);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al eliminar el usuario.", po_exception);
            }

        }

        /// <summary>
        /// Método que permite listar 
        /// todos los registros en la tabla usuario
        /// </summary>
        /// <returns> List<cls_usuario>  valor del resultado de la ejecución de la sentencia</returns>
        public static List<cls_usuario> listarUsuarios()
        {
            List<cls_usuario> vo_lista = null;
            cls_usuario poUsuario = null;
            try
            {
                String vs_comando = "PA_admi_usuarioSelect";
                cls_parameter[] vu_parametros = { };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                vo_lista = new List<cls_usuario>();
                for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
                {
                    poUsuario = new cls_usuario();

                    poUsuario.pPK_usuario = vu_dataSet.Tables[0].Rows[i]["PK_usuario"].ToString();

                    poUsuario.pFK_rol = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_rol"]);

                    poUsuario.pContrasena = vu_dataSet.Tables[0].Rows[i]["clave"].ToString();

                    poUsuario.pActivo = Convert.ToInt16(vu_dataSet.Tables[0].Rows[i]["activo"]) == 1 ? true : false;

                    poUsuario.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                    poUsuario.pApellido1 = vu_dataSet.Tables[0].Rows[i]["apellido1"].ToString();

                    poUsuario.pApellido2 = vu_dataSet.Tables[0].Rows[i]["apellido2"].ToString();

                    poUsuario.pPuesto = vu_dataSet.Tables[0].Rows[i]["puesto"].ToString();

                    poUsuario.pEmail = vu_dataSet.Tables[0].Rows[i]["email"].ToString();

                    poUsuario.pNombreRol = vu_dataSet.Tables[0].Rows[i]["nombreRol"].ToString();
                    
                    poUsuario.pFK_departamento = Convert.ToInt16(vu_dataSet.Tables[0].Rows[i]["PK_departamento"]);

                    poUsuario.pNombreDepartamento = vu_dataSet.Tables[0].Rows[i]["nombreDepartamento"].ToString();

                    vo_lista.Add(poUsuario);
                }

                return vo_lista;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el listado de los usuarios.", po_exception);
            }

        }

        /// <summary>
        /// Método que permite seleccionar  
        /// un único registro en la tabla usuario
        /// </summary>
        /// <returns>poUsuario valor del resultado de la ejecución de la sentencia</returns>
        public static cls_usuario seleccionarUsuario(cls_usuario poUsuario)
        {
            try
            {
                String vs_comando = "PA_admi_usuarioSelectOne";
                cls_parameter[] vu_parametros = { 
                                                   new cls_parameter("@paramPK_usuario", poUsuario.pPK_usuario) 
                                               };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                poUsuario = new cls_usuario();

                poUsuario.pPK_usuario = vu_dataSet.Tables[0].Rows[0]["PK_usuario"].ToString();

                poUsuario.pFK_rol = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["FK_rol"]);

                poUsuario.pContrasena = vu_dataSet.Tables[0].Rows[0]["clave"].ToString();

                poUsuario.pActivo = Convert.ToInt16(vu_dataSet.Tables[0].Rows[0]["activo"]) == 1 ? true : false;

                poUsuario.pNombre = vu_dataSet.Tables[0].Rows[0]["nombre"].ToString();

                poUsuario.pApellido1 = vu_dataSet.Tables[0].Rows[0]["apellido1"].ToString();

                poUsuario.pApellido2 = vu_dataSet.Tables[0].Rows[0]["apellido2"].ToString();

                poUsuario.pPuesto = vu_dataSet.Tables[0].Rows[0]["puesto"].ToString();
                
                poUsuario.pEmail = vu_dataSet.Tables[0].Rows[0]["email"].ToString();

                poUsuario.pFK_departamento = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["FK_departamento"]);

                return poUsuario;

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el usuario específico.", po_exception);
            }
        }

        /// <summary>
        /// Selecciona el posible rol de los usuarios
        /// </summary>
        /// <returns></returns>
        public static DataSet listarRolesUsuario()
        {
            DataSet vo_lista = null;
            try
            {
                String vs_comando = "PA_admi_rolSelect";
                cls_parameter[] vu_parametros = { };

                vo_lista = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                return vo_lista;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el listado de los roles del usuario.", po_exception);
            }
        }

        /// <summary>
        /// Método permite identificar a un usuario.
        /// </summary>
        /// <returns> cls_usuario usuario logeado, en caso de que no exista será nulo.</returns>
        public static cls_usuario Login(string ps_user, string ps_pass)
        {
            cls_usuario poUsuario = null;
            try
            {
                String vs_comando = "PA_admi_login";
                cls_parameter[] vu_parametros = { 
                                                    
                                                       new cls_parameter("@param_user", ps_user),
                                                       new cls_parameter("@param_pass", ps_pass),
                                                   
                                                   };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                if (vu_dataSet != null && vu_dataSet.Tables[0].Rows.Count == 1)
                {

                    poUsuario = new cls_usuario();

                    poUsuario.pPK_usuario = vu_dataSet.Tables[0].Rows[0]["PK_usuario"].ToString();

                    poUsuario.pFK_rol = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["FK_rol"]);

                    poUsuario.pActivo = Convert.ToInt16(vu_dataSet.Tables[0].Rows[0]["activo"]) == 1 ? true : false; ;

                    poUsuario.pNombre = vu_dataSet.Tables[0].Rows[0]["nombre"].ToString();

                    poUsuario.pApellido1 = vu_dataSet.Tables[0].Rows[0]["apellido1"].ToString();

                    poUsuario.pApellido2 = vu_dataSet.Tables[0].Rows[0]["apellido2"].ToString();

                    poUsuario.pPuesto = vu_dataSet.Tables[0].Rows[0]["puesto"].ToString();

                    poUsuario.pEmail = vu_dataSet.Tables[0].Rows[0]["email"].ToString();

                    poUsuario.pFK_departamento = vu_dataSet.Tables[0].Rows[0]["FK_departamento"] != DBNull.Value ? Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["FK_departamento"]) : -1;

                }

                return poUsuario;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Error al tratar de realizar el Login al sistema.", po_exception);
            }
        }

        /// <summary>
        /// Hace un lista de usuarios con un filtrado específico.
        /// </summary>
        /// <param name="psFiltro">String filtro.</param>
        /// <returns></returns>
        public static List<cls_usuario> listarUsuarioFiltro(string psFiltro)
        {
            List<cls_usuario> vo_lista = null;
            cls_usuario voUsuario = null;
            try
            {
                string vs_tablas = cls_constantes.USUARIO + " usu, " + cls_constantes.ROL + " rl, " + cls_constantes.DEPARTAMENTO + " dep ";
                string vs_columnas = " usu.PK_usuario PK_usuario, usu.FK_rol FK_rol, usu.nombre nombre, " + 
                                     " usu.apellido1 apellido1, usu.apellido2 apellido2, usu.puesto puesto, " + 
                                     " usu.activo activo, usu.email, rl.nombre nombreRol, usu.FK_departamento, dep.nombre nombreDepartamento ";
                psFiltro = " usu.FK_departamento = dep.PK_departamento AND usu.FK_rol= rl.Pk_rol AND usu." + psFiltro;

                DataSet vu_dataSet = cls_gestorUtil.selectFilter(vs_tablas, vs_columnas, psFiltro);

                vo_lista = new List<cls_usuario>();

                for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
                {
                    voUsuario = new cls_usuario();

                    voUsuario.pPK_usuario = vu_dataSet.Tables[0].Rows[i]["PK_usuario"].ToString();

                    voUsuario.pFK_rol = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["FK_rol"].ToString());

                    voUsuario.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                    voUsuario.pApellido1 = vu_dataSet.Tables[0].Rows[i]["apellido1"].ToString();

                    voUsuario.pApellido2 = vu_dataSet.Tables[0].Rows[i]["apellido2"].ToString();

                    voUsuario.pPuesto = vu_dataSet.Tables[0].Rows[i]["puesto"].ToString();

                    voUsuario.pActivo = Convert.ToInt16(vu_dataSet.Tables[0].Rows[i]["activo"]) == 1 ? true : false; ;

                    voUsuario.pEmail = vu_dataSet.Tables[0].Rows[i]["email"].ToString();

                    voUsuario.pNombreRol = vu_dataSet.Tables[0].Rows[i]["nombreRol"].ToString();

                    voUsuario.pFK_departamento = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["FK_departamento"].ToString());

                    voUsuario.pNombreDepartamento = vu_dataSet.Tables[0].Rows[i]["nombreDepartamento"].ToString();

                    vo_lista.Add(voUsuario);
                }

                return vo_lista;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el listado de los usuarios de manera filtrada.", po_exception);
            }
        }

        /// <summary>
        /// Método que permite actualizar 
        /// la contraseña a un registro en la tabla usuario
        /// </summary>
        /// <param name="poUsuario">Usuario a actualizar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
        public static int updateContrasenaUsuario(cls_usuario poUsuario)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_admi_usuarioContrasenaUpdate";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramPK_usuario", poUsuario.pPK_usuario),
                    new cls_parameter("@paramcontrasena", poUsuario.pContrasena)
                };


                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.MODIFICAR, cls_constantes.USUARIO, poUsuario.pPK_usuario);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al modificar el usuario.", po_exception);
            }

        }

        /// <summary>
        /// Selecciona el posible departamento de los usuarios
        /// </summary>
        /// <returns></returns>
        public static DataSet listarDepartamentosUsuario()
        {
            DataSet vo_lista = null;
            try
            {
                String vs_comando = "PA_admi_departamentoSelect";
                cls_parameter[] vu_parametros = { };

                vo_lista = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                return vo_lista;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el listado de los departamentos para el usuario.", po_exception);
            }
        }

    }
}
