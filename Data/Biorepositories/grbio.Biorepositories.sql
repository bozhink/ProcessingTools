CREATE TABLE [grbio].[Biorepositories]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [URL] VARCHAR(150) NOT NULL, 
    [Name] NVARCHAR(150) NULL, 
    [IH] VARCHAR(3) NULL, 
    [Acronym] VARCHAR(10) NULL, 
    [Additional Institution Names] NVARCHAR(200) NULL, 
    [Institutional Discipline] NVARCHAR(200) NULL, 
    [URL for main institutional website] VARCHAR(150) NULL, 
    [URL for institutional specimen catalog] VARCHAR(150) NULL, 
    [URL for institutional webservices] VARCHAR(150) NULL, 
    [Status] VARCHAR(3) NULL, 
    [Institutional Governance] NVARCHAR(50) NULL, 
    [Institution Type] NVARCHAR(10) NULL, 
    [Description of Institution] NVARCHAR(500) NULL, 
    [Mailing Address 1] NVARCHAR(50) NULL, 
    [Mailing Address 2] NVARCHAR(50) NULL, 
    [Mailing Address 3] NVARCHAR(50) NULL, 
    [City] NVARCHAR(50) NULL, 
    [Province] NVARCHAR(50) NULL, 
    [Postal Code] NCHAR(10) NULL, 
    [Country] NVARCHAR(50) NULL, 
    [Physical Address 1] NVARCHAR(50) NULL, 
    [Physical Address 2] NVARCHAR(50) NULL, 
    [Physical Address 3] NVARCHAR(50) NULL, 
    [City1] NVARCHAR(50) NULL, 
    [Province1] NVARCHAR(50) NULL, 
    [Postal Code 1] NCHAR(10) NULL, 
    [Country1] NVARCHAR(50) NULL, 
    [Primary Contact] NVARCHAR(50) NULL, 
    [CITES permit number] NCHAR(10) NULL, 
    [Date herbarium was founded] NCHAR(10) NULL, 
    [Geographic coverage of herbarium] NVARCHAR(50) NULL, 
    [Taxonomic coverage of herbarium] NVARCHAR(50) NULL, 
    [Number of specimens in herbarium] NVARCHAR(50) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of Institution',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Index Herbariorum Record',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'IH'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Institutional Code/Acronym',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Acronym'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Additional Institution Names',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Additional Institution Names'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Institutional Discipline',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Institutional Discipline'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'URL for main institutional website',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'URL for main institutional website'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'URL for institutional specimen catalog',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'URL for institutional specimen catalog'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'URL for institutional webservices',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'URL for institutional webservices'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Status of Institution: Active?',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Status'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Institutional Governance',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Institutional Governance'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Institution Type',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Institution Type'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Description of Institution',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Description of Institution'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Mailing Address 1',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Mailing Address 1'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Mailing Address 2',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Mailing Address 2'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Mailing Address 3',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Mailing Address 3'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'City/Town',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'City'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'State/Province',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Province'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Postal/Zip Code',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = 'Postal Code'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Country',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Country'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Physical Address 1',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Physical Address 1'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Physical Address 2',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Physical Address 2'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Physical Address 3',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Physical Address 3'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'City/Town',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'City1'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'State/Province',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Province1'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Postal/Zip Code',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = 'Postal Code 1'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Country',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Country1'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Primary Contact',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Primary Contact'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'CITES permit number',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'CITES permit number'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Date herbarium was founded',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Date herbarium was founded'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Geographic coverage of herbarium',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Geographic coverage of herbarium'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Taxonomic coverage of herbarium',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Taxonomic coverage of herbarium'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Number of specimens in herbarium',
    @level0type = N'SCHEMA',
    @level0name = N'grbio',
    @level1type = N'TABLE',
    @level1name = N'Biorepositories',
    @level2type = N'COLUMN',
    @level2name = N'Number of specimens in herbarium'