

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
 


IF(NOT EXISTS(SELECT 1 FROM 
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
 ))
					
BEGIN
	INSERT INTO [dbo].[Position]
		(
	 [DepartmentID],
	 [Title],
	 [ShortDesc],
	 [Description],
	 [StatusID],
	 [CreatedDate],
	 [CreatedByID],
	 [ModifiedDate],
	 [ModifiedByID]
		)
	SELECT 		
			 @DepartmentID,
	 @Title,
	 @ShortDesc,
	 @Description,
	 @StatusID,
	 @CreatedDate,
	 @CreatedByID,
	 @ModifiedDate,
	 @ModifiedByID
END

SELECT TOP 1 @ID = [ID] 
FROM 
	[dbo].[Position] e
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

SELECT @ID