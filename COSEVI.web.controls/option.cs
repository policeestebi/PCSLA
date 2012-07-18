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
    [ToolboxData("<{0}:option runat=server></{0}:option>")]
    public class option : WebControl
    {
       
        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(Text);
        }

        /// <summary>
        /// Obtiene la colección de objetos
        /// </summary>
        public override ControlCollection Controls
        {
            get
            {
                EnsureChildControls();
                return base.Controls;
            }
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
        /// <summary>
        /// Se sobreescribe para la creación de los controles hijo
        /// </summary>
        protected override void CreateChildControls()
        {

            base.CreateChildControls();
            this.options = new List<option>();
           
        }

        /// <summary>
        /// Renderiza el control
        /// </summary>
        /// <param name="writer">HtmlTextWriter</param>
        protected override void Render(HtmlTextWriter writer)
        {
            AddAttributesToRender(writer);
            writer.RenderBeginTag(HtmlTextWriterTag.Li);
            writer.AddAttribute(HtmlTextWriterAttribute.Href, this.url);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.height);

            if (this.padre != null)
            {

                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);

            }
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write(this.Text);
            writer.RenderEndTag();//Cierre del Span
            writer.RenderEndTag();//Cierre del a
            //Renderiza las opciones hijas
            if (this.options != null && this.options.Count > 0)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                //Las opciones hijas se renderizan dentro de un UL
                writer.RenderBeginTag(HtmlTextWriterTag.Ul);

                foreach(option opcion in options){

                    opcion.RenderControl(writer);

                }

                writer.RenderEndTag();//Cierre del UL
                writer.RenderEndTag();//Cierre del div
            }
            writer.RenderEndTag();//Cierre del li

            
        }

        #region Eventos

       

        #endregion

        #region Atributos

        private string url;

        private option padre;

        private List<option> options;

        private UpdatePanel updPanel;

        private string height;

        #endregion

        #region Propiedades

        [Bindable(true),
        Category("Default"),
         DefaultValue(0),
         Description("Url de la opción de menú")]
        public string Url
        {
            get
            {
                this.EnsureChildControls();
                return url;
            }
            set
            {
                this.EnsureChildControls();
                url = value;
            }
        }

        [Bindable(true),
      Category("Default"),
       DefaultValue(0),
       Description("Alto de la página.")]
        public string Height
        {
            get
            {
                this.EnsureChildControls();
                return height;
            }
            set
            {
                this.EnsureChildControls();
                height = value;
            }
        }

        /// <summary>
        /// Lista de opciones hijas
        /// </summary>
        public List<option> Options
        {
            get { return options; }
            set { options = value; }
        }
        
        //Padre de la opcion
        public option Padre
        {
            get { return padre; }
            set { padre = value; }
        }

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

        public UpdatePanel UpdPanel
        {
            get { return updPanel; }
            set { updPanel = value; }
        }

        #endregion

    }
}
