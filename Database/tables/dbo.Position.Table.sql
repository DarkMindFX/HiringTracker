USE [HiringTracker]
GO
/****** Object:  Table [dbo].[Position]    Script Date: 8/21/2021 12:26:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Position](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[DepartmentID] [bigint] NULL,
	[Title] [nvarchar](50) NOT NULL,
	[ShortDesc] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[StatusID] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_Department] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([ID])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_Department]
GO
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_PositionStatus] FOREIGN KEY([StatusID])
REFERENCES [dbo].[PositionStatus] ([ID])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_PositionStatus]
GO
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_User] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_User]
GO
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_User1] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_User1]
GO
