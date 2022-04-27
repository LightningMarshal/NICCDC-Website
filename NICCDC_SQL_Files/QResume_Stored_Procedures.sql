USE QResume_dev
GO

/* NOTE : Duplicate detection has been commented out. */


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

/*Name*/
@FirstName nvarchar(75),
@MiddleInit nvarchar(3),
@LastName nvarchar(75),
/*Address*/
@Address nvarchar(150),
@City nvarchar(100),
@State nvarchar(100),
@Zip nvarchar(10),
/*Contact Info*/
@Email nvarchar(150),
@DayPhone nvarchar(15),
@EveningPhone nvarchar(15),
@MobilePhone nvarchar(15),
/*Is US Citizen*/
@IsUSCitizen bit,
/*Misc*/
@UserID uniqueidentifier

AS
BEGIN
	SET NOCOUNT ON

	IF ((SELECT COUNT(Name) FROM Users WHERE ID = @UserID) > 0)

		UPDATE dbo.Users
		SET Name = @FirstName + ' ' + @LastName
		WHERE ID = @UserID AND Name = 'Temp'

	/*IF ((SELECT COUNT(*) FROM DataPersonal WHERE @FirstName = FirstName AND @MiddleInit = MiddleInit 
		AND	@LastName = LastName AND @Address = Address AND @City = City AND @State = State 
		AND	@Zip = Zip AND @Email = Email AND @DayPhone = DayPhone AND @EveningPhone = EveningPhone 
		AND @MobilePhone = MobilePhone AND @IsUSCitizen = IsUSCitizen) = 0) /* Prevent Duplicates */
		*/
		INSERT INTO dbo.DataPersonal(FirstName,MiddleInit,LastName,Address,City,State,Zip,Email,DayPhone,
			EveningPhone,MobilePhone,IsUSCitizen,UserID)
		Output inserted.ID
		VALUES(@FirstName,@MiddleInit,@LastName,@Address,@City,@State,@Zip,@Email,@DayPhone,@EveningPhone,
			@MobilePhone,@IsUSCitizen,@UserID);
	/*
	ELSE

		SELECT(-10)
	*/
END
GO



CREATE PROCEDURE dbo.DataAddGeneral /* This will add a General Data entry */

/*General*/
@Degree nvarchar(8),
@DegreeStatus nvarchar(30),
@AntiGradDate datetime2,
/*GPA*/
@OverallGPA decimal(3,2),
@MajorGPA decimal(3,2),
/*SAT*/
@SATV smallint = NULL,
@SATM smallint = NULL,
/*ACT*/
@ACTV smallint = NULL,
@ACTM smallint = NULL,
/*GRE*/
@GREV smallint = NULL,
@GREQ smallint = NULL,
@GREA smallint = NULL,
/*Misc*/
@UserID uniqueidentifier

AS
BEGIN
	SET NOCOUNT ON;
	/*
	IF ((SELECT COUNT(*) FROM DataGeneral WHERE @UserID = UserID AND @Degree = Degree AND @DegreeStatus = DegreeStatus 
		AND @AntiGradDate = AntiGradDate AND @HInstitution = HInstitution AND @OverallGPA = OverallGPA 
		AND @MajorGPA = MajorGPA) = 0) /* Prevent Duplicates */
	*/
		INSERT INTO dbo.DataGeneral(Degree,DegreeStatus,AntiGradDate,OverallGPA,MajorGPA,SATV,SATM,ACTV, ACTM,GREV,GREQ,GREA,UserID)
		Output inserted.ID
		VALUES(@Degree,@DegreeStatus,@AntiGradDate,@OverallGPA,@MajorGPA,@SATV,@SATM,@ACTV,@ACTM,@GREV,@GREQ,@GREA, @UserID)
	/*
	ELSE

		SELECT(-10)
	*/
END
GO



CREATE PROCEDURE dbo.DataAddEducational /* This will add an Educational Data entry */

/*General*/
@Name nvarchar(250),
@Major nvarchar(250),
@DegreeEarned nvarchar(10),
@DegreeDate datetime2(7) = NULL,
@SkillsGained nvarchar(max),
/*School Address*/
@SchoolCity nvarchar(100),
@SchoolState nvarchar(100),
@SchoolZip nvarchar(350),
/*GPA*/
@OverallGPA decimal(3,2),
@MajorGPA decimal(3,2),
/*Misc*/
@UserID uniqueidentifier


AS
BEGIN
	SET NOCOUNT ON;
	/*
	IF ((SELECT COUNT(*) FROM DataEducation WHERE @Name = Name AND @Major = Major AND @SchoolCity = SchoolCity
		AND @SchoolState = SchoolState AND @SchoolZip = SchoolZip AND @OverallGPA = OverallGPA AND @MajorGPA = MajorGPA 
		AND @UserID = UserID)= 0) /* Prevent Duplicates */
	*/
		INSERT INTO dbo.DataEducation(Name,Major,DegreeEarned,DegreeDate,SkillsGained,SchoolCity,SchoolState,SchoolZip,OverallGPA,MajorGPA,UserID)
		Output inserted.ID
		VALUES(@Name,@Major,@DegreeEarned,@DegreeDate,@SkillsGained,@SchoolCity,@SchoolState,@SchoolZip, @OverallGPA, @MajorGPA, @UserID)
	/*
	ELSE

		SELECT(-10)
	*/
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

	/*IF ((SELECT COUNT(*) FROM DataTrainings WHERE @UserID = UserID AND @Desc = Training AND @Date = EarnDate AND @Completed = Completed) = 0)
		*/
		INSERT INTO dbo.DataTrainings(UserID,Training,EarnDate,Completed)
		OUTPUT inserted.ID
		VALUES(@UserID, @Desc, @Date, @Completed)
	/*
	ELSE

		SELECT(-10)
	*/
END
GO



CREATE PROCEDURE dbo.DataAddProfessional

@UserID uniqueidentifier,
@Name nvarchar(250),
@Position nvarchar(50),
@Location nvarchar(350),
@DateStart datetime2,
@DateEnd datetime2,
@SkillsGained nvarchar(max)

AS
BEGIN
	SET NOCOUNT ON;

	/*IF ((SELECT COUNT(*) FROM DataProfessional WHERE @UserID = UserID AND @Name = Name AND @Position = Position AND @Location = Location AND @DateStart = DateStart AND @DateEnd = DateEnd AND @SkillList = SkillList) = 0)
	*/
		INSERT INTO dbo.DataProfessional(UserID,Name,Position,Location,DateStart,DateEnd,SkillsGained)
		OUTPUT inserted.ID
		VALUES(@UserID,@Name,@Position,@Location,@DateStart,@DateEnd,@SkillsGained)
	/*
	ELSE

		SELECT(-10)
	*/
END
GO



CREATE PROCEDURE dbo.DataAddKnowledge

@UserID uniqueidentifier,
@Desc nvarchar(500)

AS
BEGIN
	SET NOCOUNT ON;

	/*IF ((SELECT COUNT(*) FROM DataSkills WHERE @UserID = UserID AND @Desc = Skill) = 0)
	*/
		INSERT INTO dbo.DataSkills(UserID,Skill)
		OUTPUT inserted.ID
		VALUES(@UserID, @Desc)
	/*
	ELSE

		SELECT(-10)
	*/
END
GO



CREATE PROCEDURE dbo.DataAddAward

@UserID uniqueidentifier,
@Desc nvarchar(150),
@Date datetime2

AS
BEGIN
	SET NOCOUNT ON;

	/*IF ((SELECT COUNT(*) FROM DataAwards WHERE @UserID = UserID AND @Desc = Award AND @Date = EarnDate) = 0)
	*/
		INSERT INTO dbo.DataAwards(UserID,Award,EarnDate)
		OUTPUT inserted.ID
		VALUES(@UserID, @Desc, @Date)
	/*
	ELSE

		SELECT(-10)
	*/
END
GO
