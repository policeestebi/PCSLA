USE CSLA
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_estadoSelectOne]'))
DROP PROCEDURE [dbo].[PA_cont_estadoSelectOne]
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
CREATE PROCEDURE  PA_cont_estadoSelectOne 
@paramPK_estado int
AS 
 BEGIN 
         SELECT 
			PK_estado, 
			descripcion 
		FROM t_cont_estado 
        WHERE 
			PK_estado = @paramPK_estado
END  
 GO 

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_proyectoSelectOne]'))
DROP PROCEDURE [dbo].[PA_cont_proyectoSelectOne]
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
CREATE PROCEDURE  PA_cont_proyectoSelectOne 
  @paramPK_proyecto int
AS 
 BEGIN 

		SELECT 
			PK_proyecto,
			FK_estado,
			nombre,
			descripcion,
			objetivo,
			meta,
			fechaInicio,
			fechaFin,
			horasAsignadas,
			ISNULL((SELECT SUM(horas) FROM t_cont_registro_actividad WHERE PK_proyecto = @paramPK_proyecto),0) horasReales
		FROM t_cont_proyecto  
        WHERE 
			PK_proyecto = @paramPK_proyecto
END  
 GO 
 
 
 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_entregableSelectOne]'))
DROP PROCEDURE [dbo].[PA_cont_entregableSelectOne]
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
CREATE PROCEDURE  PA_cont_entregableSelectOne 
@paramPK_entregable int
AS 
 BEGIN 
         SELECT  
			PK_entregable, 
			codigo,
			nombre,
			descripcion  
		 FROM t_cont_entregable   
         WHERE 
			PK_entregable = @paramPK_entregable
END  
 GO
 
 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_componenteSelectOne]'))
DROP PROCEDURE [dbo].[PA_cont_componenteSelectOne]
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
CREATE PROCEDURE  PA_cont_componenteSelectOne 
@paramPK_componente int
AS 
 BEGIN 
         SELECT 
			PK_componente,
			codigo,
			nombre,
			descripcion   
		 FROM   t_cont_componente
         WHERE 
			PK_componente = @paramPK_componente
END  
 GO 


IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_paqueteSelectOne]'))
DROP PROCEDURE [dbo].[PA_cont_paqueteSelectOne]
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
CREATE PROCEDURE  PA_cont_paqueteSelectOne 
@paramPK_paquete int
AS 
 BEGIN 
         SELECT 
			PK_paquete,
			codigo,
			nombre,
			descripcion     
		 FROM t_cont_paquete  
         WHERE 
			PK_paquete = @paramPK_paquete
END  
 GO 

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_actividadSelectOne]'))
DROP PROCEDURE [dbo].[PA_cont_actividadSelectOne]
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
CREATE PROCEDURE  PA_cont_actividadSelectOne 
  @paramPK_actividad int
AS 
 BEGIN 

         SELECT 
			PK_actividad,
			codigo,
			nombre,
			descripcion      
		 FROM t_cont_actividad    
         WHERE 
			   PK_actividad = @paramPK_actividad 
END  
 GO 


   IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_actividadesPaqueteSelectOne]'))
DROP PROCEDURE [dbo].[PA_cont_actividadesPaqueteSelectOne]
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
CREATE PROCEDURE  PA_cont_actividadesPaqueteSelectOne
  @paramPK_proyecto int,
  @paramPK_paquete int,
  @paramPK_actividad int
AS 
 BEGIN 
		SELECT 
			 cont_paq_act.PK_proyecto,
			 cont_paq_act.PK_entregable,
			 cont_paq_act.PK_componente,
			 cont_paq_act.PK_paquete,
			 cont_paq_act.PK_actividad,
			 cont_act.nombre nombreActividad,       
			 cont_paq.nombre nombrePaquete
        FROM 
			t_cont_paquete_actividad cont_paq_act inner join t_cont_proyecto cont_proy 
		ON 
			cont_paq_act.PK_proyecto = cont_proy.PK_proyecto inner join t_cont_entregable cont_ent
		ON 
			cont_paq_act.PK_entregable = cont_ent.PK_entregable inner join t_cont_componente cont_comp
		ON 
			cont_paq_act.PK_componente = cont_comp.PK_componente inner join t_cont_paquete cont_paq
		ON 
			cont_paq_act.PK_paquete = cont_paq.PK_paquete inner join t_cont_actividad cont_act
		ON 
			cont_paq_act.PK_actividad = cont_act.PK_actividad
		WHERE 
			cont_paq_act.PK_proyecto = @paramPK_proyecto AND
			cont_paq_act.PK_paquete = @paramPK_paquete AND
			cont_paq_act.PK_actividad = @paramPK_actividad AND
			cont_paq_act.activo = 1
END  
 GO

  IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_actividadesPaqueteSelect]'))
DROP PROCEDURE [dbo].[PA_cont_actividadesPaqueteSelect]
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
CREATE PROCEDURE  PA_cont_actividadesPaqueteSelect
  @paramPK_proyecto int,
  @paramPK_paquete int
AS 
 BEGIN 
		SELECT 
			 cont_paq_act.PK_proyecto,
			 cont_paq_act.PK_entregable,
			 cont_paq_act.PK_componente,
			 cont_paq_act.PK_paquete,
			 cont_paq_act.PK_actividad,
			 cont_act.nombre nombreActividad,       
			 cont_paq.nombre nombrePaquete
        FROM 
			t_cont_paquete_actividad cont_paq_act inner join t_cont_proyecto cont_proy 
		ON 
			cont_paq_act.PK_proyecto = cont_proy.PK_proyecto inner join t_cont_entregable cont_ent
		ON 
			cont_paq_act.PK_entregable = cont_ent.PK_entregable inner join t_cont_componente cont_comp
		ON 
			cont_paq_act.PK_componente = cont_comp.PK_componente inner join t_cont_paquete cont_paq
		ON 
			cont_paq_act.PK_paquete = cont_paq.PK_paquete inner join t_cont_actividad cont_act
		ON 
			cont_paq_act.PK_actividad = cont_act.PK_actividad
		WHERE 
			cont_paq_act.PK_proyecto = @paramPK_proyecto AND
			cont_paq_act.PK_paquete = @paramPK_paquete AND
			cont_paq_act.activo = 1
END  
 GO

  IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_actividadAsignadaSelectOne]'))
DROP PROCEDURE [dbo].[PA_cont_actividadAsignadaSelectOne]
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
CREATE PROCEDURE  PA_cont_actividadAsignadaSelectOne
  @paramPK_proyecto int,
  @paramPK_paquete int,
  @paramPK_actividad int
AS 
 BEGIN 
		SELECT 
			 cont_asig_act.PK_actividad,
			 cont_asig_act.PK_paquete,
			 cont_asig_act.PK_componente,	
			 cont_asig_act.PK_entregable,	 	
			 cont_asig_act.PK_proyecto,
			 cont_asig_act.PK_usuario,
			 cont_asig_act.FK_estado,
			 cont_asig_act.descripcion,
			 cont_asig_act.fechaInicio,
			 cont_asig_act.fechaFin,
			 cont_asig_act.horasAsignadas,
			 ISNULL((SELECT SUM(horas) FROM t_cont_registro_actividad WHERE PK_proyecto = @paramPK_proyecto AND PK_paquete = @paramPK_paquete AND PK_actividad = @paramPK_actividad),0) horasReales,
			 cont_act.nombre nombreActividad,       
			 cont_paq.nombre nombrePaquete,
			 admi_usu.nombre nombreUsuario
        FROM 
			t_cont_asignacion_actividad cont_asig_act inner join t_cont_proyecto cont_proy 
		ON 
			cont_asig_act.PK_proyecto = cont_proy.PK_proyecto inner join t_cont_entregable cont_ent
		ON 
			cont_asig_act.PK_entregable = cont_ent.PK_entregable inner join t_cont_componente cont_comp
		ON 
			cont_asig_act.PK_componente = cont_comp.PK_componente inner join t_cont_paquete cont_paq
		ON 
			cont_asig_act.PK_paquete = cont_paq.PK_paquete inner join t_cont_actividad cont_act
		ON 
			cont_asig_act.PK_actividad = cont_act.PK_actividad inner join t_admi_usuario admi_usu
		ON 
			cont_asig_act.PK_usuario = admi_usu.PK_usuario inner join t_cont_estado cont_est
		ON 
			cont_asig_act.FK_estado = cont_est.PK_estado
		WHERE 
			cont_asig_act.PK_proyecto = @paramPK_proyecto AND
			cont_asig_act.PK_paquete = @paramPK_paquete AND
			cont_asig_act.PK_actividad = @paramPK_actividad AND
			cont_asig_act.activo = 1
END  
 GO


 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_operacionSelectOne]'))
DROP PROCEDURE [dbo].[PA_cont_operacionSelectOne]
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
CREATE PROCEDURE  PA_cont_operacionSelectOne 
  @paramPK_codigo NVARCHAR(50),
  @paramPK_usuario NVARCHAR(30)
AS 
 BEGIN 

    SELECT 
			o.PK_codigo,
			tipo,
			descripcion,
			ISNULL(FK_proyecto,-1) as FK_proyecto,
			ISNULL(ao.activo,0) as activo
		 FROM
			t_cont_operacion o
		 LEFT OUTER JOIN
			t_cont_asignacion_operacion ao
		 ON
			o.PK_codigo = ao.PK_codigo AND
			ao.PK_usuario = @paramPK_usuario
         WHERE 
			   o.PK_codigo = @paramPK_codigo
END  
 GO 


  IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_operacionRegistroSelectOne]'))
DROP PROCEDURE [dbo].[PA_cont_operacionRegistroSelectOne]
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
CREATE PROCEDURE  PA_cont_operacionRegistroSelectOne 
  @paramPK_registro   numeric(10,2),
  @paramPK_codigo	nvarchar(50),
  @paramUsuario		nvarchar(30)
AS 
 BEGIN 

         SELECT 
			PK_registro,
			PK_codigo,
			PK_usuario,
			fecha,
			horas,
			comentario
		 FROM
			t_cont_registro_operacion
         WHERE 
			   PK_codigo = @paramPK_codigo AND
			   PK_registro = @paramPK_registro AND
			   PK_usuario = @paramUsuario
END  
GO 

  IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_actividadRegistroSelectOne]'))
DROP PROCEDURE [dbo].[PA_cont_actividadRegistroSelectOne]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Esteban Ramírez G.
-- Fecha Creación:	19-04-2011
-- Fecha Actualización:	19-04-2011
-- Descripción: 
-- =============================================
CREATE PROCEDURE  PA_cont_actividadRegistroSelectOne 
@paramRegistro	INT,
  @paramActividad   INT,
  @paramPaquete		INT,
  @paramComponente  INT,
  @paramEntregable	INT,
  @paramProyecto	INT,
  @paramUsuario		nvarchar(50)
AS 
 BEGIN 

         SELECT 
			PK_registro,
			PK_actividad,
			PK_paquete ,
			PK_componente,
			PK_entregable,
			PK_proyecto,
			PK_usuario ,
			fecha,
			horas,
			comentario
		 FROM
			t_cont_registro_actividad
         WHERE 
			PK_registro = @paramRegistro AND
			PK_actividad = @paramActividad AND
			PK_paquete = @paramPaquete AND
			PK_componente = @paramComponente AND
			PK_entregable = @paramEntregable AND
			PK_proyecto =  @paramProyecto AND
			PK_usuario = @paramUsuario
END  
GO 




