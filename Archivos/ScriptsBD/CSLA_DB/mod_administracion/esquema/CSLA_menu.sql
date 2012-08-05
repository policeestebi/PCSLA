

--- Elimina lo anterior para agregar el nuevo menu
DELETE FROM t_admi_rol_pagina_permiso
DELETE FROM t_admi_pagina_permiso
DELETE FROM t_admi_pagina
DELETE FROM t_admi_menu

DBCC CHECKIDENT('t_admi_pagina', RESEED, 0)

--Insert del Menú Principal
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (1,NULL,'Inicio', 'Inicio');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (2,NULL,'Administración', 'Administración');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (3,NULL,'Control y Seguimiento', 'Control y Seguimiento');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (4,NULL,'Estadístico', 'Estadístico');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (5,NULL,'Reportes', 'Reportes');

--Registros no eliminables
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','1');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','2');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','3');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','4');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','5');

--Insert de la pestaña de Administración

INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (6,2,'Seguridad', 'Seguridad');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (7,6,'Usuarios', 'Usuarios');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (8,6,'Roles', 'Roles');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (9,6,'Permisos', 'Permisos');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (10,6,'Menú', 'Menú');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (11,6,'Páginas', 'Páginas');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (12,2,'Administración', 'Administración');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (13,12,'Departamentos', 'Departamentos');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (14,12,'Bitácora', 'Bitácora');


--Registros no eliminables
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','6');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','7');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','8');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','9');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','10');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','11');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','12');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','13');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','14');

--Insert de la pestaña de Control y Seguimiento

INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (15,3,'Proyectos', 'Proyectos');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (16,15,'Proyectos', 'Proyectos');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (17,15,'Entregables', 'Entregables');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (18,15,'Componentes', 'Componentes');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (19,15,'Paquetes', 'Paquetes');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (20,15,'Actividades', 'Actividades');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (21,3,'Administración', 'Administración');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (22,21,'Operaciones/Imprevistos', 'Operaciones');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (23,21,'Estados', 'Estados');

--Registros no eliminables
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','15');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','16');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','17');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','18');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','19');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','20');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','21');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','22');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','23');

--Gráficos
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (24,4,'Inversión Tiempos', 'Inversión Tiempos');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (25,4,'Top Actividades', 'Top Actividades');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (26,4,'Comparar Horas', 'Comparar Horas');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (27,4,'Actividades Retrasadas', 'Actividades Retrasadas');

--Registros no eliminables
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','24');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','25');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','26');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','27');



--Reportes

INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (28,5,'Registro de Tiempos Usuario', 'Registro de Tiempos Usuario');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (29,5,'Bitácora Sistema', 'Bitácora del Sistema');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (30,5,'Actividades Retrasadas', 'Actividades Retrasadas');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (31,5,'Actividades Superan Estimado', 'Actividades Superan Estimado');
INSERT INTO t_admi_menu (PK_menu,FK_menuPadre,titulo,descripcion) VALUES (32,5,'Registro Tiempos Usuarios', 'Registro Tiempos Usuarios');


--Registros no eliminables
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','28');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','29')
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','30')
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','31')
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_menu','32')


--Insert de las páginas


--Administracion
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (14,'Bitácora','#App_pages/mod.Administracion/frw_bitacora.aspx','600');							--1
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (13,'Departamentos','#App_pages/mod.Administracion/frw_departamento.aspx','600');			--2
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (9,'Permisos','#App_pages/mod.Administracion/frw_permisos.aspx','600');							--3
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (7,'Usuarios','#App_pages/mod.Administracion/frw_usuarios.aspx','600');							--4
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (8,'Roles','#App_pages/mod.Administracion/frw_roles.aspx','700');								--5
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (10,'Menu','#App_pages/mod.Administracion/frw_menu.aspx','600');									--6
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (11,'Pagina','#App_pages/mod.Administracion/frw_pagina.aspx','600');								--7

--Registros no eliminables
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','1');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','2');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','3');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','4');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','5');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','6');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','7');

--Control y Seguimiento
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (16,'Proyectos','#App_pages/mod.ControlSeguimiento/frw_proyectos.aspx','600');									--8
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (23,'Estados','#App_pages/mod.ControlSeguimiento/frw_estados.aspx','600');											--9
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (18,'Componentes','#App_pages/mod.ControlSeguimiento/frw_componentes.aspx','600');						--10
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (17,'Entregables','#App_pages/mod.ControlSeguimiento/frw_entregables.aspx','600');								--11
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (19,'Paquetes','#App_pages/mod.ControlSeguimiento/frw_paquetes.aspx','600');										--12
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (20,'Actividades','#App_pages/mod.ControlSeguimiento/frw_actividades.aspx','600');								--13
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (NULL,'Copiar Proyecto','#App_pages/mod.ControlSeguimiento/frw_copiarProyecto.aspx','600');				--14
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (22,'Operaciones/Imprevistos','#App_pages/mod.ControlSeguimiento/frw_operaciones.aspx', '600');			--15

--Registros no eliminables
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','8');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','9');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','10');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','11');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','12');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','13');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','14');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','15');



--Inicio
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (1,'Calendario','#App_pages/mod.ControlSeguimiento/frw_calendario.aspx', '700');								--16

--Registros no eliminables
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','16');

--Estadístico
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (24,'Inversión Tiempos','#App_pages/mod.Estadistico/frw_grf_inversionTiempos.aspx','900');					--17
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (25,'Top Actividades','#App_pages/mod.Estadistico/frw_grf_topActividades.aspx','900');							--18
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (26,'Comparar Horas','#App_pages/mod.Estadistico/frw_grf_compHorasActividades.aspx','900');				--19
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (27,'Actividades Retrasadas','#App_pages/mod.Estadistico/frw_grf_consActRetrasadas.aspx','900');																				--20

--Registros no eliminables
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','17');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','18');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','19');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','20');

--Reportes
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (28,'Reporte Registro Tiempo Usuario','#App_pages/mod.Reportes/RegistroTiemposUsuario/frw_rep_registroTiemposUsuarioParam.aspx','600');				--21
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (29,'Bitácora Sistema','#App_pages/mod.Reportes/Bitacora/frw_rep_bitacoraParam.aspx','600');																						--22
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (30,'Actividades Retrasadas','#App_pages/mod.Reportes/ActividadesRetrasadas/frw_rep_actividadesRetrasadasParam.aspx','600');									--23
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (31,'Actividades Superan Estimado','#App_pages/mod.Reportes/ActividadesSuperanEstimado/frw_rep_actividadesSuperanEstimadoParam.aspx','600');		--24
INSERT INTO t_admi_pagina(FK_menu,nombre,url,height) VALUES (32,'Registro Tiempos Usuarios','#App_pages/mod.Reportes/RegistroTiemposUsuarios/frw_rep_registroTiemposUsuariosParam.aspx','600');						--25

--Registros no eliminables
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','21');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','22');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','23');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','24');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_pagina','25');


--Insert de la tabla de permisos
INSERT INTO t_admi_permiso(nombre) VALUES ('Agregar');				--1
INSERT INTO t_admi_permiso(nombre) VALUES ('Modificar');			--2
INSERT INTO t_admi_permiso(nombre) VALUES ('Eliminar');				--3
INSERT INTO t_admi_permiso(nombre) VALUES ('Acceso');				--4
INSERT INTO t_admi_permiso(nombre) VALUES ('CambCont');				--5
INSERT INTO t_admi_permiso(nombre) VALUES ('TodosUsuarios');		--6
INSERT INTO t_admi_permiso(nombre) VALUES ('AsignacionMasiva');		--7
--Registros no eliminables
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_permiso','1');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_permiso','2');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_permiso','3');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_permiso','4');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_admi_permiso','5');

--Inserts de la tabla de estados
INSERT INTO t_cont_estado(descripcion) VALUES ('Iniciado');
INSERT INTO t_cont_estado(descripcion) VALUES ('Finalizado');
INSERT INTO t_cont_estado(descripcion) VALUES ('Cancelado');
INSERT INTO t_cont_estado(descripcion) VALUES ('Pausado');
--Registros no eliminables
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_cont_estado','1');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_cont_estado','2');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_cont_estado','3');
INSERT INTO t_admi_registro_permanente(tabla,registro) VALUES ('t_cont_estado','4');

--Inserts de la tabla de pagina permisos.

--Bitácora
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(1,4)

--Departamentos
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(2,1)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(2,2)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(2,3)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(2,4)

--Permisos
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(3,1)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(3,2)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(3,3)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(3,4)

--Usuarios
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(4,1)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(4,2)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(4,3)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(4,4)

--Roles
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(5,1)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(5,2)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(5,3)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(5,4)

--Menu

INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(6,1)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(6,2)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(6,3)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(6,4)

--Página 


INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(7,1)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(7,2)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(7,3)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(7,4)

--Proyectos

INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(8,1)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(8,2)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(8,3)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(8,4)

--Estados

INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(9,1)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(9,2)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(9,3)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(9,4)

--Componentes

INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(10,1)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(10,2)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(10,3)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(10,4)
--Entregables


INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(11,1)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(11,2)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(11,3)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(11,4)

--Paquetes
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(12,1)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(12,2)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(12,3)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(12,4)
--Actividades
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(13,1)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(13,2)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(13,3)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(13,4)
--CopiarProyecto
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(14,4)


--Operaciones
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(15,1)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(15,2)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(15,3)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(15,4)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(15,7)

--Inicio
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(16,4)


--Graficos
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(17,4)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(18,4)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(19,4)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(20,4)

--Reportes
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(21,4)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(22,4)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(23,4)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(24,4)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(25,4)
INSERT INTO t_admi_pagina_permiso (PK_pagina, PK_permiso) VALUES(25,6)

--Se agregan todos los privilegios para el usuario administrador
INSERT INTO t_admi_rol_pagina_permiso
SELECT 1, PK_pagina, PK_permiso FROM t_admi_pagina_permiso
