<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master" AutoEventWireup="true" CodeBehind="frw_operaciones.aspx.cs" Inherits="CSLA.web.App_pages.mod.ControlSeguimiento.frw_operaciones" %>

<%@ Register Assembly="COSEVI.web.controls" Namespace="COSEVI.web.controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>


<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="tituloPagina" runat="server">
    Lista de Operaciones
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cuerpoPagina" runat="server">

<script type="text/javascript">

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Load);

    function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined) {//Condicion si queremos manejar que hubo un error y no ejecutar nada
            Load();
        }
    }

    $(document).ready(function () {
        Load();
    });

    function Load() {
        $(document).ready(function () {
            $(".asg").fancybox({
                'width': '100%',
                'height': 400,
                'autoScale': false,
                'transitionIn': 'none',
                'transitionOut': 'none',
                'type': 'iframe',
                'closeBtn': true,
                onClosed: function () {
                    __doPostBack('<%= upd_Principal.ClientID  %>', '');

                }
            });
        });
    }

    </script>
       <asp:ScriptManager ID="scr_Man" runat="server">
    </asp:ScriptManager>
      <asp:UpdatePanel ID="upd_Principal" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>

            <act:Accordion ID="ard_principal" runat="server" SelectedIndex="0" FadeTransitions="false"
                FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="false"
                SuppressHeaderPostbacks="true" HeaderCssClass="encabezadoAcordeon" ContentCssClass="contenidoAcordeon"
                HeaderSelectedCssClass="encabezadoSeleccionadoAcordeon">
                <Panes>
                    <act:AccordionPane ID="acp_listadoDatos" runat="server">
                        <Header>
                            <a href="" style="color: #FFFFFF; font-size: 12px;">Listado de Operaciones</a>
                        </Header>
                        <Content>
                        <table id="tbl_contenido">
                             <tr>
                                    <%-- Contenido de agregado Arriba--%>
                                </tr>
                                <tr>
                                    <td>
                                        <%-- Búsqueda--%>
                                        <cc1:ucSearch ID="ucSearchPaquete" runat="server" OnSearchClick="ucSearchPaquete_searchClick" />
                                        <asp:Button ID="btn_agregar" runat="server" Text="Agregar" OnClick="btn_agregar_Click"
                                            CausesValidation="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%-- Espacio--%>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%-- GridView--%>
                                        <asp:GridView ID="grd_listaOperaciones" AllowPaging="True" runat="server" AutoGenerateColumns="False"
                                            Width="100%" CssClass="Grid" CellSpacing="1" CellPadding="3" OnRowCommand="grd_listaPaquete_RowCommand"
                                            OnPageIndexChanging="grd_listaPaquete_PageIndexChanging">
                                            <PagerSettings PageButtonCount="20" />
                                            <Columns>
                                                <asp:BoundField DataField="pPK_Codigo" HeaderText="Código" SortExpression="pPK_Codigo" />
                                                <asp:BoundField DataField="pDescTipo" HeaderText="Tipo" SortExpression="pDescTipo" />
                                                <asp:BoundField DataField="pDescripcion" HeaderText="Descripción" SortExpression="pDescripcion" />
                                                <asp:CheckBoxField DataField="pActivo" HeaderText="Activo" ReadOnly="true" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="btn_ver" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            CausesValidation="false" ImageUrl="~/App_Themes/Basico/Botones/img_ver.gif" ToolTip="Ver" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="btn_editar" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            CausesValidation="false" ImageUrl="~/App_Themes/Basico/Botones/img_editar.gif" ToolTip="Editar" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="btn_eliminar" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            CausesValidation="false" ImageUrl="~/App_Themes/Basico/botones/img_eliminar.gif" ToolTip="Eliminar"
                                                            OnClientClick="return confirm('¿Está seguro que desea eliminar este registro?');" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <a href='<%#  DataBinder.Eval(Container.DataItem,"pPK_Codigo","frw_operacionAsignacion.aspx?ope={0}" ) %>'  class="asg" runat="server">
                                                            <img id="img_asignacion" alt="asignacion" runat="server" height="16" width="16" src="~/App_Themes/Basico/imagenes/iconos/img_user.png" />
                                                        </a>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <AlternatingRowStyle CssClass="GridAltItem" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%--Contenido Agregado Abajo --%>
                                        <table id="tbl_agregado" align="right">
                                            <tr align="right">
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                        </table>
                        </Content>
                    </act:AccordionPane>
                    <act:AccordionPane ID="acp_edicionDatos" runat="server">
                        <Header>
                            <a href="" style="color: #FFFFFF; font-size: 12px;">Edici&oacute;n de Operaciones</a>
                        </Header>
                        <Content>
                            <table id="Table1">
                                <tr>
                                    <%-- Contenido de agregado Arriba--%>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%-- Espacio--%>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%-- GridView o Mantenimiento--%>
                                        <table id="tbl_mantenimiento">
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="lbl_codigo" runat="server" Text="Codigo: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_codigo" runat="server" Height="15px" Width="150px" MaxLength="50" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td>                                              
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="lbl_tipo" runat="server" Text="Tipo: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_Tipos" runat="server" ><%--AutoPostBack="true"  OnSelectedIndexChanged="ddl_Tipos_SelectedIndexChanged" >--%>
                                                        <asp:ListItem Text="Imprevisto" Value="I">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="Operación" Value="O">
                                                        </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                                                                  
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="lbl_descripcion" runat="server" Text="Descripcion: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_descripcion" runat="server" Height="15px" Width="350px" MaxLength="100" ></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="rfv_descripcion" runat="server" ControlToValidate="txt_descripcion"
                                                        ToolTip="Ingrese la descripción del entregable" ErrorMessage="Descripción es requerida"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="rfv_descripcionlenght" runat="server" ErrorMessage="La longitud máxima son 100 caracteres." 
                                                        ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txt_descripcion" Display="Dynamic"></asp:RegularExpressionValidator>                                                                                                
                                                </td>
                                            </tr>
                                               <tr align="left">
                                                <td>
                                                    <asp:Label ID="lbl_proyecto" runat="server" Text="Proyecto: "></asp:Label>
                                                </td>
                                                <td>
                                                      <asp:DropDownList ID="ddl_Proyectos" runat="server" DataTextField="pNombre" DataValueField="pPK_proyecto">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td >
                                                    <asp:Label ID="lbl_activo" runat="server" Text="Activo: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chk_activo" runat="server" Text="" Checked="true" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="center">
                                            <%--Contenido Agregado Abajo --%>
                                            <table id="Table2">
                                                <tr align="right">
                                                    <td>
                                                        <asp:Button ID="btn_cancelar" CausesValidation="false" OnClick="btn_cancelar_Click"
                                                            runat="server" Text="Cancelar" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btn_guardar" runat="server" OnClick="btn_guardar_Click" Text="Guardar" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </Content>
                    </act:AccordionPane>
                </Panes>
            </act:Accordion>
        </ContentTemplate>
      </asp:UpdatePanel>
</asp:Content>
