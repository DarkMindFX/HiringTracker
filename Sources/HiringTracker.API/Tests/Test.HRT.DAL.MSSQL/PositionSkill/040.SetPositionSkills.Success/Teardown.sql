

DECLARE @ID BIGINT = NULL
DECLARE @DepartmentID BIGINT = NULL
DECLARE @Title NVARCHAR(50) = 'Title 0f2cccdbb2dc4b8bb1527608550db423'
DECLARE @ShortDesc NVARCHAR(250) = 'ShortDesc 0f2cccdbb2dc4b8bb1527608550db423'
DECLARE @Description NVARCHAR(4000) = 'Description 0f2cccdbb2dc4b8bb1527608550db423'
DECLARE @StatusID BIGINT = 1
DECLARE @CreatedDate DATETIME = '9/7/2023 1:22:34 AM'
DECLARE @CreatedByID BIGINT = 100004
DECLARE @ModifiedDate DATETIME = '8/9/2019 6:25:34 AM'
DECLARE @ModifiedByID BIGINT = 100004

DECLARE @SkillsCount BIGINT = 0
DECLARE @Fail AS BIT = 0

SELECT @SkillsCount = COUNT(1) FROM dbo.PositionSkill ps
	INNER JOIN dbo.Position p ON ps.PositionID = p.ID
WHERE
	(CASE WHEN @DepartmentID IS NOT NULL THEN (CASE WHEN p.[DepartmentID] = @DepartmentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN p.[Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ShortDesc IS NOT NULL THEN (CASE WHEN p.[ShortDesc] = @ShortDesc THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN p.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @StatusID IS NOT NULL THEN (CASE WHEN p.[StatusID] = @StatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN p.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN p.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN p.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN p.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 
IF @SkillsCount <> 3
BEGIN
	SET @Fail = 1
END

DELETE FROM [Position]
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

IF @Fail = 1
BEGIN
	THROW 51001, '[Test Fail] Skills were not set corretly for the position', 1
END