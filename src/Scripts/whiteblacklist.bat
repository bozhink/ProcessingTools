@echo off
java -jar C:\bin\saxon9he.jar -xsl:"C:\bin\WhiteListClean.xsl" -s:"%1" -o:"%2"
copy %2 C:\bin\