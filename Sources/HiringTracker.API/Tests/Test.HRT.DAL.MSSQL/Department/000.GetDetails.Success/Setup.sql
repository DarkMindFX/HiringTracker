﻿DECLARE @Name AS NVARCHAR(50) = '[Name BEF0FD82C7C54784995C028C298706A8]'
DECLARE @UUID AS NVARCHAR(50) = '[UUID BEF0FD82C7C54784995C028C298706A8]'
DECLARE @ParentID AS BIGINT = NULL

DECLARE @ID AS BIGINT


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Department]
				WHERE 
					(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
					(CASE WHEN @UUID IS NOT NULL THEN (CASE WHEN [UUID] = @UUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
					(CASE WHEN @ParentID IS NOT NULL THEN (CASE WHEN [ParentID] = @ParentID THEN 1 ELSE 0 END) ELSE 1 END) = 1))
BEGIN
	INSERT INTO [dbo].[Department]
	SELECT 		
		@Name, @UUID, @ParentID
END

SELECT TOP 1 @ID = [ID] 
FROM 
	[dbo].[Department] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UUID IS NOT NULL THEN (CASE WHEN [UUID] = @UUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @ParentID IS NOT NULL THEN (CASE WHEN [ParentID] = @ParentID THEN 1 ELSE 0 END) ELSE 1 END) = 1

SELECT @ID