USE CSLA
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_permisoDelete]'))
DROP PROCEDURE [dbo].[PA_admi_permisoDelete]
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
CREATE PROCEDURE  PA_admi_permisoDelete 
  @paramPK_permiso int
AS 
 BEGIN 
 SET NOCOUNT ON; 
 
		DELETE FROM t_admi_permiso       
         WHERE 
			PK_permiso = @paramPK_permiso
				
END  
 GO 
 
 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_rolDelete]'))
DROP PROCEDURE [dbo].[PA_admi_rolDelete]
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
CREATE PROCEDURE  PA_admi_rolDelete 
  @paramPK_rol int
AS 
 BEGIN 
 SET NOCOUNT ON; 

		DELETE FROM t_admi_rol       
         WHERE 
			PK_rol = @paramPK_rol
			
END  
 GO 


IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_menuDelete]'))
DROP PROCEDURE [dbo].[PA_admi_menuDelete]
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
CREATE PROCEDURE  PA_admi_menuDelete 
  @paramPK_menu int
AS 
 BEGIN 
SET NOCOUNT ON; 

         DELETE FROM t_admi_menu       
         WHERE 
			PK_menu = @paramPK_menu

END  
 GO 


IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_paginaDelete]'))
DROP PROCEDURE [dbo].[PA_admi_paginaDelete]
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
CREATE PROCEDURE  PA_admi_paginaDelete 
  @paramPK_pagina int
AS 
 BEGIN 
SET NOCOUNT ON; 

		DELETE
			FROM t_admi_pagina_permiso
		WHERE
			PK_pagina = @paramPK_pagina

         DELETE FROM t_admi_pagina       
         WHERE 
			PK_pagina = @paramPK_pagina
END  
 GO 


IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_pagina_permisoDelete]'))
DROP PROCEDURE [dbo].[PA_admi_pagina_permisoDelete]
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
CREATE PROCEDURE  PA_admi_pagina_permisoDelete 
  @paramPK_pagina int, 
  @paramPK_permiso int 
AS 
 BEGIN 
SET NOCOUNT ON; 

         DELETE FROM t_admi_pagina_permiso       
         WHERE 
			   PK_pagina = @paramPK_pagina AND 
               PK_permiso = @paramPK_permiso

END  
 GO 


IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_rol_pagina_permisoDelete]'))
DROP PROCEDURE [dbo].[PA_admi_rol_pagina_permisoDelete]
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
CREATE PROCEDURE  PA_admi_rol_pagina_permisoDelete 
  @paramPK_rol int, 
  @paramPK_pagina int, 
  @paramPK_permiso int 
 
AS 
 BEGIN 
SET NOCOUNT ON; 

         DELETE FROM t_admi_rol_pagina_permiso       
         WHERE PK_rol = @paramPK_rol AND 
               PK_pagina = @paramPK_pagina AND 
               PK_permiso = @paramPK_permiso

END  
 GO 

 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_departamentoDelete]'))
DROP PROCEDURE [dbo].[PA_admi_departamentoDelete]
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
CREATE PROCEDURE  PA_admi_departamentoDelete 
  @paramPK_departamento int
AS 
 BEGIN 
SET NOCOUNT ON; 

         DELETE FROM t_admi_departamento       
         WHERE 
			PK_departamento = @paramPK_departamento

END  
 GO 
 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_usuarioDelete]'))
DROP PROCEDURE [dbo].[PA_admi_usuarioDelete]
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
CREATE PROCEDURE  PA_admi_usuarioDelete 
  @paramPK_usuario varchar(30)
AS 
 BEGIN 
SET NOCOUNT ON; 

         DELETE FROM t_admi_usuario       
         WHERE 
			PK_usuario = @paramPK_usuario
		
END  
 GO 

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_rol_pagina_permisoDeleteAll]'))
DROP PROCEDURE [dbo].[PA_admi_rol_pagina_permisoDeleteAll]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Esteban Ramírez González
-- Fecha Creación:	06-11-2011
-- Fecha Actulización:	06-11-2011
-- Descripción: Elimina todos los permismos asociados a un rol.
-- =============================================
CREATE PROCEDURE  PA_admi_rol_pagina_permisoDeleteAll
  @paramPK_rol int
AS 
 BEGIN 
         SET NOCOUNT ON; 
         DELETE FROM t_admi_rol_pagina_permiso       
         WHERE PK_rol = @paramPK_rol 
END  
 GO 
