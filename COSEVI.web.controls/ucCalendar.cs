using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

using System.Data;

namespace COSEVI.web.controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ucCalendar runat=server></{0}:ucCalendar>")]
    public class ucCalendar : CompositeControl
    {

        #region Override

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(Text);
        }


        public override ControlCollection Controls
        {
            get
            {
                EnsureChildControls();
                return base.Controls;
            }
        }

        /// <summary>
        /// Se encarga de crear los controles hijos
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            Controls.Clear();

            //Se crean los controles.
            this.CrearControles();

            //Al inicio se colocan las fechas iniciales.
            this.ColocarFechas(this.FechaActual);

            // Se coloca la fecha que se muestra para el rango de semanas
            this.ColocarFechaMes();

        }

        /// <summary>
        /// Se sobreescribe el método de render
        /// para crear la estructura la el control.
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            AddAttributesToRender(writer);

            //Se hace el render del encabezado
            this.CrearEncabezado(writer);
        }

        /// <summary>
        /// Se ejecuta al inicio del control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);


        }

        #endregion

        #region Metodos

        /// <summary>
        /// 
        /// </summary>
        private void ColocarFechaMes()
        {
            this.lblSemana.Text = String.Format("{0:dd}", this.FechaLunes) + " - " +
                String.Format("{0:dd}", this.FechaLunes.AddDays(6)) +
                " " + this.ObtenerMesDescripcion(this.FechaLunes.AddDays(6).Month) +
                ", " + String.Format("{0:yyyy}", this.FechaLunes.AddDays(6));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pi_mes"></param>
        /// <returns></returns>
        private string ObtenerMesDescripcion(int pi_mes)
        {
            string vs_mes = String.Empty;

            switch (pi_mes)
            {
                case 1:
                    vs_mes = "Enero";
                    break;
                case 2:
                    vs_mes = "Febrero";
                    break;
                case 3:
                    vs_mes = "Marzo";
                    break;
                case 4:
                    vs_mes = "Abril";
                    break;
                case 5:
                    vs_mes = "Mayo";
                    break;
                case 6:
                    vs_mes = "Junio";
                    break;
                case 7:
                    vs_mes = "Julio";
                    break;
                case 8:
                    vs_mes = "Agosto";
                    break;
                case 9:
                    vs_mes = "Setiembre";
                    break;
                case 10:
                    vs_mes = "Octubre";
                    break;
                case 11:
                    vs_mes = "Noviembre";
                    break;
                case 12:
                    vs_mes = "Diciembre";
                    break;
                default:
                    break;
            }

            return vs_mes;

        }

        /// <summary>
        /// Coloca las fechas de los encabezados según una fecha
        /// </summary>
        /// <param name="pd_fecha"></param>
        private void ColocarFechas(DateTime pd_fecha)
        {
            //Se obtiene el lunes
            DateTime vd_fechaInicial = this.ObtenerLunes(pd_fecha);

            //Por cada día la semana se coloca su respectiva fecha.
            //Lunes
            this.lblLunes.Text = " Lun, " + String.Format("{0:dd }", vd_fechaInicial);

            vd_fechaInicial = vd_fechaInicial.AddDays(1);

            this.lblMartes.Text = " Mart, " + String.Format("{0:dd }", vd_fechaInicial);

            vd_fechaInicial = vd_fechaInicial.AddDays(1);

            this.lblMiercoles.Text = " Miér, " + String.Format("{0:dd }", vd_fechaInicial);

            vd_fechaInicial = vd_fechaInicial.AddDays(1);

            this.lblJueves.Text = " Juev,  " + String.Format("{0:dd }", vd_fechaInicial);

            vd_fechaInicial = vd_fechaInicial.AddDays(1);

            this.lblViernes.Text = " Vier, " + String.Format("{0:dd }", vd_fechaInicial);

            vd_fechaInicial = vd_fechaInicial.AddDays(1);

            this.lblSabado.Text = " Sáb, " + String.Format("{0:dd }", vd_fechaInicial);

            vd_fechaInicial = vd_fechaInicial.AddDays(1);

            this.lblDomingo.Text = " Dom, " + String.Format("{0:dd }", vd_fechaInicial);



        }

        /// <summary>
        /// Crea el encabezado del
        /// del calendario.
        /// </summary>
        /// <param name="writer"></param>
        private void CrearEncabezado(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.Css);
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "8");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            //Controles de arriba
            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssHeader);
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);//TD
            this.lblProyecto.RenderControl(writer);
            writer.RenderEndTag();//END TD
            writer.RenderBeginTag(HtmlTextWriterTag.Td);//TD
            this.ddlProyectos.RenderControl(writer);
            writer.RenderEndTag();//END TD
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            this.btnAnterior.RenderControl(writer);
            writer.RenderEndTag();// End TD
            writer.RenderBeginTag(HtmlTextWriterTag.Td);//TD
            this.lnbHoy.RenderControl(writer);
            writer.RenderEndTag();//END TD
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            this.btnSiguiente.RenderControl(writer);
            writer.RenderEndTag();// End TD
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssAjaxCalendar);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            this.txtCalendario.RenderControl(writer);
            writer.RenderEndTag();// End TD

            writer.RenderEndTag();// End TD
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            this.btnCalendario.RenderControl(writer);
            this.cdeCalendario.RenderControl(writer);

            this.mskDate.RenderControl(writer);
          //  this.mdvDate.RenderControl(writer);

            writer.RenderEndTag();// End TD
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            this.lblSemana.RenderControl(writer);
            writer.RenderEndTag();// End TD
            writer.RenderBeginTag(HtmlTextWriterTag.Td);//TD
            this.btnImprevisto.RenderControl(writer);
            writer.RenderEndTag();//END TD
            writer.RenderEndTag();//End TR
            writer.RenderEndTag();//Table
            //
            writer.RenderEndTag();//TD
            writer.RenderEndTag();//TR

            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssDays);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            this.lblTarea.RenderControl(writer);
            writer.RenderEndTag();//End TD
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            this.lblLunes.RenderControl(writer);
            writer.RenderEndTag();//End TD
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            this.lblMartes.RenderControl(writer);
            writer.RenderEndTag();//End TD
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            this.lblMiercoles.RenderControl(writer);
            writer.RenderEndTag();//End TD
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            this.lblJueves.RenderControl(writer);
            writer.RenderEndTag();//End TD
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            this.lblViernes.RenderControl(writer);
            writer.RenderEndTag();//End TD
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            this.lblSabado.RenderControl(writer);
            writer.RenderEndTag();//End TD
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            this.lblDomingo.RenderControl(writer);
            writer.RenderEndTag();//End TD
            writer.RenderEndTag();//End TR

            this.cargarDatosCalendario(writer);
            this.CargarProyectos();

            writer.RenderEndTag();//End table
        }

        /// <summary>
        /// Carga todos los datos
        /// en el calendario.
        /// </summary>
        private void cargarDatosCalendario(HtmlTextWriter writer)
        {
            DataTable datos = null;
            int contador = 0;
            int contadorLinks = 0;
            DateTime vd_fechaLunes;
            try
            {
                if (this.poDatosActividades != null)
                {
                    datos = ((DataSet)this.poDatosActividades).Tables[0];

                    // Se recorre cada item de la lista y se crea una nueva fila
                    foreach (DataRow row in datos.Rows)
                    {
                        vd_fechaLunes = this.FechaLunes;

                        writer.AddAttribute(HtmlTextWriterAttribute.Class, this.cssCalendarRow);
                        writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                        //Se crea la etiqueta de la descripción
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);

                        //Este div es para agregar la funcionalidad de show more.
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, this.cssShowMore);
                        writer.RenderBeginTag(HtmlTextWriterTag.Div);

                        if (this.ddlProyectos.SelectedValue.ToString().Equals("0") ||
                            this.ddlProyectos.SelectedValue.ToString().Equals("-1"))
                        {
                            //this.crearEtiquetaActividad("lblActividad" + contador, row[this.DescripcionField].ToString()).RenderControl(writer);

                            writer.WriteLine(row[this.DescripcionField].ToString());
                        }
                        else 
                        {
                            //this.crearEtiquetaActividad("lblActividad" + contador,  row[this.DescripcionPaquete].ToString() + "-" + row[this.DescripcionField].ToString()).RenderControl(writer);
                            writer.WriteLine(row[this.DescripcionField].ToString() + "-" + row[this.DescripcionPaquete].ToString() );
                        }

                        writer.RenderEndTag();//End DIV
                        writer.RenderEndTag();//End TD

                        //Lunes
                        GeneraActividad(vd_fechaLunes, writer, row, contadorLinks);
                        contadorLinks++;
                        //vo_row = this.ObtenerRegistroActividad(vd_fechaLunes, row[this.codigoActividadField].ToString());
                        //writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        //this.crearLinkHora("vlnkHora" + contadorLinks++,
                        //                    this.ObtenerUrl(vo_row, row[this.codigoActividadField].ToString(), vd_fechaLunes, this.SelectValueProyecto),
                        //                    this.ObtenerHoras(vo_row)).RenderControl(writer);
                        //writer.RenderEndTag();//End TD
                        vd_fechaLunes = vd_fechaLunes.AddDays(1);

                        //Martes
                        GeneraActividad(vd_fechaLunes, writer, row, contadorLinks);
                        contadorLinks++;
                        //vo_row = this.ObtenerRegistroActividad(vd_fechaLunes, row[this.codigoActividadField].ToString());
                        //writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        //this.crearLinkHora("vlnkHora" + contadorLinks++,
                        //                    this.ObtenerUrl(vo_row, row[this.codigoActividadField].ToString(), vd_fechaLunes, this.SelectValueProyecto), 
                        //                    this.ObtenerHoras(vo_row)).RenderControl(writer);
                        //writer.RenderEndTag();//End TD
                        vd_fechaLunes = vd_fechaLunes.AddDays(1);

                        //Miércoles
                        GeneraActividad(vd_fechaLunes, writer, row, contadorLinks);
                        contadorLinks++;
                        //vo_row = this.ObtenerRegistroActividad(vd_fechaLunes, row[this.codigoActividadField].ToString());
                        //writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        //this.crearLinkHora("vlnkHora" + contadorLinks++,
                        //                   this.ObtenerUrl(vo_row, row[this.codigoActividadField].ToString(), vd_fechaLunes, this.SelectValueProyecto),
                        //                   this.ObtenerHoras(vo_row)).RenderControl(writer);
                        //writer.RenderEndTag();//End TD
                        vd_fechaLunes = vd_fechaLunes.AddDays(1);

                        //Jueves
                        GeneraActividad(vd_fechaLunes, writer, row, contadorLinks);
                        contadorLinks++;
                        //vo_row = this.ObtenerRegistroActividad(vd_fechaLunes, row[this.codigoActividadField].ToString());
                        //writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        //this.crearLinkHora("vlnkHora" + contadorLinks++,
                        //                   this.ObtenerUrl(vo_row, row[this.codigoActividadField].ToString(), vd_fechaLunes, this.SelectValueProyecto),
                        //                   this.ObtenerHoras(vo_row)).RenderControl(writer);
                        //writer.RenderEndTag();//End TD
                        vd_fechaLunes = vd_fechaLunes.AddDays(1);

                        //Viernes
                        GeneraActividad(vd_fechaLunes, writer, row, contadorLinks);
                        contadorLinks++;
                        //vo_row = this.ObtenerRegistroActividad(vd_fechaLunes, row[this.codigoActividadField].ToString());
                        //writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        //this.crearLinkHora("vlnkHora" + contadorLinks++,
                        //                    this.ObtenerUrl(vo_row, row[this.codigoActividadField].ToString(), vd_fechaLunes, this.SelectValueProyecto),
                        //                    this.ObtenerHoras(vo_row)).RenderControl(writer);
                        //writer.RenderEndTag();//End TD
                        vd_fechaLunes = vd_fechaLunes.AddDays(1);

                        //Sábado
                        GeneraActividad(vd_fechaLunes, writer, row, contadorLinks);
                        contadorLinks++;
                        //vo_row = this.ObtenerRegistroActividad(vd_fechaLunes, row[this.codigoActividadField].ToString());
                        //writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        //this.crearLinkHora("vlnkHora" + contadorLinks++,
                        //                    this.ObtenerUrl(vo_row, row[this.codigoActividadField].ToString(), vd_fechaLunes, this.SelectValueProyecto),
                        //                    this.ObtenerHoras(vo_row)).RenderControl(writer);
                        //writer.RenderEndTag();//End TD
                        vd_fechaLunes = vd_fechaLunes.AddDays(1);

                        //Domingo
                        GeneraActividad(vd_fechaLunes, writer, row, contadorLinks);
                        contadorLinks++;
                        //vo_row = this.ObtenerRegistroActividad(vd_fechaLunes, row[this.codigoActividadField].ToString());
                        //writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        //this.crearLinkHora("vlnkHora" + contadorLinks++,
                        //                    this.ObtenerUrl(vo_row, row[this.codigoActividadField].ToString(), vd_fechaLunes, this.SelectValueProyecto),
                        //                    this.ObtenerHoras(vo_row)).RenderControl(writer);
                        //writer.RenderEndTag();//End TD
                        vd_fechaLunes = vd_fechaLunes.AddDays(1);

                        contador++;
                        writer.RenderEndTag();//End TR
                    }


                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Genera los controles
        /// necesarios para el calendario
        /// con sus respectivos nombres
        /// de actividades y operaciones
        /// y sus registros.
        /// </summary>
        /// <param name="pd_fecha">DateTime fecha.</param>
        /// <param name="writer">HtmlWriter para realizar el render del control.</param>
        /// <param name="po_actividad">DataRow con con la información de la actividad o la operación</param>
        /// <param name="pi_contandor"></param>
        private void GeneraActividad(DateTime pd_fecha, HtmlTextWriter writer, DataRow po_actividad, int pi_contandor) 
        {
            DataRow vo_row = null;

            if (this.ddlProyectos.SelectedValue.ToString().Equals("0") ||
                this.ddlProyectos.SelectedValue.ToString().Equals("-1"))
            {
                vo_row = this.ObtenerRegistroActividad(pd_fecha, po_actividad[this.codigoActividadField].ToString());
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                this.crearLinkHora("vlnkHora" + pi_contandor,
                                    this.ObtenerUrl(vo_row, po_actividad[this.codigoActividadField].ToString(), pd_fecha, this.SelectValueProyecto),
                                    this.ObtenerHoras(vo_row)).RenderControl(writer);
                writer.RenderEndTag();//End TD
            }
            else
            {
                vo_row = this.ObtenerRegistroActividad(pd_fecha, 
                                                        po_actividad.Field<int>(this.codigoActividadField), 
                                                        po_actividad.Field<int>(this.PaqueteField), 
                                                        po_actividad.Field<int>(this.componenteField), 
                                                        po_actividad.Field<int>(this.EntregableField));
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                this.crearLinkHora("vlnkHora" + pi_contandor,
                                    this.ObtenerUrl(vo_row,
                                                        po_actividad.Field<int>(this.codigoActividadField), 
                                                        po_actividad.Field<int>(this.PaqueteField), 
                                                        po_actividad.Field<int>(this.componenteField), 
                                                        po_actividad.Field<int>(this.EntregableField),
                                                        this.SelectValueProyecto, pd_fecha),
                                    this.ObtenerHoras(vo_row)).RenderControl(writer);
                writer.RenderEndTag();//End TD
                                                                 
            }
        }

        /// <summary>
        /// Obtiene las horas de un datarow
        /// con los datos asignados
        /// Si es nulo devuelve un 0
        /// </summary>
        /// <param name="po_row">DataRow con los datos.</param>
        /// <returns>String con las horas.</returns>
        private String ObtenerHoras(DataRow po_row)
        {
            if (po_row == null)
                return "0";
            else
                return po_row[this.HorasField].ToString();
        }

        /// <summary>
        /// Obtiene la url
        /// basada en los datos
        /// que se encuentran el
        /// data row.
        /// </summary>
        /// <param name="po_row">DataRow fila</param>
        /// <returns>String con la URL</returns>
        private String ObtenerUrl(DataRow po_row,string ps_actividad, DateTime pd_fecha, string ps_proyecto)
        {
            string vs_url = this.UrlLink;

            string vs_registro = po_row == null ? "" : Convert.ToInt32(po_row[this.RegistroField]).ToString();

            vs_url += "?act=" + ps_actividad + "&" + "pro=" + ps_proyecto + "&" + "preg=" + vs_registro + "&" + "fech=" + pd_fecha.ToString("dd/MM/yyyy");

            vs_url = HttpUtility.UrlDecode(vs_url);

            return vs_url;
        }

        /// <summary>
        /// Obtiene la url
        /// basada en los datos
        /// que se encuentran el
        /// data row. 
        /// </summary>
        /// <param name="po_row">DataRow row</param>
        /// <param name="pi_actividad">int actividad</param>
        /// <param name="pi_paquete">int paquete</param>
        /// <param name="pi_componente">int componente</param>
        /// <param name="pi_entregable">int entregable</param>
        /// <param name="ps_proyecto">int proyecto</param>
        /// <param name="pd_fecha">DateTime fecha</param>
        /// <returns></returns>
        private String ObtenerUrl(DataRow po_row, 
                                    int pi_actividad, 
                                    int pi_paquete, 
                                    int pi_componente, 
                                    int pi_entregable, 
                                    string ps_proyecto,
                                    DateTime pd_fecha)
        {
            string vs_url = this.UrlLink;

            string vs_registro = po_row == null ? "" : po_row[this.RegistroField].ToString();

            vs_url += "?act=" + pi_actividad  + "&" +
                       "paq=" + pi_paquete + "&" +
                       "comp=" + pi_componente + "&" +
                       "ent=" + pi_entregable + "&" +  
                       "pro=" + ps_proyecto + "&" + 
                       "preg=" + vs_registro + "&" + 
                       "fech=" + pd_fecha.ToString("dd/MM/yyyy");

            return vs_url;
        }

        /// <summary>
        /// Carga los datos de las actividades.
        /// </summary>
        /// <param name="writer"></param>
        private void cargarDatos(HtmlTextWriter writer)
        {
            List<List<String>> datos = null;
            int contador = 0;
            try
            {
                if (this.poDatos.Count > 0)
                {
                    datos = this.poDatos;

                    // Se recorre cada item de la lista y se crea una nueva fila
                    foreach (List<String> dato in datos)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, this.cssCalendarRow);
                        writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                        for (int i = 0; i < dato.Count; i++)
                        {
                            writer.RenderBeginTag(HtmlTextWriterTag.Td);

                            if (i == 0)
                            {
                                this.crearEtiquetaActividad("lblActividad" + contador, dato[i]).RenderControl(writer);
                            }
                            else
                            {
                                this.crearLinkHora("vlnkHora" + contador, this.urlLink, dato[i]).RenderControl(writer);
                            }

                            contador++;

                            writer.RenderEndTag();//End TD
                        }

                        writer.RenderEndTag();//End TR
                    }

                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Carga los proyectos al calendario.
        /// </summary>
        private void CargarProyectos()
        {
            try
            {
                if (this.poDatosProyecto != null)
                {
                    this.ddlProyectos.DataSource = this.poDatosProyecto;
                    this.ddlProyectos.DataBind();
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Crea un link para
        /// las horas del calendario.
        /// </summary>
        /// <param name="psID">String id del link.</param>
        /// <param name="psUrl">String url del link.</param>
        /// <param name="psValor">String valor del text</param>
        /// <returns>HyperLink</returns>
        private HyperLink crearLinkHora(string psID, string psUrl, string psValor)
        {
            HyperLink vlnkHora = null;
            vlnkHora = new HyperLink();
            vlnkHora.ID = psID;
            vlnkHora.NavigateUrl = psUrl;
            vlnkHora.Text = psValor;
            vlnkHora.CssClass = this.cssLink;
            this.Controls.Add(vlnkHora);

            return vlnkHora;
        }

        /// <summary>
        /// Crea la etiqueta para
        /// la actividad.
        /// </summary>
        /// <param name="psID">String id del control.</param>
        /// <param name="psValor">String valor del label.</param>
        /// <returns></returns>
        private Label crearEtiquetaActividad(string psID, string psValor)
        {
            Label lblActividad = new Label();
            lblActividad.ID = psID;
            lblActividad.Text = psValor;
            this.Controls.Add(lblActividad);

            return lblActividad;
        }

        /// <summary>
        /// Obtiene el lunes
        /// de una semana, según una fecha dada.
        /// </summary>
        /// <param name="pd_fecha">DateTime fecha</param>
        /// <returns></returns>
        private DateTime ObtenerLunes(DateTime pd_fecha)
        {
            DateTime vd_lunes = pd_fecha;

            if (pd_fecha.DayOfWeek == DayOfWeek.Monday)
            {
                this.FechaLunes = pd_fecha;
                return pd_fecha;
            }

            //Se suma un día hasta que se llegue 
            //al lunes
            while (vd_lunes.DayOfWeek != DayOfWeek.Monday)
            {
                vd_lunes = vd_lunes.AddDays(-1);
            }

            this.FechaLunes = vd_lunes;

            return vd_lunes;
        }

        /// <summary>
        /// Crea los controles hijos
        /// del control.
        /// </summary>
        private void CrearControles()
        {
            try
            {
                this.lblLunes = new Label();
                this.lblLunes.ID = "lblLunes";
                this.Controls.Add(this.lblLunes);

                this.lblMartes = new Label();
                this.lblMartes.ID = "lblMartes";
                this.Controls.Add(this.lblMartes);

                this.lblMiercoles = new Label();
                this.lblMiercoles.ID = "lblMiercoles";
                this.Controls.Add(this.lblMiercoles);

                this.lblJueves = new Label();
                this.lblJueves.ID = "lblJueves";
                this.Controls.Add(this.lblJueves);

                this.lblViernes = new Label();
                this.lblViernes.ID = "lblViernes";
                this.Controls.Add(this.lblViernes);

                this.lblSabado = new Label();
                this.lblSabado.ID = "lblSabado";
                this.Controls.Add(this.lblSabado);

                this.lblDomingo = new Label();
                this.lblDomingo.ID = "lblDomingo";
                this.Controls.Add(this.lblDomingo);

                this.lblTarea = new Label();
                this.lblTarea.ID = "lblTarea";
                this.lblTarea.Text = "Actividad";
                this.Controls.Add(this.lblTarea);

                this.btnAnterior = new ImageButton();
                this.btnAnterior.ID = "btnAnterior";
                this.btnAnterior.Width = 30;
                this.btnAnterior.Height = 30;
                this.btnAnterior.ImageUrl = this.urlAnterior;
                this.Controls.Add(this.btnAnterior);
                this.btnAnterior.Click += new ImageClickEventHandler(btnAnterior_Click);

                this.btnSiguiente = new ImageButton();
                this.btnSiguiente.ID = "btnSiguiente";
                this.btnSiguiente.Width = 30;
                this.btnSiguiente.Height = 30;
                this.btnSiguiente.ImageUrl = this.urlSiguiente;
                this.Controls.Add(this.btnSiguiente);
                this.btnSiguiente.Click += new ImageClickEventHandler(btnSiguiente_Click);

                this.lblSemana = new Label();
                this.lblSemana.ID = "lblSemana";
                this.Controls.Add(this.lblSemana);

                this.lblProyecto = new Label();
                this.lblProyecto.ID = "lblProyecto";
                this.lblProyecto.Text = "Proyectos: ";
                this.Controls.Add(this.lblProyecto);

                this.ddlProyectos = new DropDownList();
                this.ddlProyectos.Width = 125;
                this.ddlProyectos.ID = "ddlProyectos";
                //this.ddlProyectos.Items.Add(new ListItem("Proyecto"));
                this.ddlProyectos.AutoPostBack = true;
                this.ddlProyectos.DataTextField = this.dataTextFieldProyecto;
                this.ddlProyectos.DataValueField = this.dataValueFieldProyecto;
                this.ddlProyectos.SelectedIndexChanged += new EventHandler(ddlProyectos_SelectedIndexChanged);
                this.CargarProyectos();
                this.Controls.Add(this.ddlProyectos);

                this.lnbHoy = new LinkButton();
                this.lnbHoy.ID = "lnbHoy";
                this.lnbHoy.Text = "Hoy";
                this.lnbHoy.Click += new EventHandler(lnbHoy_Click);
                this.Controls.Add(this.lnbHoy);

                this.btnImprevisto = new HyperLink();
                this.btnImprevisto.ID = "btnImprevisto";
                this.btnImprevisto.Text = " + Imprevisto";
                this.btnImprevisto.NavigateUrl = urlImprevisto;
                this.btnImprevisto.CssClass = cssBotonImprevisto;
                this.Controls.Add(this.btnImprevisto);


                this.txtCalendario = new TextBox();
                this.txtCalendario.ID = "txtCalendario";
                this.txtCalendario.Text = FechaCalendario.ToString("dd/MM/yyyy");
                this.txtCalendario.AutoPostBack = true;
                this.txtCalendario.TextChanged += new EventHandler(txtCalendario_TextChanged);
                this.Controls.Add(this.txtCalendario);

                this.btnCalendario = new ImageButton();
                this.btnCalendario.ID = "btnCalendario";
                this.btnCalendario.ImageUrl = UrlBotonCalendario;
                this.btnCalendario.CausesValidation = false;
                this.Controls.Add(this.btnCalendario);

                this.cdeCalendario = new CalendarExtender();
                this.cdeCalendario.ID = "cdeCalendario";
                this.cdeCalendario.TargetControlID = "txtCalendario";
                this.cdeCalendario.PopupButtonID = "btnCalendario";
                this.cdeCalendario.Format = "dd/MM/yyyy";
                this.Controls.Add(this.cdeCalendario);

                this.mskDate = new MaskedEditExtender();
                this.mskDate.ID = "mskDate";
                this.mskDate.TargetControlID = "txtCalendario";
                this.mskDate.Mask = "99/99/9999";
                this.mskDate.CultureName = "es-ES";
                this.mskDate.MessageValidatorTip = true;
                this.mskDate.MaskType = MaskedEditType.Date;
                this.mskDate.UserDateFormat = MaskedEditUserDateFormat.DayMonthYear;
                this.Controls.Add(this.mskDate);

                //this.mdvDate = new MaskedEditValidator();
                //this.mdvDate.ID = "mdvDate";
                //this.mdvDate.ControlExtender = "mskDate";
                //this.mdvDate.IsValidEmpty = true;
                //this.mdvDate.ControlToValidate = "txtCalendario";
                //this.mdvDate.InvalidValueMessage = "La fecha es inválida";
                //this.mdvDate.Display = ValidatorDisplay.Dynamic;
                //this.mdvDate.TooltipMessage = "Ingrese una fecha con formato dd/mm/yyyy Ejemplo: 25/05/2011";
                //this.Controls.Add(this.mdvDate);

            }
            catch (Exception po_excepcion)
            {
            }


        }

        /// <summary>
        /// Obtiene a partir de los datos
        /// de registro de actividades
        /// el que corresponde a una fecha
        /// y actividades específica
        /// </summary>
        /// <param name="pd_fecha">DateTime fecha</param>
        /// <param name="ps_actividad">ps_actividad código de la actividad</param>
        private DataRow ObtenerRegistroActividad(DateTime pd_fecha, string ps_actividad)
        {
            DataRow vo_dato = null;
            IEnumerable<DataRow> vo_rows =  null;
            try
            {
                if (poDatosRegistro != null)
                {
                    vo_rows = ((DataSet)this.poDatosRegistro).Tables[0].Select().Where(c => ConvertirFechaInicioDia(c.Field<DateTime>(this.FechaField))
                                                                                            == ConvertirFechaInicioDia(pd_fecha) &&
                                                                                            c.Field<String>(this.CodigoActividadField) == ps_actividad
                                                                                       );
                    if (vo_rows != null && vo_rows.Count() == 1)
                    {
                        vo_dato = vo_rows.ElementAt(0);
                    }

                }
            }
            catch (Exception)
            {
                vo_dato = null;
            }
            return vo_dato;
        }

        /// <summary>
        /// Obtiene a partir de los datos
        /// de registro de actividades
        /// el que corresponde a una fecha
        /// y actividades específica
        /// </summary>
        /// <param name="pd_fecha">DateTime Fecha.</param>
        /// <param name="pi_actividad">int código de la actividad.</param>
        /// <param name="pi_paquete">int código del paquete.</param>
        /// <param name="pi_componente">int código del componente.</param>
        /// <param name="pi_entregable">int entregable</param>
        /// <returns></returns>
        private DataRow ObtenerRegistroActividad(DateTime pd_fecha, 
                                                int pi_actividad,
                                                int pi_paquete, 
                                                int pi_componente, 
                                                int pi_entregable)
        {
            DataRow vo_dato = null;
            IEnumerable<DataRow> vo_rows = null;
            try
            {
                if (poDatosRegistro != null)
                {
                    vo_rows = ((DataSet)this.poDatosRegistro).Tables[0].Select().Where(c => ConvertirFechaInicioDia(c.Field<DateTime>(this.FechaField))
                                                                                            == ConvertirFechaInicioDia(pd_fecha) &&
                                                                                            c.Field<int>(this.codigoActividadField) == pi_actividad &&
                                                                                            c.Field<int>(this.PaqueteField) == pi_paquete &&
                                                                                            c.Field<int>(this.ComponenteField) == pi_componente &&
                                                                                            c.Field<int>(this.EntregableField) == pi_entregable 
                                                                                       );
                    if (vo_rows != null && vo_rows.Count() == 1)
                    {
                        vo_dato = vo_rows.ElementAt(0);
                    }

                }
            }
            catch (Exception)
            {
                vo_dato = null;
            }
            return vo_dato;
        }

        /// <summary>
        /// Convierte una fecha a una
        /// fecha a inicio de día.
        /// </summary>
        /// <param name="pd_fecha">DateTime fecha.</param>
        /// <returns>DateTime</returns>
        private DateTime ConvertirFechaInicioDia(DateTime pd_fecha)
        {
            return new DateTime(pd_fecha.Year, pd_fecha.Month, pd_fecha.Day);
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento que se ejecuta cuando se 
        /// cambia de proyecto.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProyectos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.voCambioFecha != null)
            {
                this.voCambioFecha(this, FechaCalendario, this.ddlProyectos.SelectedValue);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al dar click
        /// al control de siguiente.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnSiguiente_Click(object sender, ImageClickEventArgs e)
        {
            this.FechaLunes = this.FechaLunes.AddDays(8);
            this.FechaActual = this.FechaLunes;
            this.FechaCalendario = this.FechaActual.AddDays(-1);

            if (this.voCambioFecha != null)
            {
                
                this.voCambioFecha(this, FechaCalendario, this.ddlProyectos.SelectedValue);

            }

            this.SelectProyecto = this.ddlProyectos.SelectedValue;

            this.CreateChildControls();

            this.ddlProyectos.SelectedValue = this.SelectProyecto;
        }

        /// <summary>
        /// Evento que se ejecuta al dar click
        /// al control de anterior.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnAnterior_Click(object sender, ImageClickEventArgs e)
        {
            this.FechaLunes = this.FechaLunes.AddDays(-1);
            this.FechaActual = this.FechaLunes.AddDays(-1);
            this.FechaCalendario = this.FechaActual.AddDays(-5);

            if (this.voCambioFecha != null)
            {
                this.voCambioFecha(this, FechaCalendario, this.ddlProyectos.SelectedValue);
            }

            this.SelectProyecto = this.ddlProyectos.SelectedValue;

            this.CreateChildControls();

            this.ddlProyectos.SelectedValue = this.SelectProyecto;
        }

        /// <summary>
        /// Evento que se ejecuta 
        /// al dar click sobre el 
        /// link de "Hoy"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnbHoy_Click(object sender, EventArgs e)
        {
            //Se coloca la fecha en null para que inice al día de hoy.
            ViewState["fechaActual"] = DateTime.Today;
            this.FechaCalendario = DateTime.Today;

            if (this.voCambioFecha != null)
            {
                this.voCambioFecha(this, FechaCalendario, this.ddlProyectos.SelectedValue);
            }

            this.SelectProyecto = this.ddlProyectos.SelectedValue;

            this.CreateChildControls();

            this.ddlProyectos.SelectedValue = this.SelectProyecto;
        }

        /// <summary>
        /// Evento que se ejecuta cuando se cambia la fecha
        /// del textbox del calendario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtCalendario_TextChanged(object sender, EventArgs e)
        {
            DateTime vdfecha;

            if (DateTime.TryParse(this.txtCalendario.Text, out vdfecha))
            {
                //Se coloca la fecha a la colocada en el textbox para que inice el calendario a la fecha.
                ViewState["fechaActual"] = vdfecha;
                FechaCalendario = vdfecha;

                if (this.voCambioFecha != null)
                {
                    this.voCambioFecha(this, FechaCalendario, this.ddlProyectos.SelectedValue);
                }

                this.SelectProyecto = this.ddlProyectos.SelectedValue;

                this.CreateChildControls();

                this.ddlProyectos.SelectedValue = this.SelectProyecto;
            }

        }

        #endregion

        #region Propiedades

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                String s = (String)ViewState["Text"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Text"] = value;
            }
        }


        [Category("Style")]
        [DefaultValue("")]
        [Localizable(true)]
        public String CssHeader
        {
            get { return cssHeader; }
            set { cssHeader = value; }
        }

        [Category("Style")]
        [DefaultValue("")]
        [Localizable(true)]
        public String Css
        {
            get { return css; }
            set { css = value; }
        }

        [Category("Style")]
        [DefaultValue("")]
        [Localizable(true)]
        public String UrlAnterior
        {
            get { return urlAnterior; }
            set { urlAnterior = value; }
        }

        [Category("Style")]
        [DefaultValue("")]
        [Localizable(true)]
        public String UrlSiguiente
        {
            get { return urlSiguiente; }
            set { urlSiguiente = value; }
        }

        [Category("Style")]
        [DefaultValue("")]
        [Localizable(true)]
        public String UrlBotonCalendario
        {
            get { return urlBotonCalendario; }
            set { urlBotonCalendario = value; }
        }

        public DateTime FechaLunes
        {
            get
            {
                String s = ViewState["fechaLunes"] != null ? (String)ViewState["fechaLunes"].ToString() : String.Empty;
                return String.IsNullOrEmpty(s) ? DateTime.Now : Convert.ToDateTime(s);
            }
            set { ViewState["fechaLunes"] = value; }
        }


        public DateTime FechaActual
        {
            get
            {
                String s = ViewState["fechaActual"] != null ? (String)ViewState["fechaActual"].ToString() : String.Empty;
                return String.IsNullOrEmpty(s) ? DateTime.Now : Convert.ToDateTime(s);
            }
            set { ViewState["fechaActual"] = value; }
        }

        public DateTime FechaCalendario
        {
            get
            {
                String s = ViewState["fechaCalendario"] != null ? (String)ViewState["fechaCalendario"].ToString() : String.Empty;
                return String.IsNullOrEmpty(s) ? DateTime.Now : Convert.ToDateTime(s);
            }
            set
            {
                ViewState["fechaCalendario"] = value;
            }
        }

        public List<List<String>> poDatos
        {
            get
            {
                List<List<String>> lista = ViewState["poDatos"] != null ? (List<List<String>>)ViewState["poDatos"] : (new List<List<String>>());
                return lista;
            }
            set
            {
                ViewState["poDatos"] = value;
            }
        }

        public Object poDatosProyecto
        {
            get
            {
                Object lista = ViewState["poDatosProyecto"] != null ? ViewState["poDatosProyecto"] : null;
                return lista;
            }
            set
            {
                ViewState["poDatosProyecto"] = value;
            }
        }

        public Object poDatosRegistro
        {
            get
            {
                Object lista = ViewState["poDatosRegistro"] != null ? ViewState["poDatosRegistro"] : null;
                return lista;
            }
            set
            {
                ViewState["poDatosRegistro"] = value;
            }
        }

        public Object poDatosActividades
        {
            get
            {
                Object lista = ViewState["poDatosActividades"] != null ? ViewState["poDatosActividades"] : null;
                return lista;
            }
            set
            {
                ViewState["poDatosActividades"] = value;
            }
        }

        public string SelectValueProyecto
        {
            get
            {
                if (this.ddlProyectos != null)
                {
                    //if (ViewState["ddlProyecto"] == null)
                    //{
                    //    ViewState["ddlProyecto"] = this.ddlProyectos.SelectedValue;
                    //}

                    //return ViewState["ddlProyecto"].ToString();

                    return this.ddlProyectos.SelectedValue;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["ddlProyecto"] = value;
                ddlProyectos.SelectedValue = value;
            }
        }

        public string SelectProyecto
        {
            get
            {
                return ViewState["ddlProyecto"] != null ? ViewState["ddlProyecto"].ToString() : this.ddlProyectos.Items[0].Value;
            }
            set
            {
                ViewState["ddlProyecto"] = value;
            }
        }

        public Object DatosProyecto
        {
            get
            {
                return datosProyecto;
            }
            set
            {
                datosProyecto = value;
            }
        }

        public Object DataSourceComboProyecto
        {
            set
            {
                this.ddlProyectos.DataSource = value;
            }
        }

        public String CssDays
        {
            get { return cssDays; }
            set { cssDays = value; }
        }


        public String CssCalendarRow
        {
            get { return cssCalendarRow; }
            set { cssCalendarRow = value; }
        }

        public String CssLink
        {
            get { return cssLink; }
            set { cssLink = value; }
        }

        public String UrlLink
        {
            get { return urlLink; }
            set { urlLink = value; }
        }

        public String CssBotonImprevisto
        {
            get { return cssBotonImprevisto; }
            set { cssBotonImprevisto = value; }
        }

        public String UrlImprevisto
        {
            get { return urlImprevisto; }
            set { urlImprevisto = value; }
        }

        public String DataTextFieldProyecto
        {
            get { return dataTextFieldProyecto; }
            set { dataTextFieldProyecto = value; }
        }

        public String DataValueFieldProyecto
        {
            get { return dataValueFieldProyecto; }
            set { dataValueFieldProyecto = value; }
        }


        public String CodigoActividadField
        {
            get { return codigoActividadField; }
            set { codigoActividadField = value; }
        }

        public String DescripcionField
        {
            get { return descripcionField; }
            set { descripcionField = value; }

        }

        public String FechaField
        {
            get { return fechaField; }
            set { fechaField = value; }
        }

        public String TipoField
        {
            get { return tipoField; }
            set { tipoField = value; }
        }

        public String RegistroField
        {
            get { return registroField; }
            set { registroField = value; }
        }

        public String HorasField
        {
            get { return horasField; }
            set { horasField = value; }
        }

        public String PaqueteField
        {
            get { return paqueteField; }
            set { paqueteField = value; }
        }

        public String ComponenteField
        {
            get { return componenteField; }
            set { componenteField = value; }
        }

        public String EntregableField
        {
            get { return entregableField; }
            set { entregableField = value; }
        }

        public String DescripcionPaquete
        {
            get { return descripcionPaquete; }
            set { descripcionPaquete = value; }
        }


        public String UsuarioField
        {
            get { return usuarioField; }
            set { usuarioField = value; }
        }

        public String CssAjaxCalendar
        {
            get { return cssAjaxCalendar; }
            set { cssAjaxCalendar = value; }
        }

        public String CssShowMore
        {
            get { return cssShowMore; }
            set { cssShowMore = value; }
        }


        #endregion

        #region Atributos

        private Label lblTarea;

        private Label lblLunes;

        private Label lblMartes;

        private Label lblMiercoles;

        private Label lblJueves;

        private Label lblViernes;

        private Label lblSabado;

        private Label lblDomingo;

        private Label lblSemana;

        private Label lblProyecto;

        private DropDownList ddlProyectos;

        private LinkButton lnbHoy;

        private HyperLink btnImprevisto;

        private ImageButton btnAnterior;

        private ImageButton btnSiguiente;

        private String cssHeader;

        private String css;

        private String cssDays;

        private String cssCalendarRow;

        private String urlAnterior;

        private String urlSiguiente;

        private String urlBotonCalendario;

        private CalendarExtender cdeCalendario;

        private TextBox txtCalendario;

        private ImageButton btnCalendario;

        public event DateChange voCambioFecha;

        public delegate void DateChange(object sender, DateTime pd_fechaLunes, string proyecto);

        private String cssLink;

        private String urlLink;

        private String cssBotonImprevisto;

        private String urlImprevisto;

        private String dataTextFieldProyecto;

        private String dataValueFieldProyecto;

        private Object datosProyecto;

        private String codigoActividadField;

        private String descripcionField;

        private String fechaField;

        private String tipoField;

        private String registroField;

        private String horasField;

        private String paqueteField;

        private String componenteField;

        private String entregableField;

        private String descripcionPaquete;

        private String usuarioField;

        private MaskedEditExtender mskDate;

        private MaskedEditValidator mdvDate;

        private String cssAjaxCalendar;

        private String cssShowMore;

      

        #endregion

    }
}
