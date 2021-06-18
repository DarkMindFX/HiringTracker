DECLARE @ID BIGINT
DECLARE @DepartmentID BIGINT = 909026
DECLARE @Title NVARCHAR(50) = 'Title 0d7bbd3eaa7646c0b95c47c567359b90'
DECLARE @ShortDesc NVARCHAR(250) = 'ShortDesc 0d7bbd3eaa7646c0b95c47c567359b90'
DECLARE @Description NVARCHAR(4000) = 'Description 0d7bbd3eaa7646c0b95c47c567359b90'
DECLARE @StatusID BIGINT = 2
DECLARE @CreatedDate DATETIME = '3/7/2019 12:16:03 AM'
DECLARE @CreatedByID BIGINT = 100003
DECLARE @ModifiedDate DATETIME = '11/18/2021 3:27:03 PM'
DECLARE @ModifiedByID BIGINT = 100002

DELETE FROM [dbo].[Position]
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