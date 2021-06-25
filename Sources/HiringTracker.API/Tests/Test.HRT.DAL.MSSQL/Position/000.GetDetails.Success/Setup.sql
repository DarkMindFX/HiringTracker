

DECLARE @ID BIGINT = NULL
DECLARE @DepartmentID BIGINT = 10
DECLARE @Title NVARCHAR(50) = 'Title 56c5cef7f2e64fc5b4c66e697101e256'
DECLARE @ShortDesc NVARCHAR(250) = 'ShortDesc 56c5cef7f2e64fc5b4c66e697101e256'
DECLARE @Description NVARCHAR(4000) = 'Description 56c5cef7f2e64fc5b4c66e697101e256'
DECLARE @StatusID BIGINT = 4
DECLARE @CreatedDate DATETIME = '2/27/2020 1:34:12 PM'
DECLARE @CreatedByID BIGINT = 100001
DECLARE @ModifiedDate DATETIME = '5/21/2021 11:22:12 AM'
DECLARE @ModifiedByID BIGINT = 100001
 


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

SELECT TOP 1 
	@ID = [ID]
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

SELECT 
	@ID
