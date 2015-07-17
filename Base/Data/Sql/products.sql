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
