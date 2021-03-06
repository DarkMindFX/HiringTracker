USE [HiringTracker]
GO
/****** Object:  Table [dbo].[InterviewRole]    Script Date: 8/21/2021 12:26:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InterviewRole](
	[InterviewID] [bigint] NOT NULL,
	[UserID] [bigint] NOT NULL,
	[RoleID] [bigint] NOT NULL,
 CONSTRAINT [PK_InterviewRole_1] PRIMARY KEY CLUSTERED 
(
	[InterviewID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[InterviewRole]  WITH CHECK ADD  CONSTRAINT [FK_InterviewRole_Interview] FOREIGN KEY([InterviewID])
REFERENCES [dbo].[Interview] ([ID])
GO
ALTER TABLE [dbo].[InterviewRole] CHECK CONSTRAINT [FK_InterviewRole_Interview]
GO
ALTER TABLE [dbo].[InterviewRole]  WITH CHECK ADD  CONSTRAINT [FK_InterviewRole_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[InterviewRole] CHECK CONSTRAINT [FK_InterviewRole_Role]
GO
ALTER TABLE [dbo].[InterviewRole]  WITH CHECK ADD  CONSTRAINT [FK_InterviewRole_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[InterviewRole] CHECK CONSTRAINT [FK_InterviewRole_User]
GO
