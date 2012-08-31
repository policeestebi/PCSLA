<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master"
    AutoEventWireup="true" CodeBehind="frw_rep_registroTiemposUsuariosParam.aspx.cs"
    Inherits="CSLA.web.App_pages.mod.Reportes.RegistroTiemposUsuarios.frw_rep_registroTiemposUsuariosParam" %>

<%@ Register Assembly="COSEVI.web.controls" Namespace="COSEVI.web.controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="tituloPagina" runat="server">
    Registro de Tiempos por Usuario
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="cuerpoPagina" runat="server">
    <asp:ScriptManager ID="scr_Man" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upd_Principal" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="centrado">
        <table id="tbl_mensajeReporte" style="display: block;" class="advertencia">
            <br />
            <br />
            <tr>
                <td>
                    <asp:Label ID="lbl_Titulo" CssClass="label" runat="server" Text=" Reporte de Registro de tiempos por Usuario"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_PaqueteMensaje" runat="server" Text="Este reporte imprime el reporte de tiempos de un usuario del sistema, dentro de un rango de fechas y aprupado por operaciones, imprevistos y actividades ligadas a proyectos."></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
    <table>
        <tr>
            <td>
                <div class="filter">
                    <strong>
                            Fechas
                    </strong>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_fechaInicial" runat="server" Text="Fecha Inicial: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_fechaInicio" runat="server"></asp:TextBox>
                                <asp:ImageButton ID="img_cldFechaInicio" runat="server" ImageUrl="../../../App_Themes/Basico/botones/img_calendario.png"
                                    CausesValidation="false" />
                                <act:CalendarExtender ID="dt_fechaInicio" runat="server" TargetControlID="txt_fechaInicio"
                                    PopupButtonID="img_cldFechaInicio" Format="dd/MM/yyyy" />
                                <act:MaskedEditExtender runat="server" ID="msk_fechaInicio" TargetControlID="txt_fechaInicio"
                                    Mask="99/99/9999" CultureName="es-ES" MessageValidatorTip="true" MaskType="Date"
                                    UserDateFormat="DayMonthYear">
                                </act:MaskedEditExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_fechaFinal" runat="server" Text="Final Final: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_fechaFinal" runat="server"></asp:TextBox>
                                <asp:ImageButton ID="img_cldFechaFinal" runat="server" ImageUrl="../../../App_Themes/Basico/botones/img_calendario.png"
                                    CausesValidation="false" />
                                <act:CalendarExtender ID="dt_fechaFinal" runat="server" TargetControlID="txt_fechaFinal"
                                    PopupButtonID="img_cldFechaFinal" Format="dd/MM/yyyy" />
                                <act:MaskedEditExtender runat="server" ID="msk_fechaFinal" TargetControlID="txt_fechaFinal"
                                    Mask="99/99/9999" CultureName="es-ES" MessageValidatorTip="true" MaskType="Date"
                                    UserDateFormat="DayMonthYear">
                                </act:MaskedEditExtender>
                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:CustomValidator ID="cmv_fechas"
                                    ErrorMessage="La fecha inicial no puede ser menor a la fechaFinal" 
                                    runat="server" onservervalidate="cmv_fechas_ServerValidate" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td>
                <div class="filter"  style="height:105px;">
                    <strong>
                            Usuario
                    </strong>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_departamentos" runat="server" Text="Departamentos:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_departamentos" runat="server" DataTextField="pNombre" DataValueField="pPK_departamento"
                                    Width="150px" AutoPostBack="True" 
                                    onselectedindexchanged="ddl_departamentos_SelectedIndexChanged">
                                </asp:DropDownList>
                                <act:ListSearchExtender ID="lte_departamentos" runat="server" TargetControlID="ddl_departamentos"
                                    PromptText="Escriba para buscar" PromptCssClass="serchExtender" PromptPosition="Top" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_usuario" runat="server" Text="Usuario:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_usuarios" runat="server" DataTextField="pNombreCompleto"
                                    DataValueField="pPK_usuario" Width="150px">
                                </asp:DropDownList>
                                <act:ListSearchExtender ID="lte_usuarios" runat="server" TargetControlID="ddl_usuarios"
                                    PromptText="Escriba para buscar" PromptCssClass="serchExtender" PromptPosition="Top" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr align="center">
            <td colspan="3">
                <asp:Button ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="btn_imprimir_Click" CausesValidation="true"
                    OnClientClick="aspnetForm.target ='_blank';" />
            </td>
        </tr>
    </table>
</asp:Content>
