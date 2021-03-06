USE [HiringTracker]
GO
/****** Object:  Table [dbo].[PositionComment]    Script Date: 8/21/2021 12:26:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PositionComment](
	[PositionID] [bigint] NOT NULL,
	[CommentID] [bigint] NOT NULL,
 CONSTRAINT [PK_PositionComment_1] PRIMARY KEY CLUSTERED 
(
	[PositionID] ASC,
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PositionComment]  WITH CHECK ADD  CONSTRAINT [FK_PositionComment_Comment] FOREIGN KEY([CommentID])
REFERENCES [dbo].[Comment] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PositionComment] CHECK CONSTRAINT [FK_PositionComment_Comment]
GO
ALTER TABLE [dbo].[PositionComment]  WITH CHECK ADD  CONSTRAINT [FK_PositionComment_PositionComment] FOREIGN KEY([PositionID])
REFERENCES [dbo].[Position] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PositionComment] CHECK CONSTRAINT [FK_PositionComment_PositionComment]
GO
