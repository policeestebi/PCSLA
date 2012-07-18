
USE CSLA
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_permisoSelect]'))
DROP PROCEDURE [dbo].[PA_admi_permisoSelect]
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
CREATE PROCEDURE  PA_admi_permisoSelect
AS 
 BEGIN 
		SELECT 
         PK_permiso,
         nombre
        FROM t_admi_permiso
END  
 GO 
 
 
 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_rolSelect]'))
DROP PROCEDURE [dbo].[PA_admi_rolSelect]
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
CREATE PROCEDURE  PA_admi_rolSelect
AS 
 BEGIN 
		SELECT 
         PK_rol,
         descripcion,
		 nombre,
		 visible
        FROM t_admi_rol
END  
 GO 
 
  IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_menuSelect]'))
DROP PROCEDURE [dbo].[PA_admi_menuSelect]
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
CREATE PROCEDURE  PA_admi_menuSelect
AS 
 BEGIN 
		SELECT 
         PK_menu,
         FK_menuPadre,
		 imagen,
		 titulo,
		 descripcion
        FROM t_admi_menu
END  
 GO 
 
   IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_paginaSelect]'))
DROP PROCEDURE [dbo].[PA_admi_paginaSelect]
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
CREATE PROCEDURE  PA_admi_paginaSelect
AS 
 BEGIN 
		SELECT 
		 PK_pagina,
         FK_menu,
         nombre,
		 url,
		 height
        FROM t_admi_pagina
END  
 GO 
 
    IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_departamentoSelect]'))
DROP PROCEDURE [dbo].[PA_admi_departamentoSelect]
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
CREATE PROCEDURE  PA_admi_departamentoSelect
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
END  
 GO 
 
    IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_bitacoraSelect]'))
DROP PROCEDURE [dbo].[PA_admi_bitacoraSelect]
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
CREATE PROCEDURE  PA_admi_bitacoraSelect
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
END  
 GO 
  
      IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_usuarioSelect]'))
DROP PROCEDURE [dbo].[PA_admi_usuarioSelect]
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
CREATE PROCEDURE  PA_admi_usuarioSelect
AS 
 BEGIN 
		SELECT 
         tau.PK_usuario PK_usuario,
         tar.PK_rol PK_rol,
         tau.clave clave,
		 tau.activo activo,
		 tau.nombre nombre,
		 tau.apellido1 apellido1,
		 tau.apellido2 apellido2,
		 tau.puesto puesto,
		 tau.email email,
		 tar.nombre nombreRol,
		 tad.PK_departamento PK_departamento,
		 tad.nombre nombreDepartamento
        FROM t_admi_usuario tau, t_admi_rol tar, t_admi_departamento tad
		WHERE tar.PK_rol = tau.FK_rol AND
			  tad.PK_departamento = tau.FK_departamento
END  
 GO 

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_paginaMenuSelect]'))
DROP PROCEDURE [dbo].[PA_admi_paginaMenuSelect]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Generador
-- Fecha Creación:	10-10-2011
-- Fecha Actulización:	10-10-2011
-- Descripción: 
-- =============================================
CREATE PROCEDURE  PA_admi_paginaMenuSelect
AS 
 BEGIN 
		SELECT PK_menu menu,ISNULL(FK_menuPadre,-1)padre,titulo,ISNULL(url,'#') url, ISNULL(height,'600') height
			FROM 
				t_admi_pagina pg
			right OUTER JOIN
				t_admi_menu mn
			ON	
				pg.FK_menu = mn.PK_menu
END  
 GO 

 
IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_paginaMenuSelectRol]'))
DROP PROCEDURE [dbo].[PA_admi_paginaMenuSelectRol]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Generador
-- Fecha Creación:	19-02-2012
-- Fecha Actulización:	19-02-2012
-- Descripción: 
-- =============================================
CREATE PROCEDURE  PA_admi_paginaMenuSelectRol
	  @paramrol int
AS 
 BEGIN 
		SELECT PK_menu menu,ISNULL(FK_menuPadre,-1)padre,titulo,ISNULL(url,'#') url, ISNULL(height,'600') height, ISNULL(rpp.PK_permiso,-1) permiso
	FROM 
		t_admi_pagina pg
	right OUTER JOIN
		t_admi_menu mn
	ON	
		pg.FK_menu = mn.PK_menu
	left OUTER JOIN
		t_admi_rol_pagina_permiso rpp
	ON
		rpp.PK_rol = @paramrol AND
		pg.PK_pagina = rpp.PK_pagina
	
		
END  
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_registroPermanenteSelect]'))
DROP PROCEDURE [dbo].[PA_admi_registroPermanenteSelect]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Cristian Arce Jiménez
-- Fecha Creación:		24-01-2012
-- Fecha Actulización:	24-01-2012
-- Descripción: Select utilizado para traer todos los registros permanentes con los que trabaje la aplicación.
-- =============================================
CREATE PROCEDURE  PA_admi_registroPermanenteSelect

AS 
 BEGIN 

		SELECT 
         tabla,
         registro
        FROM t_admi_registro_permanente

END  
GO 



 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_menuSistemaSelect]'))
DROP PROCEDURE [dbo].[PA_admi_menuSistemaSelect]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Esteban Ramírez González
-- Fecha Creación:	25-10-2011
-- Fecha Actulización:	25-10-2011
-- Descripción: Select utilizado para extraer la información del menú de todo el sistema
-- =============================================
CREATE PROCEDURE  PA_admi_menuSistemaSelect
AS 
 BEGIN 
	SELECT m.PK_menu , ISNULL(m.FK_menuPadre,0) FK_menuPadre , m.titulo, ISNULL(p.PK_pagina,0) PK_pagina, ISNULL(p.nombre,'') nombre_pagina, ISNULL(p.FK_menu,0) FK_menu, ISNULL(pr.PK_permiso,0) PK_permiso, ISNULL(pr.nombre,'') nombre_permiso
		FROM
			t_admi_menu m
		LEFT OUTER JOIN
			t_admi_pagina p
		ON
			p.FK_menu = m.PK_menu
		LEFT OUTER JOIN
			t_admi_pagina_permiso pp
		ON
			pp.PK_pagina = p.PK_pagina
		LEFT OUTER JOIN
			t_admi_permiso pr
		ON
			pp.PK_permiso = pr.PK_permiso
		ORDER BY m.PK_menu asc
END  
 GO 


IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_maxSelect]'))
DROP PROCEDURE [dbo].[PA_admi_maxSelect]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Esteban Ramírez González
-- Fecha Creación:	06-11-2011
-- Fecha Actulización:	06-11-2011
-- Descripción: Select utilizado el valor máximo de una columna.
-- =============================================
CREATE PROCEDURE  PA_admi_maxSelect
	@paramTable varchar(50),
	@paramColumna varchar(50)
AS 
 BEGIN 

 DECLARE @SELECT VARCHAR(MAX)

 SELECT @SELECT = 'SELECT ISNULL(MAX(' + @paramColumna +'),0) FROM ' +  @paramTable;

 EXEC (@SELECT)

END  
 GO


 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_rol_pagina_permisoSelectRol]'))
DROP PROCEDURE [dbo].[PA_admi_rol_pagina_permisoSelectRol]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Esteban Ramírez González
-- Fecha Creación:	06-11-2011
-- Fecha Actulización:	06-11-2011
-- Descripción: Select utilizado para extraer los permisos asociados a un usuario.
-- =============================================
CREATE PROCEDURE  PA_admi_rol_pagina_permisoSelectRol
	@paramPK_rol int
AS 
 BEGIN 

 SELECT * FROM t_admi_rol_pagina_permiso
 WHERE PK_rol = @paramPK_rol  

END  
GO 

 IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_login]'))
DROP PROCEDURE [dbo].[PA_admi_login]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Esteban Ramírez González
-- Fecha Creación:	27-11-2011
-- Fecha Actulización:	27-11-2011
-- Descripción: Select utilizado determinar si el usuario existe.
-- =============================================
CREATE PROCEDURE  PA_admi_login
	@param_user varchar(30),
	@param_pass	varchar(40)
AS 
 BEGIN 

 SELECT * FROM t_admi_usuario WHERE PK_usuario = @param_user AND clave = @param_pass 

END  
GO 

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_selectFilter]'))
DROP PROCEDURE [dbo].[PA_admi_selectFilter]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Esteban Ramírez González
-- Fecha Creación:		22-01-2012
-- Fecha Actulización:	22-01-2012
-- Descripción: Select utilizado para realizar búsquedas con filtros en las tablas.
-- =============================================
CREATE PROCEDURE  PA_admi_selectFilter
	@paramTable varchar(100),
	@paramColumnas varchar(MAX),
	@paramFilter   varchar(MAX)
AS 
 BEGIN 

 DECLARE @SELECT VARCHAR(MAX),
		 @COLUMNS VARCHAR(MAX)

 IF @paramColumnas = ''
 BEGIN
	 SELECT @COLUMNS = '*'
 END
 ELSE
	SELECT @COLUMNS = @paramColumnas
 

 SELECT @SELECT = 'SELECT ' + @COLUMNS +' FROM ' +  @paramTable + ' WHERE ' + @paramFilter

 EXEC (@SELECT)

END  
GO 

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_selectPaginaRolPermisos]'))
DROP PROCEDURE [dbo].[PA_admi_selectPaginaRolPermisos]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Esteban Ramírez González
-- Fecha Creación:		27-02-2012
-- Fecha Actulización:	27-02-2012
-- Descripción: Select utilizado para obtener los permisos 
--				asociados a una página.
-- =============================================
CREATE PROCEDURE  PA_admi_selectPaginaRolPermisos
	@paramPagina VARCHAR(MAX),
	@paramRol	 int
AS 
BEGIN 

	SELECT pe.PK_permiso, pe.nombre
	FROM
		t_admi_pagina pa
	INNER JOIN
		t_admi_rol_pagina_permiso rpp
	ON
		pa.PK_pagina = rpp.PK_pagina AND
		rpp.PK_rol = @paramRol 
	INNER JOIN
		t_admi_permiso pe
	on
		rpp.PK_permiso = pe.PK_permiso
	WHERE
		pa.url = @paramPagina
END  
GO 

    IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_bitacoraSelectFiltro]'))
DROP PROCEDURE [dbo].[PA_admi_bitacoraSelectFiltro]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Generador
-- Fecha Creación:	03-06-2012
-- Fecha Actulización: 03-06-2012
-- Descripción: 
-- =============================================
CREATE PROCEDURE  PA_admi_bitacoraSelectFiltro
	@paramfechaInicio DateTime, 
	@paramfechaFinal DateTime,
	@paramUsuarioDesde Varchar(30),
	@paramUsuarioHasta Varchar(30),
	@paramTabla VARCHAR(MAX),
	@paramAccion VARCHAR(MAX),
	@paramRegistro VARCHAR(MAX)
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
			fecha_accion BETWEEN @paramfechaInicio AND @paramfechaFinal AND
			FK_usuario >=  @paramUsuarioDesde AND FK_usuario <= @paramUsuarioHasta AND
			tabla LIKE @paramTabla AND
			accion LIKE @paramAccion AND 
			numero_registro LIKE @paramRegistro
END  
 GO 

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'[dbo].[PA_admi_paginaPermisoSelect]'))
DROP PROCEDURE [dbo].[PA_admi_paginaPermisoSelect]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Generador
-- Fecha Creación:	03-06-2012
-- Fecha Actulización: 03-06-2012
-- Descripción: 
-- =============================================
CREATE PROCEDURE  PA_admi_paginaPermisoSelect
	@paramPagina int

AS 
BEGIN 
		SELECT 
			PK_permiso
        FROM t_admi_pagina_permiso
        WHERE
			PK_pagina = @paramPagina
END  
 GO  
