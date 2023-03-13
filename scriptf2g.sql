USE [master]
GO
/****** Object:  Database [F2GTRAINING]    Script Date: 13/03/2023 10:43:00 ******/
CREATE DATABASE [F2GTRAINING]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'F2GTRAINING', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.DESAROLLO\MSSQL\DATA\F2GTRAINING.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'F2GTRAINING_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.DESAROLLO\MSSQL\DATA\F2GTRAINING_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [F2GTRAINING] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [F2GTRAINING].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [F2GTRAINING] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [F2GTRAINING] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [F2GTRAINING] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [F2GTRAINING] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [F2GTRAINING] SET ARITHABORT OFF 
GO
ALTER DATABASE [F2GTRAINING] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [F2GTRAINING] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [F2GTRAINING] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [F2GTRAINING] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [F2GTRAINING] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [F2GTRAINING] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [F2GTRAINING] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [F2GTRAINING] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [F2GTRAINING] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [F2GTRAINING] SET  DISABLE_BROKER 
GO
ALTER DATABASE [F2GTRAINING] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [F2GTRAINING] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [F2GTRAINING] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [F2GTRAINING] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [F2GTRAINING] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [F2GTRAINING] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [F2GTRAINING] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [F2GTRAINING] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [F2GTRAINING] SET  MULTI_USER 
GO
ALTER DATABASE [F2GTRAINING] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [F2GTRAINING] SET DB_CHAINING OFF 
GO
ALTER DATABASE [F2GTRAINING] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [F2GTRAINING] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [F2GTRAINING] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [F2GTRAINING] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [F2GTRAINING] SET QUERY_STORE = ON
GO
ALTER DATABASE [F2GTRAINING] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [F2GTRAINING]
GO
/****** Object:  Table [dbo].[ENTRENAMIENTOS]    Script Date: 13/03/2023 10:43:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ENTRENAMIENTOS](
	[ID] [int] NOT NULL,
	[IDEQUIPO] [int] NOT NULL,
	[FECHA_INICIO] [datetime] NULL,
	[FECHA_FIN] [datetime] NULL,
	[ACTIVO] [nvarchar](2) NOT NULL,
	[NOMBRE] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_SESIONES] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EQUIPOS]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EQUIPOS](
	[ID] [int] NOT NULL,
	[IDUSUARIO] [int] NOT NULL,
	[NOMBRE] [nvarchar](100) NOT NULL,
	[IMAGEN] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_EQUIPOS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ESTADISTICAS]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ESTADISTICAS](
	[ID] [int] NOT NULL,
	[IDJUGADOR] [int] NOT NULL,
	[RITMO_GKSALTO] [int] NULL,
	[TIRO_GKPARADA] [int] NULL,
	[PASE_GKSAQUE] [int] NULL,
	[REGATE_GKREFLEJO] [int] NULL,
	[DEFENSA_GKVELOCIDAD] [int] NULL,
	[FISICO_GKPOSICION] [int] NULL,
	[TOTAL_RITMO_GKSALTO] [int] NULL,
	[TOTAL_TIRO_GKPARADA] [int] NULL,
	[TOTAL_PASE_GKSAQUE] [int] NULL,
	[TOTAL_REGATE_GKREFLEJO] [int] NULL,
	[TOTAL_DEFENSA_GKVELOCIDAD] [int] NULL,
	[TOTAL_FISICO_GKPOSICION] [int] NULL,
 CONSTRAINT [PK_ESTADISTICAS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JUGADORES]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JUGADORES](
	[ID] [int] NOT NULL,
	[IDEQUIPO] [int] NOT NULL,
	[IDPOSICION] [int] NOT NULL,
	[NOMBRE] [nvarchar](100) NOT NULL,
	[DORSAL] [int] NOT NULL,
	[EDAD] [int] NULL,
	[PESO] [int] NULL,
	[ALTURA] [int] NULL,
 CONSTRAINT [PK_JUGADORES] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JUGADORES_ENTRENAMIENTO]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JUGADORES_ENTRENAMIENTO](
	[ID] [int] NOT NULL,
	[IDJUGADOR] [int] NOT NULL,
	[IDENTRENAMIENTO] [int] NOT NULL,
	[RITMO_GKSALTO] [int] NULL,
	[TIRO_GKPARADA] [int] NULL,
	[PASE_GKSAQUE] [int] NULL,
	[REGATE_GKREFLEJO] [int] NULL,
	[DEFENSA_GKVELOCIDAD] [int] NULL,
	[FISICO_GKPOSICION] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POSICIONES]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POSICIONES](
	[ID] [int] NOT NULL,
	[POSICION] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_POSICIONES] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USUARIOS]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIOS](
	[ID] [int] NOT NULL,
	[NOM_USUARIO] [nvarchar](50) NOT NULL,
	[CORREO] [nvarchar](100) NOT NULL,
	[CONTRASENIA] [nvarchar](50) NOT NULL,
	[TELEFONO] [int] NULL,
	[TOKEN] [nvarchar](100) NULL,
 CONSTRAINT [PK_USUARIOS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[EQUIPOS] ([ID], [IDUSUARIO], [NOMBRE], [IMAGEN]) VALUES (1, 2, N'asd', N'~/images/equipos/asd.png')
INSERT [dbo].[EQUIPOS] ([ID], [IDUSUARIO], [NOMBRE], [IMAGEN]) VALUES (2, 2, N'ASD', N'~/images/equipos/asd-185.png')
INSERT [dbo].[EQUIPOS] ([ID], [IDUSUARIO], [NOMBRE], [IMAGEN]) VALUES (3, 2, N'asdf', N'~/images/equipos/asdf.png')
GO
INSERT [dbo].[JUGADORES] ([ID], [IDEQUIPO], [IDPOSICION], [NOMBRE], [DORSAL], [EDAD], [PESO], [ALTURA]) VALUES (1, 1, 1, N'SWEETALE', 44, 123, 123, 123)
INSERT [dbo].[JUGADORES] ([ID], [IDEQUIPO], [IDPOSICION], [NOMBRE], [DORSAL], [EDAD], [PESO], [ALTURA]) VALUES (2, 2, 1, N'Fernandito Delafuente', 12, 123, 123, 123)
GO
INSERT [dbo].[POSICIONES] ([ID], [POSICION]) VALUES (1, N'Portero')
INSERT [dbo].[POSICIONES] ([ID], [POSICION]) VALUES (2, N'Lateral izquierdo')
INSERT [dbo].[POSICIONES] ([ID], [POSICION]) VALUES (3, N'Lateral derecho')
INSERT [dbo].[POSICIONES] ([ID], [POSICION]) VALUES (4, N'Central')
INSERT [dbo].[POSICIONES] ([ID], [POSICION]) VALUES (5, N'Libero')
INSERT [dbo].[POSICIONES] ([ID], [POSICION]) VALUES (6, N'Mediocentro Defensivo')
INSERT [dbo].[POSICIONES] ([ID], [POSICION]) VALUES (7, N'Mediocentro')
INSERT [dbo].[POSICIONES] ([ID], [POSICION]) VALUES (8, N'Mediocentro Ofensivo')
INSERT [dbo].[POSICIONES] ([ID], [POSICION]) VALUES (9, N'Banda izquierda')
INSERT [dbo].[POSICIONES] ([ID], [POSICION]) VALUES (10, N'Banda derecha')
INSERT [dbo].[POSICIONES] ([ID], [POSICION]) VALUES (11, N'Segundo delantero')
INSERT [dbo].[POSICIONES] ([ID], [POSICION]) VALUES (12, N'Delantero centro')
INSERT [dbo].[POSICIONES] ([ID], [POSICION]) VALUES (13, N'Extremo izquierdo')
INSERT [dbo].[POSICIONES] ([ID], [POSICION]) VALUES (14, N'Extremo derecho')
GO
INSERT [dbo].[USUARIOS] ([ID], [NOM_USUARIO], [CORREO], [CONTRASENIA], [TELEFONO], [TOKEN]) VALUES (1, N'FGARCIA211', N'oxtreaz@gmail.com', N'PapaFrita123', 636969939, NULL)
INSERT [dbo].[USUARIOS] ([ID], [NOM_USUARIO], [CORREO], [CONTRASENIA], [TELEFONO], [TOKEN]) VALUES (2, N'Fernandito011', N'vandidoalo@gmail.com', N'ASDASDASD', 634020201, NULL)
GO
ALTER TABLE [dbo].[ENTRENAMIENTOS]  WITH CHECK ADD  CONSTRAINT [FK_SESIONES_EQUIPOS] FOREIGN KEY([IDEQUIPO])
REFERENCES [dbo].[EQUIPOS] ([ID])
GO
ALTER TABLE [dbo].[ENTRENAMIENTOS] CHECK CONSTRAINT [FK_SESIONES_EQUIPOS]
GO
ALTER TABLE [dbo].[EQUIPOS]  WITH CHECK ADD  CONSTRAINT [FK_EQUIPOS_USUARIOS] FOREIGN KEY([IDUSUARIO])
REFERENCES [dbo].[USUARIOS] ([ID])
GO
ALTER TABLE [dbo].[EQUIPOS] CHECK CONSTRAINT [FK_EQUIPOS_USUARIOS]
GO
ALTER TABLE [dbo].[ESTADISTICAS]  WITH CHECK ADD  CONSTRAINT [FK_ESTADISTICAS_JUGADORES] FOREIGN KEY([IDJUGADOR])
REFERENCES [dbo].[JUGADORES] ([ID])
GO
ALTER TABLE [dbo].[ESTADISTICAS] CHECK CONSTRAINT [FK_ESTADISTICAS_JUGADORES]
GO
ALTER TABLE [dbo].[JUGADORES]  WITH CHECK ADD  CONSTRAINT [FK_JUGADORES_EQUIPOS] FOREIGN KEY([IDEQUIPO])
REFERENCES [dbo].[EQUIPOS] ([ID])
GO
ALTER TABLE [dbo].[JUGADORES] CHECK CONSTRAINT [FK_JUGADORES_EQUIPOS]
GO
ALTER TABLE [dbo].[JUGADORES]  WITH CHECK ADD  CONSTRAINT [FK_JUGADORES_POSICIONES] FOREIGN KEY([IDPOSICION])
REFERENCES [dbo].[POSICIONES] ([ID])
GO
ALTER TABLE [dbo].[JUGADORES] CHECK CONSTRAINT [FK_JUGADORES_POSICIONES]
GO
ALTER TABLE [dbo].[JUGADORES_ENTRENAMIENTO]  WITH CHECK ADD  CONSTRAINT [FK_JUGADORES_ENTRENAMIENTO_ENTRENAMIENTOS] FOREIGN KEY([IDENTRENAMIENTO])
REFERENCES [dbo].[ENTRENAMIENTOS] ([ID])
GO
ALTER TABLE [dbo].[JUGADORES_ENTRENAMIENTO] CHECK CONSTRAINT [FK_JUGADORES_ENTRENAMIENTO_ENTRENAMIENTOS]
GO
ALTER TABLE [dbo].[JUGADORES_ENTRENAMIENTO]  WITH CHECK ADD  CONSTRAINT [FK_JUGADORES_ENTRENAMIENTO_JUGADORES] FOREIGN KEY([IDJUGADOR])
REFERENCES [dbo].[JUGADORES] ([ID])
GO
ALTER TABLE [dbo].[JUGADORES_ENTRENAMIENTO] CHECK CONSTRAINT [FK_JUGADORES_ENTRENAMIENTO_JUGADORES]
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_JUGADOR_ID]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[SP_DELETE_JUGADOR_ID] (@IDJUGADOR INT)
AS
    DELETE FROM JUGADORES
	WHERE ID = @IDJUGADOR
GO
/****** Object:  StoredProcedure [dbo].[SP_FIND_CORREO]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[SP_FIND_CORREO] (@CORREO NVARCHAR(50))
AS
	SELECT ID,NOM_USUARIO,CORREO,CONTRASENIA,TELEFONO,ISNULL(TOKEN,'SIN TOKEN') AS TOKEN FROM USUARIOS
	WHERE CORREO = @CORREO
GO
/****** Object:  StoredProcedure [dbo].[SP_FIND_EQUIPO_ID]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[SP_FIND_EQUIPO_ID] (@IDEQUIPO INT)
AS
	SELECT * FROM EQUIPOS
	WHERE ID = @IDEQUIPO
GO
/****** Object:  StoredProcedure [dbo].[SP_FIND_EQUIPOS_USER]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[SP_FIND_EQUIPOS_USER] (@IDUSER INT)
AS
	SELECT * FROM EQUIPOS
	WHERE IDUSUARIO = @IDUSER
GO
/****** Object:  StoredProcedure [dbo].[SP_FIND_JUGADOR_ID]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[SP_FIND_JUGADOR_ID] (@IDJUGADOR INT)
AS
	SELECT * FROM JUGADORES
	WHERE ID = @IDJUGADOR
GO
/****** Object:  StoredProcedure [dbo].[SP_FIND_JUGADORES_IDEQUIPO]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[SP_FIND_JUGADORES_IDEQUIPO] (@IDEQUIPO INT)
AS
	SELECT * FROM JUGADORES
	WHERE IDEQUIPO = @IDEQUIPO
GO
/****** Object:  StoredProcedure [dbo].[SP_FIND_NOM_USUARIO]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[SP_FIND_NOM_USUARIO] (@NOMBRE NVARCHAR(50))
AS
	SELECT ID,NOM_USUARIO,CORREO,CONTRASENIA,TELEFONO,ISNULL(TOKEN,'SIN TOKEN') AS TOKEN FROM USUARIOS
	WHERE NOM_USUARIO = @NOMBRE
GO
/****** Object:  StoredProcedure [dbo].[SP_FIND_POSITIONS]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[SP_FIND_POSITIONS]
AS
	SELECT * FROM POSICIONES
GO
/****** Object:  StoredProcedure [dbo].[SP_FIND_TELEFONO]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[SP_FIND_TELEFONO] (@TELEFONO INT)
AS
	SELECT ID,NOM_USUARIO,CORREO,CONTRASENIA,TELEFONO,ISNULL(TOKEN,'SIN TOKEN') AS TOKEN FROM USUARIOS
	WHERE TELEFONO = @TELEFONO
GO
/****** Object:  StoredProcedure [dbo].[SP_FIND_TOKEN]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[SP_FIND_TOKEN] (@TOKEN NVARCHAR(100))
AS
	SELECT ID,NOM_USUARIO,CORREO,CONTRASENIA,TELEFONO,ISNULL(TOKEN,'SIN TOKEN') AS TOKEN FROM USUARIOS
	WHERE TOKEN = @TOKEN
GO
/****** Object:  StoredProcedure [dbo].[SP_FIND_USUARIO]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[SP_FIND_USUARIO] (@NOMBRE NVARCHAR(50), @CONTRASENIA NVARCHAR(50))
AS
	SELECT ID,NOM_USUARIO,CORREO,CONTRASENIA,TELEFONO,ISNULL(TOKEN,'SIN TOKEN') AS TOKEN FROM USUARIOS
	WHERE NOM_USUARIO = @NOMBRE AND CONTRASENIA = @CONTRASENIA
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_EQUIPO]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[SP_INSERT_EQUIPO] (@IDUSER INT, @NOMBRE NVARCHAR(50),@IMAGEN NVARCHAR(1000))
AS
	INSERT INTO EQUIPOS VALUES ((SELECT ISNULL(MAX(ID),0) FROM EQUIPOS)+1,@IDUSER,@NOMBRE,@IMAGEN)
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_JUGADOR]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[SP_INSERT_JUGADOR] (@IDEQUIPO INT, @IDPOSICION INT, @NOMBRE NVARCHAR(100), @DORSAL INT, @EDAD INT, @PESO FLOAT, @ALTURA FLOAT)
AS
    INSERT INTO JUGADORES VALUES (
	(SELECT ISNULL(MAX(ID),0) FROM JUGADORES)+1,@IDEQUIPO,@IDPOSICION,@NOMBRE,@DORSAL,@EDAD,@PESO,@ALTURA)
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_USUARIO]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[SP_INSERT_USUARIO] (@NOMBRE NVARCHAR(50),@CORREO NVARCHAR(100), @CONTRASENIA NVARCHAR(50), @TELEFONO INT)
AS
	INSERT INTO USUARIOS VALUES ((SELECT ISNULL(MAX(ID),0) FROM USUARIOS)+1,@NOMBRE,@CORREO,@CONTRASENIA,@TELEFONO,NULL)
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_TOKEN]    Script Date: 13/03/2023 10:43:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[SP_UPDATE_TOKEN] (@OLDTOKEN NVARCHAR(100), @NEWTOKEN NVARCHAR(100))
AS
	UPDATE USUARIOS SET TOKEN = @NEWTOKEN WHERE TOKEN = @OLDTOKEN
GO
USE [master]
GO
ALTER DATABASE [F2GTRAINING] SET  READ_WRITE 
GO
