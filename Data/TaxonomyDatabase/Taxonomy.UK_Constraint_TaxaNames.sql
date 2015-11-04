ALTER TABLE [Taxonomy].[HigherTaxaNames]
    ADD CONSTRAINT [UK_Constraint_TaxaNames]
    UNIQUE ([Name])
