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
// cls_gestorDepartamentoProyecto.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// GENERADOR			 	16    05    2011    Se crea la clase
// Cristian Arce            22    05    2011    Se modifica los gestores producidos por el generador
// Cristian Arce Jiménez  	27    11    2011	Se agrega el manejo de excepciones personalizadas
// Esteban Ramírez          10    01    2012    Se agrega la inserción en la bitácora.
//								
//
//======================================================================

namespace COSEVI.CSLA.lib.accesoDatos.mod.Administracion
{
    public class cls_gestorDepartamento
    {
        /// <summary>
        /// Método que permite insertar 
        /// un nuevo registro en la tabla departamento
        /// </summary>
        /// <param name="poDepartamento">Departamento a insertar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
        public static int insertDepartamento(cls_departamento poDepartamento)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_admi_departamentoInsert";

                cls_sqlDatabase.beginTransaction();

                if (poDepartamento.pFK_departamento != 0)
                {
                    cls_parameter[] vu_parametros = 
                    {                    
                        new cls_parameter("@paramFK_departamento", poDepartamento.pFK_departamento),
                        new cls_parameter("@paramnombre", poDepartamento.pNombre),
                        new cls_parameter("@paramubicacion", poDepartamento.pUbicacion),
                        new cls_parameter("@paramadministrador", poDepartamento.pAdministrador),
                        new cls_parameter("@paramconsecutivo", poDepartamento.pConsecutivo)
                    };

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);
                }
                else
                {
                    cls_parameter[] vu_parametros = 
                    {                    
                        new cls_parameter("@paramFK_departamento", DBNull.Value),
                        new cls_parameter("@paramnombre", poDepartamento.pNombre),
                        new cls_parameter("@paramubicacion", poDepartamento.pUbicacion),
                        new cls_parameter("@paramadministrador", poDepartamento.pAdministrador),
                        new cls_parameter("@paramconsecutivo", poDepartamento.pConsecutivo)
                    };

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);
                }

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.DEPARTAMENTO, poDepartamento.pFK_departamento.ToString());

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al insertar el departamento.", po_exception);
            }

        }

        /// <summary>
        /// Método que permite actualizar 
        /// un registro en la tabla departamento
        /// </summary>
        /// <param name="poDepartamento">Departamento a actualizar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
        public static int updateDepartamento(cls_departamento poDepartamento)
        {
            int vi_resultado;

            try
            {

                String vs_comando = "PA_admi_departamentoUpdate";

                cls_sqlDatabase.beginTransaction();
                
                if (poDepartamento.pFK_departamento != 0)
                {

                       cls_parameter[] vu_parametros = 
                        {
                            new cls_parameter("@paramPK_departamento", poDepartamento.pPK_departamento),
                            new cls_parameter("@paramFK_departamento", poDepartamento.pFK_departamento),
                            new cls_parameter("@paramnombre", poDepartamento.pNombre),
                            new cls_parameter("@paramubicacion", poDepartamento.pUbicacion),
                            new cls_parameter("@paramadministrador", poDepartamento.pAdministrador),
                        new cls_parameter("@paramconsecutivo", poDepartamento.pConsecutivo)
                        };
                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);
                }
                else
                {
                    cls_parameter[] vu_parametros = 
                        {
                            new cls_parameter("@paramPK_departamento", poDepartamento.pPK_departamento),
                            new cls_parameter("@paramFK_departamento", DBNull.Value),
                            new cls_parameter("@paramnombre", poDepartamento.pNombre),
                            new cls_parameter("@paramubicacion", poDepartamento.pUbicacion),
                            new cls_parameter("@paramadministrador", poDepartamento.pAdministrador),
                            new cls_parameter("@paramconsecutivo", poDepartamento.pConsecutivo)
                        };

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);
                }

                cls_interface.insertarTransacccionBitacora(cls_constantes.MODIFICAR, cls_constantes.DEPARTAMENTO, poDepartamento.pPK_departamento.ToString());

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al modificar el departamento.", po_exception);
            }

        }

        /// <summary>
        /// Método que permite eliminar 
        /// un registro en la tabla departamento
        /// </summary>
        /// <param name="poDepartamento">Departamento a eliminar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
        public static int deleteDepartamento(cls_departamento poDepartamento)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_admi_departamentoDelete";
                cls_parameter[] vu_parametros = 
                {
                 		new cls_parameter("@paramPK_departamento", poDepartamento.pPK_departamento)  
                };


                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.DEPARTAMENTO, poDepartamento.pPK_departamento.ToString());

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al eliminar el departamento.", po_exception);
            }

        }

        /// <summary>
        /// Método que permite listar 
        /// todos los registros en la tabla departamento
        /// </summary>
        /// <returns> List<cls_departamento>  valor del resultado de la ejecución de la sentencia</returns>
        public static List<cls_departamento> listarDepartamento()
        {
            List<cls_departamento> vo_lista = null;
            cls_departamento poDepartamento = null;
            try
            {
                String vs_comando = "PA_admi_departamentoSelect";
                cls_parameter[] vu_parametros = { };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                vo_lista = new List<cls_departamento>();
                for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
                {
                    poDepartamento = new cls_departamento();

                    poDepartamento.pPK_departamento = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_departamento"]);

                    poDepartamento.pFK_departamento = Convert.IsDBNull(vu_dataSet.Tables[0].Rows[i]["FK_departamento"]) ? 1 : Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["FK_departamento"]);

                    poDepartamento.pUbicacion = vu_dataSet.Tables[0].Rows[i]["ubicacion"].ToString();

                    poDepartamento.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                    poDepartamento.pAdministrador = vu_dataSet.Tables[0].Rows[i]["administrador"].ToString();

                    poDepartamento.pConsecutivo = vu_dataSet.Tables[0].Rows[i]["consecutivo"].ToString();

                    vo_lista.Add(poDepartamento);
                }

                return vo_lista;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el listado de los departamentos.", po_exception);
            }
        }

        /// <summary>
        /// Método que permite seleccionar  
        /// un único registro en la tabla departamento
        /// </summary>
        /// <returns>poDepartamento valor del resultado de la ejecución de la sentencia</returns>
        public static cls_departamento seleccionarDepartamento(cls_departamento poDepartamento)
        {
            int vi_fkDepartamento = 0;

            try
            {
                String vs_comando = "PA_admi_departamentoSelectOne";
                cls_parameter[] vu_parametros = { 
                                                   new cls_parameter("@paramPK_departamento", poDepartamento.pPK_departamento) 
                                               };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                poDepartamento = new cls_departamento();

                poDepartamento.pPK_departamento = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["PK_departamento"]);

                if (Int32.TryParse(vu_dataSet.Tables[0].Rows[0]["FK_departamento"].ToString(), out vi_fkDepartamento))
                {
                    poDepartamento.pFK_departamento = vi_fkDepartamento;
                }
                else
                {
                    poDepartamento.pFK_departamento = 0;
                }
                
                poDepartamento.pUbicacion = vu_dataSet.Tables[0].Rows[0]["ubicacion"].ToString();

                poDepartamento.pNombre = vu_dataSet.Tables[0].Rows[0]["nombre"].ToString();

                poDepartamento.pAdministrador = vu_dataSet.Tables[0].Rows[0]["administrador"].ToString();

                poDepartamento.pConsecutivo = vu_dataSet.Tables[0].Rows[0]["consecutivo"].ToString();

                return poDepartamento;

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el departamento específico.", po_exception);
            }
        }

        /// <summary>
        /// Método que permite listar 
        /// todos los registros en la tabla departamento
        /// </summary>
        /// <returns> DataSet valor del resultado de la ejecución de la sentencia</returns>
        public static DataSet listarDepartamentoDpto()
        {
            try
            {
                String vs_comando = "PA_admi_departamentoSelect";
                cls_parameter[] vu_parametros = { };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                return vu_dataSet;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el listado de los departamentos.", po_exception);
            }
        }

        /// <summary>
        /// Hace un lista de departamentos con un filtrado específico.
        /// </summary>
        /// <param name="psFiltro">String filtro.</param>
        /// <returns></returns>
        public static List<cls_departamento> listarDepartamentoFiltro(string psFiltro)
        {
            List<cls_departamento> vo_lista = null;
            cls_departamento voDepartamento = null;
            try
            {
                DataSet vu_dataSet = cls_gestorUtil.selectFilter(cls_constantes.DEPARTAMENTO, string.Empty, psFiltro);

                vo_lista = new List<cls_departamento>();

                for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
                {
                    voDepartamento = new cls_departamento();

                    voDepartamento.pPK_departamento = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_departamento"]);

                    voDepartamento.pFK_departamento = Convert.IsDBNull(vu_dataSet.Tables[0].Rows[i]["FK_departamento"]) ? 1 : Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["FK_departamento"]);

                    voDepartamento.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                    voDepartamento.pUbicacion = vu_dataSet.Tables[0].Rows[i]["ubicacion"].ToString();

                    voDepartamento.pAdministrador = vu_dataSet.Tables[0].Rows[i]["administrador"].ToString();

                    voDepartamento.pConsecutivo = vu_dataSet.Tables[0].Rows[i]["consecutivo"].ToString();

                    vo_lista.Add(voDepartamento);
                }

                return vo_lista;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el listado de los departamentos de manera filtrada.", po_exception);
            }
        }

    }
}
