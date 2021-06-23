

DECLARE @ID BIGINT = NULL
DECLARE @FirstName NVARCHAR(50) = 'FirstName e47ccd0e328548dbb73b5cdd3ea1c98c'
DECLARE @MiddleName NVARCHAR(50) = 'MiddleName e47ccd0e328548dbb73b5cdd3ea1c98c'
DECLARE @LastName NVARCHAR(50) = 'LastName e47ccd0e328548dbb73b5cdd3ea1c98c'
DECLARE @Email NVARCHAR(50) = 'Email e47ccd0e328548dbb73b5cdd3ea1c98c'
DECLARE @Phone NVARCHAR(50) = 'Phone e47ccd0e328548dbb73b5cdd3ea1c98c'
DECLARE @CVLink NVARCHAR(1000) = 'CVLink e47ccd0e328548dbb73b5cdd3ea1c98c'
DECLARE @CreatedByID BIGINT = 100001
DECLARE @CreatedDate DATETIME = '12/7/2020 3:55:10 PM'
DECLARE @ModifiedByID BIGINT = 100002
DECLARE @ModifiedDate DATETIME = '12/2/2021 3:30:10 AM'
 


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

SELECT TOP 1 @ID = [ID] 
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

SELECT @ID