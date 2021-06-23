

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 61bbb193c1fd46cca7f3ffa3d373b850'
 

DELETE FROM [PositionStatus]
FROM 
	[dbo].[PositionStatus] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
