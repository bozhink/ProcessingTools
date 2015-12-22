ALTER TABLE [Environments].[Groups]
    ADD CONSTRAINT [FK_Groups_Entities]
    FOREIGN KEY ([EntityId])
    REFERENCES [Environments].[Entities] ([Id])
