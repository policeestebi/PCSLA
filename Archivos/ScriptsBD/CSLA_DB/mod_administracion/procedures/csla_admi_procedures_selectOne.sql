USE CSLA
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_permisoSelectOne]'))
DROP PROCEDURE [dbo].[PA_admi_permisoSelectOne]
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
CREATE PROCEDURE  PA_admi_permisoSelectOne
@paramPK_permiso int
AS 
 BEGIN 
		SELECT 
         PK_permiso,
         nombre
        FROM t_admi_permiso
		WHERE 
			PK_permiso = @paramPK_permiso
END  
 GO 
 
 
 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_rolSelectOne]'))
DROP PROCEDURE [dbo].[PA_admi_rolSelectOne]
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
CREATE PROCEDURE  PA_admi_rolSelectOne
@paramPK_rol int
AS 
 BEGIN 
		SELECT 
         PK_rol,
         descripcion,
		 nombre,
		 visible
        FROM t_admi_rol
		WHERE 
			PK_rol = @paramPK_rol
END  
 GO 
 
  IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_menuSelectOne]'))
DROP PROCEDURE [dbo].[PA_admi_menuSelectOne]
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
CREATE PROCEDURE  PA_admi_menuSelectOne
@paramPK_menu int
AS 
 BEGIN 
		SELECT 
         PK_menu,
         FK_menuPadre,
		 imagen,
		 titulo,
		 descripcion
        FROM t_admi_menu
		WHERE 
			PK_menu = @paramPK_menu
END  
 GO 
 
   IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_paginaSelectOne]'))
DROP PROCEDURE [dbo].[PA_admi_paginaSelectOne]
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
CREATE PROCEDURE  PA_admi_paginaSelectOne
@paramPK_pagina int
AS 
 BEGIN 
		SELECT 
		 PK_pagina,
         FK_menu,
         nombre,
		 url,
		 height
        FROM t_admi_pagina
		WHERE 
			PK_pagina = @paramPK_pagina
END  
 GO 
 
     IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_departamentoSelectOne]'))
DROP PROCEDURE [dbo].[PA_admi_departamentoSelectOne]
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
CREATE PROCEDURE  PA_admi_departamentoSelectOne
@paramPK_departamento int
AS 
 BEGIN 
		SELECT 
		 PK_departamento,
         FK_departamento,
         ubicacion,
		 nombre,
		 administrador,
		 consecutivo
        FROM t_admi_departamento
		WHERE 
			PK_departamento = @paramPK_departamento
END  
 GO 
  
     IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_bitacoraSelectOne]'))
DROP PROCEDURE [dbo].[PA_admi_bitacoraSelectOne]
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
CREATE PROCEDURE  PA_admi_bitacoraSelectOne
@paramPK_bitacora int
AS 
 BEGIN 
		SELECT 
         PK_bitacora,
         FK_departamento,
         FK_usuario,
         accion,
         fecha_accion,
         numero_registro,
         tabla,
         maquina
        FROM t_admi_bitacora
		WHERE 
			PK_bitacora = @paramPK_bitacora
END  
 GO 
 
      IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_usuarioSelectOne]'))
DROP PROCEDURE [dbo].[PA_admi_usuarioSelectOne]
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
CREATE PROCEDURE  PA_admi_usuarioSelectOne
@paramPK_usuario varchar(30)
AS 
 BEGIN 
		SELECT 
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
        FROM t_admi_usuario
		WHERE 
			PK_usuario = @paramPK_usuario 
END  
 GO 
