<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master"
    AutoEventWireup="true" CodeBehind="frw_roles.aspx.cs" Inherits="CSLA.web.App_pages.mod.Administracion.frw_rol" %>

<%@ Register Assembly="COSEVI.web.controls" Namespace="COSEVI.web.controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="tituloPagina" runat="server">
    Lista de Roles
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cuerpoPagina" runat="server">
    <asp:ScriptManager ID="scr_Man" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upd_Principal" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <%-- Modal para indicar mensaje de error al intentar eliminar un registro permanente --%>
            <act:ModalPopupExtender ID="mpe_RegistroPermante" runat="server" TargetControlID="btn_permanente"
                PopupControlID="div_PopUpMensaje" BackgroundCssClass="popUpStyle" CancelControlID="btn_permanente"
                DropShadow="true" />
            <%-- Llamado al modal para indicar mensaje de error al intentar eliminar un registro permanente --%>
            <div id="div_PopUpMensaje" class="modalPopup" style="display: none;">
                <asp:Panel ID="pan_mensajeeliminar" runat="server" CssClass="modalPopup">
                    No se puede terminar la petición debido a que el registro que intenta eliminar es
                    propio del sistema.
                    <br />
                    <asp:Button ID="btn_permanente" runat="server" Text="Close" />
                </asp:Panel>
            </div>
            <act:Accordion ID="ard_principal" runat="server" SelectedIndex="0" FadeTransitions="false" 
                FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="false" Height="700"
                SuppressHeaderPostbacks="true" HeaderCssClass="encabezadoAcordeon" ContentCssClass="contenidoAcordeon"
                HeaderSelectedCssClass="encabezadoSeleccionadoAcordeon">
                <Panes>
                    <act:AccordionPane ID="acp_listadoDatos" runat="server" Height="700" >
                        <Header>
                            <a href="" style="color: #FFFFFF; font-size: 12px;">Listado de Roles</a>
                        </Header>
                        <Content>
                            <table id="tbl_contenido">
                                <tr>
                                    <%-- Contenido de agregado Arriba--%>
                                </tr>
                                <tr>
                                    <td>
                                        <%-- Búsqueda--%>
                                        <cc1:ucSearch ID="ucSearchRol" runat="server" OnSearchClick="ucSearchRol_searchClick" />
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
                                        <asp:GridView ID="grd_listaRoles" AllowPaging="True" runat="server" AutoGenerateColumns="False"
                                            Width="100%" CssClass="Grid" CellSpacing="1" CellPadding="3" OnRowCommand="grd_listaRoles_RowCommand"
                                            OnPageIndexChanging="grd_listaRoles_PageIndexChanging">
                                            <PagerSettings PageButtonCount="20" />
                                            <Columns>
                                                <asp:BoundField DataField="pPK_rol" HeaderText="Rol" SortExpression="pPK_permiso" />
                                                <asp:BoundField DataField="pNombre" HeaderText="Nombre" SortExpression="pNombre" />
                                                <asp:BoundField DataField="pDescripcion" HeaderText="Descripcion" SortExpression="pDescripcion" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="btn_ver" CommandName="Ver" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            CausesValidation="false" ImageUrl="~/App_Themes/Basico/Botones/img_ver.gif" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="btn_editar" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            CausesValidation="false" ImageUrl="~/App_Themes/Basico/Botones/img_editar.gif" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="btn_eliminar" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            CausesValidation="false" ImageUrl="~/App_Themes/Basico/botones/img_eliminar.gif"
                                                            OnClientClick="return confirm('¿Está seguro que desea eliminar este registro?');" />
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
                            <a href="" style="color: #FFFFFF; font-size: 12px;">Edici&oacute;n de Roles</a>
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
                                                    <asp:HiddenField ID="hdf_codigo" runat="server" />
                                                    <asp:Label ID="lbl_nombre" runat="server" Text="Nombre: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_nombre" runat="server" Height="15px" Width="150px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="rfv_nombre" runat="server" ControlToValidate="txt_nombre"
                                                        ToolTip="Ingrese el nombre del rol" ErrorMessage="Nombre es requerido"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="rfv_nombrelength" runat="server" ErrorMessage="La longitud máxima son 75 caracteres."
                                                        ValidationExpression="^([\S\s]{0,75})$" ControlToValidate="txt_nombre" Display="Dynamic"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_descripcion" runat="server" Text="Descripcion: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_descripcion" Columns="30" Rows="5" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="rfv_descripcion" runat="server" ControlToValidate="txt_descripcion"
                                                        ToolTip="Ingrese la descripcion del rol" ErrorMessage="Descripción es requerida"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="rfv_descripcionlength" runat="server" ErrorMessage="La longitud máxima son 100 caracteres."
                                                        ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txt_descripcion"
                                                        Display="Dynamic"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_activo" runat="server" Text="Activo: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chk_activo" runat="server" Checked="true" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                            <tr>
                                                <td colspan="2">
                                                    <strong>
                                                        <asp:Label ID="lbl_permisos" runat="server" Text="Permisos: "></asp:Label>
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btn_marcar" runat="server" Text="Marcar Todos" CausesValidation="false" OnClick="btn_marcar_Click" />
                                                    &nbsp;
                                                    <asp:Button ID="btn_desmarcar" runat="server" Text="Desmarcar Todos" CausesValidation="false" OnClick="btn_desmarcar_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <div class="overflow" style="height: 225px; width: 500px;">
                                                        <asp:TreeView ID="trv_menu" runat="server" ShowLines="false">
                                                        </asp:TreeView>
                                                    </div>
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
