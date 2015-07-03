<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:mml="http://www.w3.org/1998/Math/MathML"
  xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:tp="http://www.plazi.org/taxpub"
  exclude-result-prefixes="xs xlink mml xsi tp">

	<xsl:output method="xml" indent="yes" encoding="utf-8" omit-xml-declaration="yes"/>

	<xsl:key name="distinct-taxa" match="taxon" use="normalize-space(.)"/>

	<xsl:template match="/">
		<taxa>
			<xsl:for-each select="//taxon[generate-id() = generate-id(key('distinct-taxa', normalize-space(.))[1])]">
				<xsl:sort/>
				<xsl:apply-templates select="."/>
			</xsl:for-each>
		</taxa>
	</xsl:template>

	<xsl:template match="@* | node()">
		<xsl:copy>
			<xsl:apply-templates select="@* | node()"/>
		</xsl:copy>
	</xsl:template>

</xsl:stylesheet>
