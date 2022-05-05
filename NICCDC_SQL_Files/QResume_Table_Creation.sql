USE QResume_dev
GO

/*************************Tables Dealing with Creator Data*******************************/

CREATE TABLE dbo.Users( /*Created when the user enters the email on main page*/
	/*User*/
	ID UNIQUEIDENTIFIER DEFAULT(NEWID()) NOT NULL PRIMARY KEY,
	Name nvarchar(100) NOT NULL, /*Filled when Personal Data is entered in DataPersonal*/
	Email nvarchar(100) NOT NULL,
	DateCreated datetime2 DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
);

CREATE TABLE dbo.DataPersonal( /*Personal info about the applicant*/
	/*Name*/
	FirstName nvarchar(75) NOT NULL,
	MiddleInit nvarchar(3) NULL,
	LastName nvarchar(75) NOT NULL,
	/*Address*/
	Address nvarchar(150) NOT NULL,
	City nvarchar(100) NOT NULL,
	State nvarchar(100) NOT NULL,
	Zip nvarchar(10) NOT NULL,
	/*Contact Info*/
	Email nvarchar(150) NOT NULL,
	DayPhone nvarchar(15) NULL,
	EveningPhone nvarchar(15) NULL,
	MobilePhone nvarchar(15) NOT NULL,
	/*Is US Citizen*/
	IsUSCitizen bit NOT NULL,
	/*Misc*/
	UserID UNIQUEIDENTIFIER NOT NULL,
	ID int IDENTITY(1,1) NOT NULL,
	DateEntered datetime2 DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
);

CREATE TABLE dbo.DataGeneral( /*General info about the applicant*/
	/*General*/
	Degree nvarchar(8) NOT NULL,
	DegreeStatus nvarchar(30) NOT NULL,
	AntiGradDate datetime2 NOT NULL,
	/*GPA*/
	OverallGPA decimal(3,2) NOT NULL,
	MajorGPA decimal(3,2) NOT NULL,
	/*SAT*/
	SATV smallint NULL,
	SATM smallint NULL,
	/*ACT*/
	ACTV smallint NULL,
	ACTM smallint NULL,
	/*GRE*/
	GREV smallint NULL,
	GREQ smallint NULL,
	GREA smallint NULL,
	/*Misc*/
	UserID UNIQUEIDENTIFIER NOT NULL,
	ID int IDENTITY(1,1) NOT NULL,
	DateEntered datetime2 DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
);

CREATE TABLE dbo.DataEducation(
	/*General*/
	Name nvarchar(250) NOT NULL,
	Major nvarchar(250) NOT NULL,
	DegreeEarned nvarchar(10) NULL,
	DegreeDate datetime2 NULL,
	SkillsGained nvarchar(max) NULL,
	/*School Address*/
	SchoolCity nvarchar(100) NOT NULL,
	SchoolState nvarchar(100) NOT NULL,
	SchoolZip nvarchar(350) NOT NULL,
	/*GPA*/
	OverallGPA decimal (3,2) NOT NULL,
	MajorGPA decimal (3,2) NOT NULL,
	/*Misc*/
	UserID UNIQUEIDENTIFIER NOT NULL,
	ID int IDENTITY(1,1) NOT NULL,
	DateEntered datetime2 DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
);

CREATE TABLE dbo.DataProfessional(
	/*Profession*/
	Name nvarchar(250) NOT NULL,
	Position nvarchar(50) NOT NULL,
	SkillsGained nvarchar(max) NULL,
	/*Location*/
	ProfessionCity nvarchar(100) NOT NULL,
	ProfessionState nvarchar(100) NOT NULL,
	/*Date*/
	DateStart datetime2 NOT NULL,
	DateEnd datetime2 NOT NULL,
	/*Misc*/
	UserID UNIQUEIDENTIFIER NOT NULL,
	ID int IDENTITY(1,1) NOT NULL,
	DateEntered datetime2 DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
);

CREATE TABLE dbo.DataCertifications(
	/*Certification*/
	Certification nvarchar(250) NOT NULL,
	Date datetime2 NOT NULL,
	Completed bit NOT NULL,
	/*Misc*/
	UserID UNIQUEIDENTIFIER NOT NULL,
	ID int IDENTITY(1,1) NOT NULL,
	DateEntered datetime2 DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
);

CREATE TABLE dbo.DataSkills(
	/*Skill*/
	Skill nvarchar(500) NOT NULL,
	/*Misc*/
	UserID UNIQUEIDENTIFIER NOT NULL,
	ID int IDENTITY(1,1) NOT NULL,
	DateEntered datetime2 DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
);

CREATE TABLE dbo.DataAwards(
	/*Award*/
	Award nvarchar(150) NOT NULL,
	EarnDate datetime2 NOT NULL,
	/*Misc*/
	UserID UNIQUEIDENTIFIER NOT NULL,
	ID int IDENTITY(1,1) NOT NULL,
	DateEntered datetime2 DEFAULT(CURRENT_TIMESTAMP) NOT NULL,
);


ALTER TABLE DataPersonal
ADD FOREIGN KEY (UserID) REFERENCES Users(ID);

ALTER TABLE DataGeneral
ADD FOREIGN KEY (UserID) REFERENCES Users(ID);

ALTER TABLE DataEducation
ADD FOREIGN KEY (UserID) REFERENCES Users(ID);

ALTER TABLE DataProfessional
ADD FOREIGN KEY (UserID) REFERENCES Users(ID);

ALTER TABLE DataCertifications
ADD FOREIGN KEY (UserID) REFERENCES Users(ID);

ALTER TABLE dbo.DataSkills
ADD FOREIGN KEY (UserID) REFERENCES Users(ID);

ALTER TABLE dbo.DataAwards
ADD FOREIGN KEY (UserID) REFERENCES Users(ID);