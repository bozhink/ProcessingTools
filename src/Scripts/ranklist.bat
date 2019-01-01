@echo off
java -jar C:\bin\saxon9he.jar -xsl:"C:\bin\RankListClean.xsl" -s:"%1" -o:"C:\temp\ranklist.xml"
java -jar C:\bin\saxon9he.jar -xsl:"C:\bin\RankListClean.xsl" -s:"C:\temp\ranklist.xml" -o:"%2"
copy %2 "C:\bin\taxonomy.rankList.xml"