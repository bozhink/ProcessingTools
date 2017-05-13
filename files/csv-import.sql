use [Biorepositories]
go

bulk
insert [grbio].[Biorepositories]
from 'C:\temp\biorepositories.csv'
with
(
	FIRSTROW = 2,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '\n'
)
go
