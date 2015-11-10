ALTER TABLE [Journals].[Journals]
    ADD CONSTRAINT [FK_Journals_Publishers]
    FOREIGN KEY ([PublisherId])
    REFERENCES [Journals].[Publishers] ([Id])
