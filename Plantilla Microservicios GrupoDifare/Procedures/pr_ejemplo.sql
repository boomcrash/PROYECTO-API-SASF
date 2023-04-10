/****************************************************************************
 *	Archivo Físico:		pr_ejemplo.sql										*
 *	Stored Procedure:	pr_ejemplo											*
 *	Base de Datos:		[Nombre Base Datos]									*
 *	Producto:			[Nombre Producto]									*
 *	Elaborado por:		[Desarrollador]										*
 *--------------------------------------------------------------------------*
 *							AVISO IMPORTANTE								*
 *	Este archivo contiene codigo fuente que forma parte integral de los		*
 *	sistemas que son propiedad intelectual de GRUPO DIFARE.					*
 *	La utilizacion, modificacion, distribucion o duplicacion en forma total	*
 *	o parcial del contenido de este archivo, sin la respectiva licencia o	*
 *	permiso emitido por la Gerencia de GRUPO DIFARE, será considerada como	*
 *	una grave violación a las leyes de propiedad intelectual de este código	*
 *	y los infractores pueden ser sujetos a demandas judiciales y todas las	*
 *	acciones permitidas bajo el marco de la ley.							*
 *--------------------------------------------------------------------------*
 *						DESCRIPCION DEL PROCEDIMIENTO						*
 *	Operaciones de ingreso, modificación, eliminación y consulta de ejemplo	*
 *--------------------------------------------------------------------------*
 *						BITACORA DE MODIFICACIONES							*
 *	FECHA		AUTOR				RAZON									*
 *	DD/MM/YYYY	[Desarrollador]		Versión inicial							* 
 *--------------------------------------------------------------------------*/

IF EXISTS (SELECT * FROM sysobjects WHERE name = 'pr_ejemplo')
	DROP PROC dbo.pr_ejemplo
GO

CREATE PROC dbo.pr_ejemplo (
	@i_usuario					udt_login			= 'batch-usr',
	@i_accion					char(1),
	@i_id_ejemplo				int					= null,
	@i_campo_uno				varchar(50)			= null,
	@i_offset					int					= null,
	@i_limit					int					= null
)
AS

IF @i_accion = 'I'
BEGIN
	INSERT	INTO tabla_ejemplo (
			id_ejemplo,
			campo_uno
	)
	VALUES (
			@i_id_ejemplo,
			@i_campo_uno
	)

	RETURN 0
END

IF @i_accion = 'M'
BEGIN
	UPDATE	tabla_ejemplo
	SET		campo_uno				= @i_campo_uno
	WHERE	id_ejemplo				= @i_id_ejemplo

	RETURN 0
END

IF @i_accion = 'C'
BEGIN
	SELECT	id_ejemplo				= a.id_ejemplo,
			campo_uno				= a.campo_uno
	FROM	tabla_ejemplo a
	WHERE	a.id_ejemplo			= @i_id_ejemplo

	RETURN 0
END

IF @i_accion = 'G'
BEGIN
	-- Se obtiene el total de registros
	SELECT	total_registros			= COUNT(1)
	FROM	tabla_ejemplo a
	WHERE	a.campo_uno				LIKE '%' + ISNULL(@i_campo_uno,a.campo_uno) + '%'

	-- Se obtienen los registros con un límite
	-- BEGIN: Consulta para versiones de SQL Server actuales
	SELECT	id_ejemplo				= a.id_ejemplo,
			campo_uno				= a.campo_uno
	FROM	tabla_ejemplo a
	WHERE	a.campo_uno				LIKE '%' + ISNULL(@i_campo_uno,a.campo_uno) + '%'
	ORDER	BY a.campo_uno
	OFFSET	(@i_offset - 1) ROWS
	FETCH	NEXT @i_limit ROWS ONLY
	-- END

	-- BEGIN: Consulta para versiones de SQL Server antiguas
	;WITH registros AS
	(
		SELECT	id_ejemplo				= a.id_ejemplo,
				campo_uno				= a.campo_uno,
				ROW_NUMBER() OVER
				(
					ORDER BY a.campo_uno
				) AS rn
		FROM	tabla_ejemplo a
		WHERE	a.campo_uno				LIKE '%' + ISNULL(@i_campo_uno,a.campo_uno) + '%'
	)

	SELECT	id_ejemplo,
			campo_uno
	FROM	registros
	WHERE	rn		>= @i_offset
		AND	rn		< @i_offset + @i_limit
	-- END

	RETURN 0
END

RAISERROR ('El código de la acción es incorrecto.',16,1)

GO
