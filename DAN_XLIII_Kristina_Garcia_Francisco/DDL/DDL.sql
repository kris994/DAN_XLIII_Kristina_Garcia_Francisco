-- Dropping the tables before recreating the database in the order depending how the foreign keys are placed.
IF OBJECT_ID('tblReport', 'U') IS NOT NULL DROP TABLE tblReport;
IF OBJECT_ID('tblUser', 'U') IS NOT NULL DROP TABLE tblUser;
if OBJECT_ID('vwUserReport','v') IS NOT NULL DROP VIEW vwUserReport;
if OBJECT_ID('vwManager','v') IS NOT NULL DROP VIEW vwManager;
if OBJECT_ID('vwUser','v') IS NOT NULL DROP VIEW vwUser;

-- Checks if the database already exists.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ReportDB')
CREATE DATABASE ReportDB;
GO

USE ReportDB
CREATE TABLE tblUser(
	UserID INT IDENTITY(1,1) PRIMARY KEY 	NOT NULL,
	FirstName VARCHAR (40)					NOT NULL,
	LastName VARCHAR (40)					NOT NULL,
	JMBG VARCHAR (13) UNIQUE				NOT NULL,
	DateOfBirth DATE 						NOT NULL,
	BankAccount VARCHAR (20) UNIQUE			NOT NULL,
	Email VARCHAR (40) UNIQUE				NOT NULL,
	Salary VARCHAR (40)						NOT NULL,
	Position VARCHAR (40)					NOT NULL,
	Username VARCHAR (40) UNIQUE			NOT NULL,
	UserPassword VARCHAR (40)				NOT NULL,
	Sector VARCHAR (20),
	Access Char (20),
);

USE ReportDB
CREATE TABLE tblReport (
	ReportID INT IDENTITY(1,1) PRIMARY KEY		NOT NULL,
	ReportDate DATE								NOT NULL,
	Project VARCHAR (40)						NOT NULL,
	ReportHours INT								NOT NULL,
	UserID INT FOREIGN KEY REFERENCES tblUser(UserID),
);

GO
CREATE VIEW vwUserReport AS
	SELECT	tblReport.*,
			tblUser.FirstName, tblUser.LastName, tblUser.Position
	FROM	tblUser, tblReport
	WHERE	tblUser.UserID = tblReport.UserID

GO
CREATE VIEW vwUser AS
	SELECT	tblUser.UserID, tblUser.FirstName, tblUser.LastName, tblUser.JMBG,
			tblUser.DateOfBirth, tblUser.BankAccount, tblUser.Email, tblUser.Salary,
			tblUser.Position, tblUser.Username, tblUser.UserPassword
	FROM	tblUser

GO
CREATE VIEW vwManager AS
	SELECT	tblUser.*
	FROM	tblUser