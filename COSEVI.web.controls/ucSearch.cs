using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COSEVI.web.controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ucSearch runat=server></{0}:ucSearch>")]
    public class ucSearch : CompositeControl
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

       


        #region Metodos

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

        /// <summary>
        /// Se encarga de crear los controles hijos
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            Controls.Clear();

            //Se crean los controles.
            this.crearControles();

            //Se inicializan los eventos.
            this.inicializarEventos();

        }

        /// <summary>
        /// Se crean los controles que posee 
        /// el control de búsqueda.
        /// </summary>
        private void crearControles()
        {

            try
            {
                //lblFiltro
                this.lblFiltro = new Label();
                this.lblFiltro.ID = "lblFiltro";
                this.lblFiltro.Text = "Búsqueda: ";

                //ddlFiltros
                this.ddlFiltros = new DropDownList();
                this.ddlFiltros.ID = "ddlFiltros";

                //btnBuscar
                this.btBuscar = new Button();
                this.btBuscar.ID = "btnBuscar";
                this.btBuscar.Text = "Buscar";
                this.btBuscar.CausesValidation = false;

                //txtBuscar
                this.txtBuscar = new TextBox();
                this.txtBuscar.ID = "txtBuscar";


                //Se agregan los controles
                this.Controls.Add(this.lblFiltro);
                this.Controls.Add(this.btBuscar);
                this.Controls.Add(this.ddlFiltros);


            }
            catch (Exception)
            {
            }
            
        }

   
        /// <summary>
        /// Se sobre escribe el método de RenderContents
        /// </summary>
        /// <param name="output"></param>
        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(Text);
        }

        /// <summary>
        /// Se sobreescribe el método de render
        /// para crear la estructura la el control.
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            AddAttributesToRender(writer);

            this.lblFiltro.RenderControl(writer);
            writer.Write("&nbsp;");
            this.txtBuscar.RenderControl(writer);
            writer.Write("&nbsp;");
            this.ddlFiltros.RenderControl(writer);
            writer.Write("&nbsp;");
            this.btBuscar.RenderControl(writer);
            writer.Write("&nbsp; &nbsp; &nbsp;");
                    
        }

        /// <summary>
        /// Inicializa los eventos de los controles.
        /// </summary>
        protected void inicializarEventos()
        {
            this.btBuscar.Click += new EventHandler(btBuscar_Click);
        }

        /// <summary>
        /// Evento que se ejecuta al dar click al botón de búsqueda.
        /// </summary>
        /// <param name="sender">Object objeto que ejecuta la accion.</param>
        /// <param name="e">Evento</param>
        protected void btBuscar_Click(object sender, EventArgs e)
        {
            if (this.SearchClick != null)
            {
                this.SearchClick(sender, e, this.TxtBuscarText, this.ddlFiltros.SelectedItem);
            }
        }

        #endregion

        #region Eventos

        public event searchClick SearchClick;
   
        public delegate void searchClick(object sender, EventArgs e, string value, ListItem seletecItem);

        #endregion

        #region Controles
        /// <summary>
        /// Botón de buscar
        /// </summary>
        Button btBuscar;

        /// <summary>
        /// Label del filtro
        /// </summary>
        Label lblFiltro;

        /// <summary>
        /// DropDownList con la lista 
        /// de filtros.
        /// </summary>
        DropDownList ddlFiltros;

        /// <summary>
        /// TextBox de buscar.
        /// </summary>
        TextBox txtBuscar;

        #endregion

        #region Propiedades

         [Bindable(true),
         Category("Default"),
         DefaultValue(null),
         Description("Colección de listItems del filtro")]
        public ListItemCollection LstCollecction
        {
            get 
            {
                this.EnsureChildControls();
                return this.ddlFiltros.Items;
            }
            
        }

         [Bindable(true)]
         [Category("Appearance")]
         [DefaultValue("")]
         [Localizable(true)]
         public string TxtBuscarText
         {
             get
             {
                 this.EnsureChildControls();
                 return this.txtBuscar.Text;
             }

             set
             {
                 this.EnsureChildControls();
                 this.txtBuscar.Text = value;
             }
         }

        /// <summary>
        /// Obtiene el Filtro del control.
        /// </summary>
         public string Filter
         {
             get
             {
                 this.EnsureChildControls();

                 string filter = this.ddlFiltros.SelectedValue + " LIKE '%" + this.TxtBuscarText + "%'";

                 return filter;
             }
         }


        #endregion

        #region Override

        /// <summary>
        /// Carga los valores del viewstate
        /// </summary>
        /// <param name="savedState"></param>
         protected override void LoadViewState(object savedState)
         {
             if (savedState != null)
             {
                 object[] myState = (object[])savedState;
                 if (myState[0] != null)
                 {
                     base.LoadViewState(myState[0]);
                 }
                 //if (myState[1] != null && !String.IsNullOrEmpty(myState[1].ToString()))
                 //{
                 //    this.TxtBuscarText = (string)myState[1];
                 //}
                 //else
                 //{
                 //    this.TxtBuscarText = Page.Request.Form[this.txtBuscar.UniqueID].ToString();
                 //}

                 this.TxtBuscarText = Page.Request.Form[this.txtBuscar.UniqueID].ToString();
             }

         }

        /// <summary>
        /// Salva los valores del view state
        /// </summary>
        /// <returns></returns>
         protected override object SaveViewState()
         {

             object baseSate = base.SaveControlState();
             object textBuscar = Page.Request.Form[this.txtBuscar.UniqueID];//this.TxtBuscarText;
             object[] allStates = new object[2];
             allStates[0] = baseSate;
             allStates[1] = textBuscar;

             return allStates;
         }

         #endregion

        
    }
}
