<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:xs="http://www.w3.org/2001/XMLSchema"
	xmlns:mml="http://www.w3.org/1998/Math/MathML"
	xmlns:xlink="http://www.w3.org/1999/xlink"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	xmlns:tp="http://www.plazi.org/taxpub"
	exclude-result-prefixes="xs mml xlink tp xsi">
	<xsl:output method="xml" encoding="UTF-8" indent="yes" />
	<xsl:template match="/">
		<coordinates>
			<xsl:for-each select="//locality-coordinates">
				<coordinate>
					<xsl:value-of select="normalize-space(.)"/>
				</coordinate>
			</xsl:for-each>
			<xsl:for-each select="//named-content[@content-type='dwc:verbatimCoordinates']">
				<coordinate>
					<xsl:value-of select="normalize-space(.)"/>
				</coordinate>
			</xsl:for-each>
		</coordinates>
	</xsl:template>
</xsl:stylesheet>