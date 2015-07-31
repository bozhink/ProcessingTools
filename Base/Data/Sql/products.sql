IF OBJECT_ID ('products') IS NOT NULL
   DROP TABLE [dbo].[products]
GO
CREATE TABLE [dbo].[products]
(
Id int IDENTITY(1,1) PRIMARY KEY,
Name VARCHAR(255) NOT NULL
);
GO

INSERT INTO [dbo].[products] (Name) VALUES ('Canon EOS Rebel T3i DSLR camera')
INSERT INTO [dbo].[products] (Name) VALUES ('Canon MP-E 65mm f/2.8 1–5× macro lens')
INSERT INTO [dbo].[products] (Name) VALUES ('Zerene Stacker AutoMontage software')
INSERT INTO [dbo].[products] (Name) VALUES ('Canon EOS 60D')
INSERT INTO [dbo].[products] (Name) VALUES ('Canon EOS 7D')
INSERT INTO [dbo].[products] (Name) VALUES ('Canon MP-E 65 mm 1–5× macro lens')
INSERT INTO [dbo].[products] (Name) VALUES ('CombineZP software')
INSERT INTO [dbo].[products] (Name) VALUES ('EF 100mm F2.8L Macro lens')
INSERT INTO [dbo].[products] (Name) VALUES ('Kenko extension tubes')
INSERT INTO [dbo].[products] (Name) VALUES ('Wild M5 and M20')
INSERT INTO [dbo].[products] (Name) VALUES ('Canon 5D Mark II')
INSERT INTO [dbo].[products] (Name) VALUES ('Visionary Digital BK Lab Plus camera mounting system')
INSERT INTO [dbo].[products] (Name) VALUES ('Photoshop CS5')
-- Leica microscope
-- JEOL 100 C
-- electron microscope
-- ImageJ
-- MS Excel
-- R 2.11.1
-- software
-- Inkscape vector graphics editor software
-- Zeiss AxioImager Z.1 light microscope
-- HRC Axiocam camera
-- Axiovision software
-- Philips CM100
-- BioScan 792
-- Orius 200 (Gatan)
-- Digital Micrograph software
-- Statistica10 (StatSoft, Inc. 2011)
-- STATISTICA 12 software (StatSoft Inc. 1984–2013)
-- HOBO H8 Pro Series Temp/External Temp data loggers
-- Baltimore Ecosystem Study databas
-- Hitachi S-2460N SEM
-- Nikon Y-IDT
