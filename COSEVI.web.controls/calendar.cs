using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Security.Permissions;
using System.Drawing;
using COSEVI.CSLA.lib.entidades;


namespace COSEVI.web.controls
{
    
   [
    AspNetHostingPermission(SecurityAction.Demand,
       Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand,
       Level = AspNetHostingPermissionLevel.Minimal),
    DefaultEvent(""),
    DefaultProperty("Text"),
    ToolboxData("<{0}:calendar runat=\"server\"> </{0}:Register>"),
   ]
    public class calendar : CompositeControl
    {
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        public override ControlCollection Controls
        {
            get
            {
                EnsureChildControls();
                return base.Controls;
            }
        }

        public override void DataBind()
        {
            base.DataBind();
        }

        void btnHoy_Click(object sender, EventArgs e)
        {
            EnsureChildControls();
            this.btnHoy.Text = DayOfWeek.Friday.ToString();
        }

        /// <summary>
        /// Crea los diferentes controles que estarán asociados con el control web
        /// </summary>
        private void crearControles()
        {
            this.grvCalendario = new GridView();
            this.grvCalendario.ID = "grvCalendario";
            this.grvCalendario.AutoGenerateColumns = false;

            this.btnAnterior = new ImageButton();
            this.btnAnterior.ID = "btnAnterior";

            this.btnSiguiente = new ImageButton();
            this.btnSiguiente.ID = "btnSiguiente";

            this.btnHoy = new Button();
            this.ddlProyectos = new DropDownList();
            this.txtCalendario = new TextBox();
            this.txtCalendario.ID = "txtCalendario";
            this.btnCalendario = new ImageButton();
            this.btnCalendario.ID = "btnCalendario";
            this.cdeCalendarExtender = new CalendarExtender();
            this.cdeCalendarExtender.PopupButtonID = this.btnCalendario.ID;
            this.cdeCalendarExtender.TargetControlID = txtCalendario.ID;

            this.Controls.Add(this.btnAnterior);
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.btnHoy);
            this.Controls.Add(this.ddlProyectos);
            this.Controls.Add(this.txtCalendario);
            this.Controls.Add(this.btnCalendario);
            this.Controls.Add(this.cdeCalendarExtender);

            //Se crea la lista que contiene las columnas agregadas al gridView
            this.columnas = new List<DataControlField>();

            //Update panel para los llamados asincronos
            this.udpPanel = new UpdatePanel();

            AsyncPostBackTrigger triggerSiguiente = new AsyncPostBackTrigger();
            triggerSiguiente.EventName = "Click";
            triggerSiguiente.ControlID = this.btnSiguiente.ID;

            AsyncPostBackTrigger triggerAnterior = new AsyncPostBackTrigger();
            triggerAnterior.EventName = "Click";
            triggerAnterior.ControlID = this.btnAnterior.ID;

            this.udpPanel.Triggers.Add(triggerSiguiente);
            this.udpPanel.Triggers.Add(triggerAnterior);

           // this.udpPanel.ContentTemplateContainer.Controls.Add(this.grvCalendario);

            this.Controls.Add(this.udpPanel);
            
        }

        //private void agregarColumnas()
        //{
        //    BoundField columnaLunes = new BoundField();
        //    columnaLunes.HeaderText = cld_resources.ResourceManager.GetString("Lunes");

        //    HyperLinkField columnaActividad = new HyperLinkField();
        //    columnaActividad.HeaderText = cld_resources.ResourceManager.GetString("Actividad");

        //    columnaActividad.DataNavigateUrlFields = new string[] { "Actividad" };
        //    columnaActividad.DataTextField = "Actividad";
        //    columnaLunes.DataField = "Lunes";
            
        //    this.gvwCalendario.Columns.Add(columnaActividad);
        //    this.gvwCalendario.Columns.Add(columnaLunes);  
        //}

        /// <summary>
        /// Se sobreescribe el método encargado de la creación de los controles hijo
        /// </summary>
        protected override void CreateChildControls()
        {
            

                base.CreateChildControls();

                Controls.Clear();
                this.crearControles();
                this.crearColumnasIniciales();
                this.inicializarEstilosGrid();
               //this.cargarHeadersConDias(DateTime.Now);
                this.inicializarEventos();
            
            
        }

        /// <summary>
        /// Se sobrescribe el método encargado de renderizar el control
        /// </summary>
        /// <param name="writer">HtmlTextWriter</param>
        protected override void Render(HtmlTextWriter writer)
        {
            AddAttributesToRender(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding,"2",false);

            writer.AddAttribute(HtmlTextWriterAttribute.Style, "display:block;Width:auto;margin:5px 5px 5px 5px");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);//Div Contenedor de los controles
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            this.btnAnterior.RenderControl(writer);
            writer.Write("&nbsp;");
            writer.RenderEndTag();//Cierre de td
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            this.txtCalendario.RenderControl(writer);
            writer.Write("&nbsp;");
            this.btnCalendario.RenderControl(writer);
            this.cdeCalendarExtender.RenderControl(writer);
            writer.Write("&nbsp;");
            writer.RenderEndTag();//Cierre de td
            writer.Write("&nbsp;");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            this.btnSiguiente.RenderControl(writer);
            writer.RenderEndTag();//Cierre de td
            writer.RenderEndTag();//Cierre del tr
            writer.RenderEndTag();//Cierre de table
            writer.RenderEndTag();//Cierre de Div contenedor de los controles

            writer.AddAttribute(HtmlTextWriterAttribute.Style, "display:block;Width:auto;margin:5px 5px 5px 5px");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);//Div Contenedor del calendario
            writer.RenderBeginTag(HtmlTextWriterTag.Table); //Contenedor
            writer.AddAttribute(HtmlTextWriterAttribute.Align,"center");
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            this.udpPanel.RenderControl(writer);
            this.grvCalendario.RenderControl(writer);
            writer.RenderEndTag();//Cierre del td
            writer.RenderEndTag();//Cierre del tr
            writer.RenderEndTag();//Cierre del Table
            writer.RenderEndTag();//Cierre del Div contenedor de los controles
        }

        /// <summary>
        /// Se agregan las columnas iniciales del calendario en este caso los 
        /// días de la semana
        /// </summary>
        protected void crearColumnasIniciales()
        {
            HyperLinkField columnaActividad = new HyperLinkField();
            columnaActividad.DataTextField = "Actividad";
            columnaActividad.HeaderText = cld_resources.ResourceManager.GetString("Actividad");

            BoundField columnaLunes = new BoundField();
            columnaLunes.DataField = "Lunes";
            columnaLunes.HeaderText = cld_resources.ResourceManager.GetString("Lunes");

            BoundField columnaMartes = new BoundField();
            columnaMartes.DataField = "Lunes";
            columnaMartes.HeaderText = cld_resources.ResourceManager.GetString("Martes");

            BoundField columnaMiercoles = new BoundField();
            columnaMiercoles.DataField = "Lunes";
            columnaMiercoles.HeaderText = cld_resources.ResourceManager.GetString("Miercoles");

            BoundField columnaJueves = new BoundField();
            columnaJueves.DataField = "Lunes";
            columnaJueves.HeaderText = cld_resources.ResourceManager.GetString("Jueves");

            BoundField columnaViernes = new BoundField();
            columnaViernes.DataField = "Lunes";
            columnaViernes.HeaderText = cld_resources.ResourceManager.GetString("Viernes");

            BoundField columnaSabado = new BoundField();
            columnaSabado.DataField = "Lunes";
            columnaSabado.HeaderText = cld_resources.ResourceManager.GetString("Sabado");

            BoundField columnaDomingo = new BoundField();
            columnaDomingo.DataField = "Lunes";
            columnaDomingo.HeaderText = cld_resources.ResourceManager.GetString("Domingo");

            

            this.grvCalendario.Columns.Add(columnaActividad);
            this.grvCalendario.Columns.Add(columnaLunes);
            this.grvCalendario.Columns.Add(columnaMartes);
            this.grvCalendario.Columns.Add(columnaMiercoles);
            this.grvCalendario.Columns.Add(columnaJueves);
            this.grvCalendario.Columns.Add(columnaViernes);
            this.grvCalendario.Columns.Add(columnaSabado);
            this.grvCalendario.Columns.Add(columnaDomingo);


            ArrayList lista = new ArrayList();

            lista.Add(new clsAsignacion("valor",1));
            lista.Add(new clsAsignacion("valor", 1));
            lista.Add(new clsAsignacion("valor", 1));
            lista.Add(new clsAsignacion("valor", 1));
            lista.Add(new clsAsignacion("valor", 1));
            lista.Add(new clsAsignacion("valor", 1));

            this.grvCalendario.DataSource = lista;

            this.grvCalendario.DataBind();

            //Se agregan las columnas a la lista de columnas
            this.columnas.Add(columnaActividad);
            this.columnas.Add(columnaLunes);
            this.columnas.Add(columnaMartes);
            this.columnas.Add(columnaMiercoles);
            this.columnas.Add(columnaJueves);
            this.columnas.Add(columnaViernes);
            this.columnas.Add(columnaSabado);
            this.columnas.Add(columnaDomingo);

        }

        

        protected virtual void inicializarEstilosGrid()
        {
            //Header
            this.grvCalendario.HeaderStyle.Font.Bold = true;
            this.grvCalendario.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            this.grvCalendario.HeaderStyle.BackColor = System.Drawing.Color.AliceBlue;
            this.grvCalendario.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            this.grvCalendario.Font.Name = "Trebuchet MS, Arial, Helvetica, Sans-serif";
 

           
            this.grvCalendario.GridLines = GridLines.Both;
            this.grvCalendario.CellPadding = 10;
            this.grvCalendario.CellSpacing = 10;
            this.grvCalendario.AllowPaging = true;
            //this.grvCalendario.CellSpacing = 5;
            this.grvCalendario.ShowFooter = true;

            this.grvCalendario.AlternatingRowStyle.BackColor = Color.White;
            this.grvCalendario.BackColor = Color.Orange;
            this.grvCalendario.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
           

            
           

        }

       //Recordario Sunday = 0 Saturday = 6
        private void cargarHeadersConDias(DateTime fecha)
        {
            int diaActual = (int)fecha.DayOfWeek;
            DateTime diaPosterior = fecha;
            DateTime diaAnterior = fecha.Subtract(new TimeSpan(1, 0, 0, 0));

            if (diaActual == 0)
            {
                diaActual = 7;
            }

            if(this.diaFinal == 0)
            {
                this.DiaFinal = 7;
            }
            
            //Coloca los días posteriores a la fecha e inclusive
            for (int i = diaActual; i <= this.DiaFinal; i++)
            {
                this.grvCalendario.Columns[i].HeaderText = diaPosterior.Day.ToString() 
                    + "/" + diaPosterior.Month.ToString() + " " +this.obtenerNombreDiaSemana(i);
                diaPosterior = diaPosterior.AddDays(1);
            }

            //Coloca los días anteriores a la fecha
            this.DiaFinalSemana = diaPosterior;

            for (int i = diaActual - 1; i > 0; i--)
            {
                this.grvCalendario.Columns[i].HeaderText = diaAnterior.Day.ToString() 
                    + "/" + diaAnterior.Month.ToString() + " " + this.obtenerNombreDiaSemana(i);
                diaAnterior = diaAnterior.Subtract(new TimeSpan(1, 0, 0, 0));
            }

            this.DiaInicioSemana = diaAnterior;

            this.grvCalendario.DataBind();
            
        }

        public void cargarColumnasConDias(DateTime fechaInicio)
        {
            this.cargarHeadersConDias(fechaInicio);
        }

        public void siguienteSemana()
        {
            this.cargarHeadersConDias(this.DiaFinalSemana);
        }

        public void anteriorSemana()
        {
            this.cargarHeadersConDias(this.DiaInicioSemana);
        }


        private String obtenerNombreDiaSemana(int dia)
        {
            string nombreDia = String.Empty;
            switch (dia)
            {
                case 1:
                    nombreDia = cld_resources.Lunes;
                    break;
                case 2:
                    nombreDia = cld_resources.Martes;
                    break;
                case 3:
                    nombreDia = cld_resources.Miercoles;
                    break;
                case 4:
                    nombreDia = cld_resources.Jueves;
                    break;
                case 5:
                    nombreDia = cld_resources.Viernes;
                    break;
                case 6:
                    nombreDia = cld_resources.Sabado;
                    break;
                case 7:
                    nombreDia = cld_resources.Domingo;
                    break;
                default:
                    nombreDia = String.Empty;
                    break;

            }
            return nombreDia;
        }

       /// <summary>
       /// Obtiene una columna por el nombre del header
       /// </summary>
       /// <param name="columnaName">Nombre de la columna</param>
       /// <returns>True si la encuentra, false si no es posible encontrar</returns>
        public bool getColumnByName(String columnaName, out DataControlField columna)
        {
            bool resultado = true;
            try
            {

                if (this.columnas != null)
                {
                    for (int i = 0; i < this.columnas.Count; i++)
                    {
                        if (this.columnas[i].HeaderText.Equals(columnaName))
                        {
                            columna = this.columnas[i];
                            return true;
                        }
                    }
                }
                else
                {
                    resultado = false; ;
                }
            }
            catch (Exception)
            {
                columna = null;
                resultado = false;
            }
            columna = null;
            return resultado;
        }

        /// <summary>
        /// Registra los diferentes eventos de los controles
        /// </summary>
        protected void inicializarEventos()
        {
            this.btnAnterior.Click += new ImageClickEventHandler(btnAnterior_Click);
            this.btnSiguiente.Click += new ImageClickEventHandler(btnSiguiente_Click);
           
        }

       /// <summary>
       /// Evento encargado del click de siguiente del control
       /// </summary>
       /// <param name="sender">Objeto que inicia el evento</param>
       /// <param name="e"></param>
        void btnSiguiente_Click(object sender, ImageClickEventArgs e)
        {
            //this.cargarHeadersConDias(this.diaFinalSemana);

            if (this.clickSiguiente != null)
            {
                this.clickSiguiente(sender, e);
            }
        }

       /// <summary>
       /// Evento encargado del click de anterior del control
       /// </summary>
       /// <param name="sender">Objeto que inicia el evento</param>
       /// <param name="e">Evento</param>
        public void btnAnterior_Click(object sender, ImageClickEventArgs e)
        {
            //this.cargarHeadersConDias(this.diaInicioSemana);

            if (this.clickAnterior != null)
            {
                this.clickAnterior(sender, e);
            }
        }

        protected override void RecreateChildControls()
        {
            EnsureChildControls();
            
        }


        #region Controles

        private ImageButton btnAnterior;

        private ImageButton btnSiguiente;

        private Button btnHoy;

        private ImageButton btnCalendario;

        private DropDownList ddlProyectos;

        private CalendarExtender cdeCalendarExtender;

        private TextBox txtCalendario;

        private GridView grvCalendario;

        private List<DataControlField> columnas;

        private int diaFinal;

        private int diaInicial;

        private UpdatePanel udpPanel;

       // private DateTime diaInicioSemana;

        private DateTime DiaInicioSemana
        {
            get { 
                 String s = (String)ViewState[constantes.diaInicioSemana].ToString();
                 return ((s == null) ? DateTime.Now : Convert.ToDateTime(s));
           
            }
            set { ViewState[constantes.diaInicioSemana] = value; }
        }

       // private DateTime diaFinalSemana;

        private DateTime DiaFinalSemana
        {
            get {
                String s = (String)ViewState[constantes.diaFinalSemana].ToString();
                return ((s == null) ? DateTime.Now : Convert.ToDateTime(s));
            }
            set { ViewState[constantes.diaFinalSemana] = value; }
        }

        #endregion


        #region Eventos

        public event ImageClick clickSiguiente;
        public event ImageClick clickAnterior;

        public delegate void  ImageClick (object sender, ImageClickEventArgs e);

       

        #endregion
       

        #region Propiedades

        [Bindable(true),
        Category("Default"),
        DefaultValue(""),
        Description("Función de BtnCalendarioOnClientDateSelectionChanged")]
        public string BtnCalendarioOnClientDateSelectionChanged
        {
            get {
                this.EnsureChildControls();
                return this.cdeCalendarExtender.OnClientDateSelectionChanged;
            }
            set {
                this.EnsureChildControls();
                this.cdeCalendarExtender.OnClientDateSelectionChanged = value; 
            }
        }

        [Bindable(true),
        Category("Default"),
        DefaultValue(""),
        Description("Dirección de la imagen que aparece en el al lado del calendario.")]
        public string BtnCalendarImage
        {
            get {
                this.EnsureChildControls();
                return this.btnCalendario.ImageUrl; 
            }
            set {
                this.EnsureChildControls();
                this.btnCalendario.ImageUrl = value; 
            }
        }

        [Bindable(true),
        Category("Default"),
        DefaultValue(""),
        Description("Formato para el calendario desplegado (Ejm. dd/mm/yy).")]
        public string BtnCalendarFormat
        {
            get {
                this.EnsureChildControls();
                return cdeCalendarExtender.Format; 
            }
            set {
                this.EnsureChildControls();
                cdeCalendarExtender.Format = value; 
            }
 
      
         }

        [Bindable(true),
        Category("Default"),
        DefaultValue(""),
        Description("Imagen para el botón siguiente.")]
        public string BtnSiguienteImage
        {
            get {
                this.EnsureChildControls();
                return btnSiguiente.ImageUrl; }
            set {
                this.EnsureChildControls();
                btnSiguiente.ImageUrl =  value; 
            }
        }

        [Bindable(true),
        Category("Default"),
        DefaultValue(""),
        Description("Imagen para el botón anterior.")]
        public string BntAnteriorImage
        {
            get {
                this.EnsureChildControls();
                return this.btnAnterior.ImageUrl; 
            }
            set {
                this.EnsureChildControls();
                this.btnAnterior.ImageUrl = value; 
            }
        }

        [Bindable(true),
         Category("Default"),
         DefaultValue(0),
         Description("Día final de la semana")]
        public int DiaFinal
        {
            get { return diaFinal; }
            set { diaFinal = value; }
        }

        [Bindable(true),
         Category("Default"),
         DefaultValue(1),
         Description("Día inicial de la semana")]
        public int DiaInicial
        {
            get { return diaInicial; }
            set { diaInicial = value; }
        }

        #endregion

    }
}
