/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000  e.Name
  , m.Name
  , s.Name
  FROM [Mediatypes].[dbo].[MimeTypePairFileExtensions] p
  JOIN [Mediatypes].[dbo].[FileExtensions] e
  ON p.FileExtension_Id = e.Id
  JOIN [Mediatypes].[dbo].[MimeTypePairs] pp
  ON pp.Id = p.MimeTypePair_Id
  JOIN [Mediatypes].[dbo].[MimeTypes] m
  ON m.Id = pp.MimeTypeId
  JOIN [Mediatypes].[dbo].MimeSubtypes s
  ON s.Id = pp.MimeSubtypeId