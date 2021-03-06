USE [PTTSystem]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 2021/7/1 下午 02:24:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[userGUID] [uniqueidentifier] NOT NULL,
	[userName] [nvarchar](50) NULL,
	[userAKA] [nvarchar](50) NULL,
	[passWord] [varchar](200) NULL,
	[salt] [varchar](200) NULL,
	[familyName] [nvarchar](10) NULL,
	[lastName] [nvarchar](10) NULL,
	[birthday] [datetime] NOT NULL,
	[mail] [varchar](100) NULL,
	[errorDate] [datetime] NULL,
	[errorTimes] [int] NOT NULL,
	[isBanned] [bit] NOT NULL,
	[onlineDays] [int] NULL,
	[lastLoginDate] [datetime] NOT NULL,
	[createTime] [datetime] NOT NULL,
	[isdelete] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[userGUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Article]    Script Date: 2021/7/1 下午 02:24:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Article](
	[article_ID] [uniqueidentifier] NOT NULL,
	[article_title] [nvarchar](50) NOT NULL,
	[article_content] [nvarchar](max) NOT NULL,
	[popularity] [nvarchar](2) NULL,
	[href] [varchar](100) NULL,
	[user_GUID] [uniqueidentifier] NULL,
	[user_ID] [nvarchar](50) NULL,
	[block_ID] [uniqueidentifier] NULL,
	[isannouncemet] [bit] NULL,
	[date] [varchar](10) NULL,
	[createdate] [varchar](50) NULL,
	[datetime] [datetime] NULL,
	[isdelete] [bit] NULL,
 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
(
	[article_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Block]    Script Date: 2021/7/1 下午 02:24:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Block](
	[block_ID] [uniqueidentifier] NOT NULL,
	[name] [varchar](25) NULL,
	[popularity] [int] NULL,
	[cName] [nvarchar](10) NULL,
	[description] [nvarchar](100) NULL,
	[url] [varchar](100) NULL,
	[createTime] [datetime] NULL,
	[isDelete] [bit] NULL,
 CONSTRAINT [PK_Subblock] PRIMARY KEY CLUSTERED 
(
	[block_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IPrecond]    Script Date: 2021/7/1 下午 02:24:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IPrecond](
	[recond_GUID] [nchar](10) NOT NULL,
	[user_GUID] [uniqueidentifier] NULL,
	[ip] [varchar](15) NULL,
	[datetime] [datetime] NULL,
 CONSTRAINT [PK_IPrecond] PRIMARY KEY CLUSTERED 
(
	[recond_GUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KeywordTable]    Script Date: 2021/7/1 下午 02:24:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KeywordTable](
	[keywordGUID] [uniqueidentifier] NOT NULL,
	[userGUID] [uniqueidentifier] NOT NULL,
	[keywordString] [nvarchar](50) NOT NULL,
	[createTime] [datetime] NOT NULL,
	[isDelete] [bit] NOT NULL,
 CONSTRAINT [PK_KeywordTable] PRIMARY KEY CLUSTERED 
(
	[keywordGUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 2021/7/1 下午 02:24:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[message_ID] [uniqueidentifier] NOT NULL,
	[user_GUID] [uniqueidentifier] NULL,
	[user_ID] [nvarchar](50) NOT NULL,
	[article_ID] [uniqueidentifier] NOT NULL,
	[message] [nvarchar](max) NULL,
	[push] [nchar](5) NULL,
	[pushFeedback] [nvarchar](max) NULL,
	[ipdatetime] [varchar](100) NULL,
	[datetime] [datetime] NOT NULL,
	[isdelete] [bit] NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[message_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserArticleRecond]    Script Date: 2021/7/1 下午 02:24:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserArticleRecond](
	[user_GUID] [uniqueidentifier] NULL,
	[forum_ID] [varchar](50) NULL,
	[block_ID] [varchar](50) NULL,
	[subblock_ID] [varchar](50) NULL,
	[article_ID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_UserArticleRecond] PRIMARY KEY CLUSTERED 
(
	[article_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_User_user_GUID]  DEFAULT (newid()) FOR [userGUID]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_Account_birthday]  DEFAULT (getdate()) FOR [birthday]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_Account_errorDate]  DEFAULT (getdate()) FOR [errorDate]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_Account_errorTimes]  DEFAULT ((0)) FOR [errorTimes]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_Account_isBanned]  DEFAULT ((0)) FOR [isBanned]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_Account_onlineDays]  DEFAULT ((0)) FOR [onlineDays]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_Account_lastLoginDate]  DEFAULT (getdate()) FOR [lastLoginDate]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_User_datetime]  DEFAULT (getdate()) FOR [createTime]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_User_isdelete]  DEFAULT ((0)) FOR [isdelete]
GO
ALTER TABLE [dbo].[Article] ADD  CONSTRAINT [DF_Article_article_ID]  DEFAULT (newid()) FOR [article_ID]
GO
ALTER TABLE [dbo].[Article] ADD  CONSTRAINT [DF_Article_subblock_ID]  DEFAULT (newid()) FOR [block_ID]
GO
ALTER TABLE [dbo].[Article] ADD  CONSTRAINT [DF_Article_isannouncemet]  DEFAULT ((0)) FOR [isannouncemet]
GO
ALTER TABLE [dbo].[Article] ADD  CONSTRAINT [DF_Article_createdate]  DEFAULT (getdate()) FOR [createdate]
GO
ALTER TABLE [dbo].[Article] ADD  CONSTRAINT [DF_Article_datetime]  DEFAULT (getdate()) FOR [datetime]
GO
ALTER TABLE [dbo].[Article] ADD  CONSTRAINT [DF_Article_isdelete]  DEFAULT ((0)) FOR [isdelete]
GO
ALTER TABLE [dbo].[Block] ADD  CONSTRAINT [DF_Subblock_subblock_ID]  DEFAULT (newid()) FOR [block_ID]
GO
ALTER TABLE [dbo].[Block] ADD  CONSTRAINT [DF_Subblock_createtime]  DEFAULT (getdate()) FOR [createTime]
GO
ALTER TABLE [dbo].[Block] ADD  CONSTRAINT [DF_Subblock_isdelete]  DEFAULT ((0)) FOR [isDelete]
GO
ALTER TABLE [dbo].[IPrecond] ADD  CONSTRAINT [DF_IPrecond_datetime]  DEFAULT (getdate()) FOR [datetime]
GO
ALTER TABLE [dbo].[KeywordTable] ADD  CONSTRAINT [DF_KeywordTable_keywordGUID]  DEFAULT (newid()) FOR [keywordGUID]
GO
ALTER TABLE [dbo].[KeywordTable] ADD  CONSTRAINT [DF_KeywordTable_datetime]  DEFAULT (getdate()) FOR [createTime]
GO
ALTER TABLE [dbo].[KeywordTable] ADD  CONSTRAINT [DF_KeywordTable_isdelete]  DEFAULT ((0)) FOR [isDelete]
GO
ALTER TABLE [dbo].[Message] ADD  CONSTRAINT [DF_Message_message_ID]  DEFAULT (newid()) FOR [message_ID]
GO
ALTER TABLE [dbo].[Message] ADD  CONSTRAINT [DF_Message_datetime]  DEFAULT (getdate()) FOR [datetime]
GO
ALTER TABLE [dbo].[Message] ADD  CONSTRAINT [DF_Message_isdelete]  DEFAULT ((0)) FOR [isdelete]
GO
ALTER TABLE [dbo].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Article_block1] FOREIGN KEY([block_ID])
REFERENCES [dbo].[Block] ([block_ID])
GO
ALTER TABLE [dbo].[Article] CHECK CONSTRAINT [FK_Article_block1]
GO
ALTER TABLE [dbo].[IPrecond]  WITH CHECK ADD  CONSTRAINT [FK_IPrecond_User] FOREIGN KEY([user_GUID])
REFERENCES [dbo].[Account] ([userGUID])
GO
ALTER TABLE [dbo].[IPrecond] CHECK CONSTRAINT [FK_IPrecond_User]
GO
ALTER TABLE [dbo].[KeywordTable]  WITH CHECK ADD  CONSTRAINT [FK_KeywordTable_Account] FOREIGN KEY([userGUID])
REFERENCES [dbo].[Account] ([userGUID])
GO
ALTER TABLE [dbo].[KeywordTable] CHECK CONSTRAINT [FK_KeywordTable_Account]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_Article1] FOREIGN KEY([article_ID])
REFERENCES [dbo].[Article] ([article_ID])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_Article1]
GO
ALTER TABLE [dbo].[UserArticleRecond]  WITH CHECK ADD  CONSTRAINT [FK_UserArticleRecond_User] FOREIGN KEY([user_GUID])
REFERENCES [dbo].[Account] ([userGUID])
GO
ALTER TABLE [dbo].[UserArticleRecond] CHECK CONSTRAINT [FK_UserArticleRecond_User]
GO
