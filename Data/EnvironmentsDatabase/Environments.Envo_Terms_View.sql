CREATE VIEW [Environments].[Envo_Terms_View]
    AS SELECT TOP 10000
        [Environments].[Names].[Content] as content,
        [Environments].[Names].[EntityId] as id,
        [Environments].[Entities].[EnvoId] as envoId
     FROM [Environments].[Names]
     INNER JOIN [Environments].[Entities]
     ON [Environments].[Names].[EntityId] = [Environments].[Entities].[Id]
     WHERE [Environments].[Names].[Content] NOT LIKE 'ENVO%'
     ORDER BY LEN(content) DESC;
