

DECLARE @ID BIGINT = NULL
DECLARE @FirstName NVARCHAR(50) = 'FirstName 650cf135595f4253b0db2b1677150d4b'
DECLARE @MiddleName NVARCHAR(50) = 'MiddleName 650cf135595f4253b0db2b1677150d4b'
DECLARE @LastName NVARCHAR(50) = 'LastName 650cf135595f4253b0db2b1677150d4b'
DECLARE @Email NVARCHAR(50) = 'Email 650cf135595f4253b0db2b1677150d4b'
DECLARE @Phone NVARCHAR(50) = 'Phone 650cf135595f4253b0db2b1677150d4b'
DECLARE @CVLink NVARCHAR(1000) = 'CVLink 650cf135595f4253b0db2b1677150d4b'
DECLARE @CreatedByID BIGINT = 100002
DECLARE @CreatedDate DATETIME = '11/26/2020 6:13:11 PM'
DECLARE @ModifiedByID BIGINT = 100002
DECLARE @ModifiedDate DATETIME = '8/16/2022 9:49:11 PM'
 

DELETE FROM [Candidate]
FROM 
	[dbo].[Candidate] e
WHERE
	(CASE WHEN @FirstName IS NOT NULL THEN (CASE WHEN [FirstName] = @FirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @MiddleName IS NOT NULL THEN (CASE WHEN [MiddleName] = @MiddleName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @LastName IS NOT NULL THEN (CASE WHEN [LastName] = @LastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Email IS NOT NULL THEN (CASE WHEN [Email] = @Email THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Phone IS NOT NULL THEN (CASE WHEN [Phone] = @Phone THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CVLink IS NOT NULL THEN (CASE WHEN [CVLink] = @CVLink THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
