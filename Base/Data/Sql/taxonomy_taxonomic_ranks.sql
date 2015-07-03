IF OBJECT_ID ('taxonomic_ranks') IS NOT NULL
	DROP TABLE [dbo].[taxonomic_ranks];
GO
CREATE TABLE [dbo].[taxonomic_ranks]
(
Id INT IDENTITY(1,1) PRIMARY KEY,
RankName VARCHAR(30) NOT NULL
);
GO
IF OBJECT_ID ('insertNewRank', 'TR') IS NOT NULL
	DROP TRIGGER insertNewRank;
GO
CREATE TRIGGER insertNewRank ON [dbo].[taxonomic_ranks]
AFTER INSERT
AS
--IF EXISTS (SELECT COUNT(RankName) FROM [dbo].[taxonomic_ranks])
--BEGIN
--	RAISERROR ('XXX',3,0)
--	ROLLBACK
--	RETURN
--END
--GO

INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('above-genus');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('clade');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('class');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('cohort');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('division');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('family');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('genus');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('infraclass');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('infrakingdom');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('infraorder');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('infraphylum');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('kingdom');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('order');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('parvorder');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('phylum');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('section');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('subclade');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('subclass');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('subdivision');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('subfamily');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('subgenus');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('subkingdom');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('suborder');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('subphylum');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('subsection');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('subtribe');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('superclass');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('superfamily');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('supergroup');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('superorder');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('superphylum');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('supertribe');
INSERT INTO [dbo].[taxonomic_ranks] (RankName) VALUES ('tribe');
