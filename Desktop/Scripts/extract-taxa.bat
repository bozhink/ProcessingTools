@echo off
java -jar C:\bin\saxon9he.jar -xsl:"C:\bin\expand-taxa-by-full-name.xsl" -s:%1 -o:%1.out.xml
java -jar C:\bin\saxon9he.jar -xsl:C:\bin\get-taxa.xsl -s:%1.out.xml -o:%1.out.xml
java -jar C:\bin\saxon9he.jar -xsl:C:\bin\get-taxa.xsl -s:%1.out.xml -o:%1.out.xml
