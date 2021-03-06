USE [HiringTracker]
GO
/****** Object:  Table [dbo].[CandidateComment]    Script Date: 8/21/2021 12:26:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CandidateComment](
	[CandidateID] [bigint] NOT NULL,
	[CommentID] [bigint] NOT NULL,
 CONSTRAINT [PK_CandidateComment_1] PRIMARY KEY CLUSTERED 
(
	[CandidateID] ASC,
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CandidateComment]  WITH CHECK ADD  CONSTRAINT [FK_CandidateComments_Candidate] FOREIGN KEY([CandidateID])
REFERENCES [dbo].[Candidate] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CandidateComment] CHECK CONSTRAINT [FK_CandidateComments_Candidate]
GO
ALTER TABLE [dbo].[CandidateComment]  WITH CHECK ADD  CONSTRAINT [FK_CandidateComments_Comment] FOREIGN KEY([CommentID])
REFERENCES [dbo].[Comment] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CandidateComment] CHECK CONSTRAINT [FK_CandidateComments_Comment]
GO
