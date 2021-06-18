DECLARE @ID BIGINT
DECLARE @Name NVARCHAR(50) = 'Name 9f2e83d49da44b4aa247494426d77b06'
DECLARE @ReqDueDate BIT = 1
DECLARE @RequiresRespInDays INT = 648

DELETE FROM [dbo].[ProposalStep]
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @ReqDueDate IS NOT NULL THEN (CASE WHEN [ReqDueDate] = @ReqDueDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @RequiresRespInDays IS NOT NULL THEN (CASE WHEN [RequiresRespInDays] = @RequiresRespInDays THEN 1 ELSE 0 END) ELSE 1 END) = 1