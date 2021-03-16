DECLARE @skillName AS NVARCHAR(50) = '[Test QAZXCVFR] Delete Skill'

IF(NOT EXISTS(SELECT 1 FROM dbo.Skill s WHERE s.Name = @skillName) ) 
BEGIN
    INSERT INTO dbo.Skill ([Name])
    VALUES (@skillName)
END