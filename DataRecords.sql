IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'DataRecords')
CREATE DATABASE DataRecords;
GO
USE DataRecords
--dropping tables
IF OBJECT_ID('tblEmployees') IS NOT NULL 
DROP TABLE tblEmployees;

IF OBJECT_ID('tblManagers') IS NOT NULL 
DROP TABLE tblManagers;

IF OBJECT_ID('tblReports') IS NOT NULL 
DROP TABLE tblReports;

IF OBJECT_ID('vwReports') IS NOT NULL 
DROP VIEW vwReports

--creating tables
--employees
CREATE TABLE tblEmployees(
	employeeId INT PRIMARY KEY IDENTITY(1,1),
	firstname VARCHAR(20),
	lastname VARCHAR(20),
	jmbg CHAR(13),
	mail VARCHAR(30),
	salary NUMERIC,
	dateOfBirth DATE,
	position VARCHAR(25),
	accountNumber VARCHAR(20),
	username VARCHAR(20) UNIQUE NOT NULL,
	password VARCHAR(20) NOT NULL,
	);
CREATE TABLE tblManagers(
	managerId INT PRIMARY KEY IDENTITY(1,1),
	employeeId INT FOREIGN KEY REFERENCES  tblEmployees(employeeId) ON DELETE SET NULL,
	sector VARCHAR(30) NOT NULL,
	access VARCHAR(15) NOT NULL
);

CREATE TABLE tblReports(
    reportId INT PRIMARY KEY IDENTITY(1,1),
	employeeId INT FOREIGN KEY REFERENCES  tblEmployees(employeeId),
	reportDate DATE NOT NULL,
	project VARCHAR(30) NOT NULL,
	workingHours INT NOT NULL
);

GO
CREATE VIEW vwReports
as
select r.reportId,r.reportDate, CONCAT(e.firstname, ' ', e.lastname) AS fullname,
r.project,e.position,r.workingHours
from tblReports r
inner join tblEmployees e
on e.employeeId = r.employeeId 