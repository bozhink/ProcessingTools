<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output indent="yes"/>
	<xsl:template match="/">
		<figures>
			<xsl:for-each select="//fig">
				<figure>
					<xsl:attribute name="id">
						<xsl:value-of select="@id"/>
					</xsl:attribute>
					<xsl:value-of select="normalize-space(label)"/>
				</figure>
			</xsl:for-each>
		</figures>
	</xsl:template>
</xsl:stylesheet>