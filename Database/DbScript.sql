USE [master]
GO
/****** Object:  Database [EventBookingDB]    Script Date: 13.12.2023 14:30:46 ******/
CREATE DATABASE [EventBookingDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EventBookingDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\EventBookingDB.mdf', SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EventBookingDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\EventBookingDB_log.ldf', SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [EventBookingDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EventBookingDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EventBookingDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EventBookingDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EventBookingDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EventBookingDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EventBookingDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [EventBookingDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EventBookingDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EventBookingDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EventBookingDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EventBookingDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EventBookingDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EventBookingDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EventBookingDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EventBookingDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EventBookingDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [EventBookingDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EventBookingDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EventBookingDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EventBookingDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EventBookingDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EventBookingDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EventBookingDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EventBookingDB] SET RECOVERY FULL 
GO
ALTER DATABASE [EventBookingDB] SET  MULTI_USER 
GO
ALTER DATABASE [EventBookingDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EventBookingDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EventBookingDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EventBookingDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EventBookingDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EventBookingDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'EventBookingDB', N'ON'
GO
ALTER DATABASE [EventBookingDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [EventBookingDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [EventBookingDB]
GO
/****** Object:  Schema [EventBookingSchema]    Script Date: 13.12.2023 14:30:46 ******/
CREATE SCHEMA [EventBookingSchema]
GO
/****** Object:  Table [EventBookingSchema].[EventRating]    Script Date: 13.12.2023 14:30:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [EventBookingSchema].[EventRating](
	[EventId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Rating] [int] NULL,
	[Comment] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserEventMapping] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [EventBookingSchema].[Events]    Script Date: 13.12.2023 14:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [EventBookingSchema].[Events](
	[EventId] [int] IDENTITY(1,1) NOT NULL,
	[OrganizerId] [int] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [EventBookingSchema].[EventUser]    Script Date: 13.12.2023 14:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [EventBookingSchema].[EventUser](
	[UserId] [int] NOT NULL,
	[EventId] [int] NOT NULL,
 CONSTRAINT [PK_EventUser] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [EventBookingSchema].[Roles]    Script Date: 13.12.2023 14:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [EventBookingSchema].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [EventBookingSchema].[Users]    Script Date: 13.12.2023 14:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [EventBookingSchema].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [EventBookingSchema].[EventRating]  WITH CHECK ADD FOREIGN KEY([EventId])
REFERENCES [EventBookingSchema].[Events] ([EventId])
GO
ALTER TABLE [EventBookingSchema].[EventRating]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [EventBookingSchema].[Users] ([UserId])
GO
ALTER TABLE [EventBookingSchema].[Events]  WITH CHECK ADD FOREIGN KEY([OrganizerId])
REFERENCES [EventBookingSchema].[Users] ([UserId])
GO
ALTER TABLE [EventBookingSchema].[EventUser]  WITH CHECK ADD FOREIGN KEY([EventId])
REFERENCES [EventBookingSchema].[Events] ([EventId])
GO
ALTER TABLE [EventBookingSchema].[EventUser]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [EventBookingSchema].[Users] ([UserId])
GO
ALTER TABLE [EventBookingSchema].[Users]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [EventBookingSchema].[Roles] ([RoleId])
GO
ALTER TABLE [EventBookingSchema].[EventRating]  WITH CHECK ADD  CONSTRAINT [CHK_RatingRange] CHECK  (([Rating]>=(1) AND [Rating]<=(10)))
GO
ALTER TABLE [EventBookingSchema].[EventRating] CHECK CONSTRAINT [CHK_RatingRange]
GO
/****** Object:  StoredProcedure [EventBookingSchema].[spEventRatings_Insert]    Script Date: 13.12.2023 14:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [EventBookingSchema].[spEventRatings_Insert]
    @EventId INT,
    @UserId INT,
    @Rating INT,
    @Comment NVARCHAR(MAX) = ''
AS
BEGIN
    -- Only users that have participated in an event can leave ratings.
    -- A rating can only be created after the event's end date
    IF EXISTS (
        SELECT *
        FROM EventBookingSchema.Events e
        INNER JOIN EventBookingSchema.EventUser eu ON e.EventId = eu.EventId
        WHERE GETDATE() > EndDate AND e.EventId = @EventId AND UserId = @UserId
    )
    BEGIN
        INSERT INTO EventBookingSchema.EventRating (
            EventId,
            UserId,
            Rating,
            Comment
        ) VALUES (
            @EventId,
            @UserId,
            @Rating,
            @Comment
        )
    END
END;
GO
/****** Object:  StoredProcedure [EventBookingSchema].[spEvents_Insert]    Script Date: 13.12.2023 14:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [EventBookingSchema].[spEvents_Insert]
    @OrganizerId INT,
    @Title NVARCHAR(50),
    @Description NVARCHAR(MAX),
    @StartDate DATETIME,
    @EndDate DATETIME
AS 
BEGIN
    -- Check if the user with the OrganizerId exists and if their role is either Organizer or Admin
    IF EXISTS (
        SELECT * 
        FROM EventBookingSchema.Users
        WHERE UserId = @OrganizerId AND RoleId > 1
    )
    BEGIN
        INSERT INTO EventBookingSchema.Events (
            OrganizerId,
            Title,
            [Description],
            StartDate,
            EndDate
        ) VALUES (
            @OrganizerId,
            @Title,
            @Description,
            @StartDate,
            @EndDate
        )
    END
END;
GO
/****** Object:  StoredProcedure [EventBookingSchema].[spEvents_Update]    Script Date: 13.12.2023 14:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [EventBookingSchema].[spEvents_Update]
    @EventId INT,
    @OrganizerId INT,
    @Title NVARCHAR(50),
    @Description NVARCHAR(MAX),
    @StartDate DATETIME,
    @EndDate DATETIME
AS 
BEGIN
    -- Check if the user with the OrganizerId exists and if their role is either Organizer or Admin
    IF EXISTS (
        SELECT * 
        FROM EventBookingSchema.Users
        WHERE UserId = @OrganizerId AND RoleId > 1
    )
    BEGIN
        UPDATE EventBookingSchema.Events 
        SET OrganizerId = @OrganizerId,
            Title = @Title,
            [Description] = @Description,
            StartDate = @StartDate,
            EndDate = @EndDate
        WHERE EventId = @EventId
    END
END;
GO
/****** Object:  StoredProcedure [EventBookingSchema].[spUsers_Delete]    Script Date: 13.12.2023 14:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [EventBookingSchema].[spUsers_Delete]
    @UserId INT
AS
BEGIN
    DELETE FROM EventBookingSchema.EventUser
    WHERE UserId = @UserId;

    DELETE FROM EventBookingSchema.Users
    WHERE UserId = @UserId;
END;
GO

INSERT INTO EventBookingSchema.Roles (
    RoleName
) VALUES (
    'User'
)
GO
INSERT INTO EventBookingSchema.Roles (
    RoleName
) VALUES (
    'Organizer'
)
GO
INSERT INTO EventBookingSchema.Roles (
    RoleName
) VALUES (
    'Admin'
)
GO

USE [master]
GO
ALTER DATABASE [EventBookingDB] SET  READ_WRITE 
GO
