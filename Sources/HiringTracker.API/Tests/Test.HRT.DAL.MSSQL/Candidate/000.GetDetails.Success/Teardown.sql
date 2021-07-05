

DECLARE @ID BIGINT = NULL
DECLARE @FirstName NVARCHAR(50) = 'FirstName b536006398f14919b98887b9a7761a33'
DECLARE @MiddleName NVARCHAR(50) = 'MiddleName b536006398f14919b98887b9a7761a33'
DECLARE @LastName NVARCHAR(50) = 'LastName b536006398f14919b98887b9a7761a33'
DECLARE @Email NVARCHAR(50) = 'Email b536006398f14919b98887b9a7761a33'
DECLARE @Phone NVARCHAR(50) = 'Phone b536006398f14919b98887b9a7761a33'
DECLARE @CVLink NVARCHAR(1000) = 'CVLink b536006398f14919b98887b9a7761a33'
DECLARE @CreatedByID BIGINT = 100002
DECLARE @CreatedDate DATETIME = '3/21/2023 6:11:32 AM'
DECLARE @ModifiedByID BIGINT = 100005
DECLARE @ModifiedDate DATETIME = '2/2/2021 12:25:32 PM'
 

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
