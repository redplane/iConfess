USE [master]
GO
/****** Object:  Database [iConfess]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE DATABASE [iConfess]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'iConfess', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\iConfess.mdf' , SIZE = 5312KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'iConfess_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\iConfess_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [iConfess] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [iConfess].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [iConfess] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [iConfess] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [iConfess] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [iConfess] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [iConfess] SET ARITHABORT OFF 
GO
ALTER DATABASE [iConfess] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [iConfess] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [iConfess] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [iConfess] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [iConfess] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [iConfess] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [iConfess] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [iConfess] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [iConfess] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [iConfess] SET  ENABLE_BROKER 
GO
ALTER DATABASE [iConfess] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [iConfess] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [iConfess] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [iConfess] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [iConfess] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [iConfess] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [iConfess] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [iConfess] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [iConfess] SET  MULTI_USER 
GO
ALTER DATABASE [iConfess] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [iConfess] SET DB_CHAINING OFF 
GO
ALTER DATABASE [iConfess] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [iConfess] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [iConfess] SET DELAYED_DURABILITY = DISABLED 
GO
USE [iConfess]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 12/11/2016 4:10:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Account]    Script Date: 12/11/2016 4:10:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Nickname] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[Role] [int] NOT NULL,
	[PhotoRelativeUrl] [nvarchar](max) NULL,
	[PhotoAbsoluteUrl] [nvarchar](max) NULL,
	[Joined] [float] NOT NULL,
	[LastModified] [float] NULL,
 CONSTRAINT [PK_dbo.Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 12/11/2016 4:10:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatorIndex] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Created] [float] NOT NULL,
	[LastModified] [float] NULL,
 CONSTRAINT [PK_dbo.Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Comment]    Script Date: 12/11/2016 4:10:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OwnerIndex] [int] NOT NULL,
	[PostIndex] [int] NOT NULL,
	[Content] [nvarchar](max) NULL,
	[Created] [float] NOT NULL,
	[LastModified] [float] NULL,
 CONSTRAINT [PK_dbo.Comment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CommentReport]    Script Date: 12/11/2016 4:10:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommentReport](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CommentIndex] [int] NOT NULL,
	[PostIndex] [int] NOT NULL,
	[CommentOwnerIndex] [int] NOT NULL,
	[CommentReporterIndex] [int] NOT NULL,
	[Body] [nvarchar](max) NULL,
	[Reason] [nvarchar](max) NULL,
	[Created] [float] NOT NULL,
 CONSTRAINT [PK_dbo.CommentReport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FollowCategory]    Script Date: 12/11/2016 4:10:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FollowCategory](
	[OwnerIndex] [int] NOT NULL,
	[CategoryIndex] [int] NOT NULL,
	[Id] [int] NOT NULL,
	[Created] [float] NOT NULL,
 CONSTRAINT [PK_dbo.FollowCategory] PRIMARY KEY CLUSTERED 
(
	[OwnerIndex] ASC,
	[CategoryIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FollowPost]    Script Date: 12/11/2016 4:10:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FollowPost](
	[FollowerIndex] [int] NOT NULL,
	[PostIndex] [int] NOT NULL,
	[Created] [float] NOT NULL,
 CONSTRAINT [PK_dbo.FollowPost] PRIMARY KEY CLUSTERED 
(
	[FollowerIndex] ASC,
	[PostIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NotificationComment]    Script Date: 12/11/2016 4:10:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationComment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CommentIndex] [int] NOT NULL,
	[PostIndex] [int] NOT NULL,
	[RecipientIndex] [int] NOT NULL,
	[BroadcasterIndex] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[IsSeen] [bit] NOT NULL,
	[Created] [float] NOT NULL,
 CONSTRAINT [PK_dbo.NotificationComment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NotificationPost]    Script Date: 12/11/2016 4:10:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationPost](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PostIndex] [int] NOT NULL,
	[RecipientIndex] [int] NOT NULL,
	[BroadcasterIndex] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[IsSeen] [bit] NOT NULL,
	[Created] [float] NOT NULL,
 CONSTRAINT [PK_dbo.NotificationPost] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Post]    Script Date: 12/11/2016 4:10:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OwnerIndex] [int] NOT NULL,
	[CategoryIndex] [int] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[Created] [float] NOT NULL,
	[LastModified] [float] NULL,
 CONSTRAINT [PK_dbo.Post] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PostReport]    Script Date: 12/11/2016 4:10:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostReport](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PostIndex] [int] NOT NULL,
	[PostOwnerIndex] [int] NOT NULL,
	[PostReporterIndex] [int] NOT NULL,
	[Body] [nvarchar](max) NULL,
	[Reason] [nvarchar](max) NULL,
	[Created] [float] NOT NULL,
 CONSTRAINT [PK_dbo.PostReport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SignalrConnection]    Script Date: 12/11/2016 4:10:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SignalrConnection](
	[Index] [nvarchar](128) NOT NULL,
	[OwnerIndex] [int] NOT NULL,
	[Created] [float] NOT NULL,
 CONSTRAINT [PK_dbo.SignalrConnection] PRIMARY KEY CLUSTERED 
(
	[Index] ASC,
	[OwnerIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [IX_CreatorIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_CreatorIndex] ON [dbo].[Category]
(
	[CreatorIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_OwnerIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_OwnerIndex] ON [dbo].[Comment]
(
	[OwnerIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PostIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_PostIndex] ON [dbo].[Comment]
(
	[PostIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CommentIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_CommentIndex] ON [dbo].[CommentReport]
(
	[CommentIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CommentOwnerIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_CommentOwnerIndex] ON [dbo].[CommentReport]
(
	[CommentOwnerIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CommentReporterIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_CommentReporterIndex] ON [dbo].[CommentReport]
(
	[CommentReporterIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PostIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_PostIndex] ON [dbo].[CommentReport]
(
	[PostIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CategoryIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_CategoryIndex] ON [dbo].[FollowCategory]
(
	[CategoryIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_OwnerIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_OwnerIndex] ON [dbo].[FollowCategory]
(
	[OwnerIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FollowerIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_FollowerIndex] ON [dbo].[FollowPost]
(
	[FollowerIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PostIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_PostIndex] ON [dbo].[FollowPost]
(
	[PostIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BroadcasterIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_BroadcasterIndex] ON [dbo].[NotificationComment]
(
	[BroadcasterIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CommentIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_CommentIndex] ON [dbo].[NotificationComment]
(
	[CommentIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PostIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_PostIndex] ON [dbo].[NotificationComment]
(
	[PostIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RecipientIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_RecipientIndex] ON [dbo].[NotificationComment]
(
	[RecipientIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BroadcasterIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_BroadcasterIndex] ON [dbo].[NotificationPost]
(
	[BroadcasterIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PostIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_PostIndex] ON [dbo].[NotificationPost]
(
	[PostIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RecipientIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_RecipientIndex] ON [dbo].[NotificationPost]
(
	[RecipientIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CategoryIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_CategoryIndex] ON [dbo].[Post]
(
	[CategoryIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_OwnerIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_OwnerIndex] ON [dbo].[Post]
(
	[OwnerIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PostIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_PostIndex] ON [dbo].[PostReport]
(
	[PostIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PostOwnerIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_PostOwnerIndex] ON [dbo].[PostReport]
(
	[PostOwnerIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PostReporterIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_PostReporterIndex] ON [dbo].[PostReport]
(
	[PostReporterIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_OwnerIndex]    Script Date: 12/11/2016 4:10:42 PM ******/
CREATE NONCLUSTERED INDEX [IX_OwnerIndex] ON [dbo].[SignalrConnection]
(
	[OwnerIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Category_dbo.Account_CreatorIndex] FOREIGN KEY([CreatorIndex])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_dbo.Category_dbo.Account_CreatorIndex]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Comment_dbo.Account_OwnerIndex] FOREIGN KEY([OwnerIndex])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_dbo.Comment_dbo.Account_OwnerIndex]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Comment_dbo.Post_PostIndex] FOREIGN KEY([PostIndex])
REFERENCES [dbo].[Post] ([Id])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_dbo.Comment_dbo.Post_PostIndex]
GO
ALTER TABLE [dbo].[CommentReport]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CommentReport_dbo.Account_CommentOwnerIndex] FOREIGN KEY([CommentOwnerIndex])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[CommentReport] CHECK CONSTRAINT [FK_dbo.CommentReport_dbo.Account_CommentOwnerIndex]
GO
ALTER TABLE [dbo].[CommentReport]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CommentReport_dbo.Account_CommentReporterIndex] FOREIGN KEY([CommentReporterIndex])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[CommentReport] CHECK CONSTRAINT [FK_dbo.CommentReport_dbo.Account_CommentReporterIndex]
GO
ALTER TABLE [dbo].[CommentReport]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CommentReport_dbo.Comment_CommentIndex] FOREIGN KEY([CommentIndex])
REFERENCES [dbo].[Comment] ([Id])
GO
ALTER TABLE [dbo].[CommentReport] CHECK CONSTRAINT [FK_dbo.CommentReport_dbo.Comment_CommentIndex]
GO
ALTER TABLE [dbo].[CommentReport]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CommentReport_dbo.Post_PostIndex] FOREIGN KEY([PostIndex])
REFERENCES [dbo].[Post] ([Id])
GO
ALTER TABLE [dbo].[CommentReport] CHECK CONSTRAINT [FK_dbo.CommentReport_dbo.Post_PostIndex]
GO
ALTER TABLE [dbo].[FollowCategory]  WITH CHECK ADD  CONSTRAINT [FK_dbo.FollowCategory_dbo.Account_OwnerIndex] FOREIGN KEY([OwnerIndex])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[FollowCategory] CHECK CONSTRAINT [FK_dbo.FollowCategory_dbo.Account_OwnerIndex]
GO
ALTER TABLE [dbo].[FollowCategory]  WITH CHECK ADD  CONSTRAINT [FK_dbo.FollowCategory_dbo.Category_CategoryIndex] FOREIGN KEY([CategoryIndex])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[FollowCategory] CHECK CONSTRAINT [FK_dbo.FollowCategory_dbo.Category_CategoryIndex]
GO
ALTER TABLE [dbo].[FollowPost]  WITH CHECK ADD  CONSTRAINT [FK_dbo.FollowPost_dbo.Account_FollowerIndex] FOREIGN KEY([FollowerIndex])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[FollowPost] CHECK CONSTRAINT [FK_dbo.FollowPost_dbo.Account_FollowerIndex]
GO
ALTER TABLE [dbo].[FollowPost]  WITH CHECK ADD  CONSTRAINT [FK_dbo.FollowPost_dbo.Post_PostIndex] FOREIGN KEY([PostIndex])
REFERENCES [dbo].[Post] ([Id])
GO
ALTER TABLE [dbo].[FollowPost] CHECK CONSTRAINT [FK_dbo.FollowPost_dbo.Post_PostIndex]
GO
ALTER TABLE [dbo].[NotificationComment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.NotificationComment_dbo.Account_BroadcasterIndex] FOREIGN KEY([BroadcasterIndex])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[NotificationComment] CHECK CONSTRAINT [FK_dbo.NotificationComment_dbo.Account_BroadcasterIndex]
GO
ALTER TABLE [dbo].[NotificationComment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.NotificationComment_dbo.Account_RecipientIndex] FOREIGN KEY([RecipientIndex])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[NotificationComment] CHECK CONSTRAINT [FK_dbo.NotificationComment_dbo.Account_RecipientIndex]
GO
ALTER TABLE [dbo].[NotificationComment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.NotificationComment_dbo.Comment_CommentIndex] FOREIGN KEY([CommentIndex])
REFERENCES [dbo].[Comment] ([Id])
GO
ALTER TABLE [dbo].[NotificationComment] CHECK CONSTRAINT [FK_dbo.NotificationComment_dbo.Comment_CommentIndex]
GO
ALTER TABLE [dbo].[NotificationComment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.NotificationComment_dbo.Post_PostIndex] FOREIGN KEY([PostIndex])
REFERENCES [dbo].[Post] ([Id])
GO
ALTER TABLE [dbo].[NotificationComment] CHECK CONSTRAINT [FK_dbo.NotificationComment_dbo.Post_PostIndex]
GO
ALTER TABLE [dbo].[NotificationPost]  WITH CHECK ADD  CONSTRAINT [FK_dbo.NotificationPost_dbo.Account_BroadcasterIndex] FOREIGN KEY([BroadcasterIndex])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[NotificationPost] CHECK CONSTRAINT [FK_dbo.NotificationPost_dbo.Account_BroadcasterIndex]
GO
ALTER TABLE [dbo].[NotificationPost]  WITH CHECK ADD  CONSTRAINT [FK_dbo.NotificationPost_dbo.Account_RecipientIndex] FOREIGN KEY([RecipientIndex])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[NotificationPost] CHECK CONSTRAINT [FK_dbo.NotificationPost_dbo.Account_RecipientIndex]
GO
ALTER TABLE [dbo].[NotificationPost]  WITH CHECK ADD  CONSTRAINT [FK_dbo.NotificationPost_dbo.Post_PostIndex] FOREIGN KEY([PostIndex])
REFERENCES [dbo].[Post] ([Id])
GO
ALTER TABLE [dbo].[NotificationPost] CHECK CONSTRAINT [FK_dbo.NotificationPost_dbo.Post_PostIndex]
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Post_dbo.Account_OwnerIndex] FOREIGN KEY([OwnerIndex])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_dbo.Post_dbo.Account_OwnerIndex]
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Post_dbo.Category_CategoryIndex] FOREIGN KEY([CategoryIndex])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_dbo.Post_dbo.Category_CategoryIndex]
GO
ALTER TABLE [dbo].[PostReport]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PostReport_dbo.Account_PostOwnerIndex] FOREIGN KEY([PostOwnerIndex])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[PostReport] CHECK CONSTRAINT [FK_dbo.PostReport_dbo.Account_PostOwnerIndex]
GO
ALTER TABLE [dbo].[PostReport]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PostReport_dbo.Account_PostReporterIndex] FOREIGN KEY([PostReporterIndex])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[PostReport] CHECK CONSTRAINT [FK_dbo.PostReport_dbo.Account_PostReporterIndex]
GO
ALTER TABLE [dbo].[PostReport]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PostReport_dbo.Post_PostIndex] FOREIGN KEY([PostIndex])
REFERENCES [dbo].[Post] ([Id])
GO
ALTER TABLE [dbo].[PostReport] CHECK CONSTRAINT [FK_dbo.PostReport_dbo.Post_PostIndex]
GO
ALTER TABLE [dbo].[SignalrConnection]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SignalrConnection_dbo.Account_OwnerIndex] FOREIGN KEY([OwnerIndex])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[SignalrConnection] CHECK CONSTRAINT [FK_dbo.SignalrConnection_dbo.Account_OwnerIndex]
GO
USE [master]
GO
ALTER DATABASE [iConfess] SET  READ_WRITE 
GO
