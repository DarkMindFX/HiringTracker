USE [HiringTracker]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_SkillProficiency_GetIDByName]    Script Date: 8/21/2021 12:28:12 PM ******/
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

	
	SELECT @Result = sp.ID FROM dbo.SkillProficiency sp WHERE LOWER(sp.[Name]) = LOWER(@Name)

	
	RETURN @Result

END
GO
