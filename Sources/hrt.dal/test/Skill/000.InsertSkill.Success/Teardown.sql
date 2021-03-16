DECLARE @skillName AS NVARCHAR(50) = '[Test RTYHGFVBN] Inserted Skill'

IF( NOT EXISTS(SELECT 1 FROM dbo.Skill s WHERE s.Name = @skillName) ) 
BEGIN
    THROW 51001, 'Skill was not inserted', 1
END
ELSE
BEGIN

    DELETE FROM dbo.Skill
    WHERE
        Name = @skillName
END