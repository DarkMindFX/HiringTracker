USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_PositionSkill_Delete]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_PositionSkill_Delete]
		@PositionID BIGINT,	
		@SkillID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[PositionSkill]  
				WHERE 
							[PositionID] = @PositionID	AND
							[SkillID] = @SkillID	
				) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[PositionSkill] 
		WHERE 
						[PositionID] = @PositionID	AND
						[SkillID] = @SkillID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
