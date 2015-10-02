ALTER TABLE [Environments].[Groups]
    ADD CONSTRAINT [FK_Groups_Entities]
    FOREIGN KEY ([ContentId])
    REFERENCES [Environments].[Entities] ([Id])
