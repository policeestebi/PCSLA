<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master"
    AutoEventWireup="true" CodeBehind="frw_registroTiempos.aspx.cs" Inherits="CSLA.web.App_pages.mod.ControlSeguimiento.frm_registroTiempos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function MostrarMensaje() {
            $(function () {
                $('#dialog').dialog('open');
            });
        }

        function DesplegarAdvertencia(title, msg) {
            $(function () {
                $('.adv_title').text(title);
                $('.adv_msg').text(msg);
                $('.advertencia').css("display", "block");
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
<asp:Content ID="Content2" ContentPlaceHolderID="tituloPagina" runat="server">
    Registro de Tiempos
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cuerpoPagina" runat="server">
    <asp:ScriptManager ID="scr_Man" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upd_Principal" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="centrado">
                <table id="tblAdvertencia" class="advertencia">
                    <tr>
                        <td>
                            <strong>
                            <p class="adv_title">
                            </p>
                            </strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p class="adv_msg">
                            </p>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div class="centrado">
                <div id="dvTiempos" class="float">
                    <table id="tbl_contenido">
                        <tr>
                            <td>
                                <asp:Label ID="lblDia" CssClass="label" runat="server" Text="Día: "></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDiaValor" runat="server" Text="20 Jun 2012"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblProyecto" CssClass="label" runat="server" Text="Proyecto: "></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblProyectoValor" runat="server" Text="Proyecto 1"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblActividad" CssClass="label" runat="server" Text="Actividad: "></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblActividadValor" runat="server" Text="Actividad 1"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblFechaInicio" CssClass="label" runat="server" Text="Fecha Inicio: " Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFechaInicioValor" runat="server" Text="" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblFechaFinal" CssClass="label" runat="server" Text="Fecha Final: " Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFechaFinalValor" runat="server" Text="" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblHoraAsignadas" CssClass="label" runat="server" Text="Horas asignadas: " Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblHorasAsignadasValor" runat="server" Text="" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblHoras" CssClass="label" runat="server" Text="Horas: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHoras" runat="server"></asp:TextBox>
                                <act:MaskedEditExtender runat="server" ID="msk_horas" TargetControlID="txtHoras"
                                    Mask="99.99" CultureName="es-ES" MessageValidatorTip="true" MaskType="Number">
                                </act:MaskedEditExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:RangeValidator ID="rvl_horas" runat="server" ControlToValidate="txtHoras" ErrorMessage="El valor no puede superar las 24 horas."
                                    MinimumValue="0" MaximumValue="24">
                                </asp:RangeValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="dvTiempoComent" class="float">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblComentarios" CssClass="label" runat="server" Text="Comentarios: "></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtComentarios" TextMode="MultiLine" Rows="10" Columns="300" Width="270"
                                    runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="clear">
                </div>
                <div class="centrado">
                    <table id="tblBotones" style="margin: 0 auto; padding: 10px;">
                        <tr>
                            <td>
                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- Diálogo que muestra que cuando la solicitud ha sido grabada de manera exitosa -->
    <div id="dialog" title="Registro de Tiempos">
        <p>
            Se ha grabado con éxito el registro de tiempos</p>
    </div>
</asp:Content>
