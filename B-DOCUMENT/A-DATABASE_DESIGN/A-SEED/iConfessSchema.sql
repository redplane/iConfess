/****** Object:  Table [dbo].[Account]    Script Date: 12/11/2016 4:41:08 PM ******/
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
/****** Object:  Table [dbo].[Category]    Script Date: 12/11/2016 4:41:09 PM ******/
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
/****** Object:  Table [dbo].[Comment]    Script Date: 12/11/2016 4:41:09 PM ******/
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
/****** Object:  Table [dbo].[CommentReport]    Script Date: 12/11/2016 4:41:09 PM ******/
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
/****** Object:  Table [dbo].[FollowCategory]    Script Date: 12/11/2016 4:41:09 PM ******/
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
/****** Object:  Table [dbo].[FollowPost]    Script Date: 12/11/2016 4:41:09 PM ******/
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
/****** Object:  Table [dbo].[NotificationComment]    Script Date: 12/11/2016 4:41:09 PM ******/
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
/****** Object:  Table [dbo].[NotificationPost]    Script Date: 12/11/2016 4:41:09 PM ******/
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
/****** Object:  Table [dbo].[Post]    Script Date: 12/11/2016 4:41:09 PM ******/
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
/****** Object:  Table [dbo].[PostReport]    Script Date: 12/11/2016 4:41:09 PM ******/
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
/****** Object:  Table [dbo].[SignalrConnection]    Script Date: 12/11/2016 4:41:09 PM ******/
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
