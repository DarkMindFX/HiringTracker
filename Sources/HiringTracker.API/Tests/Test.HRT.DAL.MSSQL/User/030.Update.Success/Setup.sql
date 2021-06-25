

DECLARE @ID BIGINT = NULL
DECLARE @Login NVARCHAR(50) = 'Login 9e43371bbbf241c5a88e5ca30862e3e8'
DECLARE @FirstName NVARCHAR(50) = 'FirstName 9e43371bbbf241c5a88e5ca30862e3e8'
DECLARE @LastName NVARCHAR(50) = 'LastName 9e43371bbbf241c5a88e5ca30862e3e8'
DECLARE @Email NVARCHAR(50) = 'Email 9e43371bbbf241c5a88e5ca30862e3e8'
DECLARE @Description NVARCHAR(255) = 'Description 9e43371bbbf241c5a88e5ca30862e3e8'
DECLARE @PwdHash NVARCHAR(255) = 'PwdHash 9e43371bbbf241c5a88e5ca30862e3e8'
DECLARE @Salt NVARCHAR(255) = 'Salt 9e43371bbbf241c5a88e5ca30862e3e8'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[User]
				WHERE 
	(CASE WHEN @Login IS NOT NULL THEN (CASE WHEN [Login] = @Login THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @FirstName IS NOT NULL THEN (CASE WHEN [FirstName] = @FirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @LastName IS NOT NULL THEN (CASE WHEN [LastName] = @LastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Email IS NOT NULL THEN (CASE WHEN [Email] = @Email THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @PwdHash IS NOT NULL THEN (CASE WHEN [PwdHash] = @PwdHash THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Salt IS NOT NULL THEN (CASE WHEN [Salt] = @Salt THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[User]
		(
	 [Login],
	 [FirstName],
	 [LastName],
	 [Email],
	 [Description],
	 [PwdHash],
	 [Salt]
		)
	SELECT 		
			 @Login,
	 @FirstName,
	 @LastName,
	 @Email,
	 @Description,
	 @PwdHash,
	 @Salt
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[User] e
WHERE
	(CASE WHEN @Login IS NOT NULL THEN (CASE WHEN [Login] = @Login THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @FirstName IS NOT NULL THEN (CASE WHEN [FirstName] = @FirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @LastName IS NOT NULL THEN (CASE WHEN [LastName] = @LastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Email IS NOT NULL THEN (CASE WHEN [Email] = @Email THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @PwdHash IS NOT NULL THEN (CASE WHEN [PwdHash] = @PwdHash THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Salt IS NOT NULL THEN (CASE WHEN [Salt] = @Salt THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
