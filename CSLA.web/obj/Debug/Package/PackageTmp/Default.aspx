<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspEstiloGeneral.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CSLA.web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="menu" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="main_container" runat="server">
    <div id="login">
        <div id="header_login">
            <p>
                Inicio de Sesión</p>
        </div>
        <div id="main_login">
            <div id="img_login">
                <table>
                    <tr>
                        <td>
                            <strong>Bienvenido a CSLA!</strong>
                            <p>
                                Tienes que usar un Nombre de Usuario y una Constraseña válidos para acceder al sistema.</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;<img src="App_Themes/Basico/imagenes/iconos/img_login.png" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="credential_login">
                <table>
                    <tr>
                        <td colspan="2">
                            <strong>Credenciales</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_usuario" runat="server" Text="Usuario:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_usuario" runat="server"></asp:TextBox>
                        </td>
                        <td>
                             <asp:RequiredFieldValidator ID="rfv_usuario" runat="server" ControlToValidate="txt_usuario" 
                                                        ToolTip="Ingrese el usuario" ErrorMessage="El usuario es requerido"><img alt="imagen" width="25px" height="20px" src="App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_contrasena" runat="server" Text="Contraseña:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_Constrasena" TextMode="Password" runat="server"></asp:TextBox>
                        </td>
                        <td>
                             <asp:RequiredFieldValidator ID="rfv_contraseña" runat="server" ControlToValidate="txt_Constrasena" 
                                                        ToolTip="Ingrese la contraseña" ErrorMessage="La contraseña es requerida"><img alt="imagen" width="25px" height="20px" src="App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lbl_usuarioInvalido" ForeColor="Red" Visible="false" runat="server" Text="El usuario o la contraseña son incorrectos."></asp:Label>
                            <asp:Label ID="lbl_usuarioInactivo" ForeColor="Red" Visible="false" runat="server" Text="El usuario no se encuntra activo, comuniquese con el administrador del sistema."></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" ><asp:ValidationSummary HeaderText="Error al logearse." DisplayMode="BulletList" ID="vds_error" runat="server" /></td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btn_acceder" runat="server" Text="Ingresar" OnClick="btn_acceder_Click" CssClass="textbox"/>
                        </td>
                        <td>
                        </td>
                        <td>    
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
