

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name a9cf78d1505f4fd0b19e9218baf25e3f'
DECLARE @ReqDueDate BIT = 0
DECLARE @RequiresRespInDays INT = 504
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[ProposalStep]
				WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ReqDueDate IS NOT NULL THEN (CASE WHEN [ReqDueDate] = @ReqDueDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RequiresRespInDays IS NOT NULL THEN (CASE WHEN [RequiresRespInDays] = @RequiresRespInDays THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[ProposalStep]
	WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ReqDueDate IS NOT NULL THEN (CASE WHEN [ReqDueDate] = @ReqDueDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RequiresRespInDays IS NOT NULL THEN (CASE WHEN [RequiresRespInDays] = @RequiresRespInDays THEN 1 ELSE 0 END) ELSE 1 END) = 1 

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'ProposalStep was not deleted', 1
END