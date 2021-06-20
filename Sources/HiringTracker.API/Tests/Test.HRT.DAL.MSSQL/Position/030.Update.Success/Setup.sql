DECLARE @ID BIGINT
DECLARE @DepartmentID BIGINT = 104211
DECLARE @Title NVARCHAR(50) = 'Title c1d3435ebbcf4d158b153def98af31b6'
DECLARE @ShortDesc NVARCHAR(250) = 'ShortDesc c1d3435ebbcf4d158b153def98af31b6'
DECLARE @Description NVARCHAR(4000) = 'Description c1d3435ebbcf4d158b153def98af31b6'
DECLARE @StatusID BIGINT = 5
DECLARE @CreatedDate DATETIME = '2/16/2022 1:40:03 AM'
DECLARE @CreatedByID BIGINT = 100003
DECLARE @ModifiedDate DATETIME = '7/7/2019 11:27:03 AM'
DECLARE @ModifiedByID BIGINT = 100004

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
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1))
BEGIN
	INSERT INTO [dbo].[Position]
		([DepartmentID],[Title],[ShortDesc],[Description],[StatusID],[CreatedDate],[CreatedByID],[ModifiedDate],[ModifiedByID])
	SELECT 		
		@DepartmentID,@Title,@ShortDesc,@Description,@StatusID,@CreatedDate,@CreatedByID,@ModifiedDate,@ModifiedByID
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