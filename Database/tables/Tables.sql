USE [HiringTracker]
GO
/****** Object:  Table [dbo].[Candidate]    Script Date: 3/16/2021 10:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Candidate](
	[CandidateID] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[CVLink] [nvarchar](1000) NULL,
	[RecruiterID] [bigint] NULL,
	[CreatedByID] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Candidate] PRIMARY KEY CLUSTERED 
(
	[CandidateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CandidateProperty]    Script Date: 3/16/2021 10:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CandidateProperty](
	[PropertyID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Value] [nvarchar](1000) NULL,
	[CandidateID] [bigint] NOT NULL,
 CONSTRAINT [PK_CandidateProperty] PRIMARY KEY CLUSTERED 
(
	[PropertyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 3/16/2021 10:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[CommentID] [bigint] IDENTITY(1,1) NOT NULL,
	[DateLeft] [datetime] NOT NULL,
	[UserID] [bigint] NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 3/16/2021 10:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[UUID] [nvarchar](50) NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Interview]    Script Date: 3/16/2021 10:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Interview](
	[InterviewID] [bigint] IDENTITY(1,1) NOT NULL,
	[InterviewTypeID] [bigint] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [int] NOT NULL,
	[InterviewStatusID] [bigint] NOT NULL,
 CONSTRAINT [PK_Interview] PRIMARY KEY CLUSTERED 
(
	[InterviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InterviewFeedback]    Script Date: 3/16/2021 10:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InterviewFeedback](
	[FeedbackID] [bigint] NOT NULL,
	[Comment] [nvarchar](50) NOT NULL,
	[Rating] [int] NOT NULL,
	[InterviewID] [bigint] NOT NULL,
	[InterviewerID] [bigint] NOT NULL,
 CONSTRAINT [PK_InterviewFeedback] PRIMARY KEY CLUSTERED 
(
	[FeedbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InterviewRole]    Script Date: 3/16/2021 10:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InterviewRole](
	[InterviewID] [bigint] NOT NULL,
	[UserID] [bigint] NOT NULL,
	[RoleID] [bigint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InterviewStatus]    Script Date: 3/16/2021 10:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InterviewStatus](
	[StatusID] [bigint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_InterviewStatus] PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InterviewType]    Script Date: 3/16/2021 10:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InterviewType](
	[TypeID] [bigint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_InterviewType] PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Position]    Script Date: 3/16/2021 10:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Position](
	[PositionID] [bigint] IDENTITY(1,1) NOT NULL,
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
	[PositionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PositionCandidates]    Script Date: 3/16/2021 10:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PositionCandidates](
	[ProposalID] [bigint] IDENTITY(1,1) NOT NULL,
	[PositionID] [bigint] NOT NULL,
	[CandidateID] [bigint] NOT NULL,
	[Proposed] [datetime] NOT NULL,
	[CurrentStepID] [bigint] NOT NULL,
	[StepSetDate] [datetime] NOT NULL,
	[NextStepID] [bigint] NULL,
	[DueDate] [datetime] NULL,
	[StatusID] [bigint] NOT NULL,
 CONSTRAINT [PK_PositionCandidates] PRIMARY KEY CLUSTERED 
(
	[ProposalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PositionCandidateStatus]    Script Date: 3/16/2021 10:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PositionCandidateStatus](
	[StatusID] [bigint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PositionCandidateState] PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PositionCandidateStep]    Script Date: 3/16/2021 10:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PositionCandidateStep](
	[StepID] [bigint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ReqDueDate] [bit] NOT NULL,
	[RequiresRespInDays] [int] NULL,
 CONSTRAINT [PK_ProposalState] PRIMARY KEY CLUSTERED 
(
	[StepID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PositionSkill]    Script Date: 3/16/2021 10:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PositionSkill](
	[PositionID] [bigint] NOT NULL,
	[SkillID] [bigint] NOT NULL,
	[IsMandatory] [bit] NOT NULL,
	[SkillProficiencyID] [bigint] NOT NULL,
 CONSTRAINT [PK_PositionSkill] PRIMARY KEY CLUSTERED 
(
	[PositionID] ASC,
	[SkillID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PositionStatus]    Script Date: 3/16/2021 10:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PositionStatus](
	[StatusID] [bigint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PositionStatusID] PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 3/16/2021 10:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [bigint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Skill]    Script Date: 3/16/2021 10:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skill](
	[SkillID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Skill] PRIMARY KEY CLUSTERED 
(
	[SkillID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SkillProficiency]    Script Date: 3/16/2021 10:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SkillProficiency](
	[ProficiencyID] [bigint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SkillProficiency] PRIMARY KEY CLUSTERED 
(
	[ProficiencyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/16/2021 10:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [bigint] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Description] [nvarchar](255) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Candidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_User] FOREIGN KEY([RecruiterID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Candidate] CHECK CONSTRAINT [FK_Candidate_User]
GO
ALTER TABLE [dbo].[Candidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_User1] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Candidate] CHECK CONSTRAINT [FK_Candidate_User1]
GO
ALTER TABLE [dbo].[Candidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_User2] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Candidate] CHECK CONSTRAINT [FK_Candidate_User2]
GO
ALTER TABLE [dbo].[CandidateProperty]  WITH CHECK ADD  CONSTRAINT [FK_CandidateProperty_Candidate] FOREIGN KEY([CandidateID])
REFERENCES [dbo].[Candidate] ([CandidateID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CandidateProperty] CHECK CONSTRAINT [FK_CandidateProperty_Candidate]
GO
ALTER TABLE [dbo].[Interview]  WITH CHECK ADD  CONSTRAINT [FK_Interview_InterviewStatus] FOREIGN KEY([InterviewStatusID])
REFERENCES [dbo].[InterviewStatus] ([StatusID])
GO
ALTER TABLE [dbo].[Interview] CHECK CONSTRAINT [FK_Interview_InterviewStatus]
GO
ALTER TABLE [dbo].[Interview]  WITH CHECK ADD  CONSTRAINT [FK_Interview_InterviewType] FOREIGN KEY([InterviewTypeID])
REFERENCES [dbo].[InterviewType] ([TypeID])
GO
ALTER TABLE [dbo].[Interview] CHECK CONSTRAINT [FK_Interview_InterviewType]
GO
ALTER TABLE [dbo].[InterviewFeedback]  WITH CHECK ADD  CONSTRAINT [FK_InterviewFeedback_Interview] FOREIGN KEY([InterviewID])
REFERENCES [dbo].[Interview] ([InterviewID])
GO
ALTER TABLE [dbo].[InterviewFeedback] CHECK CONSTRAINT [FK_InterviewFeedback_Interview]
GO
ALTER TABLE [dbo].[InterviewFeedback]  WITH CHECK ADD  CONSTRAINT [FK_InterviewFeedback_User] FOREIGN KEY([InterviewerID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[InterviewFeedback] CHECK CONSTRAINT [FK_InterviewFeedback_User]
GO
ALTER TABLE [dbo].[InterviewRole]  WITH CHECK ADD  CONSTRAINT [FK_InterviewRole_Interview] FOREIGN KEY([InterviewID])
REFERENCES [dbo].[Interview] ([InterviewID])
GO
ALTER TABLE [dbo].[InterviewRole] CHECK CONSTRAINT [FK_InterviewRole_Interview]
GO
ALTER TABLE [dbo].[InterviewRole]  WITH CHECK ADD  CONSTRAINT [FK_InterviewRole_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[InterviewRole] CHECK CONSTRAINT [FK_InterviewRole_Role]
GO
ALTER TABLE [dbo].[InterviewRole]  WITH CHECK ADD  CONSTRAINT [FK_InterviewRole_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[InterviewRole] CHECK CONSTRAINT [FK_InterviewRole_User]
GO
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_PositionStatus] FOREIGN KEY([StatusID])
REFERENCES [dbo].[PositionStatus] ([StatusID])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_PositionStatus]
GO
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_User] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_User]
GO
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_User1] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_User1]
GO
ALTER TABLE [dbo].[PositionSkill]  WITH CHECK ADD  CONSTRAINT [FK_PositionSkill_Position] FOREIGN KEY([PositionID])
REFERENCES [dbo].[Position] ([PositionID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PositionSkill] CHECK CONSTRAINT [FK_PositionSkill_Position]
GO
ALTER TABLE [dbo].[PositionSkill]  WITH CHECK ADD  CONSTRAINT [FK_PositionSkill_Skill] FOREIGN KEY([SkillID])
REFERENCES [dbo].[Skill] ([SkillID])
GO
ALTER TABLE [dbo].[PositionSkill] CHECK CONSTRAINT [FK_PositionSkill_Skill]
GO
ALTER TABLE [dbo].[PositionSkill]  WITH CHECK ADD  CONSTRAINT [FK_PositionSkill_SkillProficiency] FOREIGN KEY([SkillProficiencyID])
REFERENCES [dbo].[SkillProficiency] ([ProficiencyID])
GO
ALTER TABLE [dbo].[PositionSkill] CHECK CONSTRAINT [FK_PositionSkill_SkillProficiency]
GO
