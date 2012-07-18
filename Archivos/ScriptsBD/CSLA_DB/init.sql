/*
//======================================================================
// Consejo de Seguridad Vial (COSEVI) 2011
// Sistema CSLA
//
// Nombre:			init.sql
// Descripci�n:		Invoca a los scripts para la creaci�n de la
//					Base de datos, tanto como a los scripts encargados
//					de la creaci�n de los Procedimientos almacenados, 
//					Funciones, Triggers, Vistas, Inserciones
//
// Explicaci�n de los contenidos del archivo.
// =====================================================================
// Historial
// PERSONA 			            MES � DIA - A�O		DESCRIPCI�N
// Esteban Ram�rez Gonz�lez		05-24-2011			Se crea el archivo
//								����������������������
//								����������������������
// 
//								
//								
//
//======================================================================
*/


USE CSLA
GO

------------------------------------------------------------------------------------------------------------------------
------------------------------------------------M�dulo de Administraci�n------------------------------------------------
------------------------------------------------------------------------------------------------------------------------

--Procedures
:r C:\CSLA_DB\mod_administracion\procedures\init_adm.sql
GO

------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------M�dulo de Control y Seguimiento--------------------------------------
------------------------------------------------------------------------------------------------------------------------

--Procedures
:r C:\CSLA_DB\mod_control_seguimiento\procedures\init_cont.sql
GO

