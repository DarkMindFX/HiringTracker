

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Login NVARCHAR(50) = 'Login 1b0a9e98578f4133befb3b2c3607f2d0'
DECLARE @FirstName NVARCHAR(50) = 'FirstName 1b0a9e98578f4133befb3b2c3607f2d0'
DECLARE @LastName NVARCHAR(50) = 'LastName 1b0a9e98578f4133befb3b2c3607f2d0'
DECLARE @Email NVARCHAR(50) = 'Email 1b0a9e98578f4133befb3b2c3607f2d0'
DECLARE @Description NVARCHAR(255) = 'Description 1b0a9e98578f4133befb3b2c3607f2d0'
DECLARE @PwdHash NVARCHAR(255) = 'PwdHash 1b0a9e98578f4133befb3b2c3607f2d0'
DECLARE @Salt NVARCHAR(255) = 'Salt 1b0a9e98578f4133befb3b2c3607f2d0'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updLogin NVARCHAR(50) = 'Login f54bc868ab8b47ddb7f6ea4e354299a6'
DECLARE @updFirstName NVARCHAR(50) = 'FirstName f54bc868ab8b47ddb7f6ea4e354299a6'
DECLARE @updLastName NVARCHAR(50) = 'LastName f54bc868ab8b47ddb7f6ea4e354299a6'
DECLARE @updEmail NVARCHAR(50) = 'Email f54bc868ab8b47ddb7f6ea4e354299a6'
DECLARE @updDescription NVARCHAR(255) = 'Description f54bc868ab8b47ddb7f6ea4e354299a6'
DECLARE @updPwdHash NVARCHAR(255) = 'PwdHash f54bc868ab8b47ddb7f6ea4e354299a6'
DECLARE @updSalt NVARCHAR(255) = 'Salt f54bc868ab8b47ddb7f6ea4e354299a6'
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[User]
				WHERE 
	(CASE WHEN @updLogin IS NOT NULL THEN (CASE WHEN [Login] = @updLogin THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updFirstName IS NOT NULL THEN (CASE WHEN [FirstName] = @updFirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updLastName IS NOT NULL THEN (CASE WHEN [LastName] = @updLastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updEmail IS NOT NULL THEN (CASE WHEN [Email] = @updEmail THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDescription IS NOT NULL THEN (CASE WHEN [Description] = @updDescription THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updPwdHash IS NOT NULL THEN (CASE WHEN [PwdHash] = @updPwdHash THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSalt IS NOT NULL THEN (CASE WHEN [Salt] = @updSalt THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[User]
	WHERE 
	(CASE WHEN @Login IS NOT NULL THEN (CASE WHEN [Login] = @Login THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @FirstName IS NOT NULL THEN (CASE WHEN [FirstName] = @FirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @LastName IS NOT NULL THEN (CASE WHEN [LastName] = @LastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Email IS NOT NULL THEN (CASE WHEN [Email] = @Email THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @PwdHash IS NOT NULL THEN (CASE WHEN [PwdHash] = @PwdHash THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Salt IS NOT NULL THEN (CASE WHEN [Salt] = @Salt THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[User]
	WHERE 
	(CASE WHEN @updLogin IS NOT NULL THEN (CASE WHEN [Login] = @updLogin THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updFirstName IS NOT NULL THEN (CASE WHEN [FirstName] = @updFirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updLastName IS NOT NULL THEN (CASE WHEN [LastName] = @updLastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updEmail IS NOT NULL THEN (CASE WHEN [Email] = @updEmail THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDescription IS NOT NULL THEN (CASE WHEN [Description] = @updDescription THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updPwdHash IS NOT NULL THEN (CASE WHEN [PwdHash] = @updPwdHash THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSalt IS NOT NULL THEN (CASE WHEN [Salt] = @updSalt THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'User was not updated', 1
END