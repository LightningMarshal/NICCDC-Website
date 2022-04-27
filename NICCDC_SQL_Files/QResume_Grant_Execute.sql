USE QResume_dev
GO

/*************************Grant Execute for Reader*******************************/
GRANT EXECUTE ON dbo.GetEmail TO iusr_qresume_reader

/*************************Grant Execute for Editor*******************************/
GRANT EXECUTE ON dbo.AddUser TO iusr_qresume_editor
GRANT EXECUTE ON dbo.DataAddPersonal TO iusr_qresume_editor
GRANT EXECUTE ON dbo.DataAddGeneral TO iusr_qresume_editor
GRANT EXECUTE ON dbo.DataAddEducational TO iusr_qresume_editor
GRANT EXECUTE ON dbo.DataAddTraining TO iusr_qresume_editor
GRANT EXECUTE ON dbo.DataAddProfessional TO iusr_qresume_editor
GRANT EXECUTE ON dbo.DataAddKnowledge TO iusr_qresume_editor
GRANT EXECUTE ON dbo.DataAddAward TO iusr_qresume_editor