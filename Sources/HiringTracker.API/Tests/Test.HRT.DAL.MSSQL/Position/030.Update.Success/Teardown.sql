

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @DepartmentID BIGINT = 3
DECLARE @Title NVARCHAR(50) = 'Title 5ab2e4070dd9476c897edc3ae7e673e9'
DECLARE @ShortDesc NVARCHAR(250) = 'ShortDesc 5ab2e4070dd9476c897edc3ae7e673e9'
DECLARE @Description NVARCHAR(4000) = 'Description 5ab2e4070dd9476c897edc3ae7e673e9'
DECLARE @StatusID BIGINT = 4
DECLARE @CreatedDate DATETIME = '9/10/2020 5:42:11 AM'
DECLARE @CreatedByID BIGINT = 100002
DECLARE @ModifiedDate DATETIME = '10/25/2018 7:30:11 AM'
DECLARE @ModifiedByID BIGINT = 33000067
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updDepartmentID BIGINT = 3
DECLARE @updTitle NVARCHAR(50) = 'Title 3c6b40a98c7c4381975a3cef4c630c20'
DECLARE @updShortDesc NVARCHAR(250) = 'ShortDesc 3c6b40a98c7c4381975a3cef4c630c20'
DECLARE @updDescription NVARCHAR(4000) = 'Description 3c6b40a98c7c4381975a3cef4c630c20'
DECLARE @updStatusID BIGINT = 1
DECLARE @updCreatedDate DATETIME = '11/26/2022 3:05:11 PM'
DECLARE @updCreatedByID BIGINT = 33000067
DECLARE @updModifiedDate DATETIME = '11/26/2022 3:05:11 PM'
DECLARE @updModifiedByID BIGINT = 33000067
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[Position]
				WHERE 
	(CASE WHEN @updDepartmentID IS NOT NULL THEN (CASE WHEN [DepartmentID] = @updDepartmentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTitle IS NOT NULL THEN (CASE WHEN [Title] = @updTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updShortDesc IS NOT NULL THEN (CASE WHEN [ShortDesc] = @updShortDesc THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDescription IS NOT NULL THEN (CASE WHEN [Description] = @updDescription THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updStatusID IS NOT NULL THEN (CASE WHEN [StatusID] = @updStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[Position]
	WHERE 
	(CASE WHEN @DepartmentID IS NOT NULL THEN (CASE WHEN [DepartmentID] = @DepartmentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN [Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ShortDesc IS NOT NULL THEN (CASE WHEN [ShortDesc] = @ShortDesc THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @StatusID IS NOT NULL THEN (CASE WHEN [StatusID] = @StatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[Position]
	WHERE 
	(CASE WHEN @updDepartmentID IS NOT NULL THEN (CASE WHEN [DepartmentID] = @updDepartmentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTitle IS NOT NULL THEN (CASE WHEN [Title] = @updTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updShortDesc IS NOT NULL THEN (CASE WHEN [ShortDesc] = @updShortDesc THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDescription IS NOT NULL THEN (CASE WHEN [Description] = @updDescription THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updStatusID IS NOT NULL THEN (CASE WHEN [StatusID] = @updStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Position was not updated', 1
END