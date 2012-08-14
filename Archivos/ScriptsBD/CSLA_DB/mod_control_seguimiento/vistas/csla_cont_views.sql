USE CSLA
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_cont_reporteRegritroTiempos]'))
DROP VIEW [dbo].[v_cont_reporteRegritroTiempos]
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
CREATE VIEW v_cont_reporteRegritroTiempos
AS
SELECT 
	op.PK_codigo codigo,
	op.descripcion descripcion,
	op.tipo tipo,
	ro.PK_usuario usuario,
	SUM(ro.horas) total_horas
FROM
	t_cont_operacion op
INNER JOIN
	t_cont_registro_operacion ro
ON
	op.PK_codigo = ro.PK_codigo
WHERE
	op.FK_proyecto IS NULL
GROUP BY
	op.PK_codigo,op.descripcion,op.tipo,op.FK_proyecto,ro.PK_usuario 
	
UNION ALL

SELECT 
	pr.PK_proyecto codigo,
	pr.descripcion descripcion,
	'P' tipo,
	rp.PK_usuario usuario,
	SUM(rp.horas) total_horas
FROM
	t_cont_proyecto pr
INNER JOIN
	t_cont_registro_actividad rp
ON
	pr.PK_proyecto = rp.PK_proyecto
GROUP BY
	pr.PK_proyecto,pr.descripcion,rp.PK_usuario
GO


IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_cont_totalHorasRegistroActividades]'))
DROP VIEW [dbo].[v_cont_totalHorasRegistroActividades]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Generador
-- Fecha Creación:	19-06-2012
-- Fecha Actulización:	19-06-2012
-- Descripción: 
-- =============================================
CREATE VIEW v_cont_totalHorasRegistroActividades
AS
SELECT 
ra.PK_proyecto,
ra.PK_entregable,
ra.PK_componente,
ra.PK_paquete,
ra.PK_actividad,
ra.PK_usuario,
ac.descripcion DESCRIPCION,
MAX(ra.fecha) FECHA_ULTIMO,
SUM(horas) TOTAL_HORAS
FROM 
t_cont_registro_actividad ra
INNER JOIN
t_cont_actividad ac
ON
ra.PK_actividad = ac.PK_actividad
GROUP BY
ra.PK_proyecto,
ra.PK_entregable,
ra.PK_componente,
ra.PK_paquete,
ra.PK_actividad,
ra.PK_usuario,
ac.descripcion
GO


IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_cont_asignacionActividad]'))
DROP VIEW [dbo].[v_cont_asignacionActividad]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Generador
-- Fecha Creación:	19-06-2012
-- Fecha Actulización:	19-06-2012
-- Descripción: 
-- =============================================
CREATE VIEW v_cont_asignacionActividad
AS
SELECT 
	aa.PK_proyecto,
	pr.descripcion,
	aa.PK_usuario,
	us.nombre + ' ' + us.apellido1 +  ' ' + us.apellido2 nombreUsuario, 
	pa.descripcion + '-' + ac.descripcion descripcionActividad,
	aa.horasAsignadas horasAsignadas,
	ISNULL(rt.TOTAL_HORAS,0) horasReales,
	aa.fechaFin fechaFinalAsignada,
	ISNULL(rt.FECHA_ULTIMO,GETDATE())fechaUltimoRegistro
FROM
	t_cont_asignacion_actividad aa
LEFT OUTER JOIN
	v_cont_totalHorasRegistroActividades rt
ON
	aa.PK_proyecto = rt.PK_proyecto AND
	aa.PK_entregable = rt.PK_entregable AND
	aa.PK_componente = rt.PK_componente AND
	aa.PK_paquete = rt.PK_paquete AND
	aa.PK_actividad = rt.PK_actividad AND
	aa.PK_usuario = rt.PK_usuario 
LEFT OUTER JOIN
	t_cont_proyecto pr
ON
	aa.PK_proyecto = pr.PK_proyecto
LEFT OUTER JOIN
	t_admi_usuario us
ON
	aa.PK_usuario = us.PK_usuario
LEFT OUTER JOIN
	t_cont_actividad ac
ON
	aa.PK_actividad = ac.PK_actividad
LEFT OUTER JOIN
	t_cont_paquete pa
ON
	aa.PK_paquete = pa.PK_paquete
WHERE
 aa.activo = 1
GO






