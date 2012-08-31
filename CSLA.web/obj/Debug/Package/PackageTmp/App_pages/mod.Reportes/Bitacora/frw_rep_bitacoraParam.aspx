<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master"
    AutoEventWireup="true" CodeBehind="frw_rep_bitacoraParam.aspx.cs" Inherits="CSLA.web.App_pages.mod.Reportes.Bitacora.frw_rep_bitacoraParam" %>
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
    Reporte de la Bitácora de Sistema
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="cuerpoPagina" runat="server">
    <asp:ScriptManager ID="scr_Man" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upd_Principal" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="centrado">
        <table id="tbl_mensajePaquetes" style="display: block;" class="advertencia">
            <br />
            <br />
            <tr>
                <td>
                    <asp:Label ID="lbl_PaqueteTitulo" CssClass="label" runat="server" Text="Reporte de Bitácora de Sistema"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_PaqueteMensaje" runat="server" Text="Este reporte imprime la información almacenada en la bitácora del sistema según una serie de filtros."></asp:Label>
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
                        <p>
                            Fechas</p>
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
                                <act:calendarextender id="dt_fechaInicio" runat="server" targetcontrolid="txt_fechaInicio"
                                    popupbuttonid="img_cldFechaInicio" format="dd/MM/yyyy HH:mm:ss" />
                                <act:maskededitextender runat="server" id="msk_fechaInicio" targetcontrolid="txt_fechaInicio"
                                    mask="99/99/9999 99:99:99" culturename="es-ES" messagevalidatortip="true" masktype="DateTime"
                                    userdateformat="DayMonthYear">
                                                                    </act:maskededitextender>
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
                                <act:calendarextender id="dt_fechaFinal" runat="server" targetcontrolid="txt_fechaFinal"
                                    popupbuttonid="img_cldFechaFinal" format="dd/MM/yyyy HH:mm:ss" />
                                <act:maskededitextender runat="server" id="msk_fechaFinal" targetcontrolid="txt_fechaFinal"
                                    mask="99/99/9999 99:99:99" culturename="es-ES" messagevalidatortip="true" masktype="DateTime"
                                    userdateformat="DayMonthYear">
                                                                    </act:maskededitextender>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td>
                <div class="filter">
                    <strong>
                        <p>
                            Usuarios</p>
                    </strong>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_usuarioDesde" runat="server" Text="Desde:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_desde" runat="server" DataTextField="pPK_usuario" DataValueField="pPK_usuario"
                                    Width="150px">
                                </asp:DropDownList>
                                <act:listsearchextender id="lte_usuarioDesde" runat="server" targetcontrolid="ddl_desde"
                                    prompttext="Escriba para buscar" promptcssclass="serchExtender" promptposition="Top" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_usuarioHasta" runat="server" Text="Hasta: "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_hasta" runat="server" DataTextField="pPK_usuario" DataValueField="pPK_usuario"
                                    Width="150px">
                                </asp:DropDownList>
                                <act:listsearchextender id="lte_usuarioHasta" runat="server" targetcontrolid="ddl_hasta"
                                    prompttext="Escriba para buscar" promptcssclass="serchExtender" promptposition="Top" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td>
                <table style="padding: 2px; margin: 2px auto;" cellspacing="2px">
                    <tr>
                        <td>
                            <asp:Label ID="lbl_table" runat="server" Text="Tabla:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_table" Width="150px" runat="server">
                            </asp:DropDownList>
                            <act:listsearchextender id="lte_tabla" runat="server" targetcontrolid="ddl_table"
                                prompttext="Escriba para buscar" promptcssclass="serchExtender" promptposition="Top" />
                        </td>
                        <td>
                            <asp:Label ID="lbl_accion" runat="server" Text="Accion:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_accion" runat="server" Width="150px">
                            </asp:DropDownList>
                            <act:listsearchextender id="lte_accion" runat="server" targetcontrolid="ddl_accion"
                                prompttext="Escriba para buscar" promptcssclass="serchExtender" promptposition="Top" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_registro" runat="server" Text="Registro:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_registro" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr align="center">
            <td colspan="3">
                <asp:Button ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="btn_imprimir_Click"
                    OnClientClick="aspnetForm.target ='_blank';" />
            </td>
        </tr>
    </table>
</asp:Content>
