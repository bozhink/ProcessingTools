<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:xs="http://www.w3.org/2001/XMLSchema"
	xmlns:mml="http://www.w3.org/1998/Math/MathML"
	xmlns:xlink="http://www.w3.org/1999/xlink"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	xmlns:tp="http://www.plazi.org/taxpub"
	exclude-result-prefixes="xs xsi">

	<xsl:output method="xml" encoding="utf-8" indent="yes"/>

	<xsl:preserve-space elements="*"/>

	<xsl:template match="/">
		<xsl:apply-templates />
	</xsl:template>

	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="tp:taxon-name">
		<tp:taxon-name full-name="{normalize-space(.)}">
			<xsl:for-each select="tp:taxon-name-part[normalize-space(.)!='']">
				<xsl:attribute name="{@taxon-name-part-type}">
					<xsl:value-of select="normalize-space(.)"/>
				</xsl:attribute>
			</xsl:for-each>
			<xsl:copy-of select="node()"/>
		</tp:taxon-name>
	</xsl:template>
</xsl:stylesheet>