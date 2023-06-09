USE [CursosSa]
GO
/****** Object:  StoredProcedure [dbo].[miProcedimiento]    Script Date: 10/04/2023 17:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[miProcedimiento]
    @tabla VARCHAR(50),
	@accion VARCHAR(50),
	@id INT= NULL,
    @id_curso INT = NULL,
    @nombre_curso VARCHAR(255) = NULL,
    @descripcion_curso VARCHAR(500) = NULL,
    @id_profesor INT = NULL,
    @id_estudiante INT = NULL,
    @nombre_estudiante VARCHAR(255) = NULL,
    @email_estudiante VARCHAR(255) = NULL,
    @fecha_inscripcion DATE = NULL,
    @nombre_profesor VARCHAR(255) = NULL,
    @email_profesor VARCHAR(255) = NULL,
	@nombre_materia VARCHAR(255) = NULL,
	@id_materia INT = NULL,
	@limiteInicial INT = NULL,
	@limiteFinal INT = NULL,
	@estado char(1) = NULL,
	@apellido_profesor VARCHAR(255) = NULL,
	@cedula VARCHAR(10) = NULL,
	@apellido_estudiante VARCHAR(255) = NULL,
	@fecha_finalizacion Date = NULL,
	@cupos_disponibles INT =NULL,
	@id_incripcion INT =NULL,
	@id_insertado INT=0,
	@calificacion INT=NULL
AS
BEGIN
	-------------------------------  AUX -----------------------------------------

    IF (@tabla = 'Inscripcion') And (@accion = 'Aux') 
		BEGIN
			SELECT MAX(id_inscripcion) FROM Inscripcion
		END

	-------------------------------  ASIGNAR -----------------------------------------
	ELSE IF (@tabla = 'DetalleInscripcion') And (@accion = 'Asignar') 
	BEGIN
		IF (@id_curso IS NOT NULL AND @id_estudiante IS NOT NULL AND @calificacion IS NOT NULL)
			BEGIN
				UPDATE detalle_inscripcion
				SET calificacion = @calificacion
				FROM detalle_inscripcion di
				INNER JOIN Inscripcion i ON di.id_inscripcion = i.id_inscripcion
				WHERE i.id_curso = @id_curso AND i.id_estudiante = @id_estudiante;
					SET @id_insertado = @@ROWCOUNT;
					SELECT @id_insertado;
			END
		ELSE
			BEGIN
				SET @id_insertado = 0;
				SELECT @id_insertado;
			END
	END
	-------------------------------  INSERTAR -----------------------------------------

    ELSE IF (@tabla = 'Curso') And (@accion = 'Insertar') 
    BEGIN
        IF (@nombre_curso IS NOT NULL AND @descripcion_curso IS NOT NULL AND @id_profesor IS NOT NULL AND @cupos_disponibles IS NOT NULL AND @fecha_finalizacion IS NOT NULL)
        BEGIN
            INSERT INTO Curso (nombre_curso, descripcion_curso,id_profesor,cupos_disponibles,fecha_finalizacion)
            VALUES (@nombre_curso, @descripcion_curso,@id_profesor,@cupos_disponibles,@fecha_finalizacion);
				SET @id_insertado = SCOPE_IDENTITY();
				SELECT @id_insertado;
			END
		ELSE
			BEGIN
				SELECT @id_insertado;
			END
    END
    ELSE IF (@tabla = 'Estudiante') And (@accion = 'Insertar') 
    BEGIN
        IF (@nombre_estudiante IS NOT NULL AND @email_estudiante IS NOT NULL  AND @apellido_estudiante IS NOT NULL AND @cedula IS NOT NULL )
        BEGIN
            INSERT INTO Estudiante (nombre_estudiante, email_estudiante, apellido_estudiante,cedula)
            VALUES (@nombre_estudiante, @email_estudiante,@apellido_estudiante,@cedula);
				SET @id_insertado = SCOPE_IDENTITY();
				SELECT @id_insertado;
			END
		ELSE
			BEGIN
				SELECT @id_insertado;
			END
    END
    ELSE IF (@tabla = 'Inscripcion') And (@accion = 'Insertar') 
    BEGIN

        IF (@id_curso IS NOT NULL AND @id_estudiante IS NOT NULL )
			BEGIN
				INSERT INTO Inscripcion (id_curso, id_estudiante) VALUES (@id_curso, @id_estudiante);
				SET @id_insertado = SCOPE_IDENTITY();
				SELECT @id_insertado;
			END
		ELSE
			BEGIN
				SET @id_insertado = 0;
				SELECT @id_insertado;
			END
    END
	ELSE IF (@tabla = 'DetalleInscripcion') And (@accion = 'Insertar') 
		BEGIN
			IF ( @id IS NOT NULL)
				BEGIN
				INSERT INTO detalle_inscripcion (id_inscripcion,aprobado,calificacion)
				VALUES (@id,0,0);
					SET @id_insertado = SCOPE_IDENTITY();
					SELECT @id_insertado;
				END
			ELSE
				BEGIN
					SELECT @id_insertado;
				END
		END
    ELSE IF (@tabla = 'Profesor') And (@accion = 'Insertar') 
    BEGIN
		
        IF (@nombre_profesor IS NOT NULL AND @email_profesor IS NOT NULL AND @apellido_profesor IS NOT NULL AND @cedula IS NOT NULL )
			BEGIN
				INSERT INTO Profesor (nombre_profesor, email_profesor, apellido_profesor,cedula)
				VALUES (@nombre_profesor, @email_profesor, @apellido_profesor,@cedula);
			
				SET @id_insertado = SCOPE_IDENTITY();
				SELECT @id_insertado;
			END
		ELSE
			BEGIN
				SELECT @id_insertado;
			END
    END
	ELSE IF (@tabla = 'Materia') AND (@accion = 'Insertar')
	BEGIN
		IF (@nombre_materia IS NOT NULL)
		BEGIN
			INSERT INTO Materia (nombre_materia)
			VALUES (@nombre_materia);
				SET @id_insertado = SCOPE_IDENTITY();
				SELECT @id_insertado;
			END
		ELSE
			BEGIN
				SELECT @id_insertado;
			END
	END
	ELSE IF (@tabla = 'MatCurso') AND (@accion = 'Insertar')
		BEGIN
			IF (@id_curso IS NOT NULL AND @id_materia IS NOT NULL )
			BEGIN
				IF NOT EXISTS (SELECT 1 FROM MatCurso WHERE id_curso = @id_curso AND id_materia = @id_materia)
				BEGIN
					INSERT INTO MatCurso (id_curso, id_materia)
					VALUES (@id_curso, @id_materia);
					SET @id_insertado = SCOPE_IDENTITY();
					SELECT @id_insertado;
				END
				ELSE
				BEGIN
					SET @id_insertado = 0;
					SELECT @id_insertado;
				END
			END
			ELSE
			BEGIN
				SELECT @id_insertado;
			END
		END

	-------------------------------  CONSULTAR -----------------------------------------


	IF (@tabla = 'Curso') AND (@accion = 'Consultar') 
	BEGIN
		SELECT COUNT(*) as total_registros FROM Curso WHERE estado =  @estado;

		SELECT *
		FROM Curso
		WHERE estado =  @estado
		ORDER BY id_curso
		OFFSET COALESCE(@limiteInicial, 0) ROWS
		FETCH NEXT COALESCE(@limiteFinal - @limiteInicial + 1, (SELECT COUNT(*) FROM Curso)) ROWS ONLY;
	END
	ELSE IF (@tabla = 'Estudiante') AND (@accion = 'Consultar')
	BEGIN
		SELECT COUNT(*) as total_registros FROM Estudiante WHERE estado =  @estado; 

		SELECT *
		FROM Estudiante
		WHERE estado = @estado
		ORDER BY id_estudiante
		OFFSET COALESCE(@limiteInicial, 0) ROWS
		FETCH NEXT COALESCE(@limiteFinal - @limiteInicial + 1, (SELECT COUNT(*) FROM Estudiante)) ROWS ONLY;
	END
	ELSE IF (@tabla = 'Inscripcion') AND (@accion = 'Consultar') 
	BEGIN
		SELECT COUNT(*) as total_registros FROM Inscripcion WHERE estado =  @estado;

		SELECT *
		FROM Inscripcion
		WHERE estado = @estado
		ORDER BY id_inscripcion
		OFFSET COALESCE(@limiteInicial, 0) ROWS
		FETCH NEXT COALESCE(@limiteFinal - @limiteInicial + 1, (SELECT COUNT(*) FROM Inscripcion)) ROWS ONLY;
	END
	ELSE IF (@tabla = 'Profesor') AND (@accion = 'Consultar') 
	BEGIN
		SELECT COUNT(*) as total_registros FROM Profesor WHERE estado =  @estado;

		SELECT *
		FROM Profesor
		WHERE estado = @estado
		ORDER BY id_profesor
		OFFSET COALESCE(@limiteInicial, 0) ROWS
		FETCH NEXT COALESCE(@limiteFinal - @limiteInicial + 1, (SELECT COUNT(*) FROM Profesor)) ROWS ONLY;
	END
	ELSE IF (@tabla = 'Materia') AND (@accion = 'Consultar')
	BEGIN
		SELECT COUNT(*) as total_registros FROM Materia WHERE estado =  @estado;

		SELECT *
		FROM Materia
		WHERE estado = @estado
		ORDER BY id_materia
		OFFSET COALESCE(@limiteInicial, 0) ROWS
		FETCH NEXT COALESCE(@limiteFinal - @limiteInicial + 1, (SELECT COUNT(*) FROM Materia)) ROWS ONLY;
	END

		-------------------------------  CONSULTAR POR ID -----------------------------------------

		IF (@tabla = 'Curso') AND (@accion = 'ConsultarPorId') AND (@id IS NOT NULL)
		BEGIN
			SELECT * FROM Curso WHERE id_curso = @id and estado='A';
		END
		ELSE IF (@tabla = 'Estudiante') AND (@accion = 'ConsultarPorId') AND (@id IS NOT NULL)
		BEGIN
			SELECT * FROM Estudiante WHERE id_estudiante = @id and estado='A';
		END
		ELSE IF (@tabla = 'Inscripcion') AND (@accion = 'ConsultarPorId') AND (@id IS NOT NULL)
		BEGIN
			SELECT * FROM Inscripcion WHERE id_inscripcion = @id and estado='A';
		END
		ELSE IF (@tabla = 'Profesor') AND (@accion = 'ConsultarPorId') AND (@id IS NOT NULL)
		BEGIN
			SELECT * FROM Profesor WHERE id_profesor = @id and estado='A';
		END
		ELSE IF (@tabla = 'Materia') AND (@accion = 'ConsultarPorId')
		BEGIN
			SELECT * FROM Materia WHERE id_materia = @id and estado='A';
		END


	--------------------------------- MODIFICAR   ---------------------------------------

	IF (@tabla = 'Curso') And (@accion = 'Modificar') 
    BEGIN
        UPDATE Curso SET
            nombre_curso = ISNULL(@nombre_curso, nombre_curso),
            descripcion_curso = ISNULL(@descripcion_curso, descripcion_curso),
			id_profesor = ISNULL(@id_profesor, id_profesor),
			cupos_disponibles = ISNULL(@cupos_disponibles, cupos_disponibles),
			fecha_finalizacion=  ISNULL(@fecha_finalizacion, fecha_finalizacion)

        WHERE id_curso = @id;
		select @id;
    END
    ELSE IF (@tabla = 'Estudiante') And (@accion = 'Modificar') 
    BEGIN
        UPDATE Estudiante SET
            nombre_estudiante = ISNULL(@nombre_estudiante, nombre_estudiante),
            email_estudiante = ISNULL(@email_estudiante, email_estudiante),
			apellido_estudiante = ISNULL(@apellido_estudiante, apellido_estudiante),
			cedula = ISNULL(@cedula, cedula)

        WHERE id_estudiante = @id;
		select @id;
    END
    ELSE IF (@tabla = 'Inscripcion') And (@accion = 'Modificar') 
    BEGIN
        UPDATE Inscripcion SET
            id_curso = ISNULL(@id_curso, id_curso),
            id_estudiante = ISNULL(@id_estudiante, id_estudiante)
        WHERE id_inscripcion = @id;
		select @id;
    END
    ELSE IF (@tabla = 'Profesor') And (@accion = 'Modificar') 
    BEGIN
        UPDATE Profesor SET
            nombre_profesor = ISNULL(@nombre_profesor, nombre_profesor),
            email_profesor = ISNULL(@email_profesor, email_profesor),
			apellido_profesor = ISNULL(@apellido_profesor, apellido_profesor),
			cedula = ISNULL(@cedula, cedula)
        WHERE id_profesor = @id;
		select @id;
    END
	ELSE IF (@tabla = 'Materia') AND (@accion = 'Modificar')
	BEGIN
		UPDATE Materia SET 
			nombre_materia = ISNULL(@nombre_materia, nombre_materia)
		WHERE id_materia = @id_materia;
		select @id;
	END

	--------------------------------- ELIMINAR   ----------------------------------------
	 IF (@tabla = 'Curso') And (@accion = 'Eliminar') 
    BEGIN
        UPDATE Curso SET estado = 'I' WHERE id_curso = @id;
    END
    ELSE IF (@tabla = 'Estudiante') And (@accion = 'Eliminar') 
    BEGIN
        UPDATE Estudiante SET estado = 'I' WHERE id_estudiante = @id;
    END
    ELSE IF (@tabla = 'Inscripcion') And (@accion = 'Eliminar') 
    BEGIN
        UPDATE Inscripcion SET estado = 'I' WHERE id_inscripcion = @id;
    END
    ELSE IF (@tabla = 'Profesor') And (@accion = 'Eliminar') 
    BEGIN
        UPDATE Profesor SET estado = 'I' WHERE id_profesor = @id;
    END
	ELSE IF (@tabla = 'Materia') AND (@accion = 'Eliminar')
	BEGIN
		UPDATE Materia SET estado = 'I', fecha_actualizacion = GETDATE() WHERE id_materia = @id_materia;
	END

		--------------------------------- Consultas Especificas   ----------------------------------------



	-------------- Numero de Inscritos en un curso -------------------
	ELSE IF (@tabla = 'Especifico') AND (@accion = 'ConsultarTotalInscritosByCurso') AND (@id_curso IS NOT NULL)
	BEGIN
	SELECT COUNT(*) AS total_inscritos
	FROM Inscripcion
	WHERE id_curso = @id_curso;
	END

	-------------- Cupos disponibles en un curso -------------------
	ELSE IF (@tabla = 'Especifico') AND (@accion = 'ConsultarCuposDisponibles') AND (@id_curso IS NOT NULL)
	BEGIN
	SELECT cupos_disponibles
	FROM Curso
	WHERE id_curso = @id_curso;
	END

	-------------- Calificacion de un estudiante en un curso especifico -------------------
	ELSE IF (@tabla = 'Especifico') AND (@accion = 'ConsultarCalificacionEstudianteByCurso') AND (@id_curso IS NOT NULL) AND (@id_estudiante IS NOT NULL)
	BEGIN
	SELECT detalle_inscripcion.calificacion
	FROM detalle_inscripcion
	JOIN Inscripcion ON detalle_inscripcion.id_inscripcion = Inscripcion.id_inscripcion
	WHERE Inscripcion.id_curso = @id_curso
	AND Inscripcion.id_estudiante = @id_estudiante;
	END

	-------------- Materias de un curso especifico -------------------

	ELSE IF (@tabla = 'Especifico') AND (@accion = 'ConsultarMateriasByCurso')
	BEGIN
	IF (@id_curso IS NOT NULL)

		SELECT COUNT(*) as total_registros
		FROM Materia AS M
		JOIN MatCurso AS MC ON M.id_materia = MC.id_materia
		WHERE MC.id_curso = @id_curso;

	BEGIN
	SELECT M.*
	FROM Materia AS M
	JOIN MatCurso AS MC ON M.id_materia = MC.id_materia
	WHERE MC.id_curso = @id_curso
		ORDER BY M.id_materia
		OFFSET COALESCE(@limiteInicial-1, 0) ROWS
		FETCH NEXT COALESCE(@limiteFinal - @limiteInicial + 1, (SELECT COUNT(*) FROM Materia)) ROWS ONLY;
	END
	END

	-------------- Cursos que esta registrado un estudiante -------------------

	ELSE IF (@tabla = 'Especifico') AND (@accion = 'ConsultarRegistradosByEstudiante')
	BEGIN
	SELECT COUNT(*) as total_registros 	FROM Curso
	INNER JOIN Inscripcion ON Curso.id_curso = Inscripcion.id_curso
	WHERE Inscripcion.id_estudiante = @id_estudiante;

	IF (@id_estudiante IS NOT NULL)
	BEGIN
	SELECT Curso.*
	FROM Curso
	INNER JOIN Inscripcion ON Curso.id_curso = Inscripcion.id_curso
	WHERE Inscripcion.id_estudiante = @id_estudiante
		ORDER BY id_curso
		OFFSET COALESCE(@limiteInicial-1, 0) ROWS
		FETCH NEXT COALESCE(@limiteFinal - @limiteInicial + 1, (SELECT COUNT(*) FROM Curso)) ROWS ONLY;
	END
	END

	-------------- Cursos que finalizados por un estudiante -------------------

	ELSE IF (@tabla = 'Especifico') AND (@accion = 'ConsultarFinalizadosByEstudiante')

	BEGIN

		SELECT COUNT(*) as total_registros FROM Curso
		JOIN Inscripcion ON Curso.id_curso = Inscripcion.id_curso
		JOIN detalle_inscripcion ON Inscripcion.id_inscripcion = detalle_inscripcion.id_inscripcion
		WHERE Inscripcion.id_estudiante = @id_estudiante
		AND detalle_inscripcion.aprobado = 1
		AND Curso.fecha_finalizacion < GETDATE();

	IF (@id_estudiante IS NOT NULL)
	BEGIN
	SELECT Curso.*
	FROM Curso
	JOIN Inscripcion ON Curso.id_curso = Inscripcion.id_curso
	JOIN detalle_inscripcion ON Inscripcion.id_inscripcion = detalle_inscripcion.id_inscripcion
	WHERE Inscripcion.id_estudiante = @id_estudiante
	AND detalle_inscripcion.aprobado = 1
	AND Curso.fecha_finalizacion < GETDATE()
		ORDER BY Curso.id_curso
		OFFSET COALESCE(@limiteInicial-1, 0) ROWS
		FETCH NEXT COALESCE(@limiteFinal - @limiteInicial + 1, (SELECT COUNT(*) FROM Curso)) ROWS ONLY;


	END
	END
	-------------- Cursos aprobados de un estudiante -------------------
	
	ELSE IF (@tabla = 'Especifico') AND (@accion = 'ConsultarCursosAprobados')
	BEGIN
		SELECT COUNT(*) as total_registros
		FROM Curso
		JOIN Inscripcion ON Curso.id_curso = Inscripcion.id_curso
		JOIN detalle_inscripcion ON Inscripcion.id_inscripcion = detalle_inscripcion.id_inscripcion
		WHERE Inscripcion.id_estudiante = @id_estudiante
		AND detalle_inscripcion.aprobado = 1;


	IF (@id_estudiante IS NOT NULL)
	BEGIN
	SELECT Curso.*
	FROM Curso
	JOIN Inscripcion ON Curso.id_curso = Inscripcion.id_curso
	JOIN detalle_inscripcion ON Inscripcion.id_inscripcion = detalle_inscripcion.id_inscripcion
	WHERE Inscripcion.id_estudiante = @id_estudiante
	AND detalle_inscripcion.aprobado = 1
		ORDER BY Curso.id_curso
		OFFSET COALESCE(@limiteInicial-1, 0) ROWS
		FETCH NEXT COALESCE(@limiteFinal - @limiteInicial + 1, (SELECT COUNT(*) FROM Curso)) ROWS ONLY;
	END
	END

	-------------- Cursos reprobados de un estudiante -------------------

	ELSE IF (@tabla = 'Especifico') AND (@accion = 'ConsultarCursosReprobados')
	BEGIN

		SELECT COUNT(*) as total_registros
		FROM Curso
		JOIN Inscripcion ON Curso.id_curso = Inscripcion.id_curso
		JOIN detalle_inscripcion ON Inscripcion.id_inscripcion = detalle_inscripcion.id_inscripcion
		WHERE Inscripcion.id_estudiante = @id_estudiante
		AND detalle_inscripcion.aprobado = 0;

	IF (@id_estudiante IS NOT NULL)
	BEGIN
	SELECT Curso.*
	FROM Curso
	JOIN Inscripcion ON Curso.id_curso = Inscripcion.id_curso
	JOIN detalle_inscripcion ON Inscripcion.id_inscripcion = detalle_inscripcion.id_inscripcion
	WHERE Inscripcion.id_estudiante = @id_estudiante
	AND detalle_inscripcion.aprobado = 0
		ORDER BY Curso.id_curso
		OFFSET COALESCE(@limiteInicial-1, 0) ROWS
		FETCH NEXT COALESCE(@limiteFinal - @limiteInicial + 1, (SELECT COUNT(*) FROM Curso)) ROWS ONLY;
	END
	END

	-------------- Cursos a cargo de un profesor especifico -------------------

	ELSE IF (@tabla = 'Especifico') AND (@accion = 'ConsultarCursosByProfesor')
	BEGIN

	IF (@id_profesor IS NOT NULL)

	
		SELECT COUNT(*) as total_registros
		FROM Curso
		WHERE id_profesor = @id_profesor;

		BEGIN
		SELECT *
		FROM Curso
		WHERE id_profesor = @id_profesor
			ORDER BY Curso.id_curso
			OFFSET COALESCE(@limiteInicial-1, 0) ROWS
			FETCH NEXT COALESCE(@limiteFinal - @limiteInicial + 1, (SELECT COUNT(*) FROM Curso)) ROWS ONLY;
	END
	END

	-------------- Estudiantes Inscritos en un curso -------------------

	ELSE IF (@tabla = 'Especifico') AND (@accion = 'ConsultarIncritosByCurso')
	BEGIN
	IF (@id_curso IS NOT NULL)

		SELECT COUNT(*) as total_registros
		FROM Estudiante
		JOIN Inscripcion ON Estudiante.id_estudiante = Inscripcion.id_estudiante
		WHERE Inscripcion.id_curso = @id_curso	

	BEGIN
	SELECT Estudiante.*
	FROM Estudiante
	JOIN Inscripcion ON Estudiante.id_estudiante = Inscripcion.id_estudiante
	WHERE Inscripcion.id_curso = @id_curso
			ORDER BY Estudiante.id_estudiante
			OFFSET COALESCE(@limiteInicial-1, 0) ROWS
			FETCH NEXT COALESCE(@limiteFinal - @limiteInicial + 1, (SELECT COUNT(*) FROM Estudiante)) ROWS ONLY;
	END
	END

END