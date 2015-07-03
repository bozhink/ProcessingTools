<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:tp="http://www.plazi.org/taxpub">
	<xsl:template match="/">
		<ranks>
			<xsl:for-each select="distinct-values(//tp:taxon-name-part/@taxon-name-part-type)">
				<xsl:sort />
				<rank>
					<xsl:value-of select="." />
				</rank>
			</xsl:for-each>
		</ranks>
	</xsl:template>
</xsl:stylesheet>