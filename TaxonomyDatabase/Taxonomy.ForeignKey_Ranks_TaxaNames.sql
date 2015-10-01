ALTER TABLE [Taxonomy].[HigherTaxaNames]
    ADD CONSTRAINT [ForeignKey_Ranks_TaxaNames]
    FOREIGN KEY (RankId)
    REFERENCES [Taxonomy].[Ranks] (Id)
