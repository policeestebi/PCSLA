USE CSLA
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_permisoInsert]'))
DROP PROCEDURE [dbo].[PA_admi_permisoInsert]
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
CREATE PROCEDURE  PA_admi_permisoInsert 
  @paramnombre varchar(50) 
 
AS 
 BEGIN 
 SET NOCOUNT ON; 

         INSERT INTO t_admi_permiso
        ( 
         nombre
        ) 
        VALUES
        ( 
         @paramnombre
        )

END  
 GO 


IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_rolInsert]'))
DROP PROCEDURE [dbo].[PA_admi_rolInsert]
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
CREATE PROCEDURE  PA_admi_rolInsert 
  @paramdescripcion varchar(100), 
  @paramnombre varchar(75), 
  @paramvisible smallint 
 
AS 
 BEGIN 
 SET NOCOUNT ON; 

         INSERT INTO t_admi_rol
        ( 
         descripcion,
         nombre,
         visible
        ) 
        VALUES
        ( 
         @paramdescripcion,
         @paramnombre,
         @paramvisible
        ) 

END  
 GO 

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_menuInsert]'))
DROP PROCEDURE [dbo].[PA_admi_menuInsert]
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
CREATE PROCEDURE  PA_admi_menuInsert 
  @paramPK_menu int, 
  @paramFK_menuPadre int, 
  @paramimagen varchar(100), 
  @paramtitulo varchar(100), 
  @paramdescripcion varchar(500) 
 
AS 
 BEGIN 
 SET NOCOUNT ON; 
		
		DECLARE @codigo DECIMAL(38,0);
	
		SELECT @codigo = ISNULL(MAX(PK_menu),0) +1  FROM t_admi_menu;
		
         INSERT INTO t_admi_menu
        ( 
         PK_menu,
         FK_menuPadre,
         imagen,
         titulo,
         descripcion
        ) 
        VALUES
        ( 
         @codigo,
         @paramFK_menuPadre,
         @paramimagen,
         @paramtitulo,
         @paramdescripcion
        ) 

END  
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_paginaInsert]'))
DROP PROCEDURE [dbo].[PA_admi_paginaInsert]
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
CREATE PROCEDURE  PA_admi_paginaInsert 
  @paramFK_menu int, 
  @paramnombre varchar(100), 
  @paramurl varchar(100),
  @paramheight varchar(1000)   
 
AS 
 BEGIN 
 SET NOCOUNT ON; 
 

         INSERT INTO t_admi_pagina
        (
         FK_menu,
         nombre,
         url,
		 height
        ) 
        VALUES
        (
         @paramFK_menu,
         @paramnombre,
         @paramurl,
		 @paramheight
        ) 

END  
 GO 


IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_pagina_permisoInsert]'))
DROP PROCEDURE [dbo].[PA_admi_pagina_permisoInsert]
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
CREATE PROCEDURE  PA_admi_pagina_permisoInsert 
  @paramPK_pagina int, 
  @paramPK_permiso int 
AS 
 BEGIN 
 SET NOCOUNT ON; 

         INSERT INTO t_admi_pagina_permiso
        ( 
         PK_pagina,
         PK_permiso
        ) 
        VALUES
        ( 
         @paramPK_pagina,
         @paramPK_permiso
        ) 

END  
 GO 


IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_rol_pagina_permisoInsert]'))
DROP PROCEDURE [dbo].[PA_admi_rol_pagina_permisoInsert]
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
CREATE PROCEDURE  PA_admi_rol_pagina_permisoInsert 
  @paramPK_rol int, 
  @paramPK_pagina int, 
  @paramPK_permiso int 
AS 
 BEGIN 
 SET NOCOUNT ON; 

         INSERT INTO t_admi_rol_pagina_permiso
        ( 
         PK_rol,
         PK_pagina,
         PK_permiso
        ) 
        VALUES
        ( 
         @paramPK_rol,
         @paramPK_pagina,
         @paramPK_permiso
        ) 
		
END  
 GO  
 
 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_departamentoInsert]'))
DROP PROCEDURE [dbo].[PA_admi_departamentoInsert]
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
CREATE PROCEDURE  PA_admi_departamentoInsert 
  @paramFK_departamento int, 
  @paramnombre varchar(30), 
  @paramubicacion varchar(100), 
  @paramadministrador varchar(30),
  @paramconsecutivo varchar(45) 
AS 
 BEGIN 
 SET NOCOUNT ON; 

         INSERT INTO t_admi_departamento
        ( 
         FK_departamento,
         nombre,
         ubicacion,
         administrador,
		 consecutivo
        ) 
        VALUES
        ( 
         @paramFK_departamento,
         @paramnombre,
         @paramubicacion,
         @paramadministrador,
		 @paramconsecutivo
        )
	
END  
 GO 
  
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_bitacoraInsert]'))
DROP PROCEDURE [dbo].[PA_admi_bitacoraInsert]
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
CREATE PROCEDURE  PA_admi_bitacoraInsert 
  @paramPK_bitacora numeric(10,2), 
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

         INSERT INTO t_admi_bitacora
        ( 
		 PK_bitacora,
         FK_departamento,
         FK_usuario,
         accion,
         fecha_accion,
         numero_registro,
         tabla,
         maquina
        ) 
        VALUES
        ( 
		 @paramPK_bitacora,
         @paramFK_departamento,
         @paramFK_usuario,
         @paramaccion,
         @paramfecha_accion,
         @paramnumero_registro,
         @paramtabla,
         @parammaquina
        ) 
		
END  
 GO 

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_usuarioInsert]'))
DROP PROCEDURE [dbo].[PA_admi_usuarioInsert]
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
CREATE PROCEDURE  PA_admi_usuarioInsert 
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

         INSERT INTO t_admi_usuario
        ( 
         PK_usuario,
         FK_rol,
         clave,
		 activo,
		 nombre,
		 apellido1,
		 apellido2,
		 puesto,
		 email,
		 FK_departamento		
        ) 
        VALUES
        ( 
         @paramPK_usuario,
         @paramFK_rol,
         @paramclave,
    	 @paramactivo,
		 @paramnombre,
		 @paramapellido1,
		 @paramapellido2,
		 @parampuesto,
		 @paramemail,
		 @paramFK_departamento				 
        ) 		
		
END  
 GO 

 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_imprimeReporteRegistro]'))
DROP PROCEDURE [dbo].[PA_cont_imprimeReporteRegistro]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Generador
-- Fecha Creación:	12-06-2012
-- Fecha Actulización:	12-06-2012
-- Descripción: 
-- =============================================
CREATE PROCEDURE  PA_cont_imprimeReporteRegistro
  @paramUsuario varchar(30),
  @paramFecha	DATE
AS
BEGIN 
DECLARE @departamento INT,
		@consecutivo  INT,
		@codigoDep NVARCHAR(30),
		@codigo NVARCHAR(45),
		@bitacora NUMERIC(10,0) 
		
SELECT @bitacora = ISNULL(MAX(PK_bitacora) + 1,1) FROM t_admi_bitacora

	IF EXISTS
		(SELECT 
			PK_usuario 
		 FROM
			t_admi_consecutivo c
		WHERE
			c.PK_usuario = @paramUsuario AND
			c.PK_fecha_impresion = @paramFecha)
	BEGIN
	
		SELECT 
			 @codigo = PK_consecutivo
		 FROM
			t_admi_consecutivo c
		WHERE
			c.PK_usuario = @paramUsuario AND
			c.PK_fecha_impresion = @paramFecha
		
		UPDATE t_admi_consecutivo 
				SET reimpresion = reimpresion + 1
		WHERE
			PK_usuario = @paramUsuario AND
			PK_fecha_impresion = @paramFecha
		
		INSERT t_admi_bitacora (PK_bitacora,
								FK_departamento,
								FK_usuario,
								accion,
								fecha_accion, 
								numero_registro,
								tabla,maquina)
				VALUES
				(@bitacora, NULL,@paramUsuario,
				'Modificar',
				GETDATE(),
				@paramUsuario + '/' + @codigo + '/' + CONVERT(VARCHAR,@paramFecha,103),
				't_admi_consecutivo','')
			SELECT @codigo;	
		
	END
	ELSE
	BEGIN
		SELECT @departamento = FK_departamento from t_admi_usuario
		WHERE PK_usuario = @paramUsuario
		
		SELECT @consecutivo = CONVERT(int,consecutivo) + 1,
			   @codigoDep = nombre
		 FROM  t_admi_departamento
		WHERE PK_departamento = @departamento;
		  
		SELECT @codigo = 'IF-' + @codigoDep + '-'+ CONVERT(VARCHAR,YEAR(@paramFecha)) + '-' +  CONVERT(VARCHAR,@consecutivo)
		
		INSERT INTO t_admi_consecutivo (PK_usuario,PK_consecutivo,PK_fecha_impresion,reimpresion)
		VALUES (@paramUsuario,@codigo,@paramFecha,0)
		
		INSERT t_admi_bitacora (PK_bitacora,
								FK_departamento,
								FK_usuario,
								accion,
								fecha_accion, 
								numero_registro,
								tabla,maquina)
				VALUES
				(@bitacora,NULL,@paramUsuario,
				'Insertar',
				GETDATE(),
				@paramUsuario + '/' + @codigo + '/' + CONVERT(VARCHAR,@paramFecha,103),
				't_admi_consecutivo','')
				
		UPDATE t_admi_departamento
				SET consecutivo = CONVERT(nvarchar(45), @consecutivo)
		WHERE PK_departamento = @departamento;
		
		SELECT @codigo;
		
	END

END   
 GO

