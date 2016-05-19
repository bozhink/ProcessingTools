ALTER TABLE [Taxonomy].[HigherTaxaNames]
    ADD CONSTRAINT [FK_Ranks_TaxaNames]
    FOREIGN KEY (RankId)
    REFERENCES [Taxonomy].[Ranks] (Id)
