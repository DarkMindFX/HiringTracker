

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_{Entity}_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_{Entity}_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_{Entity}_GetDetails]
	{PARAMS_PK_LIST},
	@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(SELECT 1 FROM dbo.Candidate c WHERE {WHERE_PK_LIST})) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[{Entity}] e
		WHERE 
			{WHERE_PK_LIST}
	END
	ELSE
		SET @Found = 0;
END
GO