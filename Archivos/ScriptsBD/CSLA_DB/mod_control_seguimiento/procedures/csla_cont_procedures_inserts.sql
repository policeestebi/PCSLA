USE CSLA
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_estadoInsert]'))
DROP PROCEDURE [dbo].[PA_cont_estadoInsert]
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
CREATE PROCEDURE  PA_cont_estadoInsert 
  @paramdescripcion varchar(30)  
AS
 BEGIN 
 SET NOCOUNT ON; 

         INSERT INTO t_cont_estado
        ( 
         descripcion
        ) 
        VALUES
        ( 
         @paramdescripcion
        ) 

END   
 GO 
 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_proyectoInsert]'))
DROP PROCEDURE [dbo].[PA_cont_proyectoInsert]
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
CREATE PROCEDURE  PA_cont_proyectoInsert
  @paramFK_estado int, 
  @paramnombre varchar(100) , 
  @paramdescripcion varchar(100) , 
  @paramobjetivo varchar(500) ,
  @parammeta varchar(500) , 
  @paramfechaInicio datetime, 
  @paramfechaFin datetime, 
  @paramhorasAsignadas decimal
AS 
 BEGIN 
 SET NOCOUNT ON; 

         INSERT INTO t_cont_proyecto
        (
         FK_estado,
         nombre,
         descripcion,
         objetivo,
         meta,
         fechaInicio,
         fechaFin,
         horasAsignadas
        ) 
        VALUES
        ( 
         @paramFK_estado,
         @paramnombre,
         @paramdescripcion,
         @paramobjetivo,
         @parammeta,
         @paramfechaInicio,
         @paramfechaFin,
         @paramhorasAsignadas
        ) 

END   
 GO 


IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_departamento_proyectoInsert]'))
DROP PROCEDURE [dbo].[PA_cont_departamento_proyectoInsert]
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
CREATE PROCEDURE  PA_cont_departamento_proyectoInsert 
  @paramPK_departamento int, 
  @paramPK_proyecto int 
AS 
 BEGIN 
 SET NOCOUNT ON; 

         INSERT INTO t_cont_departamento_proyecto
        ( 
         PK_departamento,
         PK_proyecto
        ) 
        VALUES
        ( 
         @paramPK_departamento,
         @paramPK_proyecto
        ) 

END   
 GO 

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_entregableInsert]'))
DROP PROCEDURE [dbo].[PA_cont_entregableInsert]
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
CREATE PROCEDURE  PA_cont_entregableInsert 
  @paramcodigo varchar(20) , 
  @paramnombre varchar(100) , 
  @paramdescripcion varchar(100)  
AS 
 BEGIN 
 SET NOCOUNT ON; 

         INSERT INTO t_cont_entregable
        ( 
         codigo,
         nombre,
         descripcion
        ) 
        VALUES
        ( 
         @paramcodigo,
         @paramnombre,
         @paramdescripcion
        ) 

END   
 GO 
 
 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_proyecto_entregableInsert]'))
DROP PROCEDURE [dbo].[PA_cont_proyecto_entregableInsert]
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
CREATE PROCEDURE  PA_cont_proyecto_entregableInsert
  @paramPK_entregable int,
  @paramPK_proyecto int 
AS 
 BEGIN 
 SET NOCOUNT ON;

         INSERT INTO t_cont_proyecto_entregable
        (
         PK_entregable,
         PK_proyecto
        ) 
        VALUES
        ( 
         @paramPK_entregable,
         @paramPK_proyecto
        ) 

END   
 GO 
 
 
 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_componenteInsert]'))
DROP PROCEDURE [dbo].[PA_cont_componenteInsert]
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
CREATE PROCEDURE  PA_cont_componenteInsert 
  @paramcodigo varchar(20) , 
  @paramnombre varchar(100) , 
  @paramdescripcion varchar(100)  
AS
 BEGIN 
 SET NOCOUNT ON; 

         INSERT INTO t_cont_componente
        ( 
         codigo,
         nombre,
         descripcion
        ) 
        VALUES
        ( 
         @paramcodigo,
         @paramnombre,
         @paramdescripcion
        ) 

END   
 GO 
 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_entregable_componenteInsert]'))
DROP PROCEDURE [dbo].[PA_cont_entregable_componenteInsert]
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
CREATE PROCEDURE  PA_cont_entregable_componenteInsert 
  @paramPK_componente int, 
  @paramPK_entregable int, 
  @paramPK_proyecto int 
 
AS 
 BEGIN 
 SET NOCOUNT ON; 

         INSERT INTO t_cont_entregable_componente
        (
         PK_componente,
         PK_entregable,
         PK_proyecto
        ) 
        VALUES
        ( 
         @paramPK_componente,
         @paramPK_entregable,
         @paramPK_proyecto
        ) 

END   
 GO 

 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_paqueteInsert]'))
DROP PROCEDURE [dbo].[PA_cont_paqueteInsert]
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
CREATE PROCEDURE  PA_cont_paqueteInsert
  @paramcodigo varchar(20) , 
  @paramnombre varchar(100) , 
  @paramdescripcion varchar(100)  
 
AS 
 BEGIN 
 SET NOCOUNT ON; 

         INSERT INTO t_cont_paquete
        ( 
         codigo,
         nombre,
         descripcion
        ) 
        VALUES
        ( 
         @paramcodigo,
         @paramnombre,
         @paramdescripcion
        ) 

END   
 GO 
 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_componente_paqueteInsert]'))
DROP PROCEDURE [dbo].[PA_cont_componente_paqueteInsert]
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
CREATE PROCEDURE  PA_cont_componente_paqueteInsert
  @paramPK_paquete int, 
  @paramPK_componente int, 
  @paramPK_entregable int, 
  @paramPK_proyecto int 
 
AS 
 BEGIN 
 SET NOCOUNT ON; 

         INSERT INTO t_cont_componente_paquete
        (
         PK_paquete,
         PK_componente,
         PK_entregable,
         PK_proyecto
        ) 
        VALUES
        ( 
         @paramPK_paquete,
         @paramPK_componente,
         @paramPK_entregable,
         @paramPK_proyecto
        ) 

END   
 GO 
 
 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_actividadInsert]'))
DROP PROCEDURE [dbo].[PA_cont_actividadInsert]
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
CREATE PROCEDURE  PA_cont_actividadInsert
  @paramcodigo varchar(20) , 
  @paramnombre varchar(100) , 
  @paramdescripcion varchar(100)  
 
AS 
 BEGIN 
 SET NOCOUNT ON; 

         INSERT INTO t_cont_actividad
        ( 
         codigo,
         nombre,
         descripcion
        ) 
        VALUES
        ( 
         @paramcodigo,
         @paramnombre,
         @paramdescripcion
        ) 

END   
 GO 


IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_paquete_actividadInsert]'))
DROP PROCEDURE [dbo].[PA_cont_paquete_actividadInsert]
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
CREATE PROCEDURE  PA_cont_paquete_actividadInsert
  @paramPK_actividad int,   
  @paramPK_paquete int, 
  @paramPK_componente int, 
  @paramPK_entregable int, 
  @paramPK_proyecto int 
 
AS 
 BEGIN 
 SET NOCOUNT ON; 

         INSERT INTO t_cont_paquete_actividad
        (
		 PK_actividad,
         PK_paquete,
         PK_componente,
         PK_entregable,
         PK_proyecto
        ) 
        VALUES
        ( 
		 @paramPK_actividad,
         @paramPK_paquete,
         @paramPK_componente,
         @paramPK_entregable,
         @paramPK_proyecto
        ) 

END   
 GO 
 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_asignacionActividadInsert]'))
DROP PROCEDURE [dbo].[PA_cont_asignacionActividadInsert]
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
CREATE PROCEDURE  PA_cont_asignacionActividadInsert 
  @paramPK_actividad int, 
  @paramPK_paquete int,
  @paramPK_componente int, 
  @paramPK_entregable int, 
  @paramPK_proyecto int, 
  @paramPK_usuario varchar(30) , 
  @paramFK_estado int, 
  @paramdescripcion varchar(100) , 
  @paramfechaInicio datetime, 
  @paramfechaFin datetime, 
  @paramhorasAsignadas numeric(10,2)
  
AS 
 BEGIN 
 SET NOCOUNT ON; 

         INSERT INTO t_cont_asignacion_actividad
        ( 
		 PK_actividad,
         PK_paquete,
         PK_componente,
         PK_entregable,
         PK_proyecto,
         PK_usuario,
         FK_estado,
         descripcion,
         fechaInicio,
         fechaFin,
         horasAsignadas
        ) 
        VALUES
        ( 
		 @paramPK_actividad, 
         @paramPK_paquete,
         @paramPK_componente,
         @paramPK_entregable,
         @paramPK_proyecto,
         @paramPK_usuario,
         @paramFK_estado,
         @paramdescripcion,
         @paramfechaInicio,
         @paramfechaFin,
         @paramhorasAsignadas
        ) 

END   
 GO 


IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_operacionInsert]'))
DROP PROCEDURE [dbo].[PA_cont_operacionInsert]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Esteban Ramírez González.
-- Fecha Creación:	11-04-2012
-- Fecha Actulización:	11-04-2011
-- Descripción: Procedimiento que inserta en la tabla
--				t_cont_operacion
-- =============================================
CREATE PROCEDURE  PA_cont_operacionInsert
  @paramTipo nvarchar(1),
  @paramDescripcion	nvarchar(100),
  @paramUsuario		nvarchar(50),
  @paramProyecto	int,
  @paramActivo		SMALLINT,
  @param_PK_codigo nvarchar(50) OUTPUT,
  @paramAsignacionMasiva int
AS 
 BEGIN 
 SET NOCOUNT ON; 
		
		DECLARE @codigo DECIMAL(38,0);
	
		SELECT @codigo = (ISNULL(MAX(CONVERT(decimal(38,0),PK_codigo )),0) + 1) FROM t_cont_operacion;
		
		SELECT @param_PK_codigo = CONVERT(NVARCHAR(50),@codigo);

        INSERT INTO t_cont_operacion
        (
		 PK_codigo,
		 tipo,
		 descripcion,
		 FK_proyecto
        ) 
        VALUES
        ( 
		 @codigo,
		 @paramTipo,
		 @paramDescripcion,
		 @paramProyecto
        ) 

		IF (@paramAsignacionMasiva = 1 AND @paramActivo = 1) OR @paramAsignacionMasiva = 0
        BEGIN
			INSERT INTO t_cont_asignacion_operacion
			(
			PK_codigo,
			PK_usuario,
			comentario,
			 activo
			)
			VALUES
			(
			@codigo,
			@paramUsuario,
			@paramDescripcion,
			@paramActivo
			)
		END

END   
 GO 


IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_obtenerUltimaOperacion]'))
DROP PROCEDURE [dbo].[PA_cont_obtenerUltimaOperacion]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Esteban Ramírez González.
-- Fecha Creación:	11-04-2012
-- Fecha Actulización:	11-04-2011
-- Descripción: Procedimiento la última operación insertada
-- =============================================
CREATE PROCEDURE  PA_cont_obtenerUltimaOperacion
AS 
 BEGIN 
 SET NOCOUNT ON; 
		
		SELECT CONVERT(NVARCHAR(50),MAX(CONVERT(decimal(38,0),PK_codigo ))) as codigo FROM t_cont_operacion;
END   
 GO 

 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_operacionRegistroInsert]'))
DROP PROCEDURE [dbo].[PA_cont_operacionRegistroInsert]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Esteban Ramírez González.
-- Fecha Creación:	11-04-2012
-- Fecha Actulización:	11-04-2011
-- Descripción: Procedimiento que inserta en la tabla
--				t_cont_registro_operacion
-- =============================================
CREATE PROCEDURE  PA_cont_operacionRegistroInsert
  @paramPK_codigo	nvarchar(50),
  @paramComentario	nvarchar(100),
  @paramUsuario		nvarchar(50),
  @paramFecha		DateTime,
  @paramHoras		NUMERIC(10,2)
AS 
 BEGIN 
 SET NOCOUNT ON; 
		
		DECLARE @registro numeric(10,2);

		SELECT @registro = (SELECT ISNULL(MAX(PK_REGISTRO),0) +1  FROM t_cont_registro_operacion);

        INSERT INTO t_cont_registro_operacion
        (
		 PK_registro,
		 PK_codigo,
		 PK_usuario,
		 fecha,
		 horas,
		 comentario
        ) 
        VALUES
        ( 
        @registro,
		@paramPK_codigo,
		@paramUsuario,
		@paramFecha,
		@paramHoras,
		@paramComentario
        ) 

END   
GO 

 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_actividadRegistroInsert]'))
DROP PROCEDURE [dbo].[PA_cont_actividadRegistroInsert]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Esteban Ramírez González.
-- Fecha Creación:	17-04-2012
-- Fecha Actulización:	1-04-2011
-- Descripción: Procedimiento que inserta en la tabla
--				t_cont_registro_actividad
-- =============================================
CREATE PROCEDURE  PA_cont_actividadRegistroInsert
  @paramActividad   INT,
  @paramPaquete		INT,
  @paramComponente  INT,
  @paramEntregable	INT,
  @paramProyecto	INT,
  @paramComentario	nvarchar(100),
  @paramUsuario		nvarchar(50),
  @paramFecha		DateTime,
  @paramHoras		NUMERIC(10,2)
AS 
 BEGIN 
 SET NOCOUNT ON; 
		
		DECLARE @registro numeric(10,2);

		SELECT @registro = (SELECT ISNULL(MAX(PK_REGISTRO),0) +1  FROM t_cont_registro_actividad);

        INSERT INTO t_cont_registro_actividad
        (
		PK_registro,
		PK_actividad,
		PK_paquete,
		PK_componente,
		PK_entregable,
		PK_proyecto,
		PK_usuario,
		fecha,
		comentario,
		horas
        ) 
        VALUES
        ( 
		  @registro,
		  @paramActividad   ,
		  @paramPaquete		,
		  @paramComponente  ,
		  @paramEntregable	,
		  @paramProyecto	,
		  @paramUsuario		,
		  @paramFecha		,
		  @paramComentario	,
		  @paramHoras		
        ) 

END   
GO 

  IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_proyectoCopiaInsert]'))
DROP PROCEDURE [dbo].[PA_cont_proyectoCopiaInsert]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Generador
-- Fecha Creación:	15-05-2012
-- Fecha Actulización:	15-05-2012
-- Descripción: 
-- =============================================
CREATE PROCEDURE  PA_cont_proyectoCopiaInsert
  @paramPK_proyectoNuevo int,
  @paramPK_proyectoOriginal int
AS 
 BEGIN 
		--Se inserta en cada una de las tablas, tomando como base el proyecto original, y asignando la PK de cada proyecto nuevo
		INSERT INTO t_cont_proyecto_entregable(PK_entregable,PK_proyecto) 
			   (SELECT PK_entregable,@paramPK_proyectoNuevo FROM t_cont_proyecto_entregable WHERE PK_proyecto = @paramPK_proyectoOriginal);

		INSERT INTO t_cont_entregable_componente(PK_componente,PK_entregable,PK_proyecto) 
			   (SELECT PK_componente,PK_entregable,@paramPK_proyectoNuevo FROM t_cont_entregable_componente WHERE PK_proyecto = @paramPK_proyectoOriginal);
		
		INSERT INTO t_cont_componente_paquete(PK_paquete,PK_componente,PK_entregable,PK_proyecto) 
			   (SELECT PK_paquete,PK_componente,PK_entregable,@paramPK_proyectoNuevo FROM t_cont_componente_paquete WHERE PK_proyecto = @paramPK_proyectoOriginal);

		INSERT INTO t_cont_paquete_actividad(PK_actividad,PK_paquete,PK_componente,PK_entregable,PK_proyecto) 
			   (SELECT PK_actividad,PK_paquete,PK_componente,PK_entregable,@paramPK_proyectoNuevo FROM t_cont_paquete_actividad WHERE PK_proyecto = @paramPK_proyectoOriginal);

 END
 GO


 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_cont_asignacionOperacionInsert]'))
DROP PROCEDURE [dbo].[PA_cont_asignacionOperacionInsert]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Generador
-- Fecha Creación:	03-09-2012
-- Fecha Actulización:	03-09-2012
-- Descripción: 
-- =============================================
CREATE PROCEDURE  PA_cont_asignacionOperacionInsert
	  @paramOperacion		int,
	  @paramUsuario			NVARCHAR(30),
	  @paramActivo			int
AS
BEGIN 
	
	IF NOT EXISTS( 
		SELECT * FROM t_cont_asignacion_operacion
		WHERE  PK_codigo = @paramOperacion AND PK_usuario = @paramUsuario
	)
	BEGIN
		INSERT INTO
			t_cont_asignacion_operacion
			(PK_codigo, PK_usuario,activo)
			VALUES
			(@paramOperacion,@paramUsuario,@paramActivo)
	END
	ELSE
		UPDATE
			t_cont_asignacion_operacion
		SET 
			borrado = 0,
			activo = 1
		WHERE
			PK_codigo = @paramOperacion AND PK_usuario = @paramUsuario
		
END  
GO 


