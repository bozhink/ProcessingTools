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
	<xsl:template match="/">
		<xsl:apply-templates />
	</xsl:template>
	<!-- standard copy template -->
	<xsl:template match="@*|node()[name()!='tp:taxon-name']">
		<xsl:copy>
			<xsl:apply-templates select="@*" />
			<xsl:apply-templates />
		</xsl:copy>
	</xsl:template>
	
	<xsl:template match="tp:taxon-name">
		<xsl:variable name="taxon" select="node()"/>
		<xsl:variable name="ntaxon" select="normalize-space(.)"/>
		<tp:taxon-name>
			<xsl:choose>
				<xsl:when test="matches($ntaxon,'^[A-Z][a-z]+$')">
					<tp:taxon-name-part>
						<xsl:attribute name="taxon-name-type" select="'genus'"/>
						<xsl:value-of select="$ntaxon"/>
					</tp:taxon-name-part>
				</xsl:when>
				<xsl:when test="matches($ntaxon,'^[A-Z][a-z\.]+\s+[a-z\-]+$')">
					<tp:taxon-name-part>
						<xsl:attribute name="taxon-name-type" select="'genus'"/>
						<xsl:value-of select="substring-before($ntaxon,' ')"/>
					</tp:taxon-name-part>
					<xsl:text> </xsl:text>
					<tp:taxon-name-part>
						<xsl:attribute name="taxon-name-type" select="'species'"/>
						<xsl:value-of select="substring-after($ntaxon,' ')"/>
					</tp:taxon-name-part>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$taxon"/>
				</xsl:otherwise>
			</xsl:choose>
		</tp:taxon-name>
	</xsl:template>
</xsl:stylesheet>