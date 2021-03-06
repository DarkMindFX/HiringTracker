USE [HiringTracker]
GO
/****** Object:  Table [dbo].[InterviewFeedback]    Script Date: 8/21/2021 12:26:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InterviewFeedback](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Comment] [nvarchar](max) NOT NULL,
	[Rating] [int] NOT NULL,
	[InterviewID] [bigint] NOT NULL,
	[InterviewerID] [bigint] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedByID] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_InterviewFeedback] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[InterviewFeedback]  WITH CHECK ADD  CONSTRAINT [FK_InterviewFeedback_Interview] FOREIGN KEY([InterviewID])
REFERENCES [dbo].[Interview] ([ID])
GO
ALTER TABLE [dbo].[InterviewFeedback] CHECK CONSTRAINT [FK_InterviewFeedback_Interview]
GO
ALTER TABLE [dbo].[InterviewFeedback]  WITH CHECK ADD  CONSTRAINT [FK_InterviewFeedback_User] FOREIGN KEY([InterviewerID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[InterviewFeedback] CHECK CONSTRAINT [FK_InterviewFeedback_User]
GO
ALTER TABLE [dbo].[InterviewFeedback]  WITH CHECK ADD  CONSTRAINT [FK_InterviewFeedback_User1] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[InterviewFeedback] CHECK CONSTRAINT [FK_InterviewFeedback_User1]
GO
ALTER TABLE [dbo].[InterviewFeedback]  WITH CHECK ADD  CONSTRAINT [FK_InterviewFeedback_User2] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[InterviewFeedback] CHECK CONSTRAINT [FK_InterviewFeedback_User2]
GO
