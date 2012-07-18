<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master"
    AutoEventWireup="true" CodeBehind="frw_departamento.aspx.cs" Inherits="CSLA.web.App_pages.mod.Administracion.frw_departamento" %>

<%@ Register Assembly="COSEVI.web.controls" Namespace="COSEVI.web.controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="tituloPagina" runat="server">
    Lista de Departamentos
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cuerpoPagina" runat="server">
    <asp:ScriptManager ID="scr_Man" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upd_Principal" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>

            <%-- Modal para indicar mensaje de error al intentar eliminar un registro permanente --%>
            <act:ModalPopupExtender ID="mpe_RegistroPermante" runat="server" 
                TargetControlID="btn_permanente"
                PopupControlID="div_PopUpMensaje" 
                BackgroundCssClass="popUpStyle" 
                CancelControlID="btn_permanente" 
                DropShadow="true"
                />

            <%-- Llamado al modal para indicar mensaje de error al intentar eliminar un registro permanente --%>
            <div id="div_PopUpMensaje" class="modalPopup" style="display:none;">
                <asp:Panel ID="pan_mensajeeliminar" runat="server" CssClass="modalPopup">
                    No se puede terminar la petición debido a que el registro que intenta eliminar 
                    es propio del sistema.
                <br />
                <asp:Button ID="btn_permanente" runat="server" Text="Close" />
                </asp:Panel>
            </div>
        
            <act:Accordion ID="ard_principal" runat="server" SelectedIndex="0" FadeTransitions="false"
                FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="false"
                SuppressHeaderPostbacks="true" HeaderCssClass="encabezadoAcordeon" ContentCssClass="contenidoAcordeon"
                HeaderSelectedCssClass="encabezadoSeleccionadoAcordeon">
                <Panes>
                    <act:AccordionPane ID="acp_listadoDatos" runat="server">
                        <Header>
                            <a href="" style="color: #FFFFFF; font-size: 12px;">Listado de Departamentos</a>
                        </Header>
                        <Content>
                            <table id="tbl_contenido">
                                <tr>
                                    <%-- Contenido de agregado Arriba--%>
                                </tr>
                                <tr>
                                    <td>
                                        <%-- Búsqueda--%>
                                        <cc1:ucSearch ID="ucSearchDepartamento" runat="server" OnSearchClick="ucSearchDepartamento_searchClick" />
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
                                        <asp:GridView ID="grd_listaDepartamentos" AllowPaging="True" runat="server" AutoGenerateColumns="False"
                                            Width="100%" CssClass="Grid" CellSpacing="1" CellPadding="3" OnRowCommand="grd_listaDepartamentos_RowCommand"
                                            OnPageIndexChanging="grd_listaDepartamentos_PageIndexChanging">
                                            <PagerSettings PageButtonCount="20" />
                                            <Columns>
                                                <asp:BoundField DataField="pPK_departamento" HeaderText="Departamento" SortExpression="pPK_departamento"
                                                    Visible="false" />
                                                <asp:BoundField DataField="pFK_departamento" HeaderText="DepartamentoPadre" SortExpression="pFK_departamento"
                                                    Visible="false" />
                                                <asp:BoundField DataField="pNombre" HeaderText="Nombre" SortExpression="pNombre" />
                                                <asp:BoundField DataField="pUbicacion" HeaderText="Ubicacion" SortExpression="pUbicacion" />
                                                <asp:BoundField DataField="pAdministrador" HeaderText="Administrador" SortExpression="pAdministrador" />
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
                            <a href="" style="color: #FFFFFF; font-size: 12px;">Edici&oacute;n de Departamentos</a>
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
                                                <tr align="left">
                                                    <td>
                                                        <asp:Label ID="lbl_departamentoPadre" runat="server" Text="Depto Padre: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddl_departamentoPadre" runat="server" DataTextField="pNombre"
                                                            DataValueField="pPK_departamento">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td>
                                                        <asp:Label ID="lbl_nombre" runat="server" Text="Nombre: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_nombre" runat="server" Height="15px" Width="150px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfv_nombre" runat="server" ControlToValidate="txt_nombre"
                                                            ToolTip="Ingrese el nombre del departamento" ErrorMessage="Nombre es requerido"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rfv_nombrelength" runat="server" ErrorMessage="La longitud máxima son 30 caracteres." 
                                                            ValidationExpression="^([\S\s]{0,30})$" ControlToValidate="txt_nombre" Display="Dynamic"></asp:RegularExpressionValidator>                                                  
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td>
                                                        <asp:Label ID="lbl_ubicacion" runat="server" Text="Ubicacion: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_ubicacion" runat="server" Height="15px" Width="150px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfv_ubicacion" runat="server" ControlToValidate="txt_ubicacion"
                                                            ToolTip="Ingrese la ubicacion" ErrorMessage="Ubicacion es requerida"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="txt_ubicacionlength" runat="server" ErrorMessage="La longitud máxima son 100 caracteres." 
                                                            ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txt_ubicacion" Display="Dynamic"></asp:RegularExpressionValidator>                                                                                                      
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td>
                                                        <asp:Label ID="lbl_administrador" runat="server" Text="Administrador: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_administrador" runat="server" Height="15px" Width="150px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfv_administrador" runat="server" ControlToValidate="txt_administrador"
                                                            ToolTip="Ingrese el administrador" ErrorMessage="Administrador es requerido"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rfv_administradorlength" runat="server" ErrorMessage="La longitud máxima son 30 caracteres." 
                                                            ValidationExpression="^([\S\s]{0,30})$" ControlToValidate="txt_administrador" Display="Dynamic"></asp:RegularExpressionValidator>                                                                                                      
                                                    
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td>
                                                        <asp:Label ID="lbl_consecutivo" runat="server" Text="Consecutivo: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_consecutivo" runat="server" Height="15px" Width="150px" TextMode="SingleLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfv_consecutivo" runat="server" ControlToValidate="txt_consecutivo"
                                                            ToolTip="Ingrese el consecutivo del departamento" ErrorMessage="Consecutivo requerido"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rfv_consecutivolength" runat="server" ErrorMessage="La longitud máxima son 45 caracteres." 
                                                            ValidationExpression="^([\S\s]{0,45})$" ControlToValidate="txt_consecutivo" Display="Dynamic"></asp:RegularExpressionValidator>                                                                                                      
                                                    </td>
                                                </tr>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="center">
                                            <%--Contenido Agregado Abajo --%>
                                            <table>
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
