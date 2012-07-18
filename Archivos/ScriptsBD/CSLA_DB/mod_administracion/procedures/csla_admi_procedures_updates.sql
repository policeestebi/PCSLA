
USE CSLA
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_permisoUpdate]'))
DROP PROCEDURE [dbo].[PA_admi_permisoUpdate]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Generador
-- Fecha Creación:	15-05-2011
-- Fecha Actulización:	15-05-2011
-- Descripción: 
-- =============================================
CREATE PROCEDURE  PA_admi_permisoUpdate 
  @paramPK_permiso int,
  @paramnombre varchar(50)
AS 
 BEGIN 
SET NOCOUNT ON; 

         UPDATE t_admi_permiso
		 SET 
			nombre = @paramnombre       
         WHERE 
			PK_permiso = @paramPK_permiso

END   
 GO 

 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_rolUpdate]'))
DROP PROCEDURE [dbo].[PA_admi_rolUpdate]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Generador
-- Fecha Creación:	15-05-2011
-- Fecha Actulización:	15-05-2011
-- Descripción: 
-- =============================================
CREATE PROCEDURE  PA_admi_rolUpdate 
  @paramPK_rol int,
  @paramdescripcion varchar(100), 
  @paramnombre varchar(75), 
  @paramvisible smallint 
AS 
 BEGIN 
SET NOCOUNT ON; 

         UPDATE t_admi_rol
		 SET 
			descripcion = @paramdescripcion ,
			nombre = @paramnombre ,
			visible = @paramvisible       
         WHERE 
			PK_rol = @paramPK_rol

END   
 GO 


IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_menuUpdate]'))
DROP PROCEDURE [dbo].[PA_admi_menuUpdate]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Generador
-- Fecha Creación:	15-05-2011
-- Fecha Actulización:	15-05-2011
-- Descripción: 
-- =============================================
CREATE PROCEDURE  PA_admi_menuUpdate 
  @paramPK_menu int,
  @paramFK_menuPadre int, 
  @paramimagen varchar(100), 
  @paramtitulo varchar(100), 
  @paramdescripcion varchar(500) 
AS 
 BEGIN 
SET NOCOUNT ON; 

         UPDATE t_admi_menu
         SET 
			FK_menuPadre = @paramFK_menuPadre ,
			imagen = @paramimagen ,
			titulo = @paramtitulo ,
			descripcion = @paramdescripcion       
         WHERE 
			PK_menu = @paramPK_menu

END   
 GO 

 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_paginaUpdate]'))
DROP PROCEDURE [dbo].[PA_admi_paginaUpdate]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Generador
-- Fecha Creación:	15-05-2011
-- Fecha Actulización:	15-05-2011
-- Descripción: 
-- =============================================
CREATE PROCEDURE  PA_admi_paginaUpdate 
  @paramPK_pagina int,
  @paramFK_menu int, 
  @paramnombre varchar(100), 
  @paramurl varchar(100),
  @paramheight varchar(1000)  
AS 
 BEGIN 
SET NOCOUNT ON; 

         UPDATE t_admi_pagina
         SET 
			FK_menu = @paramFK_menu ,
			nombre = @paramnombre ,
			url = @paramurl,
			height = @paramheight 			
         WHERE 
			PK_pagina = @paramPK_pagina


END   
 GO 

 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_departamentoUpdate]'))
DROP PROCEDURE [dbo].[PA_admi_departamentoUpdate]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Generador
-- Fecha Creación:	15-05-2011
-- Fecha Actulización:	15-05-2011
-- Descripción: 
-- =============================================
CREATE PROCEDURE  PA_admi_departamentoUpdate
  @paramPK_departamento int,
  @paramFK_departamento int, 
  @paramnombre varchar(30), 
  @paramubicacion varchar(100), 
  @paramadministrador varchar(30),
  @paramconsecutivo varchar(45)
AS 
 BEGIN 
SET NOCOUNT ON; 

         UPDATE t_admi_departamento
         SET FK_departamento = @paramFK_departamento ,
			nombre = @paramnombre ,
			ubicacion = @paramubicacion ,
			administrador = @paramadministrador,
			consecutivo = @paramconsecutivo         
         WHERE 
			PK_departamento = @paramPK_departamento       

END   
 GO 

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_bitacoraUpdate]'))
DROP PROCEDURE [dbo].[PA_admi_bitacoraUpdate]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Generador
-- Fecha Creación:	15-05-2011
-- Fecha Actulización:	15-05-2011
-- Descripción: 
-- =============================================
CREATE PROCEDURE  PA_admi_bitacoraUpdate 
  @paramPK_bitacora int,
  @paramFK_departamento int, 
  @paramFK_usuario varchar(30), 
  @paramaccion varchar(50), 
  @paramfecha_accion datetime, 
  @paramnumero_registro varchar(100), 
  @paramtabla varchar(50), 
  @parammaquina varchar(100)  
AS 
 BEGIN 
SET NOCOUNT ON;

         UPDATE t_admi_bitacora
         SET 
			FK_departamento = @paramFK_departamento ,
			FK_usuario = @paramFK_usuario ,
			accion = @paramaccion ,
			fecha_accion = @paramfecha_accion ,
			numero_registro = @paramnumero_registro ,
			tabla = @paramtabla ,
			maquina = @parammaquina       
         WHERE 
			PK_bitacora = @paramPK_bitacora

END   
 GO 

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_usuarioUpdate]'))
DROP PROCEDURE [dbo].[PA_admi_usuarioUpdate]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Generador
-- Fecha Creación:	15-05-2011
-- Fecha Actulización:	15-05-2011
-- Descripción: 
-- =============================================
CREATE PROCEDURE  PA_admi_usuarioUpdate 
  @paramPK_usuario varchar(30),
  @paramFK_rol int, 
  @paramclave varchar(40),
  @paramactivo smallint,
  @paramnombre varchar(45), 
  @paramapellido1 varchar(45), 
  @paramapellido2 varchar(45),
  @parampuesto varchar(45),
  @paramemail varchar(45),
  @paramFK_departamento int 
AS 
 BEGIN 
SET NOCOUNT ON; 

         UPDATE t_admi_usuario
         SET 
			FK_rol = @paramFK_rol ,
			clave = @paramclave,
			activo = @paramactivo,
			nombre = @paramnombre ,
			apellido1 = @paramapellido1 ,
			apellido2 = @paramapellido2,
			puesto = @parampuesto,
			email = @paramemail,
			FK_departamento = @paramFK_departamento			
         WHERE 
			PK_usuario = @paramPK_usuario

END   
 GO 

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_usuarioContrasenaUpdate]'))
DROP PROCEDURE [dbo].[PA_admi_usuarioContrasenaUpdate]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Cristian Arce
-- Fecha Creación:	29-01-2012
-- Fecha Actulización:	29-01-2012
-- Descripción: 
-- =============================================
CREATE PROCEDURE  PA_admi_usuarioContrasenaUpdate 
  @paramPK_usuario varchar(30),
  @paramcontrasena varchar(40)

AS 
 BEGIN 
SET NOCOUNT ON; 

         UPDATE t_admi_usuario
         SET 
			clave = @paramcontrasena
					
         WHERE 
			PK_usuario = @paramPK_usuario

END   
 GO 