<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master"
    AutoEventWireup="true" CodeBehind="frw_edicionUsuario.aspx.cs" Inherits="CSLA.web.App_pages.mod.Administracion.frw_edicionUsuario" %>

<%@ Register Assembly="COSEVI.web.controls" Namespace="COSEVI.web.controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function MostrarMensaje() {
            $(function () {
                $('#dialog').dialog('open');
            });
        }

        $(function () {
            $('#dialog').dialog({
                autoOpen: false,
                buttons: {
                    "OK": function () {
                        $(this).dialog("close");
                    }
                },
                modal: true
            });
        }); 
    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="tituloPagina" runat="server">
    <asp:Label ID="lbl_usuarioEdicion" runat="server" Text="Edición de usuario"></asp:Label>
    <br />
    <asp:Label ID="lbl_usuarioActual" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cuerpoPagina" runat="server">
    <asp:ScriptManager ID="scr_Man" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upd_Principal" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
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
                                    <asp:TextBox ID="txt_usuario" Enabled="false" runat="server" Height="15px" Width="150px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <asp:Label ID="lbl_contrasena" runat="server" Text="Contraseña: "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_contrasena" TextMode="Password" runat="server" Height="15px"
                                        Width="150px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RegularExpressionValidator ID="rfv_contrasenalength" runat="server" ErrorMessage="La longitud máxima son 45 caracteres."
                                        ValidationGroup="usuario" ValidationExpression="^([\S\s]{0,45})$" ControlToValidate="txt_contrasena"
                                        Display="Dynamic"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <asp:Label ID="lbl_confirmarContrasena" runat="server" Text="Reescribir Contraseña: "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_confirmarContrasena" TextMode="Password" runat="server" Height="15px"
                                        Width="150px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RegularExpressionValidator ID="rfv_reescribirlength" runat="server" ErrorMessage="La longitud máxima son 45 caracteres."
                                        ValidationGroup="usuario" ValidationExpression="^([\S\s]{0,45})$" ControlToValidate="txt_confirmarContrasena"
                                        Display="Dynamic"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:CompareValidator ID="cpv_contrasena" ControlToValidate="txt_contrasena" ControlToCompare="txt_confirmarContrasena"
                                        ValidationGroup="usuario" Type="String" Operator="Equal" Text="Las contraseñas deben ser iguales."
                                        runat="Server" />
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <asp:Label ID="lbl_nombre" runat="server" Text="Nombre: "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_nombre" runat="server" Enabled="false" Height="15px" Width="150px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <asp:Label ID="lbl_apellido1" runat="server" Text="Apellido1: "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_apellido1" runat="server" Enabled="false" Height="15px" Width="150px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <asp:Label ID="lbl_apellido2" runat="server" Text="Apellido2: "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_apellido2" runat="server" Enabled="false" Height="15px" Width="150px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <asp:Label ID="lbl_email" runat="server" Text="Email: "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_email" runat="server" Height="15px" Width="150px"></asp:TextBox>
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
                                    <asp:Label ID="lbl_puesto" runat="server" Text="Puesto: "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_puesto" runat="server" Height="15px" Width="150px" Enabled="false"></asp:TextBox>
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
                                    <asp:Label ID="lbl_activo" runat="server" Text="Activo: "></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chk_activo" Enabled="false" runat="server" Checked="True"></asp:CheckBox>
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
                                        <asp:Button ID="btn_guardar" runat="server" Text="Guardar" OnClick="btn_guardar_Click"
                                            ValidationGroup="usuario" CausesValidation="true" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- Diálogo que muestra que cuando la solicitud ha sido grabada de manera exitosa -->
    <div id="dialog" title="Edición de Usuario">
        <p>
            Se ha modificado con éxito el usuario.</p>
    </div>
</asp:Content>
