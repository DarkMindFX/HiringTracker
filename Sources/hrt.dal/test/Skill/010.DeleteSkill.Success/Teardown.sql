DECLARE @skillName AS NVARCHAR(50) = '[Test QAZXCVFR] Delete Skill'

IF( EXISTS(SELECT 1 FROM dbo.Skill s WHERE s.Name = @skillName) ) 
BEGIN
    THROW 51001, 'Skill was not deleted', 1
END
ELSE


DELETE FROM dbo.Skill
    WHERE
        Name = @skillName
