<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master" AutoEventWireup="true" CodeBehind="frw_ayuda.aspx.cs" Inherits="CSLA.web.App_pages.mod.Ayuda.frw_ayuda" %>

<asp:Content ID="Content5" ContentPlaceHolderID="head" runat="server">


 <script type="text/javascript">
     window.onload = function () {
         var myPDF = new PDFObject({ url: "../../PDF/CSLA_Manual_Usuario.pdf" }).embed();
     };
  </script>

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="tituloPagina" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="cuerpoPagina" runat="server">

    
    <p>Al parecer usted no tiene Adobe Reader o soporte PDF en este navegador. <a href="../../PDF/CSLA_Manual_Usuario.pdf">Click aquí para descargar el PDF.</a></p>

</asp:Content>
