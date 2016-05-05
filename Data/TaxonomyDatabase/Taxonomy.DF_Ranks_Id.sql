ALTER TABLE [Taxonomy].[Ranks]
    ADD CONSTRAINT [DF_Ranks_Id]
    DEFAULT (newid())
    FOR [Id]
