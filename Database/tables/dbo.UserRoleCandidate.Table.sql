USE [HiringTracker]
GO
/****** Object:  Table [dbo].[UserRoleCandidate]    Script Date: 8/21/2021 12:26:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoleCandidate](
	[CandidateID] [bigint] NOT NULL,
	[UserID] [bigint] NOT NULL,
	[RoleID] [bigint] NOT NULL,
 CONSTRAINT [PK_UserRoleCandidate_1] PRIMARY KEY CLUSTERED 
(
	[CandidateID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserRoleCandidate]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleCandidate_Candidate] FOREIGN KEY([CandidateID])
REFERENCES [dbo].[Candidate] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoleCandidate] CHECK CONSTRAINT [FK_UserRoleCandidate_Candidate]
GO
ALTER TABLE [dbo].[UserRoleCandidate]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleCandidate_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[UserRoleCandidate] CHECK CONSTRAINT [FK_UserRoleCandidate_Role]
GO
ALTER TABLE [dbo].[UserRoleCandidate]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleCandidate_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoleCandidate] CHECK CONSTRAINT [FK_UserRoleCandidate_User]
GO
