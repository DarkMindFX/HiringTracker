USE [HiringTracker]
GO
/****** Object:  Table [dbo].[UserRoleSystem]    Script Date: 8/21/2021 12:26:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoleSystem](
	[UserID] [bigint] NOT NULL,
	[RoleID] [bigint] NOT NULL,
 CONSTRAINT [PK_UserRoleSystem] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
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
