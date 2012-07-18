﻿using System;
﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using COSEVI.CSLA.lib.accesoDatos;
using COSEVI.CSLA.lib.accesoDatos.App_Database;

//=======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_gestorUtil.cs
//
// Gestor que posee funciones utilitarias que pertenecen a una tabla en específico
// =========================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// GENERADOR			 	06    11    2011    Se crea la clase
//								
//								
//
//======================================================================

namespace COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes
{
    public class cls_gestorUtil
    {

        /// <summary>
        /// Método permite obtener el valor máximo
        /// de una columna de una tabla.
        /// </summary>
        /// <param name="table"> String nombre de la tabla.</param>
        /// <param name="columna">String nombre de la columna</param>
        /// <returns>Object con el valor.</returns>
        public static object selectMax(string psTabla, string psColumna)
        {
            Object po_valor = null;
            try
            {
                String vs_comando = "PA_admi_maxSelect";
                cls_parameter[] vu_parametros = { 
                                                   new cls_parameter("@paramTable", psTabla),
                                                   new cls_parameter("@paramColumna", psColumna),
                                               };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                if (vu_dataSet != null && vu_dataSet.Tables[0].Rows.Count > 0)
                {
                    po_valor = vu_dataSet.Tables[0].Rows[0][0];
                }


            }
            catch (Exception po_exception)
            {
                throw new Exception("Error al tratar de obtener el máximo valor de la columna:" + psColumna + " en la tabla:" + psTabla + " ", po_exception);
            }

            return po_valor;
        }


        /// <summary>
        /// Método permite obtener un select filtrado
        /// de una tabla específica y sus columnas
        /// </summary>
        /// <param name="psTabla">String nombre de la tabla.</param>
        /// <param name="psFilter">String filtro para la tabla.</param>
        /// <param name="psColumnas">String lista de la tablas.</param>
        /// <returns>DataSet con los datos.</returns>
        public static DataSet selectFilter(string psTabla, string psColumnas, string psFilter)
        {
            DataSet vu_dataSet = null;

            try
            {
                String vs_comando = "PA_admi_selectFilter";
                cls_parameter[] vu_parametros = { 
                                                   new cls_parameter("@paramTable", psTabla),
                                                   new cls_parameter("@paramColumnas", psColumnas),
                                                   new cls_parameter("@paramFilter", psFilter)
                                               };

                vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);


            }
            catch (Exception po_exception)
            {
                throw new Exception("Error al tratar de obtener un filtro de la tabla :" + psTabla + " ", po_exception);
            }

            return vu_dataSet; ;
        }

        /// <summary>
        /// Método permite obtener los registros de las tablas que no pueden ser eliminados
        /// </summary>
        /// <returns>DataSet con los datos.</returns>
        public static DataSet selectRegistroPermanente()
        {
            DataSet vu_dataSet = null;

            try
            {
                String vs_comando = "PA_admi_registroPermanenteSelect";
                cls_parameter[] vu_parametros = { 
                                                  
                                               };

                vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);


            }
            catch (Exception po_exception)
            {
                throw new Exception("Error al tratar de obtener los registros permanentes de la aplicación", po_exception);
            }

            return vu_dataSet; ;
        }

    }
}

