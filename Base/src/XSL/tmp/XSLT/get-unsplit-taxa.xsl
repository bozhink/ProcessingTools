<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xml:lang="en"
	xmlns:xml="http://www.w3.org/XML/1998/namespace"
	xmlns:mml="http://www.w3.org/1998/Math/MathML"
	xmlns:xlink="http://www.w3.org/1999/xlink"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	xmlns:tp="http://www.plazi.org/taxpub">
	
	<xsl:output indent="yes" encoding="utf-8" method="xml"/>
	
	<xsl:template match="/">
		<taxa>
			<xsl:for-each select="distinct-values(//tp:taxon-name[count(tp:taxon-name-part)=0])">
				<xsl:sort/>
				<taxon><xsl:value-of select="normalize-space(.)"/></taxon>
			</xsl:for-each>
		</taxa>
	</xsl:template>
</xsl:stylesheet>