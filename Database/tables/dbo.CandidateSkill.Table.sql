USE [HiringTracker]
GO
/****** Object:  Table [dbo].[CandidateSkill]    Script Date: 8/21/2021 12:26:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CandidateSkill](
	[CandidateID] [bigint] NOT NULL,
	[SkillID] [bigint] NOT NULL,
	[SkillProficiencyID] [bigint] NOT NULL,
 CONSTRAINT [PK_CandidateSkill] PRIMARY KEY CLUSTERED 
(
	[CandidateID] ASC,
	[SkillID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CandidateSkill]  WITH CHECK ADD  CONSTRAINT [FK_CandidateSkill_Candidate] FOREIGN KEY([CandidateID])
REFERENCES [dbo].[Candidate] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CandidateSkill] CHECK CONSTRAINT [FK_CandidateSkill_Candidate]
GO
ALTER TABLE [dbo].[CandidateSkill]  WITH CHECK ADD  CONSTRAINT [FK_CandidateSkill_Skill] FOREIGN KEY([SkillID])
REFERENCES [dbo].[Skill] ([ID])
GO
ALTER TABLE [dbo].[CandidateSkill] CHECK CONSTRAINT [FK_CandidateSkill_Skill]
GO
ALTER TABLE [dbo].[CandidateSkill]  WITH CHECK ADD  CONSTRAINT [FK_CandidateSkill_SkillProficiency] FOREIGN KEY([SkillProficiencyID])
REFERENCES [dbo].[SkillProficiency] ([ID])
GO
ALTER TABLE [dbo].[CandidateSkill] CHECK CONSTRAINT [FK_CandidateSkill_SkillProficiency]
GO
