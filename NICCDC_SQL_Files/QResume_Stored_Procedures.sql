USE QResume_dev
GO

/*************************Procedures Dealing with Creator Data*******************************/

CREATE PROCEDURE dbo.AddTempUser /* This will add the user as a temporary user */

AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.Users(Name,Email)
	Output inserted.ID
	VALUES('Temp','No Email')

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

	IF (SELECT Name FROM Users WHERE @UserID = ID) = 'Temp' AND (SELECT Email FROM Users WHERE @UserID = ID) = 'No Email'
	BEGIN	
		UPDATE dbo.Users
		SET Name = @FirstName, Email = @Email
		WHERE Users.ID = @UserID;
	END

	INSERT INTO dbo.DataPersonal(UserID,FirstName,LastName,Email,PhoneNumber,Address,Goal)
	Output inserted.ID
	VALUES(@UserID,@FirstName,@LastName,@Email,@PhoneNumber,@Address,@Goal);

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

	INSERT INTO dbo.DataEducation(UserID,Name,Location,DateStart,DateEnd,SkillList,GPA)
	Output inserted.ID
	VALUES(@UserID,@Name,@Location,@DateStart,@DateEnd,@SkillList,@GPA)

END
GO
