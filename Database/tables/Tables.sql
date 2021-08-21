USE [HiringTracker]
GO
/****** Object:  Table [dbo].[Candidate]    Script Date: 8/21/2021 11:21:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Candidate](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[CVLink] [nvarchar](1000) NULL,
	[CreatedByID] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedByID] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Candidate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CandidateComment]    Script Date: 8/21/2021 11:21:38 AM ******/
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
/****** Object:  Table [dbo].[CandidateProperty]    Script Date: 8/21/2021 11:21:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CandidateProperty](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Value] [nvarchar](1000) NULL,
	[CandidateID] [bigint] NOT NULL,
 CONSTRAINT [PK_CandidateProperty] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CandidateSkill]    Script Date: 8/21/2021 11:21:38 AM ******/
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
/****** Object:  Table [dbo].[Comment]    Script Date: 8/21/2021 11:21:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 8/21/2021 11:21:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[UUID] [nvarchar](50) NULL,
	[ParentID] [bigint] NULL,
	[ManagerID] [bigint] NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Interview]    Script Date: 8/21/2021 11:21:38 AM ******/
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
/****** Object:  Table [dbo].[InterviewFeedback]    Script Date: 8/21/2021 11:21:38 AM ******/
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
/****** Object:  Table [dbo].[InterviewRole]    Script Date: 8/21/2021 11:21:38 AM ******/
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
/****** Object:  Table [dbo].[InterviewStatus]    Script Date: 8/21/2021 11:21:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InterviewStatus](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_InterviewStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InterviewType]    Script Date: 8/21/2021 11:21:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InterviewType](
	[ID] [bigint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_InterviewType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Position]    Script Date: 8/21/2021 11:21:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Position](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[DepartmentID] [bigint] NULL,
	[Title] [nvarchar](50) NOT NULL,
	[ShortDesc] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[StatusID] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PositionComment]    Script Date: 8/21/2021 11:21:38 AM ******/
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
/****** Object:  Table [dbo].[PositionSkill]    Script Date: 8/21/2021 11:21:38 AM ******/
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
/****** Object:  Table [dbo].[PositionStatus]    Script Date: 8/21/2021 11:21:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PositionStatus](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PositionStatusID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proposal]    Script Date: 8/21/2021 11:21:38 AM ******/
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
/****** Object:  Table [dbo].[ProposalComment]    Script Date: 8/21/2021 11:21:38 AM ******/
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
/****** Object:  Table [dbo].[ProposalStatus]    Script Date: 8/21/2021 11:21:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProposalStatus](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PositionCandidateState] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProposalStep]    Script Date: 8/21/2021 11:21:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProposalStep](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ReqDueDate] [bit] NOT NULL,
	[RequiresRespInDays] [int] NULL,
 CONSTRAINT [PK_ProposalState] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 8/21/2021 11:21:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Skill]    Script Date: 8/21/2021 11:21:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skill](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Skill] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SkillProficiency]    Script Date: 8/21/2021 11:21:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SkillProficiency](
	[ID] [bigint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SkillProficiency] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 8/21/2021 11:21:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Description] [nvarchar](255) NULL,
	[PwdHash] [nvarchar](255) NOT NULL,
	[Salt] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoleCandidate]    Script Date: 8/21/2021 11:21:38 AM ******/
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
/****** Object:  Table [dbo].[UserRolePosition]    Script Date: 8/21/2021 11:21:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRolePosition](
	[PositionID] [bigint] NOT NULL,
	[UserID] [bigint] NOT NULL,
	[RoleID] [bigint] NOT NULL,
 CONSTRAINT [PK_UserRolePosition_1] PRIMARY KEY CLUSTERED 
(
	[PositionID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoleSystem]    Script Date: 8/21/2021 11:21:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoleSystem](
	[UserID] [bigint] NOT NULL,
	[RoleID] [bigint] NOT NULL,
 CONSTRAINT [PK_SystemUserRole] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Candidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_User1] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Candidate] CHECK CONSTRAINT [FK_Candidate_User1]
GO
ALTER TABLE [dbo].[Candidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_User2] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Candidate] CHECK CONSTRAINT [FK_Candidate_User2]
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
ALTER TABLE [dbo].[CandidateProperty]  WITH CHECK ADD  CONSTRAINT [FK_CandidateProperty_Candidate] FOREIGN KEY([CandidateID])
REFERENCES [dbo].[Candidate] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CandidateProperty] CHECK CONSTRAINT [FK_CandidateProperty_Candidate]
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
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_User] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_User]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_User1] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_User1]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Department] FOREIGN KEY([ParentID])
REFERENCES [dbo].[Department] ([ID])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Department]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_User] FOREIGN KEY([ManagerID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_User]
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
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_Department] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([ID])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_Department]
GO
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_PositionStatus] FOREIGN KEY([StatusID])
REFERENCES [dbo].[PositionStatus] ([ID])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_PositionStatus]
GO
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_User] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_User]
GO
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_User1] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_User1]
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
ALTER TABLE [dbo].[UserRolePosition]  WITH CHECK ADD  CONSTRAINT [FK_PositionUserRole_Position] FOREIGN KEY([PositionID])
REFERENCES [dbo].[Position] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRolePosition] CHECK CONSTRAINT [FK_PositionUserRole_Position]
GO
ALTER TABLE [dbo].[UserRolePosition]  WITH CHECK ADD  CONSTRAINT [FK_PositionUserRole_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[UserRolePosition] CHECK CONSTRAINT [FK_PositionUserRole_Role]
GO
ALTER TABLE [dbo].[UserRolePosition]  WITH CHECK ADD  CONSTRAINT [FK_PositionUserRole_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRolePosition] CHECK CONSTRAINT [FK_PositionUserRole_User]
GO
ALTER TABLE [dbo].[UserRoleSystem]  WITH CHECK ADD  CONSTRAINT [FK_SystemUserRole_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[UserRoleSystem] CHECK CONSTRAINT [FK_SystemUserRole_Role]
GO
ALTER TABLE [dbo].[UserRoleSystem]  WITH CHECK ADD  CONSTRAINT [FK_SystemUserRole_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoleSystem] CHECK CONSTRAINT [FK_SystemUserRole_User]
GO
