

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name cec36ff822884c6ab8b3435c1e765507'
DECLARE @ReqDueDate BIT = 0
DECLARE @RequiresRespInDays INT = 504
 

DELETE FROM [ProposalStep]
FROM 
	[dbo].[ProposalStep] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ReqDueDate IS NOT NULL THEN (CASE WHEN [ReqDueDate] = @ReqDueDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RequiresRespInDays IS NOT NULL THEN (CASE WHEN [RequiresRespInDays] = @RequiresRespInDays THEN 1 ELSE 0 END) ELSE 1 END) = 1 
