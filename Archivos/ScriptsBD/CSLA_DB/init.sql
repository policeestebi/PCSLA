/*
//======================================================================
// Consejo de Seguridad Vial (COSEVI) 2011
// Sistema CSLA
//
// Nombre:			init.sql
// Descripción:		Invoca a los scripts para la creación de la
//					Base de datos, tanto como a los scripts encargados
//					de la creación de los Procedimientos almacenados, 
//					Funciones, Triggers, Vistas, Inserciones
//
// Explicación de los contenidos del archivo.
// =====================================================================
// Historial
// PERSONA 			            MES – DIA - AÑO		DESCRIPCIÓN
// Esteban Ramírez González		05-24-2011			Se crea el archivo
//								…………………………………………………………
//								…………………………………………………………
// 
//								
//								
//
//======================================================================
*/


USE CSLA
GO

------------------------------------------------------------------------------------------------------------------------
------------------------------------------------Módulo de Administración------------------------------------------------
------------------------------------------------------------------------------------------------------------------------

--Procedures
:r C:\CSLA_DB\mod_administracion\procedures\init_adm.sql
GO

------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------Módulo de Control y Seguimiento--------------------------------------
------------------------------------------------------------------------------------------------------------------------

--Procedures
:r C:\CSLA_DB\mod_control_seguimiento\procedures\init_cont.sql
GO

