USE QResume_dev
GO

/*************************Procedures Dealing with Creator Data*******************************/

/***************************************Reader Calls*****************************************/
CREATE PROCEDURE dbo.GetEmail

@UserID uniqueidentifier

AS
BEGIN
	SET NOCOUNT ON;	

	SELECT Email FROM Users WHERE ID = @UserID

END
GO

/***************************************Writer Calls*****************************************/
CREATE PROCEDURE dbo.AddUser /* This will add the user as a temporary user */

@Email nvarchar(100)

AS
BEGIN
	SET NOCOUNT ON;

	IF ((SELECT COUNT(*) FROM Users WHERE Email = @Email) = 0)
		INSERT INTO dbo.Users(Name,Email)
		Output inserted.ID
		VALUES('Temp',@Email)
	ELSE
		SELECT ID FROM Users WHERE Email = @Email


END
GO



CREATE PROCEDURE dbo.DataAddPersonal /* This will add a Personal Data entry */

@UserID uniqueidentifier,
@FirstName nvarchar(75),
@LastName nvarchar(75),
@Email nvarchar(150),
@PhoneNumber nvarchar(15),
@Address nvarchar(350),
@Goal nvarchar(500)

AS
BEGIN
	SET NOCOUNT ON

	IF ((SELECT COUNT(Name) FROM Users WHERE ID = @UserID) > 0)
		UPDATE dbo.Users
		SET Name = @FirstName + ' ' + @LastName
		WHERE ID = @UserID AND Name = 'Temp'

	IF ((SELECT COUNT(*) FROM DataPersonal WHERE @UserID = UserID AND @FirstName = FirstName AND @LastName = LastName AND @Email = Email AND @PhoneNumber = PhoneNumber AND @Address = Address AND @Goal = Goal) = 0) /* Prevent Duplicates */
		INSERT INTO dbo.DataPersonal(UserID,FirstName,LastName,Email,PhoneNumber,Address,Goal)
		Output inserted.ID
		VALUES(@UserID,@FirstName,@LastName,@Email,@PhoneNumber,@Address,@Goal);
	ELSE
		SELECT(-10)

END
GO



CREATE PROCEDURE dbo.DataAddEducational /* This will add an Educational Data entry */

@UserID uniqueidentifier,
@Name nvarchar(250),
@Location nvarchar(350),
@DateStart datetime2(7),
@DateEnd datetime2(7),
@SkillList nvarchar(max),
@GPA decimal(3,2)


AS
BEGIN
	SET NOCOUNT ON;

	IF ((SELECT COUNT(*) FROM DataEducation WHERE @UserID = UserID AND @Name = Name AND @Location = Location AND @DateStart = DateStart AND @DateEnd = DateEnd AND @GPA = GPA AND @SkillList = SkillList) = 0)
		INSERT INTO dbo.DataEducation(UserID,Name,Location,DateStart,DateEnd,SkillList,GPA)
		Output inserted.ID
		VALUES(@UserID,@Name,@Location,@DateStart,@DateEnd,@SkillList,@GPA)
	ELSE
		SELECT(-10)
END
GO



CREATE PROCEDURE dbo.DataAddTraining

@UserID uniqueidentifier,
@Desc nvarchar(250),
@Date datetime2(7),
@Completed bit

AS
BEGIN
	SET NOCOUNT ON;

	IF ((SELECT COUNT(*) FROM DataTrainings WHERE @UserID = UserID AND @Desc = Training AND @Date = EarnDate AND @Completed = Completed) = 0)
		INSERT INTO dbo.DataTrainings(UserID,Training,EarnDate,Completed)
		OUTPUT inserted.ID
		VALUES(@UserID, @Desc, @Date, @Completed)
	ELSE
		SELECT(-10)

END
GO



CREATE PROCEDURE dbo.DataAddProfessional

@UserID uniqueidentifier,
@Name nvarchar(250),
@Position nvarchar(50),
@Location nvarchar(350),
@DateStart datetime2,
@DateEnd datetime2,
@SkillList nvarchar(max)

AS
BEGIN
	SET NOCOUNT ON;

	IF ((SELECT COUNT(*) FROM DataProfessional WHERE @UserID = UserID AND @Name = Name AND @Position = Position AND @Location = Location AND @DateStart = DateStart AND @DateEnd = DateEnd AND @SkillList = SkillList) = 0)
		INSERT INTO dbo.DataProfessional(UserID,Name,Position,Location,DateStart,DateEnd,SkillList)
		OUTPUT inserted.ID
		VALUES(@UserID,@Name,@Position,@Location,@DateStart,@DateEnd,@SkillList)
	ELSE
		SELECT(-10)

END
GO



CREATE PROCEDURE dbo.DataAddKnowledge

@UserID uniqueidentifier,
@Desc nvarchar(500)

AS
BEGIN
	SET NOCOUNT ON;

	IF ((SELECT COUNT(*) FROM DataSkills WHERE @UserID = UserID AND @Desc = Skill) = 0)
		INSERT INTO dbo.DataSkills(UserID,Skill)
		OUTPUT inserted.ID
		VALUES(@UserID, @Desc)
	ELSE
		SELECT(-10)

END
GO



CREATE PROCEDURE dbo.DataAddAward

@UserID uniqueidentifier,
@Desc nvarchar(150),
@Date datetime2

AS
BEGIN
	SET NOCOUNT ON;

	IF ((SELECT COUNT(*) FROM DataAwards WHERE @UserID = UserID AND @Desc = Award AND @Date = EarnDate) = 0)
		INSERT INTO dbo.DataAwards(UserID,Award,EarnDate)
		OUTPUT inserted.ID
		VALUES(@UserID, @Desc, @Date)
	ELSE
		SELECT(-10)

END
GO
