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
    [ToolboxData("<{0}:menu runat=server></{0}:menu>")]
    public class menu : CompositeControl
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

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            if (this.options != null && this.options.Count > 0)
            {
                foreach (option op in this.options)
                {
                    this.Controls.Add(op);
                }
            }

            //this.options = new List<option>();
            //this.inicializarPrueba();
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(Text);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ID);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
            if (options != null)
            {
                foreach (option opcion in options)
                {
                    opcion.RenderControl(writer);
                }
            }
            writer.RenderEndTag();//Fin del ul
            writer.RenderEndTag();//Fin del div
        }

        private List<option> options;

        public List<option> Options
        {
            get { return options; }
            set { options = value; }
        }

        private UpdatePanel updPanel;

        public UpdatePanel UpdPanel
        {
            get { return updPanel; }
            set { updPanel = value; }
        }
      

        private void inicializarPrueba()
        {
            option opcionPadre = new option();
            opcionPadre.Text = "Administración";
            opcionPadre.CssClass = "parent";
            opcionPadre.Url = "#";

            option opcionHija = new option();
            opcionHija.Text = "Usuarios";
            opcionHija.Url = "#";
            opcionHija.CssClass = "parent";
            opcionHija.Padre = opcionPadre;

            option opcion2 = new option();
            opcion2.Text = "Permisos";
            opcion2.Url = "#../App_pages/mod.Administracion/frw_permisos.aspx";
            opcion2.Padre = opcionHija;
            opcion2.UpdPanel = this.updPanel;

            option opcion3 = new option();
            opcion3.Text = "Prueba";
            opcion3.Url = "#../prueba.aspx";
            opcion3.Padre = opcionHija;
            opcion3.UpdPanel = this.updPanel;

            opcionHija.addOption(opcion2);
            opcionHija.addOption(opcion3);

            opcionPadre.addOption(opcionHija);

            this.addOption(opcionPadre);
            this.CssClass = "menu";
            
        }

        public bool addOption(option opcion)
        {
            bool resultado = true;

            try
            {
                if (this.options == null)
                {
                    this.options = new List<option>();
                }

                this.options.Add(opcion);

            }
            catch (Exception)
            {
                resultado = false;
            }

            return resultado;
        }
    }
}
