DECLARE @ID BIGINT
DECLARE @Name NVARCHAR(50) = 'Name c06aefb0220b48279015ee646de91e80'

IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[ProposalStatus]
				WHERE 
					(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1))
BEGIN
	INSERT INTO [dbo].[ProposalStatus]
		([Name])
	SELECT 		
		@Name
END

SELECT TOP 1 @ID = [ID] 
FROM 
	[dbo].[ProposalStatus] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1

SELECT @ID