<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master" AutoEventWireup="true" CodeBehind="frw_ayuda.aspx.cs" Inherits="CSLA.web.App_pages.mod.Ayuda.frw_ayuda" %>

<asp:Content ID="Content5" ContentPlaceHolderID="head" runat="server">

<style type="text/css">
    
    body{
        background-image:url(../../App_Themes/Basico/imagenes/css/madera.png);
    }
    
    p
    {
        text-align:justify;
    }
    
</style>

<script type="text/javascript" >
    $(function () {

        $('#mybook').booklet({
            menu: '#custom-menu-2',
            chapterSelector: true,
            pageSelector: true,
            arrows: true,
            width:  1000,
            height: 600
        });
        
    });
</script>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="tituloPagina" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="cuerpoPagina" runat="server">

<div id="mybook">
	<div title="This is a page title" rel="Chapter 1"> 
		<h1>
            Manual de Usuario
            Versión 1.0
        </h1>
	</div>
	<div title="This is a page title" rel="Chapter 2"> 
		<h1>Introducción</h1>
        <br />
        <p>
            El siguiente documento tiene el fin de guiar y servir de ayuda a los usuario del sistema del Control y Seguimiento de Labores (CSLA), en donde se encontrará información sobre las diferentes funcionalidades y características del mismo. 
        </p>
        <p>
            El documento le mostrará cada una de las partes que componen la interfaz de usuario, así como una explicación clara de cada una de las páginas que componen el sistema, además de su relación entre las mismas y los procesos que se lleva a cabo en el sistema.
        </p>

	</div>
	<div> 
		<h1>Primeros pasos con el CSLA</h1>
        <br />
        <h2>
        Componentes
        </h2>
        <br />
        <h3>
        Formulario de Ingreso
        </h3>
        <br />
        <p>
            A continuación se describen los principales componentes de la interfaz de usuario.
        </p>
	</div>
	<div> 
		
	</div>
    <div> 
		
	</div>
	<div> 
		
	</div>
    <div> 
		
	</div>
	<div> 
		
	</div>
</div>

</asp:Content>
