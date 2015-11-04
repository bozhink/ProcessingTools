CREATE TABLE [Environments].[Names]
(
    [Id] INT NOT NULL IDENTITY, 
    [ContentId] CHAR(10) NOT NULL , 
    [Content] NVARCHAR(MAX) NOT NULL, 
    PRIMARY KEY ([Id])
)
