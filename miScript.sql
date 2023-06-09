USE [master]
GO
/****** Object:  Database [CursosSa]    Script Date: 10/04/2023 17:14:50 ******/
CREATE DATABASE [CursosSa]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CursosSa', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\CursosSa.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CursosSa_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\CursosSa_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [CursosSa] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CursosSa].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CursosSa] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CursosSa] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CursosSa] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CursosSa] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CursosSa] SET ARITHABORT OFF 
GO
ALTER DATABASE [CursosSa] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CursosSa] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CursosSa] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CursosSa] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CursosSa] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CursosSa] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CursosSa] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CursosSa] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CursosSa] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CursosSa] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CursosSa] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CursosSa] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CursosSa] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CursosSa] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CursosSa] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CursosSa] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CursosSa] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CursosSa] SET RECOVERY FULL 
GO
ALTER DATABASE [CursosSa] SET  MULTI_USER 
GO
ALTER DATABASE [CursosSa] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CursosSa] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CursosSa] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CursosSa] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CursosSa] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CursosSa] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'CursosSa', N'ON'
GO
ALTER DATABASE [CursosSa] SET QUERY_STORE = ON
GO
ALTER DATABASE [CursosSa] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [CursosSa]
GO
/****** Object:  User [boom]    Script Date: 10/04/2023 17:14:51 ******/
CREATE USER [boom] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [boom]
GO
/****** Object:  Table [dbo].[Curso]    Script Date: 10/04/2023 17:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Curso](
	[id_curso] [int] IDENTITY(1,1) NOT NULL,
	[nombre_curso] [varchar](255) NULL,
	[descripcion_curso] [varchar](500) NULL,
	[estado] [char](1) NULL,
	[fecha_creacion] [date] NOT NULL,
	[fecha_actualizacion] [datetime] NULL,
	[id_profesor] [int] NULL,
	[cupos_disponibles] [int] NULL,
	[fecha_finalizacion] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_curso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detalle_inscripcion]    Script Date: 10/04/2023 17:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalle_inscripcion](
	[id_detalle_inscripcion] [int] IDENTITY(1,1) NOT NULL,
	[id_inscripcion] [int] NOT NULL,
	[calificacion] [float] NOT NULL,
	[aprobado] [bit] NOT NULL,
 CONSTRAINT [PK_detalle_inscripcion] PRIMARY KEY CLUSTERED 
(
	[id_detalle_inscripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estudiante]    Script Date: 10/04/2023 17:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estudiante](
	[id_estudiante] [int] IDENTITY(1,1) NOT NULL,
	[nombre_estudiante] [varchar](255) NULL,
	[email_estudiante] [varchar](255) NULL,
	[estado] [char](1) NULL,
	[fecha_creacion] [date] NOT NULL,
	[fecha_actualizacion] [datetime] NULL,
	[apellido_estudiante] [varchar](255) NULL,
	[cedula] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_estudiante] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inscripcion]    Script Date: 10/04/2023 17:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inscripcion](
	[id_inscripcion] [int] IDENTITY(1,1) NOT NULL,
	[id_curso] [int] NULL,
	[id_estudiante] [int] NULL,
	[fecha_inscripcion] [date] NULL,
	[estado] [char](1) NULL,
	[fecha_actualizacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_inscripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MatCurso]    Script Date: 10/04/2023 17:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MatCurso](
	[id_matcurso] [int] IDENTITY(1,1) NOT NULL,
	[id_materia] [int] NOT NULL,
	[id_curso] [int] NOT NULL,
	[fecha_asignacion] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_matcurso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Materia]    Script Date: 10/04/2023 17:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Materia](
	[id_materia] [int] IDENTITY(1,1) NOT NULL,
	[nombre_materia] [varchar](255) NULL,
	[estado] [char](1) NULL,
	[fecha_creacion] [date] NOT NULL,
	[fecha_actualizacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_materia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profesor]    Script Date: 10/04/2023 17:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profesor](
	[id_profesor] [int] IDENTITY(1,1) NOT NULL,
	[nombre_profesor] [varchar](255) NULL,
	[email_profesor] [varchar](255) NULL,
	[estado] [char](1) NOT NULL,
	[fecha_creacion] [date] NOT NULL,
	[fecha_actualizacion] [datetime] NULL,
	[cedula] [varchar](10) NULL,
	[apellido_profesor] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_profesor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Curso] ADD  DEFAULT ('A') FOR [estado]
GO
ALTER TABLE [dbo].[Curso] ADD  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[Curso] ADD  DEFAULT (getdate()) FOR [fecha_actualizacion]
GO
ALTER TABLE [dbo].[Curso] ADD  DEFAULT ((28)) FOR [cupos_disponibles]
GO
ALTER TABLE [dbo].[Estudiante] ADD  DEFAULT ('A') FOR [estado]
GO
ALTER TABLE [dbo].[Estudiante] ADD  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[Estudiante] ADD  DEFAULT (getdate()) FOR [fecha_actualizacion]
GO
ALTER TABLE [dbo].[Inscripcion] ADD  CONSTRAINT [df_fecha_inscripcion]  DEFAULT (getdate()) FOR [fecha_inscripcion]
GO
ALTER TABLE [dbo].[Inscripcion] ADD  DEFAULT ('A') FOR [estado]
GO
ALTER TABLE [dbo].[Inscripcion] ADD  DEFAULT (getdate()) FOR [fecha_actualizacion]
GO
ALTER TABLE [dbo].[MatCurso] ADD  DEFAULT (getdate()) FOR [fecha_asignacion]
GO
ALTER TABLE [dbo].[Materia] ADD  DEFAULT ('A') FOR [estado]
GO
ALTER TABLE [dbo].[Materia] ADD  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[Materia] ADD  DEFAULT (getdate()) FOR [fecha_actualizacion]
GO
ALTER TABLE [dbo].[Profesor] ADD  DEFAULT ('A') FOR [estado]
GO
ALTER TABLE [dbo].[Profesor] ADD  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[Profesor] ADD  DEFAULT (getdate()) FOR [fecha_actualizacion]
GO
ALTER TABLE [dbo].[Curso]  WITH CHECK ADD  CONSTRAINT [fk_curso_profesor] FOREIGN KEY([id_profesor])
REFERENCES [dbo].[Profesor] ([id_profesor])
GO
ALTER TABLE [dbo].[Curso] CHECK CONSTRAINT [fk_curso_profesor]
GO
ALTER TABLE [dbo].[detalle_inscripcion]  WITH CHECK ADD  CONSTRAINT [FK_detalle_inscripcion_inscripcion] FOREIGN KEY([id_inscripcion])
REFERENCES [dbo].[Inscripcion] ([id_inscripcion])
GO
ALTER TABLE [dbo].[detalle_inscripcion] CHECK CONSTRAINT [FK_detalle_inscripcion_inscripcion]
GO
ALTER TABLE [dbo].[Inscripcion]  WITH CHECK ADD FOREIGN KEY([id_curso])
REFERENCES [dbo].[Curso] ([id_curso])
GO
ALTER TABLE [dbo].[Inscripcion]  WITH CHECK ADD FOREIGN KEY([id_estudiante])
REFERENCES [dbo].[Estudiante] ([id_estudiante])
GO
ALTER TABLE [dbo].[MatCurso]  WITH CHECK ADD  CONSTRAINT [FK_MatCurso_Curso] FOREIGN KEY([id_curso])
REFERENCES [dbo].[Curso] ([id_curso])
GO
ALTER TABLE [dbo].[MatCurso] CHECK CONSTRAINT [FK_MatCurso_Curso]
GO
ALTER TABLE [dbo].[MatCurso]  WITH CHECK ADD  CONSTRAINT [FK_MatCurso_Materia] FOREIGN KEY([id_materia])
REFERENCES [dbo].[Materia] ([id_materia])
GO
ALTER TABLE [dbo].[MatCurso] CHECK CONSTRAINT [FK_MatCurso_Materia]
GO
/****** Object:  StoredProcedure [dbo].[miProcedimiento]    Script Date: 10/04/2023 17:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[miProcedimiento]
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
GO
USE [master]
GO
ALTER DATABASE [CursosSa] SET  READ_WRITE 
GO
