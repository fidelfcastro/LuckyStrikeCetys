CREATE DATABASE luckystrike

USE [luckystrike]
GO


CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Active] [smallint] NULL
) ON [PRIMARY]

GO

USE [luckystrike]
GO


CREATE TABLE [dbo].[Statistic](
	[StatsId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Playtime] [time](7) NOT NULL,
	[TokenWins] [int] NOT NULL,
	[Tokenlose] [int] NOT NULL,
	[CurrentTokens] [int] NULL
) ON [PRIMARY]

GO

USE [luckystrike]
GO

CREATE TABLE [dbo].[Session](
	[SessionId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeBegin] [datetime] NULL,
	[TimeEnd] [datetime] NULL
) ON [PRIMARY]

GO

ALTER TABLE dbo.[Session]
	ADD CONSTRAINT fk_sessionLog
	FOREIGN KEY (UserId)
	REFERENCES dbo.[User](UserId)

ALTER TABLE dbo.[Statistic]
	ADD CONSTRAINT fk_userStats
	FOREIGN KEY (UserId)
	REFERENCES dbo.[User](UserId)



USE [luckystrike]
GO

CREATE VIEW [dbo].[vw_currentTokens]
AS
SELECT CurrentTokens FROM Statistic s INNER JOIN [User] u on  s.UserId = u.UserId WHERE Active = 1
GO

USE [luckystrike]
GO

CREATE VIEW [dbo].[vw_TotalStats] 
AS
SELECT UserId, 
CAST(DATEADD(millisecond,SUM(DATEDIFF(millisecond,0,CAST(Playtime as datetime))),0) as time) PlaytimeTotal,
SUM(TokenWins) TokenWinsTotal, 
SUM(Tokenlose) TokenloseTotal
FROM Statistic GROUP BY UserId
GO

USE [luckystrike]
GO

CREATE VIEW [dbo].[vw_getUser]
AS
SELECT FirstName, LastName, Username FROM [User] WHERE Active = 1

GO

USE [luckystrike]
GO

CREATE VIEW [dbo].[vw_lastSessionStats]
AS
SELECT TOP 1 Playtime,TokenWins, Tokenlose 
FROM Statistic s 
INNER JOIN [User] u on  s.UserId = u.UserId 
WHERE Active = 1
ORDER BY StatsId DESC

GO


USE [luckystrike]
GO



CREATE PROCEDURE [dbo].[sp_CreateUser]
@Name NVARCHAR(50),
@LastName NVARCHAR(50),
@UserName NVARCHAR(50),
@Password NVARCHAR(50)
AS
BEGIN
INSERT INTO [User](FirstName,LastName,Username,[Password])
VALUES(@Name,@LastName,@UserName,@Password)
END
GO

USE [luckystrike]
GO

CREATE PROCEDURE [dbo].[sp_editUsername]
@Username VARCHAR(50)
AS
BEGIN 
UPDATE [User]
SET Username = @Username
WHERE Active = 1
END


GO

USE [luckystrike]
GO

CREATE PROCEDURE [dbo].[sp_loginValidation]
@UserName NVARCHAR(50), 
@Password NVARCHAR(50),
@active int = 0
AS

	SELECT Username, [Password]  FROM [User] 
	WHERE Username = @UserName AND [Password] = @Password 
	UPDATE [User]
	SET Active = 1 WHERE Username = @UserName
	INSERT INTO [Session] (UserId,TimeBegin)
	VALUES ((SELECT UserId FROM [User] WHERE Username = @UserName), GETDATE())

GO

USE [luckystrike]
GO

CREATE PROCEDURE [dbo].[sp_logout]

AS
BEGIN

UPDATE [User]
SET Active = 0 WHERE Active = 1 


UPDATE [Session]
SET TimeEnd = (GETDATE()) WHERE TimeEnd IS NULL
END


GO

USE [luckystrike]
GO

CREATE PROCEDURE [dbo].[sp_Session]
@ACTIVE INT = 0
AS
BEGIN
INSERT INTO dbo.[Session] (UserId, TimeBegin)
VALUES((SELECT UserId FROM [User] WHERE Active = 1),GETDATE())
END



GO

USE [luckystrike]
GO


CREATE PROCEDURE [dbo].[sp_statsUpdate]
@tkWin int,
@tkLose int,
@tkCurrent int = 100
AS
BEGIN

INSERT INTO Statistic(UserId, Playtime,TokenWins, Tokenlose, CurrentTokens)
VALUES((select DISTINCT UserId from [Session] WHERE TimeEnd = (SELECT MAX(TimeEnd) FROM [Session])),(SELECT convert(varchar(8), dateadd(second, SUM(DATEDIFF(SECOND, TimeBegin,TimeEnd)), 0),  108) from Session WHERE TimeEnd = (SELECT MAX(TimeEnd) FROM [Session])),@tkWin, @tkLose,@tkCurrent)

END





GO










