<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/">
		<ids>
			<xsl:for-each select="//a[normalize-space(@id)!='']">
				<id>
					<xsl:value-of select="@id"/>
				</id>
			</xsl:for-each>
		</ids>
	</xsl:template>
</xsl:stylesheet>