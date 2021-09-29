CREATE TABLE [dbo].[NewUsers]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [Name] VARCHAR(50) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [EmailAddress] VARCHAR(50) NOT NULL, 
    [PersonImage] VARCHAR(50) NULL,

)
