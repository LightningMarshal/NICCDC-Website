USE QResume_dev
GO

/*************************Grant Execute for Reader*******************************/


/*************************Grant Execute for Editor*******************************/
GRANT EXECUTE ON dbo.DataAddPersonal TO iusr_qresume_editor
GRANT EXECUTE ON dbo.DataAddEducational TO iusr_qresume_editor
GRANT EXECUTE ON dbo.AddTempUser TO iusr_qresume_editor