ALTER TABLE [Environments].[Names]
    ADD CONSTRAINT [FK_Names_Entities]
    FOREIGN KEY ([EntityId])
    REFERENCES [Environments].[Entities] ([Id])
