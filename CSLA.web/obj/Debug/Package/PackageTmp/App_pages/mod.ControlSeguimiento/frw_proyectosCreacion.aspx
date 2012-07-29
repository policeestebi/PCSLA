<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master"
    AutoEventWireup="true" CodeBehind="frw_proyectosCreacion.aspx.cs" Inherits="CSLA.web.App_pages.mod.ControlSeguimiento.frw_proyectosCreacion" %>

<%@ Register Assembly="COSEVI.web.controls" Namespace="COSEVI.web.controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="tituloPagina" runat="server">
    Creación de Proyectos
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cuerpoPagina" runat="server">
    <asp:ScriptManager ID="scr_Man" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">

            var xPosEntAsociados, yPosEntAsociados, xPosEntregables, yPosEntregables,
                xPosEntregablesAsociados, yPosEntregablesAsociados, xPosCompAsociados, yPosCompAsociados, xPosComponentes, yPosComponentes,
                xPosComponentesAsociados, yPosEComponentesAsociados, xPosPaqAsociados, yPosPaqAsociados, xPosPaquetes, yPosPaquetes,
                xPosPaquetesAsociados, yPosPaquetesAsociados, xPosActAsociadas, yPosActAsociadas, xPosActividades, yPosActividades;

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            function BeginRequestHandler(sender, args) {
                
                if ($get('<%=lbx_entasociados.ClientID %>') != null) {
                    xPosEntAsociados = $get('<%=lbx_entasociados.ClientID %>').scrollLeft;
                    yPosEntAsociados = $get('<%=lbx_entasociados.ClientID %>').scrollTop;
                }
                if ($get('<%=lbx_entregables.ClientID %>') != null) {
                    xPosEntregables = $get('<%=lbx_entregables.ClientID %>').scrollLeft;
                    yPosEntregables = $get('<%=lbx_entregables.ClientID %>').scrollTop;
                }

                if ($get('<%=lbx_entregablesasociados.ClientID %>') != null) {
                    xPosEntregablesAsociados = $get('<%=lbx_entregablesasociados.ClientID %>').scrollLeft;
                    yPosEntregablesAsociados = $get('<%=lbx_entregablesasociados.ClientID %>').scrollTop;
                }
                if ($get('<%=lbx_compasociados.ClientID %>') != null) {
                    xPosCompAsociados = $get('<%=lbx_compasociados.ClientID %>').scrollLeft;
                    yPosCompAsociados = $get('<%=lbx_compasociados.ClientID %>').scrollTop;
                }
                if ($get('<%=lbx_componentes.ClientID %>') != null) {
                    xPosComponentes = $get('<%=lbx_componentes.ClientID %>').scrollLeft;
                    yPosComponentes = $get('<%=lbx_componentes.ClientID %>').scrollTop;
                }

                if ($get('<%=lbx_componentesasociados.ClientID %>') != null) {
                    xPosComponentesAsociados = $get('<%=lbx_componentesasociados.ClientID %>').scrollLeft;
                    yPosEComponentesAsociados = $get('<%=lbx_componentesasociados.ClientID %>').scrollTop;
                }
                if ($get('<%=lbx_paqasociados.ClientID %>') != null) {
                    xPosPaqAsociados = $get('<%=lbx_paqasociados.ClientID %>').scrollLeft;
                    yPosPaqAsociados = $get('<%=lbx_paqasociados.ClientID %>').scrollTop;
                }
                if ($get('<%=lbx_paquetes.ClientID %>') != null) {
                    xPosPaquetes = $get('<%=lbx_paquetes.ClientID %>').scrollLeft;
                    yPosPaquetes = $get('<%=lbx_paquetes.ClientID %>').scrollTop;
                }

                if ($get('<%=lbx_paquetesasociados.ClientID %>') != null) {
                    xPosPaquetesAsociados = $get('<%=lbx_paquetesasociados.ClientID %>').scrollLeft;
                    yPosPaquetesAsociados = $get('<%=lbx_paquetesasociados.ClientID %>').scrollTop;
                }
                if ($get('<%=lbx_actasociadas.ClientID %>') != null) {
                    xPosActAsociadas = $get('<%=lbx_actasociadas.ClientID %>').scrollLeft;
                    yPosActAsociadas = $get('<%=lbx_actasociadas.ClientID %>').scrollTop;
                }
                if ($get('<%=lbx_actividades.ClientID %>') != null) {
                    xPosActividades = $get('<%=lbx_actividades.ClientID %>').scrollLeft;
                    yPosActividades = $get('<%=lbx_actividades.ClientID %>').scrollTop;
                }
               
            }

            function EndRequestHandler(sender, args) {
                Asignación de scrolling para entregables
                if ($get('<%=lbx_entasociados.ClientID %>') != null) {
                    $get('<%=lbx_entasociados.ClientID %>').scrollLeft = xPosEntAsociados;
                    $get('<%=lbx_entasociados.ClientID %>').scrollTop = yPosEntAsociados;
                }
                if ($get('<%=lbx_entregables.ClientID %>') != null) {
                    $get('<%=lbx_entregables.ClientID %>').scrollLeft = xPosEntregables;
                    $get('<%=lbx_entregables.ClientID %>').scrollTop = yPosEntregables;
                }

                if ($get('<%=lbx_entregablesasociados.ClientID %>') != null) {
                    $get('<%=lbx_entregablesasociados.ClientID %>').scrollLeft = xPosEntregablesAsociados;
                    $get('<%=lbx_entregablesasociados.ClientID %>').scrollTop = yPosEntregablesAsociados;
                }
                if ($get('<%=lbx_compasociados.ClientID %>') != null) {
                    $get('<%=lbx_compasociados.ClientID %>').scrollLeft = xPosCompAsociados;
                    $get('<%=lbx_compasociados.ClientID %>').scrollTop = yPosCompAsociados;
                }
                if ($get('<%=lbx_componentes.ClientID %>') != null) {
                    $get('<%=lbx_componentes.ClientID %>').scrollLeft = xPosComponentes;
                    $get('<%=lbx_componentes.ClientID %>').scrollTop = yPosComponentes;
                }

                if ($get('<%=lbx_componentesasociados.ClientID %>') != null) {
                    $get('<%=lbx_componentesasociados.ClientID %>').scrollLeft = xPosComponentesAsociados;
                    $get('<%=lbx_componentesasociados.ClientID %>').scrollTop = yPosComponentesAsociados;
                }
                if ($get('<%=lbx_paqasociados.ClientID %>') != null) {
                    $get('<%=lbx_paqasociados.ClientID %>').scrollLeft = xPosPaqAsociados;
                    $get('<%=lbx_paqasociados.ClientID %>').scrollTop = yPosPaqAsociados;
                }
                if ($get('<%=lbx_paquetes.ClientID %>') != null) {
                    $get('<%=lbx_paquetes.ClientID %>').scrollLeft = xPosPaquetes;
                    $get('<%=lbx_paquetes.ClientID %>').scrollTop = yPosPaquetes;
                }

                if ($get('<%=lbx_paquetesasociados.ClientID %>') != null) {
                    $get('<%=lbx_paquetesasociados.ClientID %>').scrollLeft = xPosPaquetesAsociados;
                    $get('<%=lbx_paquetesasociados.ClientID %>').scrollTop = yPosPaquetesAsociados;
                }
                if ($get('<%=lbx_actasociadas.ClientID %>') != null) {
                    $get('<%=lbx_actasociadas.ClientID %>').scrollLeft = xPosActAsociadas;
                    $get('<%=lbx_actasociadas.ClientID %>').scrollTop = yPosActAsociadas;
                }
                if ($get('<%=lbx_actividades.ClientID %>') != null) {
                    $get('<%=lbx_actividades.ClientID %>').scrollLeft = xPosActividades;
                    $get('<%=lbx_actividades.ClientID %>').scrollTop = yPosActividades;
                }
            }

            prm.add_beginRequest(BeginRequestHandler);
            prm.add_endRequest(EndRequestHandler);        
    </script>

    <asp:UpdatePanel ID="upd_Principal" runat="server" UpdateMode="Conditional">
    </asp:UpdatePanel>

    <%--<div class="left">
        <table id="tbl_Proyecto" width="10px">
            <tr align="left">
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td align="left">
                    <asp:Label ID="lbl_proyecto" runat="server" Text="Proyecto: "></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txt_proyecto" runat="server" Width="100px" Enabled="false">
                    </asp:TextBox>
                </td>
            </tr>
        </table>
    </div>--%>
    <%--Contenido Agregado Abajo --%>
    <%--Define the wizard properties--%>
    <asp:Wizard ID="wiz_creacion" runat="server" ActiveStepIndex="1" BackColor="#FFFBD6"
        BorderColor="#FFDFAD" Width="80%" SideBarStyle-BorderWidth="50" DisplaySideBar="false"
        SideBarStyle-BorderStyle="None" SideBarStyle-HorizontalAlign="Left" SideBarButtonStyle-Font-Size="Small"
        SideBarButtonStyle-Font-Bold="true" HeaderStyle-BackColor="Black" SideBarStyle-Width="100px"
        SideBarStyle-BackColor="AliceBlue" SideBarStyle-BorderColor="Black" OnFinishButtonClick="wiz_creacion_FinishButtonClick"
        OnActiveStepChanged="OnActiveStepChanged" CancelButtonText="Cancelar" FinishCompleteButtonText="Finalizar"
        StartNextButtonText="Siguiente" StepNextButtonText="Siguiente" 
        StepPreviousButtonText="Atrás">
        <WizardSteps>
            <asp:WizardStep ID="wzs_inicio" runat="server" Title="Inicio" StepType="Auto">
                <div class="centrado">
                    <table id="tblInicio" style="display: block;" class="advertencia">
                        <br />
                        <br />
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="lblAdvertencia" CssClass="label" runat="server" Text="Wizard de Creación de Proyectos"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Image ID="img_creacion" AlternateText="imgCreacionProyecto" runat="server" ImageUrl="~/App_Themes/Basico/imagenes/iconos/img_proyecto.png" />
                            </td>
                            <td>
                                <asp:Label ID="lblAdvertenciaContenido" runat="server" Text="El siguiente Wizard le permitirá la creación de un Proyecto. En los siguientes pasos deberá realizar la asignación de los entregables, componentes, paquetes y actividades que compondrán el proyecto."></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                </div>
            </asp:WizardStep>
            <asp:WizardStep ID="wzs_entregables" runat="server" Title="Entregables" StepType="Step">
                <div class="left">
                    <table id="tbl_Proyecto" width="10px">
                        <tr align="left">
                            <td></td><td></td><td></td><td></td>
                            <td align="left">
                                <asp:Label ID="lbl_proyecto" runat="server" Text="Proyecto: "></asp:Label>
                            </td>
                            <td></td><td></td><td></td><td></td>
                            <td align="left">
                                <asp:TextBox ID="txt_proyecto" runat="server" Width="600px" Enabled="false">
                                </asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="centrado">
                    <table id="tblMensajeEntregables" style="display: block;" class="advertencia">
                        <br />
                        <br />
                        <tr>
                            <td>
                                <asp:Label ID="lblEntregablesTitulo" CssClass="label" runat="server" Text="Agregar Entregables"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEntregablesMensaje" runat="server" Text="Seleccione los entragables desplagados a las derecha y agreguélos por medio de los botones a la lista de la izquierda."></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                </div>
                <div class="center">
                    <table id="tbl_asignacionEntregables">
                        <tr align="left">
                            <td>
                                <asp:Label ID="lbl_entregables" runat="server" Text="Entregables Asociados: "></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lbl_listaEntregables" runat="server" Text="Entregables: "></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                                <act:ListSearchExtender ID="lse_entasociados" runat="server" TargetControlID="lbx_entasociados"
                                    PromptText="Digite para buscar..." PromptPosition="Top" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <act:ListSearchExtender ID="lse_entregables" runat="server" TargetControlID="lbx_entregables"
                                    PromptText="Digite para buscar..." PromptPosition="Top" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="lbx_entasociados" runat="server" SelectionMode="Multiple" Width="200px"
                                    Height="150px"></asp:ListBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btn_asignarEntregable" runat="server" Text="&lt;" OnClick="btn_asignarEntregable_Click"
                                    Width="35px" colspan="2" />
                                <br />
                                <asp:Button ID="btn_removerEntregable" runat="server" Text="&gt;" OnClick="btn_removerEntregable_Click"
                                    Width="35px" colspan="2" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:ListBox ID="lbx_entregables" runat="server" SelectionMode="Multiple" Width="200px"
                                    Height="150px"></asp:ListBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                </div>
            </asp:WizardStep>
            <asp:WizardStep ID="wzs_componentes" runat="server" Title="Componentes" StepType="Step">
                <div class="centrado">
                    <table id="tblMensajeComponentes" style="display: block;" class="advertencia">
                        <br />
                        <br />
                        <tr>
                            <td>
                                <asp:Label ID="lbl_componentesTitulo" CssClass="label" runat="server" Text="Agregar Componentes"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_componentesMensaje" runat="server" Text="La primera lista contiene los entregables seleccionados en el paso anterior, seleccione alguno de estos y se desplegaran los componentes que pueden ser agregados por medio de los botones."></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                </div>
                <div class="center">
                    <table id="tbl_componentes">
                        <tr>
                            <td>
                                <asp:Label ID="lblEntregables" runat="server" Text="Entregables Asignados"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblComponentesAsignados" runat="server" Text="Componentes Asignados"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            &nbsp;
                            <td>
                                <asp:Label ID="lblComponentes" runat="server" Text="Componentes"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <act:ListSearchExtender ID="lse_entregablesasociados" runat="server" TargetControlID="lbx_entregablesasociados"
                                    PromptText="Digite para buscar..." PromptPosition="Top" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <act:ListSearchExtender ID="lse_compasociados" runat="server" TargetControlID="lbx_compasociados"
                                    PromptText="Digite para buscar..." PromptPosition="Top" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            &nbsp;
                            <td>
                                <act:ListSearchExtender ID="lse_componentes" runat="server" TargetControlID="lbx_componentes"
                                    PromptText="Digite para buscar..." PromptPosition="Top" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="lbx_entregablesasociados" runat="server" SelectionMode="Multiple"
                                    Width="200px" Height="150px" OnSelectedIndexChanged="lbx_entregables_SelectedIndexChanged"
                                    AutoPostBack="true"></asp:ListBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:ListBox ID="lbx_compasociados" runat="server" SelectionMode="Multiple" Width="200px"
                                    Height="150px"></asp:ListBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btn_asignarComponente" runat="server" Text="&lt;" OnClick="btn_asignarComponente_Click"
                                    Width="35px" colspan="2" />
                                <br />
                                <asp:Button ID="btn_removerComponente" runat="server" Text="&gt;" OnClick="btn_removerComponente_Click"
                                    Width="35px" colspan="2" />
                            </td>
                            &nbsp;
                            <td>
                                <asp:ListBox ID="lbx_componentes" runat="server" SelectionMode="Multiple" Width="200px"
                                    Height="150px"></asp:ListBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                </div>
            </asp:WizardStep>
            <asp:WizardStep ID="wzs_paquetes" runat="server" Title="Paquetes" StepType="Step">
                <div class="centrado">
                    <table id="tbl_mensajePaquetes" style="display: block;" class="advertencia">
                        <br />
                        <br />
                        <tr>
                            <td>
                                <asp:Label ID="lbl_PaqueteTitulo" CssClass="label" runat="server" Text="Agregar Paquetes"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_PaqueteMensaje" runat="server" Text="La primera lista contiene los componentes seleccionados en el paso anterior, seleccione alguno de estos y se desplegaran los paquetes que pueden ser agregados por medio de los botones."></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                </div>
                <div class="center">
                    <table id="tbl_paquetes">
                        <tr>
                            <td>
                                <asp:Label ID="lblComponentesAsociados" runat="server" Text="Componentes Asociados"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblPaquetesAsociados" runat="server" Text="Paquetes Asociados"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            &nbsp;
                            <td>
                                <asp:Label ID="lblPaquetes" runat="server" Text="Paquetes"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <act:ListSearchExtender ID="lse_componentesasociados" runat="server" TargetControlID="lbx_componentesasociados"
                                    PromptText="Digite para buscar..." PromptPosition="Top" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <act:ListSearchExtender ID="lse_paqasociados" runat="server" TargetControlID="lbx_paqasociados"
                                    PromptText="Digite para buscar..." PromptPosition="Top" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            &nbsp;
                            <td>
                                <act:ListSearchExtender ID="lse_paquetes" runat="server" TargetControlID="lbx_paquetes"
                                    PromptText="Digite para buscar..." PromptPosition="Top" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="lbx_componentesasociados" runat="server" SelectionMode="Multiple"
                                    Width="200px" Height="150px" OnSelectedIndexChanged="lbx_componentes_SelectedIndexChanged"
                                    AutoPostBack="true"></asp:ListBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:ListBox ID="lbx_paqasociados" runat="server" SelectionMode="Multiple" Width="200px"
                                    Height="150px"></asp:ListBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btn_asignarPaquete" runat="server" Text="&lt;" OnClick="btn_asignarPaquete_Click"
                                    Width="35px" colspan="2" />
                                <br />
                                <asp:Button ID="btn_removerPaquete" runat="server" Text="&gt;" OnClick="btn_removerPaquete_Click"
                                    Width="35px" colspan="2" />
                            </td>
                            &nbsp;
                            <td>
                                <asp:ListBox ID="lbx_paquetes" runat="server" SelectionMode="Multiple" Width="200px"
                                    Height="150px"></asp:ListBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                </div>
            </asp:WizardStep>
            <asp:WizardStep ID="wzs_actividades" runat="server" Title="Actividades" StepType="Step">
                <div class="centrado">
                    <table id="tbl_mensajeActividades" style="display: block;" class="advertencia">
                        <br />
                        <br />
                        <tr>
                            <td>
                                <asp:Label ID="lbl_tituloActividades" CssClass="label" runat="server" Text="Agregar Actividades"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_actividadesMensaje" runat="server" Text="La primera lista contiene los paquetes seleccionados en el paso anterior, seleccione alguno de estos y se desplegaran las actividades que pueden ser agregados por medio de los botones."></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                </div>
                <div class="center">
                    <table id="Table1">
                        <tr>
                            <td>
                                <asp:Label ID="lblPaquetesAsignados" runat="server" Text="Paquetes Asignados"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblActividadesAsignadas" runat="server" Text="Actividades Asignadas"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            &nbsp;
                            <td>
                                <asp:Label ID="lblActividades" runat="server" Text="Actividades"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <act:ListSearchExtender ID="lse_paquetesasociados" runat="server" TargetControlID="lbx_paquetesasociados"
                                    PromptText="Digite para buscar..." PromptPosition="Top" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <act:ListSearchExtender ID="lse_actasociadas" runat="server" TargetControlID="lbx_actasociadas"
                                    PromptText="Digite para buscar..." PromptPosition="Top" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            &nbsp;
                            <td>
                                <act:ListSearchExtender ID="lse_actividades" runat="server" TargetControlID="lbx_actividades"
                                    PromptText="Digite para buscar..." PromptPosition="Top" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="lbx_paquetesasociados" runat="server" SelectionMode="Multiple" Width="200px"
                                    Height="150px" OnSelectedIndexChanged="lbx_paquetes_SelectedIndexChanged" AutoPostBack="true">
                                </asp:ListBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:ListBox ID="lbx_actasociadas" runat="server" SelectionMode="Multiple" Width="200px"
                                    Height="150px"></asp:ListBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btn_asignarActividad" runat="server" Text="&lt;" OnClick="btn_asignarActividad_Click"
                                    Width="35px" colspan="2" />
                                <br />
                                <asp:Button ID="btn_removerActividad" runat="server" Text="&gt;" OnClick="btn_removerActividad_Click"
                                    Width="35px" colspan="2" />
                            </td>
                            &nbsp;
                            <td>
                                <asp:ListBox ID="lbx_actividades" runat="server" SelectionMode="Multiple" Width="200px"
                                    Height="150px"></asp:ListBox>
                                <%--  <act:listsearchextender id="lte_Actividades" runat="server" targetcontrolid="lbx_actividades" prompttext="Escriba para buscar"
                                                            promptcssclass="serchExtender" promptposition="Top"   />--%>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                </div>
            </asp:WizardStep>
            <asp:WizardStep ID="wzs_finalizacion" runat="server" Title="Finalización" StepType="Finish">
                <div class="centrado">
                    <table id="tbl_mensajeFinalizacion" style="display: block;" class="advertencia">
                        <br />
                        <br />
                        <tr>
                            <td>
                                <asp:Label ID="lbl_tituloFinalizacion" CssClass="label" runat="server" Text="Finalización del Wizard"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_finalizacionMensaje" runat="server" Text="Finalización del Wizard de Creación de Proyecto, al dar click en el botón de Finalizar, se modificará el proyecto según el desglose realizado en los pasos anteriores."></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                </div>
            </asp:WizardStep>
        </WizardSteps>
        <StartNavigationTemplate>
            <table cellpadding="3" cellspacing="3">
                <tr>
                    <td>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="false"
                            OnClientClick="return confirm('Seguro que desea cancelar la creación del proyecto?');"
                            OnClick="btnCancel_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnNext" runat="server" Text="Siguiente >>" CausesValidation="true"
                            CommandName="MoveNext" OnCommand="btnNext_Click" />
                    </td>
                </tr>
            </table>
        </StartNavigationTemplate>
        <StepNavigationTemplate>
            <table cellpadding="3" cellspacing="3">
                <tr>
                    <td>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="false"
                            OnClientClick="return confirm('Seguro que desea cancelar la creación del proyecto?');"
                            OnClick="btnCancel_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnPrevious" runat="server" Text="<< Anterior" CausesValidation="false"
                            CommandName="MovePrevious" OnInit="btnPrev_Click" />
                        <asp:Button ID="btnNext" runat="server" Text="Siguiente >>" CausesValidation="true"
                            CommandName="MoveNext" OnInit="btnNext_Click" />
                    </td>
                </tr>
            </table>
        </StepNavigationTemplate>
        <FinishNavigationTemplate>
            <table cellpadding="3" cellspacing="3">
                <tr>
                    <td>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="false"
                            OnClientClick="return confirm('Seguro que desea cancelar la creación del proyecto?');"
                            OnClick="btnCancel_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnPrevious" runat="server" Text="<< Anterior" CausesValidation="false"
                            CommandName="MovePrevious" OnInit="btnPrev_Click" />
                        <asp:Button ID="btnFinish" runat="server" Text="Finalizar" CausesValidation="true"
                            CommandName="MoveComplete" />
                    </td>
                </tr>
            </table>
        </FinishNavigationTemplate>
    </asp:Wizard>
</asp:Content>
