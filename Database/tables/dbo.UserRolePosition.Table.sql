USE [HiringTracker]
GO
/****** Object:  Table [dbo].[UserRolePosition]    Script Date: 8/21/2021 12:26:39 PM ******/
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
