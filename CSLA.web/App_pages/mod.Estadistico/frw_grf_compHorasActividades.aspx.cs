using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
using COSEVI.CSLA.lib.accesoDatos.mod.Estadistico;
using COSEVI.CSLA.lib.entidades.mod.Estadistico;
using System.Drawing;
using CSLA.web.App_Variables;
using ExceptionManagement.Exceptions;
using CSLA.web.App_Constantes;
using COSEVI.CSLA.lib.entidades.mod.Administracion;
using COSEVI.CSLA.lib.accesoDatos.mod.Administracion;

// =========================================================================
// COSEVI - Consejo de Seguridad Vial. - 2011
// Sistema CSLA (Sistema para el Control y Seguimiento de Labores)
//
// frw_grf_compHorasActividades.aspx.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			           MES – DIA - AÑO		DESCRIPCIÓN
// Esteban Ramírez Gónzalez  	03 – 06  - 2012	 	Se crea la clase
// 
//								
//								
//
// =========================================================================


namespace CSLA.web.App_pages.mod.Estadistico
{
    public partial class frw_grf_compHorasActividades : System.Web.UI.Page
    {

        #region Inicialización

        /// <summary>
        /// Función que se ejecuta al cargar
        /// la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.validarSession();

            if (!Page.IsPostBack)
            {
                try
                {
                    this.validarAcceso();
                    cargarProyectos();
                    cargarUsuarios();
                }
                catch (Exception po_exception)
                {
                    String vs_error_usuario = "Error en la consulta de datos para el gráfco de comparación de horas de actividades por proyecto.";
                    this.lanzarExcepcion(po_exception, vs_error_usuario);
                }

            }
        }

        /// <summary>
        /// Función que se ejecuta al inicializar la página.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            try
            {
                base.OnInit(e);
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Error al inicializar los controles de la ventana para el gráfico de comparación de horas de actividades por proyecto.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Función que se ejecute cada vez que se realice un postback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grafico_Init(object sender, EventArgs e)
        {
            try
            {
              if (!Page.IsPostBack)
              {
                //    //Cuando se está ingresando a la página, se limpian las variables de sesión para evitar valores incorrectos
                //    LimpiarVariablesSession();
                txt_fechaInicio.Text = DateTime.Today.AddMonths(-1).ToString().Substring(0, 10);
                txt_fechaFin.Text = DateTime.Today.ToString().Substring(0, 10);
              }
                //else
                //{
                //    if (!(Session[cls_constantes.CODIGOPROYECTO] == null))
                //    {
                //        CargaGrafico(Convert.ToInt32(Session[cls_constantes.CODIGOPROYECTO]), Convert.ToInt32(Session[cls_constantes.CODIGOPAQUETE]));
                //    }
                //}
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al inicializar los controles del gráfico.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }


        #endregion Inicialización

        #region Métodos Privados

        /// <summary>
        /// Método que realiza la consulta en BD para obtener la información por proyecto y cargar sus valores
        /// </summary>
        private void CargaGrafico(int pi_proyecto, int pi_paquete, DateTime pd_fechaDesde, DateTime pd_fechaHasta, string ps_usuario)
        {
            try
            {
                //Si se está obteniendo información para un proyecto que NO es el proyecto por defecto
                if (pi_proyecto > 0)
                {
                  obtenerGraficoActividadesPorProyecto(pi_proyecto, pi_paquete, pd_fechaDesde, pd_fechaHasta, ps_usuario);
                }
                else
                {
                    this.lanzarExcepcion(new Exception(cls_constantes.MENSAJEADVERTENCIA), cls_constantes.ADVERTENCIA);
                    obtenerGraficoPorDefecto();
                }
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al definir el tipo de gráfico.", po_exception);
            }
        }

        /// <summary>
        /// Metodo que carga el ddl de proyectos para escoger por cual se quiere filtrar
        /// </summary>
        private void cargarProyectos()
        {
            try
            {
                this.ddl_proyecto.DataSource = cls_gestorEstadistico.listarProyectos();
                this.ddl_proyecto.DataTextField = "pNombre";
                this.ddl_proyecto.DataValueField = "pPK_proyecto";
                this.ddl_proyecto.DataBind();

                this.ddl_proyecto.Items.Insert(0, new ListItem("Seleccione un proyecto", "0"));
                this.ddl_proyecto.SelectedIndex = 0;

                this.ddl_paquete.Items.Insert(0, new ListItem("Seleccione un paquete", "0"));
                this.ddl_paquete.SelectedIndex = 0;

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar la lista de proyectos.", po_exception);
            }
        }

        /// <summary>
        /// Metodo que carga el ddl de paquetes para escoger por cual se quiere filtrar
        /// </summary>
        private void cargarPaquetes(int pi_proyecto)
        {
            try
            {
                this.ddl_paquete.DataSource = cls_gestorEstadistico.listarPaquetesAsignacion(pi_proyecto);
                this.ddl_paquete.DataTextField = "pNombre";
                this.ddl_paquete.DataValueField = "pPK_paquete";
                this.ddl_paquete.DataBind();

                this.ddl_paquete.Items.Insert(0, new ListItem("Seleccione un paquete", "0"));
                this.ddl_paquete.SelectedIndex = 0;
                this.ddl_paquete.Enabled = true;

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar la lista de proyectos.", po_exception);
            }
        }

        /// <summary>
        /// Método que obtiene la información con la que se va a cargar en gráfico
        /// </summary>
        private void obtenerGraficoActividadesPorProyecto(int pi_proyecto, int pi_paquete, DateTime pd_fechaDesde, DateTime pd_fechaHasta, string ps_usuario)
        {
            try
            {
                //Se procede a obtener la información por proyecto
                cls_compHorasActividades vo_compHorasActividades = new cls_compHorasActividades();
                vo_compHorasActividades.pPK_proyecto = pi_proyecto;
                vo_compHorasActividades.pPK_paquete = pi_paquete;
                vo_compHorasActividades.pFechaDesde = pd_fechaDesde;
                vo_compHorasActividades.pFechaHasta = pd_fechaHasta;
                vo_compHorasActividades.pPK_usuario = ps_usuario;

                List<cls_compHorasActividades> vl_topActividades = cls_gestorEstadistico.CompHorasActividadesPorProyecto(vo_compHorasActividades);

                //Se asignan los tooltips para el gráfico
                Grafico.Series["Leyendas"].ToolTip = "##VALX\n#PERCENT";
                //Grafico.Series["Leyendas"].ToolTip = "#VALX: #VAL{d} horas";
                Grafico.Series["Leyendas"].LegendToolTip = "#VALX\n#PERCENT";
                //Grafico.Series["Leyendas"].LegendToolTip = "#VALX: #VAL{d} Horas";
                Grafico.Series["Leyendas"].IsVisibleInLegend = false;
                Grafico.Series["Leyendas"].Label = "#VAL{d} horas";
                //Grafico.Series["Leyendas"].Label = "#VALX\n#PERCENT";
                Grafico.Series["Leyendas"].PostBackValue = "#INDEX";
                Grafico.Series["Leyendas"].LegendPostBackValue = "#INDEX";

                //Se realiza el binding de la información que se obtuvo en la consulta
                Grafico.Series["Leyendas"].Points.DataBindXY(vl_topActividades, "pNombreActividad", vl_topActividades, "pHorasReales");

                // Set pyramid chart type
                Grafico.Series["Leyendas"].ChartType = SeriesChartType.Line;
                // set the markers for each point of the Pareto Line
                Grafico.Series["Leyendas"].IsValueShownAsLabel = true;
                Grafico.Series["Leyendas"].MarkerColor = Color.LightSkyBlue;
                Grafico.Series["Leyendas"].MarkerBorderColor = Color.LightSkyBlue;
                Grafico.Series["Leyendas"].MarkerStyle = MarkerStyle.Circle;
                Grafico.Series["Leyendas"].MarkerSize = 8;
                Grafico.Series["Leyendas"].LabelFormat = "0.#";  // format with one decimal and leading zero
                // Set Color of line Pareto chart
                Grafico.Series["Leyendas"].Color = Color.LightSkyBlue;


                // Disable axis labels auto fitting of text
                Grafico.ChartAreas["AreaGrafico"].AxisX.IsLabelAutoFit = false;
                // Set axis labels font
                Grafico.ChartAreas["AreaGrafico"].AxisX.LabelStyle.Font = new Font("Arial", 10);
                // Set axis labels angle
                Grafico.ChartAreas["AreaGrafico"].AxisX.LabelStyle.Angle = -90;
                // Disable offset labels style
                Grafico.ChartAreas["AreaGrafico"].AxisX.LabelStyle.IsStaggered = false;
                // Enable X axis labels
                Grafico.ChartAreas["AreaGrafico"].AxisX.LabelStyle.Enabled = true;
                // Enable AntiAliasing for either Text and Graphics or just Graphics
                Grafico.AntiAliasing = AntiAliasingStyles.All; // AntiAliasingStyles.Graphics and AntiAliasingStyles.Text

                // create the destination series and add it to the chart
                Series destSeries = new Series("Pareto");

                if (Grafico.Series.IndexOf("Pareto") != 1)
                {
                    Grafico.Series.Add(destSeries);
                }
                // ensure the destination series is a Line or Spline chart type
                destSeries.ChartType = SeriesChartType.Line;
                destSeries.BorderWidth = 3;
                destSeries.Color = Color.Firebrick;
                // assign the series to the same chart area as the column chart
                destSeries.ChartArea = Grafico.Series["Leyendas"].ChartArea;
                Grafico.ChartAreas["AreaGrafico"].AxisX.LabelStyle.IsEndLabelVisible = false;

                Grafico.Legends[0].Enabled = false;
                //Se realiza el binding de la información que se obtuvo en la consulta
                Grafico.Series["Pareto"].Points.DataBindXY(vl_topActividades, "pNombreActividad", vl_topActividades, "pHorasAsignadas");

                Grafico.Series["Pareto"]["BackColor"] = "Transparent";
                // Set chart types for output data
                Grafico.Series["Pareto"].ChartType = SeriesChartType.Line;
                // set the markers for each point of the Pareto Line
                Grafico.Series["Pareto"].IsValueShownAsLabel = true;
                Grafico.Series["Pareto"].MarkerColor = Color.Red;
                Grafico.Series["Pareto"].MarkerBorderColor = Color.DarkRed;
                Grafico.Series["Pareto"].MarkerStyle = MarkerStyle.Circle;
                Grafico.Series["Pareto"].MarkerSize = 8;
                Grafico.Series["Pareto"].LabelFormat = "0.#";  // format with one decimal and leading zero
                // Set Color of line Pareto chart
                Grafico.Series["Pareto"].Color = Color.Tomato;

                // Set 3D mode
                Grafico.ChartAreas["AreaGrafico"].Area3DStyle.Enable3D = true;
                // Show a 30% perspective
                Grafico.ChartAreas["AreaGrafico"].Area3DStyle.Perspective = 0;
                // Set the X Angle to 30
                Grafico.ChartAreas["AreaGrafico"].Area3DStyle.Inclination = 0;
                // Set the Y Angle to 40
                Grafico.ChartAreas["AreaGrafico"].Area3DStyle.Rotation = 5;

                //Se aplica el estilo pastel a los colores definidos para el gráfico
                Grafico.Palette = ChartColorPalette.BrightPastel;
                Grafico.ApplyPaletteColors();
                //Para que el estilo tome efecto se debe asignar a cada uno de los puntos de la serie en el gráfico
                foreach (var series in Grafico.Series)
                {
                    foreach (var point in series.Points)
                    {
                        point.Color = Color.FromArgb(220, point.Color);
                    }
                }

                // Set axis title
                Grafico.ChartAreas["AreaGrafico"].AxisY.Title = "Horas Invertidas";
                Grafico.ChartAreas["AreaGrafico"].AxisY.TitleFont = new Font("Times New Roman", 12, FontStyle.Bold);
                Grafico.ChartAreas["AreaGrafico"].AxisX2.Title = "Actividades Registradas";
                Grafico.ChartAreas["AreaGrafico"].AxisX2.TitleFont = new Font("Times New Roman", 12, FontStyle.Bold);

                // Properties
                Grafico.ChartAreas["AreaGrafico"].BackColor = Color.Khaki;
                Grafico.ChartAreas["AreaGrafico"].AxisY.Interval = 8;
                Grafico.ChartAreas["AreaGrafico"].AxisX.Interval = 1;
                Grafico.ChartAreas["AreaGrafico"].AxisX.TextOrientation = TextOrientation.Rotated90;
                Grafico.ChartAreas["AreaGrafico"].AxisX.LabelStyle.IsEndLabelVisible = false;

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar el gráfico con la información de la base de datos.", po_exception);
            }
        }

        /// <summary>
        /// Método que obtiene la información para el gráfico por defecto
        /// </summary>
        private void obtenerGraficoPorDefecto()
        {
            try
            {
                string[] valoresX = new string[] { "Sin Actividades" };
                decimal[] valoresY = new decimal[] { 0 };

                //Se realiza el binding de la información que se obtuvo en la consulta
                Grafico.Series["Leyendas"].Points.DataBindXY(valoresX, valoresY);
                Grafico.Legends[0].Enabled = false;

                //Se asignan los colores de la composición del gráfico
                Grafico.Series["Leyendas"].Points[0].Color = Color.White;

                //Se indica que tipo de gráfico se va a presentar al usuario
                Grafico.Series["Leyendas"].ChartType = SeriesChartType.Pie;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar el gráfico con la información por defecto.", po_exception);
            }
        }

        /// <summary>
        /// Método utilizado para limpiar las 3 variables de sesión que utiliza este gráfico
        /// </summary>
        //private void LimpiarVariablesSession()
        //{
        //    Session[cls_constantes.CODIGOPROYECTO] = null;
        //    Session[cls_constantes.CODIGOPAQUETE] = null;
        //}

        /// <summary>
        /// Se carga la lista con la totalidad de usuarios que pueden ser asignados a una actividad
        /// </summary>
        private void cargarUsuarios()
        {
            try
            {
                /*
                 NOta: * Revisar los selects de los listar, para ver que tanto es necesario cambiar los "pNombre" por los nombres de la tabla => "pNombre" - "pNombreUsuario"
                       * Ver si es relevante cambiar los nombres a los listbox
                 */
                lbx_usuarios.DataSource = cls_gestorUsuario.listarUsuarios();
                lbx_usuarios.DataTextField = "pNombre";
                lbx_usuarios.DataValueField = "pPK_usuario";
                lbx_usuarios.DataBind();

                this.lbx_usuarios.Items.Insert(0, new ListItem("Todas las actividades", "0"));
                this.lbx_usuarios.SelectedIndex = 0;

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error cargando la lista de usuarios.", po_exception);
            }
        }


        /// <summary>
        /// Método que lanza la excepción personalizada
        /// </summary>
        /// <param name="po_exception">Excepción a levantar</param>
        /// <param name="ps_mensajeUsuario">Mensaje a comunicar al usuario</param>
        private void lanzarExcepcion(Exception po_exception, String ps_mensajeUsuario)
        {
            try
            {
                String vs_error_usuario = ps_mensajeUsuario;
                vs_error_usuario = vs_error_usuario.Replace(" ", "_");
                vs_error_usuario = vs_error_usuario.Replace("'", "|");

                String vs_error_tecnico = po_exception.Message;
                vs_error_tecnico = vs_error_tecnico.Replace(" ", "_");
                vs_error_tecnico = vs_error_tecnico.Replace("'", "|");

                String vs_script = "window.showModalDialog(\"../../frw_error.aspx?vs_error_usuario=" + vs_error_usuario + "&vs_error_tecnico=" + vs_error_tecnico + "\",\"Ventana\",\"dialogHeight:450px;dialogWidth:625px;center:yes;status:no;menubar:no;resizable:no;scrollbars:yes;toolbar:no;location:no;directories:no\");";
                ScriptManager.RegisterClientScriptBlock(this.Grafico, this.Grafico.GetType(), "jsKeyScript", vs_script, true);

                throw new GeneralException("GeneralException", po_exception);
            }
            catch (GeneralException po_general_exception)
            {
                ExceptionManagement.ExceptionManager.Publish(po_general_exception);
            }
        }

        #endregion Métodos Privados

        #region Eventos

        /// <summary>
        /// Evento para levantar el explode del gráfico(Explode = destacar, hacer sobresalir una de las secciones)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Generar_Click(object sender, EventArgs e)
        {
            try
            {
                //Session[cls_constantes.CODIGOPROYECTO] = ddl_proyecto.SelectedValue;
                //Session[cls_constantes.CODIGOPAQUETE] = ddl_paquete.SelectedValue;

                //Si el proyecto es un proyecto válido, se carga, de lo contrario, se limpia la variable en memoria
                //Si el proyecto es el "0", no se traerá nada, por lo que no se mostrará nada en ventana, que es el 
                //caso defecto, y está bien
                if (Convert.ToInt32(ddl_proyecto.SelectedValue) > -1)
                {
                    if (Convert.ToInt32(lbx_usuarios.SelectedIndex) > 0)
                    {
                      CargaGrafico(Convert.ToInt32(ddl_proyecto.SelectedValue), Convert.ToInt32(ddl_paquete.SelectedValue), Convert.ToDateTime(txt_fechaInicio.Text), Convert.ToDateTime(txt_fechaFin.Text), lbx_usuarios.SelectedValue.ToString());
                    }
                    else
                    {
                      CargaGrafico(Convert.ToInt32(ddl_proyecto.SelectedValue), Convert.ToInt32(ddl_paquete.SelectedValue), Convert.ToDateTime(txt_fechaInicio.Text), Convert.ToDateTime(txt_fechaFin.Text), string.Empty);
                    }
                }
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al enfocar la sección del gráfico.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Evento obtiene el valor del ddl para el llamado al método que lo carga en ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.ddl_proyecto.SelectedValue = ((DropDownList)sender).SelectedValue;
                //Session[cls_constantes.CODIGOPROYECTO] = ddl_proyecto.SelectedValue;

                //Si el proyecto es un proyecto válido, se carga, de lo contrario, se limpia la variable en memoria
                //Si el proyecto es el "0", no se traerá nada, por lo que no se mostrará nada en ventana, que es el 
                //caso defecto, y está bien
                if (Convert.ToInt32(ddl_proyecto.SelectedValue) > -1)
                {
                    cargarPaquetes(Convert.ToInt32(ddl_proyecto.SelectedValue));
                }
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al obtener intentar cambiar el proyecto.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        #endregion Eventos

        #region Seguridad

        /// <summary>
        /// Valida si el usuario
        /// tiene acceso a la página de lo contrario
        /// destruye la sessión
        /// 
        /// </summary>
        private void validarAcceso()
        {
            if (!this.pbAcceso)
            {
                this.Session.Abandon();
                this.Session.Clear();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Salida", cls_constantes.SCRIPTLOGOUT, true);
            }
        }

        /// <summary>
        /// Determina si la sesión se encuentra
        /// activa, si no es así se envía a la página de inicio.
        /// </summary>
        private void validarSession()
        {
            if (this.Session["cls_usuario"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Salida", cls_constantes.SCRIPTLOGOUT, true);
            }
        }

        /// <summary>
        /// Valida el acceso del usuario en la página
        /// </summary>
        private bool pbAcceso
        {
            get
            {
                if (Session[cls_constantes.PAGINA] != null)
                {
                    return (Session[cls_constantes.PAGINA] as cls_pagina)[cls_constantes.ACCESO] != null;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion Seguridad

    }
}