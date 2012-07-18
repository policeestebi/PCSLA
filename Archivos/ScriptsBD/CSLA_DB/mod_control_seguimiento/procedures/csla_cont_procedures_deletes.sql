USE CSLA
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_estadoDelete]'))
DROP PROCEDURE [dbo].[PA_cont_estadoDelete]
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
CREATE PROCEDURE  PA_cont_estadoDelete 
  @paramPK_estado int
AS 
 BEGIN 
SET NOCOUNT ON;

         DELETE FROM t_cont_estado       
         WHERE 
			PK_estado = @paramPK_estado

END   
 GO 
 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_proyectoDelete]'))
DROP PROCEDURE [dbo].[PA_cont_proyectoDelete]
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
CREATE PROCEDURE  PA_cont_proyectoDelete 
  @paramPK_proyecto int

AS 
 BEGIN 
SET NOCOUNT ON; 

         DELETE FROM t_cont_proyecto       
         WHERE 
			PK_proyecto = @paramPK_proyecto
			
END   
 GO 
 
 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_departamento_proyectoDelete]'))
DROP PROCEDURE [dbo].[PA_cont_departamento_proyectoDelete]
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
CREATE PROCEDURE  PA_cont_departamento_proyectoDelete 
  @paramPK_departamento int, 
  @paramPK_proyecto int 
 
AS 
 BEGIN 
SET NOCOUNT ON; 

         DELETE FROM t_cont_departamento_proyecto       
         WHERE 
			   PK_departamento = @paramPK_departamento AND 
               PK_proyecto = @paramPK_proyecto

END   
 GO 

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_departamento_proyectoDeleteMasivo]'))
DROP PROCEDURE [dbo].[PA_cont_departamento_proyectoDeleteMasivo]
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
CREATE PROCEDURE  PA_cont_departamento_proyectoDeleteMasivo 
  @paramPK_proyecto int 
 
AS 
 BEGIN 
SET NOCOUNT ON; 

         DELETE FROM t_cont_departamento_proyecto       
         WHERE 
               PK_proyecto = @paramPK_proyecto

END   
 GO 

 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_entregableDelete]'))
DROP PROCEDURE [dbo].[PA_cont_entregableDelete]
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
CREATE PROCEDURE  PA_cont_entregableDelete 
  @paramPK_entregable int
AS 
 BEGIN 
SET NOCOUNT ON; 

         DELETE FROM t_cont_entregable       
         WHERE 
			PK_entregable = @paramPK_entregable

END   
 GO 

 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_proyecto_entregableDelete]'))
DROP PROCEDURE [dbo].[PA_cont_proyecto_entregableDelete]
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
CREATE PROCEDURE  PA_cont_proyecto_entregableDelete 
  @paramPK_entregable int, 
  @paramPK_proyecto int 
AS 
 BEGIN 
SET NOCOUNT ON; 

         DELETE FROM t_cont_proyecto_entregable       
         WHERE 
			   PK_entregable = @paramPK_entregable AND 
               PK_proyecto = @paramPK_proyecto

END   
 GO 

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_componenteDelete]'))
DROP PROCEDURE [dbo].[PA_cont_componenteDelete]
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
CREATE PROCEDURE  PA_cont_componenteDelete 
  @paramPK_componente int
AS 
 BEGIN 
SET NOCOUNT ON; 

         DELETE FROM t_cont_componente       
         WHERE 
			PK_componente = @paramPK_componente

END   
 GO 

 
 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_entregable_componenteDelete]'))
DROP PROCEDURE [dbo].[PA_cont_entregable_componenteDelete]
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
CREATE PROCEDURE  PA_cont_entregable_componenteDelete 
  @paramPK_componente int, 
  @paramPK_entregable int, 
  @paramPK_proyecto int 
AS 
 BEGIN 
SET NOCOUNT ON; 

         DELETE FROM t_cont_entregable_componente       
         WHERE 
			   PK_componente = @paramPK_componente AND 
               PK_entregable = @paramPK_entregable AND 
               PK_proyecto = @paramPK_proyecto

END   
 GO 


 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_paqueteDelete]'))
DROP PROCEDURE [dbo].[PA_cont_paqueteDelete]
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
CREATE PROCEDURE  PA_cont_paqueteDelete 
  @paramPK_paquete int
AS 
 BEGIN 
SET NOCOUNT ON; 

         DELETE FROM t_cont_paquete       
         WHERE 
			PK_paquete = @paramPK_paquete

END   
 GO 
 
 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_componente_paqueteDelete]'))
DROP PROCEDURE [dbo].[PA_cont_componente_paqueteDelete]
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
CREATE PROCEDURE  PA_cont_componente_paqueteDelete 
  @paramPK_paquete int, 
  @paramPK_componente int, 
  @paramPK_entregable int, 
  @paramPK_proyecto int 
AS 
 BEGIN 
SET NOCOUNT ON; 

         DELETE FROM t_cont_componente_paquete       
         WHERE 
			   PK_paquete = @paramPK_paquete AND 
               PK_componente = @paramPK_componente AND 
               PK_entregable = @paramPK_entregable AND 
               PK_proyecto = @paramPK_proyecto

END   
 GO 

 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_actividadDelete]'))
DROP PROCEDURE [dbo].[PA_cont_actividadDelete]
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
CREATE PROCEDURE  PA_cont_actividadDelete 
  @paramPK_actividad int
AS 
 BEGIN 
SET NOCOUNT ON; 

         DELETE FROM t_cont_actividad       
         WHERE 
			   PK_actividad = @paramPK_actividad

END   
 GO 

 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_actividadAsignadaDelete]'))
DROP PROCEDURE [dbo].[PA_cont_actividadAsignadaDelete]
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
CREATE PROCEDURE  PA_cont_actividadAsignadaDelete 
  @paramPK_actividad int, 
  @paramPK_paquete int, 
  @paramPK_componente int, 
  @paramPK_entregable int, 
  @paramPK_proyecto int, 
  @paramPK_usuario varchar(30) 
AS 
 BEGIN 
SET NOCOUNT ON; 

         DELETE FROM t_cont_asignacion_actividad
         WHERE 
			   PK_actividad = @paramPK_actividad AND 
               PK_paquete = @paramPK_paquete AND 
               PK_componente = @paramPK_componente AND 
               PK_entregable = @paramPK_entregable AND 
               PK_proyecto = @paramPK_proyecto AND 
               PK_usuario = @paramPK_usuario

END   
 GO 

  IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_operacionDelete]'))
DROP PROCEDURE [dbo].[PA_cont_operacionDelete]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Esteban Ramírez G.
-- Fecha Creación:	11-04-2011
-- Fecha Actulización:	11-04-2011
-- Descripción: 
-- =============================================
CREATE PROCEDURE  PA_cont_operacionDelete 
  @paramPK_operacion NVARCHAR(50),
  @paramUsuario		 NVARCHAR(30)
AS 
BEGIN 
SET NOCOUNT ON; 

		DELETE FROM t_cont_asignacion_operacion
		WHERE PK_usuario = @paramUsuario AND PK_codigo = @paramPK_operacion;
		
         DELETE FROM t_cont_operacion      
         WHERE 
			PK_codigo = @paramPK_operacion
			
END   
GO 