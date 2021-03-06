USE [HiringTracker]
GO
/****** Object:  Table [dbo].[Interview]    Script Date: 8/21/2021 12:26:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Interview](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ProposalID] [bigint] NOT NULL,
	[InterviewTypeID] [bigint] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[InterviewStatusID] [bigint] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[CretedDate] [datetime] NOT NULL,
	[ModifiedByID] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Interview] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Interview]  WITH CHECK ADD  CONSTRAINT [FK_Interview_InterviewStatus] FOREIGN KEY([InterviewStatusID])
REFERENCES [dbo].[InterviewStatus] ([ID])
GO
ALTER TABLE [dbo].[Interview] CHECK CONSTRAINT [FK_Interview_InterviewStatus]
GO
ALTER TABLE [dbo].[Interview]  WITH CHECK ADD  CONSTRAINT [FK_Interview_InterviewType] FOREIGN KEY([InterviewTypeID])
REFERENCES [dbo].[InterviewType] ([ID])
GO
ALTER TABLE [dbo].[Interview] CHECK CONSTRAINT [FK_Interview_InterviewType]
GO
ALTER TABLE [dbo].[Interview]  WITH CHECK ADD  CONSTRAINT [FK_Interview_Proposal] FOREIGN KEY([ProposalID])
REFERENCES [dbo].[Proposal] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Interview] CHECK CONSTRAINT [FK_Interview_Proposal]
GO
ALTER TABLE [dbo].[Interview]  WITH CHECK ADD  CONSTRAINT [FK_Interview_User] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Interview] CHECK CONSTRAINT [FK_Interview_User]
GO
ALTER TABLE [dbo].[Interview]  WITH CHECK ADD  CONSTRAINT [FK_Interview_User1] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Interview] CHECK CONSTRAINT [FK_Interview_User1]
GO
