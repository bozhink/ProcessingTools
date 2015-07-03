<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:tp="http://www.plazi.org/taxpub" exclude-result-prefixes="tp">
	<xsl:output encoding="utf-8" omit-xml-declaration="yes" method="xml" indent="yes"/>
	<xsl:template match="/">
		<taxa>
			<xsl:for-each select="distinct-values(//tn[@type='higher']|//tp:taxon-name[@type='higher'])">
				<xsl:sort/>
				<taxon>
					<xsl:value-of select="normalize-space(.)"/>
				</taxon>
			</xsl:for-each>
		</taxa>
	</xsl:template>
</xsl:stylesheet>