<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/">
		<list>
			<xsl:for-each select="distinct-values(//td[normalize-space(.)!=''])">
				<item>
					<xsl:value-of select="."/>
				</item>
			</xsl:for-each>
		</list>
	</xsl:template>
</xsl:stylesheet>