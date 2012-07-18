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

------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------Sistema CSLA-----------------------------------------------------
------------------------------------------------------------------------------------------------------------------------

-- Scripts procedures

--Procedures de delets
:r C:\CSLA_DB\mod_control_seguimiento\procedures\csla_cont_procedures_delets.sql
GO
--Procedures de inserts
:r C:\CSLA_DB\mod_control_seguimiento\procedures\csla_cont_procedures_inserts.sql
GO

--Procedures de select one
:r C:\CSLA_DB\mod_control_seguimiento\procedures\csla_cont_procedures_selectOne.sql
GO

--Procedures de selects
:r C:\CSLA_DB\mod_control_seguimiento\procedures\csla_cont_procedures_selects.sql
GO

--Procedures de updates
:r C:\CSLA_DB\mod_control_seguimiento\procedures\csla_cont_procedures_updates.sql
GO
