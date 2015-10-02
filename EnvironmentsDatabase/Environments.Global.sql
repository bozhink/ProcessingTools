CREATE TABLE [Environments].[Global]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Content] NVARCHAR(MAX) NOT NULL, 
    [Status] CHAR(1) NOT NULL
)
