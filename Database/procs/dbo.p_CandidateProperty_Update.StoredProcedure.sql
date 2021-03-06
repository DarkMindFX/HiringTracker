USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_CandidateProperty_Update]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_CandidateProperty_Update]
			@ID BIGINT,
			@Name NVARCHAR(50),
			@Value NVARCHAR(1000),
			@CandidateID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[CandidateProperty]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[CandidateProperty]
		SET
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) ,
									[Value] = IIF( @Value IS NOT NULL, @Value, [Value] ) ,
									[CandidateID] = IIF( @CandidateID IS NOT NULL, @CandidateID, [CandidateID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'CandidateProperty was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[CandidateProperty] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Value IS NOT NULL THEN (CASE WHEN e.[Value] = @Value THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CandidateID IS NOT NULL THEN (CASE WHEN e.[CandidateID] = @CandidateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
