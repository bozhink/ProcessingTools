CREATE TABLE [Environments].[Names]
(
    [Id] INT NOT NULL IDENTITY, 
    [EntityId] CHAR(10) NOT NULL , 
    [Content] NVARCHAR(MAX) NOT NULL, 
    PRIMARY KEY ([Id])
)
