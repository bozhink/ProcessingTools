<?xml version="1.0" encoding="utf-8"?>
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
		<taxons>
			<xsl:for-each select="distinct-values(//tp:taxon-name[count(tp:taxon-name-part)=0])">
				<xsl:sort />
				<taxon>
					<xsl:value-of select="normalize-space(.)" />
				</taxon>
			</xsl:for-each>
			<xsl:for-each select="distinct-values(//tn[count(tp:taxon-name-part)=0])">
				<xsl:sort />
				<taxon>
					<xsl:value-of select="normalize-space(.)" />
				</taxon>
			</xsl:for-each>
			<xsl:for-each select="distinct-values(//taxon[count(tp:taxon-name-part)=0])">
				<xsl:sort />
				<taxon>
					<xsl:value-of select="normalize-space(.)" />
				</taxon>
			</xsl:for-each>
		</taxons>
	</xsl:template>
</xsl:stylesheet>