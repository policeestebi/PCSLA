<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master"
    AutoEventWireup="true" CodeBehind="frw_usuarios.aspx.cs" Inherits="CSLA.web.App_pages.mod.Administracion.frw_usuario" %>

<%@ Register Assembly="COSEVI.web.controls" Namespace="COSEVI.web.controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="tituloPagina" runat="server">
    Lista de Usuarios
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
            <%-- Modal para el cambio de contraseña --%>
            <act:ModalPopupExtender ID="mpe_CambioPassword" runat="server" TargetControlID="btn_close"
                PopupControlID="div_PopUp" BackgroundCssClass="popUpStyle" CancelControlID="btn_close"
                DropShadow="true" />
            <%-- Llamado del modal para el cambio de contraseña --%>
            <div id="div_PopUp" class="modalPopup" style="display: none;">
                <asp:Panel runat="Server" ID="pan_cambiopassword" CssClass="tablaPopup">
                    <table id="tbl_popup" class="tablaPopup">
                        <tr align="left">
                            <td>
                                <asp:Label ID="lbl_usuariocambio" runat="server" Text="Usuario: " Visible="true"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_usuariocambio" runat="server" Visible="true" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                                <asp:Label ID="lbl_nuevopassword" runat="server" Text="Contraseña: " Visible="true"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_nuevopassword" TextMode="Password" runat="server" Visible="true"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator Visible="true" ID="rfv_nuevopassword" runat="server"  ValidationGroup="password"
                                    ControlToValidate="txt_nuevopassword" ToolTip="Ingrese la contraseña del usuario"
                                    ErrorMessage="Contraseña es requerida"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                                <asp:Label ID="lbl_repetirpassword" runat="server" Text="Repetir Contraseña: " Visible="true"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_repetirpassword" TextMode="Password" runat="server" Visible="true"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator Visible="true" ID="rfv_repetirpassword" runat="server" ValidationGroup="password"
                                    ControlToValidate="txt_repetirpassword" ToolTip="Repita la contraseña del usuario"
                                    ErrorMessage="Repetir contraseña"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="btn_close" runat="server" Text="Cerrar" CausesValidation="false" />
                <asp:Button ID="btn_password" runat="server" Text="Cambiar" OnClick="btn_password_Click" ValidationGroup="password"/>
                <br />
            </div>
            <act:Accordion ID="ard_principal" runat="server" SelectedIndex="0" FadeTransitions="false"
                FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="false"
                SuppressHeaderPostbacks="true" HeaderCssClass="encabezadoAcordeon" ContentCssClass="contenidoAcordeon"
                HeaderSelectedCssClass="encabezadoSeleccionadoAcordeon">
                <Panes>
                    <act:AccordionPane ID="acp_listadoDatos" runat="server">
                        <Header>
                            <a href="" style="color: #FFFFFF; font-size: 12px;">Listado de Usuarios</a>
                        </Header>
                        <Content>
                            <table id="tbl_contenido">
                                <tr>
                                    <%-- Contenido de agregado Arriba--%>
                                </tr>
                                <tr>
                                    <td>
                                        <%-- Búsqueda--%>
                                        <cc1:ucSearch ID="ucSearchUsuario" runat="server" OnSearchClick="ucSearchUsuario_searchClick" />
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
                                        <asp:GridView ID="grd_listaUsuarios" AllowPaging="True" runat="server" AutoGenerateColumns="False"
                                            Width="100%" CssClass="Grid" CellSpacing="1" CellPadding="3" OnRowCommand="grd_listaUsuarios_RowCommand"
                                            OnPageIndexChanging="grd_listaUsuarios_PageIndexChanging">
                                            <PagerSettings PageButtonCount="20" />
                                            <Columns>
                                                <asp:BoundField DataField="pPK_usuario" HeaderText="Usuario" SortExpression="pPK_usuario" />
                                                <asp:BoundField DataField="pNombre" HeaderText="Nombre" SortExpression="pNombre" />
                                                <asp:BoundField DataField="pContrasena" HeaderText="Contrasena" SortExpression="pContrasena"
                                                    Visible="false" />
                                                <asp:BoundField DataField="pApellido1" HeaderText="Apellido1" SortExpression="pApellido1" />
                                                <asp:BoundField DataField="pApellido2" HeaderText="Apellido2" SortExpression="pApellido2" />
                                                <asp:BoundField DataField="pFK_rol" HeaderText="Rol" SortExpression="pFK_rol" Visible="false"
                                                    ShowHeader="false" />
                                                <asp:BoundField DataField="pNombreRol" HeaderText="NombreRol" SortExpression="pNombreRol" />
                                                <asp:BoundField DataField="pPuesto" HeaderText="Puesto" SortExpression="pPuesto" />
                                                <asp:CheckBoxField DataField="pActivo" HeaderText="Activo" ReadOnly="true" />
                                                <asp:BoundField DataField="pEmail" HeaderText="Email" SortExpression="pEmail" />
                                                <asp:BoundField DataField="pFK_departamento" HeaderText="Departamento" SortExpression="pFK_departamento" Visible="false"
                                                    ShowHeader="false" />
                                                <asp:BoundField DataField="pNombreDepartamento" HeaderText="NombreDepartamento" SortExpression="pNombreDepartamento" Visible="false"
                                                    ShowHeader="false" />
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
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="btn_cambiar" CommandName="Cambiar" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            CausesValidation="false" ImageUrl="~/App_Themes/Basico/botones/img_check.png" />
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
                            <a href="" style="color: #FFFFFF; font-size: 12px;">Edici&oacute;n de Usuarios</a>
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
                                                    <asp:Label ID="lbl_usuario" runat="server" Text="Usuario Login: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_usuario" runat="server" Height="15px" Width="150px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="rfv_usuarioLogin" runat="server" ControlToValidate="txt_usuario"
                                                        ValidationGroup="usuario" ToolTip="Ingrese Login del usuario" ErrorMessage="Login del usuario es requerido"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="rfv_usuariolength" runat="server" ErrorMessage="La longitud máxima son 30 caracteres."
                                                        ValidationGroup="usuario" ValidationExpression="^([\S\s]{0,30})$" ControlToValidate="txt_usuario"
                                                        Display="Dynamic"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="lbl_contrasena" runat="server" Text="Contraseña: " Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_contrasena" TextMode="Password" runat="server" Visible="false"
                                                        Height="15px" Width="150px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator Visible="false" ID="rfv_contrasena" runat="server" ControlToValidate="txt_contrasena"
                                                        ValidationGroup="usuario" ToolTip="Ingrese la contraseña del usuario" ErrorMessage="Contraseña es requerida"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="rfv_contrasenalength" runat="server" ErrorMessage="La longitud máxima son 45 caracteres."
                                                        ValidationGroup="usuario" ValidationExpression="^([\S\s]{0,45})$" ControlToValidate="txt_contrasena"
                                                        Display="Dynamic"></asp:RegularExpressionValidator>
                                                    
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="lbl_confirmarContrasena" runat="server" Text="Reescribir Contraseña: "
                                                        Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_confirmarContrasena" TextMode="Password" runat="server" Visible="false"
                                                        Height="15px" Width="150px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator Visible="false" ID="rfv_confirmarContrasena" runat="server"
                                                        ControlToValidate="txt_confirmarContrasena" ValidationGroup="usuario" ToolTip="Reescriba la contraseña del usuario"
                                                        ErrorMessage="Reescripción es requerida"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="rfv_reescribirlength" runat="server" ErrorMessage="La longitud máxima son 45 caracteres."
                                                        ValidationGroup="usuario" ValidationExpression="^([\S\s]{0,45})$" ControlToValidate="txt_confirmarContrasena"
                                                        Display="Dynamic"></asp:RegularExpressionValidator>
                                                    <asp:CompareValidator ID="cpv_contrasena" ControlToValidate="txt_contrasena" ControlToCompare="txt_confirmarContrasena" ValidationGroup="usuario"
                                                        Type="String" Operator="Equal" Text="Las contraseñas deben ser iguales." runat="Server" />
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
                                                        ValidationGroup="usuario" ToolTip="Ingrese el nombre del usuario" ErrorMessage="Nombre es requerido"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="rfv_nombrelength" runat="server" ErrorMessage="La longitud máxima son 45 caracteres."
                                                        ValidationGroup="usuario" ValidationExpression="^([\S\s]{0,45})$" ControlToValidate="txt_nombre"
                                                        Display="Dynamic"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="lbl_apellido1" runat="server" Text="Apellido1: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_apellido1" runat="server" Height="15px" Width="150px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="rfv_apellido1" runat="server" ControlToValidate="txt_apellido1"
                                                        ValidationGroup="usuario" ToolTip="Ingrese el apellido1 del usuario" ErrorMessage="Apellido1 es requerido"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="rfv_apellido1length" runat="server" ErrorMessage="La longitud máxima son 45 caracteres."
                                                        ValidationGroup="usuario" ValidationExpression="^([\S\s]{0,45})$" ControlToValidate="txt_apellido1"
                                                        Display="Dynamic"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="lbl_apellido2" runat="server" Text="Apellido2: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_apellido2" runat="server" Height="15px" Width="150px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="rfv_apellido2" runat="server" ControlToValidate="txt_apellido2"
                                                        ValidationGroup="usuario" ToolTip="Ingrese el apellido2 del usuario" ErrorMessage="Apellido2 es requerido"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="rfv_apellido2length" runat="server" ErrorMessage="La longitud máxima son 45 caracteres."
                                                        ValidationGroup="usuario" ValidationExpression="^([\S\s]{0,45})$" ControlToValidate="txt_apellido2"
                                                        Display="Dynamic"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="lbl_email" runat="server" Text="Email: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_email" runat="server" Height="15px" Width="150px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="rfv_email" runat="server" ControlToValidate="txt_email"
                                                        ValidationGroup="usuario" ToolTip="Ingrese el correo del usuario" ErrorMessage="Email es requerido"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="rfv_emaillength" runat="server" ErrorMessage="La longitud máxima son 45 caracteres."
                                                        ValidationGroup="usuario" ValidationExpression="^([\S\s]{0,45})$" ControlToValidate="txt_email"
                                                        Display="Dynamic"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="lbl_departamento" runat="server" Text="Departamento: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_departamento" runat="server" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="lbl_puesto" runat="server" Text="Puesto: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_puesto" runat="server" Height="15px" Width="150px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="rfv_puesto" runat="server" ControlToValidate="txt_puesto"
                                                        ValidationGroup="usuario" ToolTip="Ingrese el puesto del usuario" ErrorMessage="Puesto es requerido"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="rfv_puestolength" runat="server" ErrorMessage="La longitud máxima son 45 caracteres."
                                                        ValidationGroup="usuario" ValidationExpression="^([\S\s]{0,45})$" ControlToValidate="txt_puesto"
                                                        Display="Dynamic"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="lbl_rol" runat="server" Text="Rol: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_rol" runat="server" OnSelectedIndexChanged="ddlRol_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="lbl_activo" runat="server" Text="Activo: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chk_activo" runat="server" Checked="True"></asp:CheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%--Contenido Agregado Abajo --%>
                                        <div class="center">
                                            <table id="Table2">
                                                <tr align="right">
                                                    <td>
                                                        <asp:Button ID="btn_cancelar" CausesValidation="false" OnClick="btn_cancelar_Click"
                                                            runat="server" Text="Cancelar" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btn_guardar" runat="server" Text="Guardar" OnClick="btn_guardar_Click"
                                                            ValidationGroup="usuario" CausesValidation="true" />
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
