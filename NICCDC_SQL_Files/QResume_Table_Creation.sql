USE QResume_dev
GO

/*************************Tables Dealing with Creator Data*******************************/
CREATE TABLE dbo.Users(
	ID UNIQUEIDENTIFIER DEFAULT(NEWID()) NOT NULL PRIMARY KEY,
	Name nvarchar(100) NOT NULL,
	Email nvarchar(100) NOT NULL,
	DateCreated datetime2 DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
);

CREATE TABLE dbo.DataPersonal(
	UserID UNIQUEIDENTIFIER NOT NULL,
	ID int IDENTITY(1,1) NOT NULL,
	FirstName nvarchar(75) NOT NULL,
	LastName nvarchar(75) NOT NULL,
	Email nvarchar(150) NOT NULL,
	PhoneNumber nvarchar(15) NOT NULL,
	Address nvarchar(350) NOT NULL,
	Goal nvarchar(500) NULL,
	DateEntered datetime2 DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
);

CREATE TABLE dbo.DataEducation(
	UserID UNIQUEIDENTIFIER NOT NULL,
	ID int IDENTITY(1,1) NOT NULL,
	Name nvarchar(250) NOT NULL,
	Location nvarchar(350) NOT NULL,
	DateStart datetime2 NOT NULL,
	DateEnd datetime2 NOT NULL,
	SkillList nvarchar(max) NULL,
	GPA decimal (3,2) NOT NULL,
	DateEntered datetime2 DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
);

CREATE TABLE dbo.DataTrainings(
	UserID UNIQUEIDENTIFIER NOT NULL,
	ID int IDENTITY(1,1) NOT NULL,
	Training nvarchar(250) NOT NULL,
	EarnDate datetime2 NOT NULL,
	Completed bit NOT NULL,
	DateEntered datetime2 DEFAULT(CURRENT_TIMESTAMP) NOT NULL,

);

CREATE TABLE dbo.DataProfessional(
	UserID UNIQUEIDENTIFIER NULL,
	ID int IDENTITY(1,1) NOT NULL,
	Name nvarchar(250) NOT NULL,
	Location nvarchar(350) NOT NULL,
	DateStart datetime2 NOT NULL,
	DateEnd datetime2 NOT NULL,
	SkillList nvarchar(max) NULL,
	Position nvarchar(50) NOT NULL,
	DateEntered datetime2 DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
);

CREATE TABLE dbo.DataAwards(
	UserID UNIQUEIDENTIFIER NOT NULL,
	ID int IDENTITY(1,1) NOT NULL,
	Award nvarchar(150) NOT NULL,
	EarnDate datetime2 NOT NULL,
	DateEntered datetime2 DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
);

CREATE TABLE dbo.DataSkills(
	UserID UNIQUEIDENTIFIER NOT NULL,
	ID int IDENTITY(1,1) NOT NULL,
	Skill nvarchar(500) NOT NULL,
	DateEntered datetime2 DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
);


ALTER TABLE DataPersonal
ADD FOREIGN KEY (UserID) REFERENCES Users(ID);

ALTER TABLE DataEducation
ADD FOREIGN KEY (UserID) REFERENCES Users(ID);

ALTER TABLE DataTrainings
ADD FOREIGN KEY (UserID) REFERENCES Users(ID);

ALTER TABLE DataProfessional
ADD FOREIGN KEY (UserID) REFERENCES Users(ID);

ALTER TABLE dbo.DataAwards
ADD FOREIGN KEY (UserID) REFERENCES Users(ID);

ALTER TABLE dbo.DataSkills
ADD FOREIGN KEY (UserID) REFERENCES Users(ID);