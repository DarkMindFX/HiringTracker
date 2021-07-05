

DECLARE @ID BIGINT = NULL
DECLARE @FirstName NVARCHAR(50) = 'FirstName 7f9466385b1e4fea889cd491d1b1771c'
DECLARE @MiddleName NVARCHAR(50) = 'MiddleName 7f9466385b1e4fea889cd491d1b1771c'
DECLARE @LastName NVARCHAR(50) = 'LastName 7f9466385b1e4fea889cd491d1b1771c'
DECLARE @Email NVARCHAR(50) = 'Email 7f9466385b1e4fea889cd491d1b1771c'
DECLARE @Phone NVARCHAR(50) = 'Phone 7f9466385b1e4fea889cd491d1b1771c'
DECLARE @CVLink NVARCHAR(1000) = 'CVLink 7f9466385b1e4fea889cd491d1b1771c'
DECLARE @CreatedByID BIGINT = 100003
DECLARE @CreatedDate DATETIME = '3/19/2019 2:13:32 PM'
DECLARE @ModifiedByID BIGINT = 100003
DECLARE @ModifiedDate DATETIME = '6/4/2021 11:36:32 PM'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Candidate]
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
 ))
					
BEGIN
	INSERT INTO [dbo].[Candidate]
		(
	 [FirstName],
	 [MiddleName],
	 [LastName],
	 [Email],
	 [Phone],
	 [CVLink],
	 [CreatedByID],
	 [CreatedDate],
	 [ModifiedByID],
	 [ModifiedDate]
		)
	SELECT 		
			 @FirstName,
	 @MiddleName,
	 @LastName,
	 @Email,
	 @Phone,
	 @CVLink,
	 @CreatedByID,
	 @CreatedDate,
	 @ModifiedByID,
	 @ModifiedDate
END

SELECT TOP 1 
	@ID = [ID]
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

SELECT 
	@ID
