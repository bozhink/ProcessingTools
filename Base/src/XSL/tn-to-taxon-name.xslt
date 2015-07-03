<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:mml="http://www.w3.org/1998/Math/MathML"
  xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:tp="http://www.plazi.org/taxpub"
  exclude-result-prefixes="xs xsi">

	<xsl:output encoding="utf-8" method="xml" indent="yes"/>
	<xsl:preserve-space elements="*"/>

	<xsl:template match="@*|*|comment()">
		<xsl:copy>
			<xsl:apply-templates select="@*"/>
			<xsl:apply-templates/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="@*|*|comment()" mode="tn">
		<xsl:copy>
			<xsl:apply-templates select="@*" mode="tn"/>
			<xsl:apply-templates mode="tn"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="@type" mode="tn">
		<xsl:attribute name="taxon-name-part-type">
			<xsl:value-of select="."/>
		</xsl:attribute>
	</xsl:template>

	<xsl:template match="tn">
		<tp:taxon-name>
			<xsl:apply-templates/>
		</tp:taxon-name>
	</xsl:template>

	<xsl:template match="tn-part">
		<tp:taxon-name-part>
			<xsl:apply-templates select="@*" mode="tn"/>
			<xsl:apply-templates/>
		</tp:taxon-name-part>
	</xsl:template>
</xsl:stylesheet>
