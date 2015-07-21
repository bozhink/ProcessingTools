IF OBJECT_ID ('morphology') IS NOT NULL
   DROP TABLE [dbo].[morphology]
GO
CREATE TABLE [dbo].[morphology]
(
Id int IDENTITY(1,1) PRIMARY KEY,
Name VARCHAR(255) NOT NULL
);
GO

INSERT INTO [dbo].[morphology] (Name) VALUES ('aedeagus')
INSERT INTO [dbo].[morphology] (Name) VALUES ('apex')
INSERT INTO [dbo].[morphology] (Name) VALUES ('basistipes')
INSERT INTO [dbo].[morphology] (Name) VALUES ('cardo')
INSERT INTO [dbo].[morphology] (Name) VALUES ('costa')
INSERT INTO [dbo].[morphology] (Name) VALUES ('costae')
INSERT INTO [dbo].[morphology] (Name) VALUES ('elytra')
INSERT INTO [dbo].[morphology] (Name) VALUES ('epipharynx')
INSERT INTO [dbo].[morphology] (Name) VALUES ('head')
INSERT INTO [dbo].[morphology] (Name) VALUES ('interstrial costae')
INSERT INTO [dbo].[morphology] (Name) VALUES ('labial palpus')
INSERT INTO [dbo].[morphology] (Name) VALUES ('lateral costa')
INSERT INTO [dbo].[morphology] (Name) VALUES ('legs')
INSERT INTO [dbo].[morphology] (Name) VALUES ('longitudinal costae')
INSERT INTO [dbo].[morphology] (Name) VALUES ('mandibles')
INSERT INTO [dbo].[morphology] (Name) VALUES ('marginal costa')
INSERT INTO [dbo].[morphology] (Name) VALUES ('maxillae')
INSERT INTO [dbo].[morphology] (Name) VALUES ('maxillary palpus')
INSERT INTO [dbo].[morphology] (Name) VALUES ('median costa')
INSERT INTO [dbo].[morphology] (Name) VALUES ('mesocoxa')
INSERT INTO [dbo].[morphology] (Name) VALUES ('mesocoxae')
INSERT INTO [dbo].[morphology] (Name) VALUES ('mesosternum')
INSERT INTO [dbo].[morphology] (Name) VALUES ('mesotarsi')
INSERT INTO [dbo].[morphology] (Name) VALUES ('metasternum')
INSERT INTO [dbo].[morphology] (Name) VALUES ('metatarsi')
INSERT INTO [dbo].[morphology] (Name) VALUES ('metatibia')
INSERT INTO [dbo].[morphology] (Name) VALUES ('palpus')
INSERT INTO [dbo].[morphology] (Name) VALUES ('paramedian costa')
INSERT INTO [dbo].[morphology] (Name) VALUES ('procoxa')
INSERT INTO [dbo].[morphology] (Name) VALUES ('profemur')
INSERT INTO [dbo].[morphology] (Name) VALUES ('pronotal surface')
INSERT INTO [dbo].[morphology] (Name) VALUES ('pronotum')
INSERT INTO [dbo].[morphology] (Name) VALUES ('protarsus')
INSERT INTO [dbo].[morphology] (Name) VALUES ('prothorax')
INSERT INTO [dbo].[morphology] (Name) VALUES ('protibia')
INSERT INTO [dbo].[morphology] (Name) VALUES ('pygidium')
INSERT INTO [dbo].[morphology] (Name) VALUES ('segment')
INSERT INTO [dbo].[morphology] (Name) VALUES ('sublateral costa')
INSERT INTO [dbo].[morphology] (Name) VALUES ('tibial apex')

--anthers
--buds
--calyx
--corolla
--corona
--fruit
--fruits
--gynostegium
--hoya
--inflorescence
--inflorescences
--leaves
--pedicel
--petioles
--pollinia
--stems
--root
--basal colleter
--caudicle
--caudicles
--colleters
--corolla lobes
--corona
--corona lobes
--corpusculum
--lamina
--ovary
--pedicels
--peduncle
--peduncles
--seed
--seeds
--stems
