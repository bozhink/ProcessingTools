CREATE TABLE [Taxonomy].[HigherTaxaNames]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(60) NOT NULL, 
    [RankId] INT NOT NULL, 
    [WhiteListed] BIT NOT NULL DEFAULT 0
)
