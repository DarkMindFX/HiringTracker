

DECLARE @ID BIGINT = NULL
DECLARE @FirstName NVARCHAR(50) = 'FirstName 9022f43bcc424fe6a8bc313d88f13ebe'
DECLARE @MiddleName NVARCHAR(50) = 'MiddleName 9022f43bcc424fe6a8bc313d88f13ebe'
DECLARE @LastName NVARCHAR(50) = 'LastName 9022f43bcc424fe6a8bc313d88f13ebe'
DECLARE @Email NVARCHAR(50) = 'Email 9022f43bcc424fe6a8bc313d88f13ebe'
DECLARE @Phone NVARCHAR(50) = 'Phone 9022f43bcc424fe6a8bc313d88f13ebe'
DECLARE @CVLink NVARCHAR(1000) = 'CVLink 9022f43bcc424fe6a8bc313d88f13ebe'
DECLARE @CreatedByID BIGINT = 33000067
DECLARE @CreatedDate DATETIME = '2/12/2020 8:17:10 AM'
DECLARE @ModifiedByID BIGINT = 100002
DECLARE @ModifiedDate DATETIME = '8/9/2020 2:04:10 PM'
 


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
