USE [HiringTracker]
GO
/****** Object:  Table [dbo].[PositionSkill]    Script Date: 8/21/2021 12:26:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PositionSkill](
	[PositionID] [bigint] NOT NULL,
	[SkillID] [bigint] NOT NULL,
	[IsMandatory] [bit] NOT NULL,
	[SkillProficiencyID] [bigint] NOT NULL,
 CONSTRAINT [PK_PositionSkill_1] PRIMARY KEY CLUSTERED 
(
	[PositionID] ASC,
	[SkillID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PositionSkill]  WITH CHECK ADD  CONSTRAINT [FK_PositionSkill_Position] FOREIGN KEY([PositionID])
REFERENCES [dbo].[Position] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PositionSkill] CHECK CONSTRAINT [FK_PositionSkill_Position]
GO
ALTER TABLE [dbo].[PositionSkill]  WITH CHECK ADD  CONSTRAINT [FK_PositionSkill_Skill] FOREIGN KEY([SkillID])
REFERENCES [dbo].[Skill] ([ID])
GO
ALTER TABLE [dbo].[PositionSkill] CHECK CONSTRAINT [FK_PositionSkill_Skill]
GO
ALTER TABLE [dbo].[PositionSkill]  WITH CHECK ADD  CONSTRAINT [FK_PositionSkill_SkillProficiency] FOREIGN KEY([SkillProficiencyID])
REFERENCES [dbo].[SkillProficiency] ([ID])
GO
ALTER TABLE [dbo].[PositionSkill] CHECK CONSTRAINT [FK_PositionSkill_SkillProficiency]
GO
