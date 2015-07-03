<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output encoding="utf-8" indent="yes" omit-xml-declaration="no"/>
	<xsl:template match="/data-set">
		<mime-types>
			<xsl:for-each select="data">
				<!-- <xsl:sort select="@extension"/> -->
				<xsl:sort select="@mime-type"/>
				<mime-type>
					<!-- <xsl:attribute name="extension">
						<xsl:value-of select="@extension"/>
					</xsl:attribute>
					<xsl:attribute name="mime-sub-type">
						<xsl:value-of select="@mime-type"/>
					</xsl:attribute> -->
					<xsl:attribute name="mime-sub-type">
						<xsl:value-of select="@mime-type"/>
					</xsl:attribute>
					<xsl:attribute name="extension">
						<xsl:value-of select="@extension"/>
					</xsl:attribute>
				</mime-type>
			</xsl:for-each>
		</mime-types>
	</xsl:template>
</xsl:stylesheet>