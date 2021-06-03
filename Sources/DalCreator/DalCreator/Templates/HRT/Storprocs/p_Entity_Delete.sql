

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_{Entity}_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_{Entity}_Delete]
GO

CREATE PROCEDURE [dbo].[p_{Entity}_Delete]
	{PARAMS_PK_LIST},
	@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[{Entity}]  WHERE {WHERE_PK_LIST} ) )
	BEGIN
		DELETE 
		FROM 
			[dbo].[{Entity}] 
		WHERE 
			{WHERE_PK_LIST}

		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO