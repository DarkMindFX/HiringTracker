USE [HiringTracker]
GO
/****** Object:  StoredProcedure [dbo].[p_Position_GetByDepartmentID]    Script Date: 8/21/2021 12:25:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_Position_GetByDepartmentID]

	@DepartmentID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Position] c 
				WHERE
					[DepartmentID] = @DepartmentID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Position] e
		WHERE 
			[DepartmentID] = @DepartmentID	

	END
	ELSE
		SET @Found = 0;
END
GO
