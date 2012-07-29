<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master"
    AutoEventWireup="true" CodeBehind="frw_bitacora.aspx.cs" Inherits="CSLA.web.App_pages.mod.Administracion.frw_bitacora" %>

<%@ Register Assembly="COSEVI.web.controls" Namespace="COSEVI.web.controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                    "Ok": function () {
                        $(this).dialog("close");
                    }
                },
                modal: true
            });
        }); 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="tituloPagina" runat="server">
    Lista de Registros
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cuerpoPagina" runat="server">
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
                            <a href="" style="color: #FFFFFF; font-size: 12px;">Listado de Registros</a>
                        </Header>
                        <Content>
                            <table id="tbl_contenido">
                                <tr>
                                    <%-- Contenido de agregado Arriba--%>
                                </tr>
                                <tr>
                                    <td>
                                        <%-- Búsqueda--%>
                                        <%-- &nbsp; &nbsp; &nbsp;
                        <cc1:ucSearch ID="ucSearchBitacora" runat="server" OnSearchClick="ucSearchBitacora_searchClick" />
                        <asp:Button ID="btn_agregar" runat="server" Visible="false" CausesValidation="false" />--%>
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
                                                                    <asp:ImageButton ID="img_cldFechaInicio" runat="server" ImageUrl="../../App_Themes/Basico/botones/img_calendario.png"
                                                                        CausesValidation="false" />
                                                                    <act:CalendarExtender ID="dt_fechaInicio" runat="server" TargetControlID="txt_fechaInicio"
                                                                        PopupButtonID="img_cldFechaInicio" Format="dd/MM/yyyy HH:mm:ss" />
                                                                    <act:MaskedEditExtender runat="server" ID="msk_fechaInicio" TargetControlID="txt_fechaInicio"
                                                                        Mask="99/99/9999 99:99:99" CultureName="es-ES" MessageValidatorTip="true" MaskType="DateTime"
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
                                                                    <asp:ImageButton ID="img_cldFechaFinal" runat="server" ImageUrl="../../App_Themes/Basico/botones/img_calendario.png"
                                                                        CausesValidation="false" />
                                                                    <act:CalendarExtender ID="dt_fechaFinal" runat="server" TargetControlID="txt_fechaFinal"
                                                                        PopupButtonID="img_cldFechaFinal" Format="dd/MM/yyyy HH:mm:ss" />
                                                                    <act:MaskedEditExtender runat="server" ID="msk_fechaFinal" TargetControlID="txt_fechaFinal"
                                                                        Mask="99/99/9999 99:99:99" CultureName="es-ES" MessageValidatorTip="true" MaskType="DateTime"
                                                                        UserDateFormat="DayMonthYear">
                                                                    </act:MaskedEditExtender>
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
                                                                    <act:listsearchextender id="lte_usuarioDesde" runat="server" targetcontrolid="ddl_desde" prompttext="Escriba para buscar"
                                                                        promptcssclass="serchExtender" promptposition="Top"  />
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
                                                                    <act:listsearchextender id="lte_usuarioHasta" runat="server" targetcontrolid="ddl_hasta" prompttext="Escriba para buscar"
                                                                        promptcssclass="serchExtender" promptposition="Top"    />
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
                                                                 <act:listsearchextender id="lte_tabla" runat="server" targetcontrolid="ddl_table" prompttext="Escriba para buscar"
                                                                        promptcssclass="serchExtender" promptposition="Top"   />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_accion" runat="server" Text="Accion:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddl_accion" runat="server" Width="150px">
                                                                </asp:DropDownList>

                                                                   <act:listsearchextender id="lte_accion" runat="server" targetcontrolid="ddl_accion" prompttext="Escriba para buscar"
                                                                        promptcssclass="serchExtender" promptposition="Top"   />

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
                                        </table>
                                        <br />
                                        <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CausesValidation="true"
                                            OnClick="btn_buscar_Click" />
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
                                        <asp:GridView ID="grd_listaBitacora" AllowPaging="True" runat="server" AutoGenerateColumns="False"
                                            Width="100%" CssClass="Grid" CellSpacing="1" CellPadding="3" OnPageIndexChanging="grd_listaBitacora_PageIndexChanging">
                                            <PagerSettings PageButtonCount="20" />
                                            <Columns>
                                                <asp:BoundField DataField="pPK_bitacora" HeaderText="Bitacora" SortExpression="pPK_bitacora" />
                                                <asp:BoundField DataField="pFK_departamento" HeaderText="Departamento" SortExpression="pFK_departamento" />
                                                <asp:BoundField DataField="pFK_usuario" HeaderText="Usuario" SortExpression="pFK_usuario" />
                                                <asp:BoundField DataField="pAccion" HeaderText="Accion" SortExpression="pAccion" />
                                                <asp:BoundField DataField="pFechaAccion" HeaderText="Fecha" SortExpression="pFechaAccion" />
                                                <asp:BoundField DataField="pNumeroRegistro" HeaderText="NumeroRegistro" SortExpression="pNumeroRegistro" />
                                                <asp:BoundField DataField="pTabla" HeaderText="Tabla" SortExpression="pTabla" />
                                                <asp:BoundField DataField="pMaquina" HeaderText="Maquina" SortExpression="pMaquina" />
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
                </Panes>
            </act:Accordion>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--    <div class="dialog" title="Bitácora">
        <p>La fecha inicial debe ser menor a la fecha final.</p>
    </div>--%>
</asp:Content>
