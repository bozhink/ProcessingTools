<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output encoding="UTF-8" method="xml" indent="yes"/>
	<xsl:template match="/">
		<taxa>
			<xsl:for-each select="distinct-values(//rank)">
				<xsl:sort/>
				<taxon>
					<xsl:copy-of select="."/>
				</taxon>
			</xsl:for-each>
		</taxa>
	</xsl:template>
</xsl:stylesheet>