ALTER TABLE [Environments].[Names]
    ADD CONSTRAINT [FK_Names_Entities]
    FOREIGN KEY ([ContentId])
    REFERENCES [Environments].[Entities] ([Id])
