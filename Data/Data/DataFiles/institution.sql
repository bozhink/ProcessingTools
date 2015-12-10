IF OBJECT_ID ('institutions') IS NOT NULL
   DROP TABLE [dbo].[institutions]
GO
CREATE TABLE [dbo].[institutions]
(
Id int IDENTITY(1,1) PRIMARY KEY,
Name VARCHAR(255) NOT NULL
);
GO

INSERT INTO [dbo].[institutions] (Name) VALUES ('Conselho Nacional de Desenvolvimento Científico e Tecnológico')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Departamento de Zoologia')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Museo de Historia Natural Noel Kempff Mercado')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Museu de Zoologia')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Museu de Zoologia, Universidade de São Paulo')
INSERT INTO [dbo].[institutions] (Name) VALUES ('National Museum of Natural History')
INSERT INTO [dbo].[institutions] (Name) VALUES ('PPG Biologia Animal')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Universidade de São Paulo')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Universidade Federal do Rio Grande do Sul')
INSERT INTO [dbo].[institutions] (Name) VALUES ('University of Nebraska State Museum')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Department of Botany')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Department of Botany, University of Hawaii at Manoa')
INSERT INTO [dbo].[institutions] (Name) VALUES ('University of Hawaii at Manoa')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Royal Botanic Gardens Victoria')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Auckland War Memorial Museum')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Herbarium Pacificum')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Allan Herbarium')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Harvard University')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Kew Royal Botanic Gardens')
INSERT INTO [dbo].[institutions] (Name) VALUES ('National Herbarium of Victoria')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Missouri Botanical Garden')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Herbier National de Paris')
INSERT INTO [dbo].[institutions] (Name) VALUES ('National Tropical Botanical Garden')
INSERT INTO [dbo].[institutions] (Name) VALUES ('United States National Herbarium')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Museum of New Zealand Te Papa Tongarewa')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Kyushu University')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Kyushu University Museum')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Laboratory of Ecological Science, Department of Biology, Faculty of Sciences, Kyushu University')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Department of Zoology, Division of Biology, Faculty of Science, University of Zagreb')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Faculty of Biology and Geology, Cluj-Napoca')
INSERT INTO [dbo].[institutions] (Name) VALUES ('Hungarian Department of Biology and Ecology, Babeș-Bolyai University, Clinicilor')
INSERT INTO [dbo].[institutions] (Name) VALUES ('University of Zagreb')
