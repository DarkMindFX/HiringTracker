

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 2e6c7a744bed4e15a56836caf5ed47b3'
DECLARE @ReqDueDate BIT = 0
DECLARE @RequiresRespInDays INT = 504
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updName NVARCHAR(50) = 'Name bdf696d8afbc4e39b2334acb9fd65a0b'
DECLARE @updReqDueDate BIT = 0
DECLARE @updRequiresRespInDays INT = 504
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[ProposalStep]
				WHERE 
	(CASE WHEN @updName IS NOT NULL THEN (CASE WHEN [Name] = @updName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updReqDueDate IS NOT NULL THEN (CASE WHEN [ReqDueDate] = @updReqDueDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updRequiresRespInDays IS NOT NULL THEN (CASE WHEN [RequiresRespInDays] = @updRequiresRespInDays THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[ProposalStep]
	WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ReqDueDate IS NOT NULL THEN (CASE WHEN [ReqDueDate] = @ReqDueDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RequiresRespInDays IS NOT NULL THEN (CASE WHEN [RequiresRespInDays] = @RequiresRespInDays THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[ProposalStep]
	WHERE 
	(CASE WHEN @updName IS NOT NULL THEN (CASE WHEN [Name] = @updName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updReqDueDate IS NOT NULL THEN (CASE WHEN [ReqDueDate] = @updReqDueDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updRequiresRespInDays IS NOT NULL THEN (CASE WHEN [RequiresRespInDays] = @updRequiresRespInDays THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'ProposalStep was not updated', 1
END