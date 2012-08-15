USE CSLA
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_estadoSelect]'))
DROP PROCEDURE [dbo].[PA_cont_estadoSelect]
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
CREATE PROCEDURE  PA_cont_estadoSelect 
AS
 BEGIN 
		SELECT 
			PK_estado, 
			descripcion 
		FROM t_cont_estado
END  
 GO 
 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_proyectoSelect]'))
DROP PROCEDURE [dbo].[PA_cont_proyectoSelect]
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
CREATE PROCEDURE  PA_cont_proyectoSelect
AS 
 BEGIN 
		SELECT 
         tcp.PK_proyecto,
         tcp.FK_estado,
         tcp.nombre,
         tcp.descripcion,
         tcp.objetivo,
         tcp.meta,
         tcp.fechaInicio,
         tcp.fechaFin,
         tcp.horasAsignadas,
         ISNULL((SELECT SUM(horas) FROM t_cont_registro_actividad tcra WHERE tcra.PK_proyecto = tcp.PK_proyecto),0) horasReales,
         tce.descripcion nombreEstado
		FROM t_cont_proyecto tcp INNER JOIN t_cont_estado tce ON tcp.FK_estado = tce.PK_estado
END  
 GO 
  
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_entregableSelect]'))
DROP PROCEDURE [dbo].[PA_cont_entregableSelect]
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
CREATE PROCEDURE  PA_cont_entregableSelect 
AS 
 BEGIN 
 
		SELECT  
         PK_entregable,
         codigo,
         nombre,
         descripcion
        FROM t_cont_entregable
END  
 GO 

 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_componenteSelect]'))
DROP PROCEDURE [dbo].[PA_cont_componenteSelect]
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
CREATE PROCEDURE  PA_cont_componenteSelect 
AS
 BEGIN 
 
		SELECT 
         PK_componente,
         codigo,
         nombre,
         descripcion
        FROM t_cont_componente
END  
 GO 
 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_paqueteSelect]'))
DROP PROCEDURE [dbo].[PA_cont_paqueteSelect]
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
CREATE PROCEDURE  PA_cont_paqueteSelect
AS 
 BEGIN 
 
		SELECT 
         PK_paquete,
         codigo,
         nombre,
         descripcion
        FROM t_cont_paquete
END  
 GO 
 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_actividadSelect]'))
DROP PROCEDURE [dbo].[PA_cont_actividadSelect]
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
CREATE PROCEDURE  PA_cont_actividadSelect 
AS 
 BEGIN 
 
		SELECT
         PK_actividad,
         codigo,
         nombre,
         descripcion
        FROM t_cont_actividad
END  
 GO 

 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_departamentoProyectoSelect]'))
DROP PROCEDURE [dbo].[PA_cont_departamentoProyectoSelect]
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
CREATE PROCEDURE  PA_cont_departamentoProyectoSelect
  @paramPK_proyecto int
AS 
 BEGIN 
		SELECT 
			 cont_dep_proy.PK_proyecto,
			 cont_dep_proy.PK_departamento,
			 admi_dep.nombre         
        FROM 
			t_cont_departamento_proyecto cont_dep_proy inner join t_cont_proyecto cont_proy 
		ON 
			cont_dep_proy.PK_proyecto = cont_proy.PK_proyecto inner join t_admi_departamento admi_dep
		ON 
			cont_dep_proy.PK_departamento = admi_dep.PK_departamento
		WHERE 
			cont_dep_proy.PK_proyecto = @paramPK_proyecto AND
			cont_dep_proy.activo = 1
END  
 GO 

 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_proyectoEntregableSelect]'))
DROP PROCEDURE [dbo].[PA_cont_proyectoEntregableSelect]
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
CREATE PROCEDURE  PA_cont_proyectoEntregableSelect
  @paramPK_proyecto int
AS 
 BEGIN 
		SELECT 
			 cont_proy_ent.PK_proyecto,
			 cont_proy_ent.PK_entregable,
			 cont_ent.nombre 
        
        FROM 
			t_cont_proyecto_entregable cont_proy_ent inner join t_cont_proyecto cont_proy 
		ON 
			cont_proy_ent.PK_proyecto = cont_proy.PK_proyecto inner join t_cont_entregable cont_ent
		ON 
			cont_proy_ent.PK_entregable = cont_ent.PK_entregable
		WHERE 
			cont_proy_ent.PK_proyecto = @paramPK_proyecto AND
			cont_proy_ent.activo = 1
END  
 GO 

  IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_entregableComponenteSelect]'))
DROP PROCEDURE [dbo].[PA_cont_entregableComponenteSelect]
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
CREATE PROCEDURE  PA_cont_entregableComponenteSelect
  @paramPK_proyecto int,
  @paramPK_entregable int
AS 
 BEGIN 
		SELECT 
			 cont_ent_comp.PK_proyecto,
			 cont_ent_comp.PK_entregable,
			 cont_ent_comp.PK_componente,
			 cont_comp.nombre        
        FROM 
			t_cont_entregable_componente cont_ent_comp inner join t_cont_proyecto cont_proy 
		ON 
			cont_ent_comp.PK_proyecto = cont_proy.PK_proyecto inner join t_cont_entregable cont_ent
		ON 
			cont_ent_comp.PK_entregable = cont_ent.PK_entregable inner join t_cont_componente cont_comp
		ON 
			cont_ent_comp.PK_componente = cont_comp.PK_componente
		WHERE 
			cont_ent_comp.PK_proyecto = @paramPK_proyecto AND
			cont_ent_comp.PK_entregable = @paramPK_entregable AND
			cont_ent_comp.activo = 1
END  
 GO 

  IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_entregableComponenteSelectAll]'))
DROP PROCEDURE [dbo].[PA_cont_entregableComponenteSelectAll]
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
CREATE PROCEDURE  PA_cont_entregableComponenteSelectAll
  @paramPK_proyecto int
AS 
 BEGIN 
		SELECT 
			 cont_ent_comp.PK_proyecto,
			 cont_ent_comp.PK_entregable,
			 cont_ent_comp.PK_componente,
			 cont_comp.nombre        
        FROM 
			t_cont_entregable_componente cont_ent_comp inner join t_cont_proyecto cont_proy 
		ON 
			cont_ent_comp.PK_proyecto = cont_proy.PK_proyecto inner join t_cont_entregable cont_ent
		ON 
			cont_ent_comp.PK_entregable = cont_ent.PK_entregable inner join t_cont_componente cont_comp
		ON 
			cont_ent_comp.PK_componente = cont_comp.PK_componente
		WHERE 
			cont_ent_comp.PK_proyecto = @paramPK_proyecto AND
			cont_ent_comp.activo = 1
END  
 GO 

  IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_componentePaqueteSelect]'))
DROP PROCEDURE [dbo].[PA_cont_componentePaqueteSelect]
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
CREATE PROCEDURE  PA_cont_componentePaqueteSelect
  @paramPK_proyecto int,
  @paramPK_componente int
AS 
 BEGIN 
		SELECT 
			 cont_comp_paq.PK_proyecto,
			 cont_comp_paq.PK_entregable,
			 cont_comp_paq.PK_componente,
			 cont_comp_paq.PK_paquete,
			 cont_paq.nombre        
        FROM 
			t_cont_componente_paquete cont_comp_paq inner join t_cont_proyecto cont_proy 
		ON 
			cont_comp_paq.PK_proyecto = cont_proy.PK_proyecto inner join t_cont_entregable cont_ent
		ON 
			cont_comp_paq.PK_entregable = cont_ent.PK_entregable inner join t_cont_componente cont_comp
		ON 
			cont_comp_paq.PK_componente = cont_comp.PK_componente inner join t_cont_paquete cont_paq
		ON 
			cont_comp_paq.PK_paquete = cont_paq.PK_paquete
		WHERE 
			cont_comp_paq.PK_proyecto = @paramPK_proyecto AND
			cont_comp_paq.PK_componente = @paramPK_componente AND
			cont_comp_paq.activo = 1
END  
 GO 

  IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_componentePaqueteSelectAll]'))
DROP PROCEDURE [dbo].[PA_cont_componentePaqueteSelectAll]
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
CREATE PROCEDURE  PA_cont_componentePaqueteSelectAll
  @paramPK_proyecto int
AS 
 BEGIN 
		SELECT 
			 cont_comp_paq.PK_proyecto,
			 cont_comp_paq.PK_entregable,
			 cont_comp_paq.PK_componente,
			 cont_comp_paq.PK_paquete,
			 cont_paq.nombre        
        FROM 
			t_cont_componente_paquete cont_comp_paq inner join t_cont_proyecto cont_proy 
		ON 
			cont_comp_paq.PK_proyecto = cont_proy.PK_proyecto inner join t_cont_entregable cont_ent
		ON 
			cont_comp_paq.PK_entregable = cont_ent.PK_entregable inner join t_cont_componente cont_comp
		ON 
			cont_comp_paq.PK_componente = cont_comp.PK_componente inner join t_cont_paquete cont_paq
		ON 
			cont_comp_paq.PK_paquete = cont_paq.PK_paquete
		WHERE 
			cont_comp_paq.PK_proyecto = @paramPK_proyecto AND
			cont_comp_paq.activo = 1
END  
 GO 


  IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_paqueteActividadSelect]'))
DROP PROCEDURE [dbo].[PA_cont_paqueteActividadSelect]
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
CREATE PROCEDURE  PA_cont_paqueteActividadSelect
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
			 cont_act.nombre        
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


  IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_paqueteActividadSelectAll]'))
DROP PROCEDURE [dbo].[PA_cont_paqueteActividadSelectAll]
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
CREATE PROCEDURE  PA_cont_paqueteActividadSelectAll
  @paramPK_proyecto int
AS 
 BEGIN 
		SELECT 
			 cont_paq_act.PK_proyecto,
			 cont_paq_act.PK_entregable,
			 cont_paq_act.PK_componente,
			 cont_paq_act.PK_paquete,
			 cont_paq_act.PK_actividad,
			 cont_act.nombre        
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
			cont_paq_act.activo = 1
END  
 GO 

 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_operacionSelectAll]'))
DROP PROCEDURE [dbo].[PA_cont_operacionSelectAll]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Esteban Ramírez González
-- Fecha Creación:	11-04-2012
-- Fecha Actulización:	11-04-2012
-- Descripción: 
-- =============================================
CREATE PROCEDURE  PA_cont_operacionSelectAll
  @paramUsuario	  NVARCHAR(30),
  @paramTodos	  INT
AS 
 BEGIN 

	IF @paramTodos = 0
	BEGIN
		SELECT 
			o.PK_codigo,
			o.tipo,
			o.descripcion,
			ao.activo
		FROM
			t_cont_operacion o
		INNER JOIN
			t_cont_asignacion_operacion ao
		ON
			o.PK_codigo = ao.PK_codigo AND
			ao.PK_usuario = @paramUsuario AND
			ao.borrado = 0
	END
	ELSE
		SELECT 
			o.PK_codigo,
			o.tipo,
			o.descripcion,
			ISNULL(ao.activo,0) activo
		FROM
			t_cont_operacion o
		LEFT OUTER JOIN
			t_cont_asignacion_operacion ao
		ON
			o.PK_codigo = ao.PK_codigo AND
			ao.PK_usuario = @paramUsuario AND
			ao.borrado = 0
		
END  
 GO 


 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_proyectoSelectUsuario]'))
DROP PROCEDURE [dbo].[PA_cont_proyectoSelectUsuario]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Esteban Ramírez González
-- Fecha Creación:	12-04-2012
-- Fecha Actulización:	12-04-2012
-- Descripción: Se utiliza para seleccionar en cuales
--				se encuentra el usuario asociado
--				en donde estos esten en estado
--				iniciado.
-- =============================================
CREATE PROCEDURE  PA_cont_proyectoSelectUsuario
	 @paramUsuario	  NVARCHAR(30)
AS 
 BEGIN 
		SELECT DISTINCT
			 pro.PK_proyecto,
			 pro.FK_estado,
			 pro.nombre,
			 pro.descripcion,
			 pro.objetivo,
			 pro.meta,
			 pro.fechaInicio,
			 pro.fechaFin,
			 pro.horasAsignadas,
			 ISNULL((SELECT SUM(horas) FROM t_cont_registro_actividad tcra WHERE tcra.PK_proyecto = pro.PK_proyecto),0) horasReales
		 FROM 
			t_cont_proyecto pro
		 INNER JOIN
			t_cont_asignacion_actividad acs
		  ON
		  pro.PK_proyecto = acs.PK_proyecto AND
		  acs.PK_usuario = @paramUsuario
		WHERE
			pro.FK_estado = 1
END  
GO 


 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_paquetesProyectoSelectAll]'))
DROP PROCEDURE [dbo].[PA_cont_paquetesProyectoSelectAll]
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
CREATE PROCEDURE  PA_cont_paquetesProyectoSelectAll
  @paramPK_proyecto int
AS 
 BEGIN 
		
		SELECT DISTINCT
			 cont_paq_act.PK_paquete,
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
			cont_paq_act.PK_paquete = cont_paq.PK_paquete
		WHERE 
			cont_paq_act.PK_proyecto = @paramPK_proyecto AND
			cont_paq_act.activo = 1
END  
 GO

 
 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_operacionSelectUsuario]'))
DROP PROCEDURE [dbo].[PA_cont_operacionSelectUsuario]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Esteban Ramírez González
-- Fecha Creación:	12-04-2012
-- Fecha Actulización:	12-04-2012
-- Descripción: Se utiliza para seleccionar en cuales
--				las operaciones asociadas 
--				a un usuario. Sólo aquellas
--				que estan activas
-- =============================================
CREATE PROCEDURE  PA_cont_operacionSelectUsuario
	 @paramUsuario	  NVARCHAR(30),
	 @paramTipo		  NVARCHAR(1)
AS 
 BEGIN 
	SELECT 
		op.PK_codigo,
		op.descripcion
	FROM 
		t_cont_asignacion_operacion aop
	INNER JOIN
		t_cont_operacion op
	ON
		aop.PK_codigo = op.PK_codigo AND
		aop.PK_usuario = @paramUsuario
	WHERE 
		op.FK_proyecto IS NULL AND
		op.tipo = @paramTipo AND
		aop.activo = 1 AND
		aop.borrado= 0
	ORDER BY op.descripcion asc
END  
GO 

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_registroOperacionSelectUsuario]'))
DROP PROCEDURE [dbo].[PA_cont_registroOperacionSelectUsuario]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Esteban Ramírez González
-- Fecha Creación:	12-04-2012
-- Fecha Actulización:	12-04-2012
-- Descripción: Se utiliza para seleccionar en cuales
--				las operaciones asociadas 
--				a un usuario.
-- =============================================
CREATE PROCEDURE  PA_cont_registroOperacionSelectUsuario
	 @paramUsuario	  NVARCHAR(30),
	 @paramTipo		  NVARCHAR(1),
	 @paramFechaInicio	DATETIME,
	 @paramFechaFin		DATETIME
AS 
 BEGIN 
	SELECT
	op.PK_codigo,
	op.tipo,
	op.descripcion,
	ro.PK_registro,
	ro.PK_usuario,
	ro.fecha,
	ro.horas,
	ro.comentario
FROM 
	t_cont_registro_operacion ro
RIGHT OUTER JOIN
	t_cont_operacion op
ON
	ro.PK_codigo = op.PK_codigo AND
	ro.PK_usuario = @paramUsuario
WHERE
	op.FK_proyecto IS NULL AND
	op.tipo = @paramTipo	 AND
	ro.fecha between @paramFechaInicio AND @paramFechaFin
ORDER BY op.descripcion asc

END  
GO 



IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_registroActividadSelectUsuario]'))
DROP PROCEDURE [dbo].[PA_cont_registroActividadSelectUsuario]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Esteban Ramírez González
-- Fecha Creación:	17-04-2012
-- Fecha Actulización:	17-04-2012
-- Descripción: Se utiliza para seleccionar en cuales
--				las actividades registradas por el 
--				usuario.
-- =============================================
CREATE PROCEDURE  PA_cont_registroActividadSelectUsuario
	 @paramProyecto   INT,
	 @paramUsuario	  NVARCHAR(30),
	 @paramFechaInicio	DATETIME,
	 @paramFechaFin		DATETIME
AS 
 BEGIN 
 
 SELECT * FROM (
	SELECT
		ra.PK_registro,
		ra.PK_actividad PK_codigo,
		ra.PK_paquete,
		ra.PK_componente,
		ra.PK_entregable,
		ra.PK_proyecto,
		ra.PK_usuario,
		ra.fecha,
		ra.horas,
		act.descripcion descripcion,
		pa.descripcion descripcion_paquete
	FROM 
		t_cont_registro_actividad ra
	INNER JOIN
		t_cont_actividad act
	ON
		ra.PK_actividad = act.PK_actividad AND
		ra.PK_usuario = @paramUsuario AND
		ra.PK_proyecto = @paramProyecto
	INNER JOIN
		t_cont_paquete pa
	ON
		ra.PK_paquete = pa.PK_paquete 
	WHERE
		ra.fecha between @paramFechaInicio AND @paramFechaFin	
	UNION
	SELECT
		ro.PK_registro,
		op.PK_codigo PK_codigo,
		-1 PK_paquete,
		-1 PK_componente,
		-1 PK_entregable,
		op.FK_proyecto PK_proyecto ,
		@paramUsuario PK_usuario,
		ro.fecha,
		ro.horas,
		op.descripcion,
		'' descripcion_paquete
	FROM 
		t_cont_registro_operacion ro
	RIGHT OUTER JOIN
		t_cont_operacion op
	ON
		ro.PK_codigo = op.PK_codigo AND
		ro.PK_usuario = @paramUsuario 
	WHERE
		op.FK_proyecto = @paramProyecto AND
		ro.fecha between @paramFechaInicio AND @paramFechaFin	
	) t
	ORDER BY descripcion asc
	
END 
GO 

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_actividadSelectUsuario]'))
DROP PROCEDURE [dbo].[PA_cont_actividadSelectUsuario]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Esteban Ramírez González
-- Fecha Creación:	12-04-2012
-- Fecha Actulización:	12-04-2012
-- Descripción: Se utiliza para seleccionar en cuales
--				las operaciones asociadas 
--				a un usuario.
-- =============================================
CREATE PROCEDURE  PA_cont_actividadSelectUsuario
	 @paramUsuario	  NVARCHAR(30),
	 @paramProyecto	  INT	  
AS 
 BEGIN 
 SELECT * FROM
 (
	SELECT 
		aact.PK_actividad PK_codigo,
		aact.PK_paquete,
		aact.PK_componente,
		aact.PK_entregable,
		aact.PK_proyecto,
		aact.PK_usuario,
		act.descripcion descripcion,
		pa.descripcion descripcion_paquete
	FROM 
		t_cont_asignacion_actividad aact
	INNER JOIN
		t_cont_actividad act
	ON
		aact.PK_actividad = act.PK_actividad AND
		aact.PK_proyecto = @paramProyecto AND
		aact.PK_usuario = 	@paramUsuario
	INNER JOIN
		t_cont_paquete pa
	ON
		aact.PK_paquete = pa.PK_paquete
	UNION
	SELECT 
		op.PK_codigo PK_codigo,
		-1	PK_paquete,
		-1  PK_componente,
		-1  PK_entregable,
		op.FK_proyecto PK_proyecto,
		@paramUsuario PK_usuario,
		op.descripcion,
		'' descripcion_paquete
	FROM
		t_cont_operacion op
	INNER JOIN
		t_cont_asignacion_operacion ap
	ON
		op.PK_codigo = ap.PK_codigo AND
		ap.PK_usuario = @paramUsuario AND
		op.FK_proyecto = @paramProyecto AND
		ap.borrado = 0
	) t
	ORDER BY descripcion ASC
	
END  
GO 

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_estd_inversionTiempos]'))
DROP PROCEDURE [dbo].[PA_estd_inversionTiempos]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Cristian Arce Jiménez.
-- Fecha Creación:	01-06-2012
-- Fecha Actualización:	01-06-2012
-- Descripción: 
-- =============================================
CREATE PROCEDURE  PA_estd_inversionTiempos
  @paramProyecto	INT,  
  @paramFechaInicio	datetime,
  @paramFechaFin	datetime,
  @paramUsuario varchar(50)
AS 
 BEGIN 
	
	IF (@paramUsuario = '')
	BEGIN
	Select 'Actividad' as tipo, COUNT(PK_registro)AS cantidad 
			From t_cont_registro_actividad tcra 
			Where tcra.PK_proyecto = @paramProyecto AND
				  tcra.fecha BETWEEN @paramFechaInicio AND @paramFechaFin
	Union all
	Select tipo = CASE tco.tipo 
					WHEN 'I' THEN 'Imprevisto'
					WHEN 'O' THEN 'Operacion' END, 
			   COUNT(tco.tipo) AS cantidad 
			From t_cont_registro_operacion tcro INNER JOIN t_cont_operacion tco
			ON tcro.PK_codigo = tco.PK_codigo, t_cont_proyecto tcp
			WHERE tcro.fecha between tcp.fechaInicio AND tcp.fechaFin
			AND tcp.PK_proyecto = @paramProyecto 
		Group by tco.tipo
	END
	ELSE
	BEGIN
	Select 'Actividad' as tipo, COUNT(PK_registro)AS cantidad 
			From t_cont_registro_actividad tcra 
			Where tcra.PK_proyecto = @paramProyecto and tcra.pk_usuario = @paramUsuario AND
			      tcra.fecha BETWEEN @paramFechaInicio AND @paramFechaFin
	Union all
	Select tipo = CASE tco.tipo 
					WHEN 'I' THEN 'Imprevisto'
					WHEN 'O' THEN 'Operacion' END, 
			   COUNT(tco.tipo) AS cantidad 
			From t_cont_registro_operacion tcro INNER JOIN t_cont_operacion tco
			ON tcro.PK_codigo = tco.PK_codigo, t_cont_proyecto tcp
			WHERE tcro.fecha between tcp.fechaInicio AND tcp.fechaFin
			AND tcp.PK_proyecto = @paramProyecto 
            AND tcro.pk_usuario = @paramUsuario
		Group by tco.tipo
	END
END  
GO 

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_estd_actividadesTopProyecto]'))
DROP PROCEDURE [dbo].[PA_estd_actividadesTopProyecto]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Cristian Arce Jiménez.
-- Fecha Creación:	01-06-2012
-- Fecha Actualización:	01-06-2012
-- Descripción: 
-- =============================================
CREATE PROCEDURE  [dbo].[PA_estd_actividadesTopProyecto]
  @paramProyecto	INT,
  @paramFechaInicio	datetime,
  @paramFechaFin	datetime,
  @paramUsuario     varchar(50)
AS 
 BEGIN 
	IF(@paramUsuario = '')
	BEGIN
		Select TOP(10)tca.nombre AS actividad, SUM(horas) AS cantidadHoras 
				From t_cont_registro_actividad tcra INNER JOIN t_cont_proyecto tcp
					ON 
					   tcra.PK_proyecto = tcp.PK_proyecto INNER JOIN t_cont_actividad tca
					ON 
					   tcra.PK_actividad = tca.PK_actividad INNER JOIN t_cont_asignacion_actividad tcaa 
					ON 
					   tcra.PK_actividad = tcaa.PK_actividad AND
					   tcra.PK_paquete = tcaa.PK_paquete AND
					   tcra.PK_componente = tcaa.PK_componente AND
					   tcra.PK_entregable = tcaa.PK_entregable AND
					   tcra.PK_proyecto = tcaa.PK_proyecto AND
					   tcra.PK_usuario = tcaa.PK_usuario 
				Where 
					tcra.PK_proyecto = @paramProyecto AND
					tcra.fecha BETWEEN @paramFechaInicio AND @paramFechaFin
		GROUP BY tca.nombre
		ORDER BY cantidadHoras DESC
	END
	ELSE
	BEGIN
		Select TOP(10)tca.nombre AS actividad, SUM(horas) AS cantidadHoras 
				From t_cont_registro_actividad tcra INNER JOIN t_cont_proyecto tcp
					ON 
					   tcra.PK_proyecto = tcp.PK_proyecto INNER JOIN t_cont_actividad tca
					ON 
					   tcra.PK_actividad = tca.PK_actividad INNER JOIN t_cont_asignacion_actividad tcaa 
					ON 
					   tcra.PK_actividad = tcaa.PK_actividad AND
					   tcra.PK_paquete = tcaa.PK_paquete AND
					   tcra.PK_componente = tcaa.PK_componente AND
					   tcra.PK_entregable = tcaa.PK_entregable AND
					   tcra.PK_proyecto = tcaa.PK_proyecto AND
					   tcra.PK_usuario = tcaa.PK_usuario 
				Where 
					tcra.PK_proyecto = @paramProyecto AND
					tcra.fecha BETWEEN @paramFechaInicio AND @paramFechaFin AND
					tcra.PK_usuario = @paramUsuario
		GROUP BY tca.nombre
		ORDER BY cantidadHoras DESC
	END
END  
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_estd_comparacionHorasActividad]'))
DROP PROCEDURE [dbo].[PA_estd_comparacionHorasActividad]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Cristian Arce Jiménez.
-- Fecha Creación:	01-06-2012
-- Fecha Actualización:	01-06-2012
-- Descripción: 
-- =============================================
CREATE PROCEDURE  [dbo].[PA_estd_comparacionHorasActividad]
  @paramProyecto	INT,
  @paramPaquete		INT,
  @paramFechaInicio	datetime,
  @paramFechaFin	datetime,
  @paramUsuario     varchar(50)
AS 
 BEGIN 
	IF(@paramUsuario = '')
		BEGIN
			Select tca.nombre AS nombreActividad,
			   tcaa.horasAsignadas AS horasAsignadas,
			   SUM(tcra.horas) AS horasReales
				From t_cont_registro_actividad tcra INNER JOIN t_cont_proyecto tcp
					ON 
					   tcra.PK_proyecto = tcp.PK_proyecto INNER JOIN t_cont_actividad tca
					ON 
					   tcra.PK_actividad = tca.PK_actividad INNER JOIN t_cont_asignacion_actividad tcaa 
					ON 
					   tcra.PK_actividad = tcaa.PK_actividad AND
					   tcra.PK_paquete = tcaa.PK_paquete AND
					   tcra.PK_componente = tcaa.PK_componente AND
					   tcra.PK_entregable = tcaa.PK_entregable AND
					   tcra.PK_proyecto = tcaa.PK_proyecto AND
					   tcra.PK_usuario = tcaa.PK_usuario 
				Where 
					tcra.PK_proyecto = @paramProyecto  AND
					tcra.PK_paquete = @paramPaquete AND
					tcra.fecha BETWEEN @paramFechaInicio AND @paramFechaFin
				GROUP BY tca.nombre, tcaa.horasAsignadas
		END
	ELSE
		BEGIN
			Select tca.nombre AS nombreActividad,
			   tcaa.horasAsignadas AS horasAsignadas,
			   SUM(tcra.horas) AS horasReales
				From t_cont_registro_actividad tcra INNER JOIN t_cont_proyecto tcp
					ON 
					   tcra.PK_proyecto = tcp.PK_proyecto INNER JOIN t_cont_actividad tca
					ON 
					   tcra.PK_actividad = tca.PK_actividad INNER JOIN t_cont_asignacion_actividad tcaa 
					ON 
					   tcra.PK_actividad = tcaa.PK_actividad AND
					   tcra.PK_paquete = tcaa.PK_paquete AND
					   tcra.PK_componente = tcaa.PK_componente AND
					   tcra.PK_entregable = tcaa.PK_entregable AND
					   tcra.PK_proyecto = tcaa.PK_proyecto AND
					   tcra.PK_usuario = tcaa.PK_usuario 
				Where 
					tcra.PK_proyecto = @paramProyecto AND
					tcra.PK_paquete = @paramPaquete AND
					tcra.fecha BETWEEN @paramFechaInicio AND @paramFechaFin AND 
					tcra.PK_usuario = @paramUsuario 
				GROUP BY tca.nombre, tcaa.horasAsignadas
		END
END 
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_estd_paquetesAsignacionActSelect]'))
DROP PROCEDURE [dbo].[PA_estd_paquetesAsignacionActSelect]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Cristian Arce Jiménez.
-- Fecha Creación:	01-06-2012
-- Fecha Actualización:	01-06-2012
-- Descripción: 
-- =============================================
CREATE PROCEDURE  [dbo].[PA_estd_paquetesAsignacionActSelect]
  @paramProyecto	INT
AS 
 BEGIN 

		Select distinct tcpa.PK_paquete,
			   tcpa.nombre AS nombrePaquete
			From t_cont_registro_actividad tcra INNER JOIN t_cont_proyecto tcp
				ON 
				   tcra.PK_proyecto = tcp.PK_proyecto INNER JOIN t_cont_paquete tcpa
				ON 
				   tcra.PK_paquete = tcpa.PK_paquete INNER JOIN t_cont_asignacion_actividad tcaa 
				ON 
				   tcra.PK_actividad = tcaa.PK_actividad AND
				   tcra.PK_paquete = tcaa.PK_paquete AND
				   tcra.PK_componente = tcaa.PK_componente AND
				   tcra.PK_entregable = tcaa.PK_entregable AND
				   tcra.PK_proyecto = tcaa.PK_proyecto AND
				   tcra.PK_usuario = tcaa.PK_usuario 
			Where 
				tcra.PK_proyecto = @paramProyecto 

END 
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_verificarActividadRetrasada]'))
DROP PROCEDURE [dbo].[PA_cont_verificarActividadRetrasada]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Esteban Ramírez González
-- Fecha Creación:	01-07-2012
-- Fecha Actualización:	01-07-2012
-- Descripción: 
-- =============================================
CREATE PROCEDURE  [dbo].[PA_cont_verificarActividadRetrasada]
  @paramProyecto	INT,
  @paramActividad	INT,
  @paramPaquete		INT,
  @paramComponente	INT,
  @paramEntregable	INT,
  @paramUsuario		NVARCHAR(30)
AS 
 BEGIN 

		SELECT 
			CASE
				WHEN t.fechaFin < t.fecha_ultimo
				THEN	
					1
				ELSE
					0
				END
				ATRASADA,
			CASE 
				WHEN t.horasAsignadas < t.horas_reales
				THEN	
					1
				ELSE
					0
				END
					SUPERA_ESTIMADO
		FROM 
		(
		SELECT 
			aa.PK_actividad,
			aa.PK_paquete,
			aa.PK_componente,
			aa.PK_entregable,
			aa.PK_proyecto,
			aa.PK_usuario,
			aa.fechaInicio,
			aa.fechaFin,
			aa.horasAsignadas,
			SUM(ra.horas) horas_reales,
			MAX(ra.fecha) fecha_ultimo
		FROM
			t_cont_asignacion_actividad aa
		INNER JOIN
			t_cont_registro_actividad ra
		ON
			aa.PK_actividad = ra.PK_actividad AND
			aa.PK_paquete = ra.PK_paquete AND
			aa.PK_componente = ra.PK_componente AND
			aa.PK_entregable = ra.PK_entregable AND
			aa.PK_proyecto = ra.PK_proyecto
		WHERE
			aa.PK_actividad = @paramActividad AND
			aa.PK_paquete = @paramPaquete AND
			aa.PK_componente = @paramComponente AND
			aa.PK_entregable = @paramEntregable AND
			aa.PK_proyecto = @paramProyecto AND
			aa.PK_usuario = @paramUsuario	
		GROUP BY
			aa.PK_actividad,
			aa.PK_paquete,
			aa.PK_componente,
			aa.PK_entregable,
			aa.PK_proyecto,
			aa.PK_usuario,
			aa.fechaInicio,
			aa.fechaFin,
			aa.horasAsignadas
		) t
END 
GO




IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_estd_consultaActRetrasadas]'))
DROP PROCEDURE [dbo].[PA_estd_consultaActRetrasadas]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Cristian Arce Ramírez
-- Fecha Creación:	01-07-2012
-- Fecha Actualización:	01-07-2012
-- Descripción: 
-- =============================================
CREATE PROCEDURE  [dbo].[PA_estd_consultaActRetrasadas]
  @paramProyecto	INT,
  @paramPaquete		INT,  
  @paramFechaInicio	datetime,
  @paramFechaFin	datetime,
  @paramUsuario		NVARCHAR(30)
AS 
 BEGIN 

		SELECT 
			CASE
				WHEN t.fechaFin < t.fecha_ultimo
				THEN	
					DATEDIFF(day,t.fechaFin,t.fecha_ultimo)
				ELSE
					0
				END
				diasRetraso,
			CASE 
				WHEN t.horasAsignadas < t.horas_reales
				THEN	
					t.horas_reales - t.horasAsignadas
				ELSE
					0
				END
				horasRetraso,
				t.nombreActividad
		FROM 
		(
		SELECT 
			aa.PK_actividad,
			aa.PK_paquete,
			aa.PK_componente,
			aa.PK_entregable,
			aa.PK_proyecto,
			aa.PK_usuario,
			aa.fechaInicio,
			aa.fechaFin,
			aa.horasAsignadas,
			SUM(ra.horas) horas_reales,
			MAX(ra.fecha) fecha_ultimo,
			a.nombre nombreActividad,
			aa.FK_estado
		FROM
			t_cont_asignacion_actividad aa
		INNER JOIN
			t_cont_registro_actividad ra
		ON
			aa.PK_actividad = ra.PK_actividad AND
			aa.PK_paquete = ra.PK_paquete AND
			aa.PK_componente = ra.PK_componente AND
			aa.PK_entregable = ra.PK_entregable AND
			aa.PK_proyecto = ra.PK_proyecto
		INNER JOIN 
			t_cont_actividad a
		ON
			aa.PK_actividad = a.PK_actividad
		WHERE
			aa.PK_paquete = @paramPaquete AND
			aa.PK_proyecto = @paramProyecto AND
      ra.fecha BETWEEN @paramFechaInicio AND @paramFechaFin AND
			aa.PK_usuario = @paramUsuario	AND
			aa.FK_estado = 1
		GROUP BY
			aa.PK_actividad,
			aa.PK_paquete,
			aa.PK_componente,
			aa.PK_entregable,
			aa.PK_proyecto,
			aa.PK_usuario,
			aa.fechaInicio,
			aa.fechaFin,
			aa.horasAsignadas,
			a.nombre,
			aa.FK_Estado
		) t

END
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_asignacionOperacion]'))
DROP PROCEDURE [dbo].[PA_cont_asignacionOperacion]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Generador
-- Fecha Creación:	02-09-2012
-- Fecha Actulización:	02-09-2012
-- Descripción: 
-- =============================================
CREATE PROCEDURE  PA_cont_asignacionOperacion 
	  @paramOperacion		int
AS
 BEGIN 
		SELECT 
			PK_codigo,
			PK_usuario
		FROM
			t_cont_asignacion_operacion
		WHERE
			PK_codigo = @paramOperacion AND
			borrado = 0
END  
 GO 





	

	

