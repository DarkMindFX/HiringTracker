USE [HiringTracker]
GO
/****** Object:  Table [dbo].[Proposal]    Script Date: 8/21/2021 12:26:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proposal](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[PositionID] [bigint] NOT NULL,
	[CandidateID] [bigint] NOT NULL,
	[Proposed] [datetime] NOT NULL,
	[CurrentStepID] [bigint] NOT NULL,
	[StepSetDate] [datetime] NOT NULL,
	[NextStepID] [bigint] NULL,
	[DueDate] [datetime] NULL,
	[StatusID] [bigint] NOT NULL,
	[CreatedByID] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_PositionCandidates] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Proposal]  WITH CHECK ADD  CONSTRAINT [FK_Proposal_Candidate] FOREIGN KEY([CandidateID])
REFERENCES [dbo].[Candidate] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Proposal] CHECK CONSTRAINT [FK_Proposal_Candidate]
GO
ALTER TABLE [dbo].[Proposal]  WITH CHECK ADD  CONSTRAINT [FK_Proposal_Position] FOREIGN KEY([PositionID])
REFERENCES [dbo].[Position] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Proposal] CHECK CONSTRAINT [FK_Proposal_Position]
GO
ALTER TABLE [dbo].[Proposal]  WITH CHECK ADD  CONSTRAINT [FK_Proposal_ProposalStatus] FOREIGN KEY([StatusID])
REFERENCES [dbo].[ProposalStatus] ([ID])
GO
ALTER TABLE [dbo].[Proposal] CHECK CONSTRAINT [FK_Proposal_ProposalStatus]
GO
ALTER TABLE [dbo].[Proposal]  WITH CHECK ADD  CONSTRAINT [FK_Proposal_ProposalStep_Current] FOREIGN KEY([CurrentStepID])
REFERENCES [dbo].[ProposalStep] ([ID])
GO
ALTER TABLE [dbo].[Proposal] CHECK CONSTRAINT [FK_Proposal_ProposalStep_Current]
GO
ALTER TABLE [dbo].[Proposal]  WITH CHECK ADD  CONSTRAINT [FK_Proposal_ProposalStep_Next] FOREIGN KEY([NextStepID])
REFERENCES [dbo].[ProposalStep] ([ID])
GO
ALTER TABLE [dbo].[Proposal] CHECK CONSTRAINT [FK_Proposal_ProposalStep_Next]
GO
ALTER TABLE [dbo].[Proposal]  WITH CHECK ADD  CONSTRAINT [FK_Proposal_User_Created] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Proposal] CHECK CONSTRAINT [FK_Proposal_User_Created]
GO
ALTER TABLE [dbo].[Proposal]  WITH CHECK ADD  CONSTRAINT [FK_Proposal_User_Modified] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Proposal] CHECK CONSTRAINT [FK_Proposal_User_Modified]
GO
