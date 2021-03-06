USE [HiringTracker]
GO
/****** Object:  Table [dbo].[ProposalComment]    Script Date: 8/21/2021 12:26:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProposalComment](
	[ProposalID] [bigint] NOT NULL,
	[CommentID] [bigint] NOT NULL,
 CONSTRAINT [PK_PositionCandidateComment] PRIMARY KEY CLUSTERED 
(
	[ProposalID] ASC,
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProposalComment]  WITH CHECK ADD  CONSTRAINT [FK_PositionCandidateComment_Comment] FOREIGN KEY([CommentID])
REFERENCES [dbo].[Comment] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProposalComment] CHECK CONSTRAINT [FK_PositionCandidateComment_Comment]
GO
ALTER TABLE [dbo].[ProposalComment]  WITH CHECK ADD  CONSTRAINT [FK_ProposalComment_Proposal] FOREIGN KEY([ProposalID])
REFERENCES [dbo].[Proposal] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProposalComment] CHECK CONSTRAINT [FK_ProposalComment_Proposal]
GO
