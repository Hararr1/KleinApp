CREATE PROCEDURE [dbo].[spUserRegister]
@UserId nvarchar (128),
@FirstName nvarchar(50),
@LastName nvarchar(50),
@EmailAddress nvarchar(256)

AS
BEGIN
	
	
	INSERT INTO [dbo].[User] (UserId, FirstName, LastName, EmailAddress)
	VALUES (@UserId, @FirstName, @LastName, @EmailAddress)
END
