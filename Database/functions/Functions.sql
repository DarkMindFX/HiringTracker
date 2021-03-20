USE [HiringTracker]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Skill_GetIDByName]    Script Date: 3/20/2021 11:11:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fn_Skill_GetIDByName]
(
	@Name NVARCHAR(50)
)
RETURNS BIGINT
AS
BEGIN

	DECLARE @Result AS BIGINT = NULL

	
	SELECT @Result = sp.SkillID FROM dbo.Skill sp WHERE LOWER(sp.[Name]) = LOWER(@Name)

	
	RETURN @Result

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_SkillProficiency_GetIDByName]    Script Date: 3/20/2021 11:11:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fn_SkillProficiency_GetIDByName]
(
	@Name NVARCHAR(50)
)
RETURNS BIGINT
AS
BEGIN

	DECLARE @Result AS BIGINT = NULL

	
	SELECT @Result = sp.ProficiencyID FROM dbo.SkillProficiency sp WHERE LOWER(sp.[Name]) = LOWER(@Name)

	
	RETURN @Result

END
GO
