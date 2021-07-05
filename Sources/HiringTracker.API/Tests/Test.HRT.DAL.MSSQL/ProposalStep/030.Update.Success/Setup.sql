

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 2e6c7a744bed4e15a56836caf5ed47b3'
DECLARE @ReqDueDate BIT = 0
DECLARE @RequiresRespInDays INT = 504
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[ProposalStep]
				WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ReqDueDate IS NOT NULL THEN (CASE WHEN [ReqDueDate] = @ReqDueDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RequiresRespInDays IS NOT NULL THEN (CASE WHEN [RequiresRespInDays] = @RequiresRespInDays THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[ProposalStep]
		(
	 [Name],
	 [ReqDueDate],
	 [RequiresRespInDays]
		)
	SELECT 		
			 @Name,
	 @ReqDueDate,
	 @RequiresRespInDays
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[ProposalStep] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ReqDueDate IS NOT NULL THEN (CASE WHEN [ReqDueDate] = @ReqDueDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RequiresRespInDays IS NOT NULL THEN (CASE WHEN [RequiresRespInDays] = @RequiresRespInDays THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
