<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:xs="http://www.w3.org/2001/XMLSchema"
	xmlns:mml="http://www.w3.org/1998/Math/MathML"
	xmlns:xlink="http://www.w3.org/1999/xlink"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	xmlns:tp="http://www.plazi.org/taxpub"
	exclude-result-prefixes="xs">
	<xsl:output method="xml" encoding="UTF-8" indent="yes" />
	<xsl:preserve-space elements="*"/>

	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="tp:taxon-name">
		<xsl:choose>
			<xsl:when test="count(tp:taxon-name-part)=0">
				<tp:taxon-name>
					<xsl:call-template name="taxon-split"/>
				</tp:taxon-name>
			</xsl:when>
			<xsl:otherwise>
				<xsl:copy-of select="."/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template name="taxon-split">
		<xsl:variable name="taxon" select="replace(normalize-space(.),'\s+',' ')"/>
		<xsl:choose>
			<xsl:when test="matches($taxon,'^[A-Z][a-z\.]+\s+[a-z\-]+$')">
				<tp:taxon-name-part>
					<xsl:attribute name="taxon-name-part-type">genus</xsl:attribute>
					<xsl:value-of select="substring-before($taxon,' ')"/>
				</tp:taxon-name-part>
				<xsl:text> </xsl:text>
				<tp:taxon-name-part>
					<xsl:attribute name="taxon-name-part-type">species</xsl:attribute>
					<xsl:value-of select="substring-after($taxon, ' ')"/>
				</tp:taxon-name-part>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$taxon"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>