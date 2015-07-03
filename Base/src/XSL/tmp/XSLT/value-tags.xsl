<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output encoding="utf-8" method="xml" indent="yes" omit-xml-declaration="yes"/>
	<xsl:template match="@id|node()">
		<xsl:copy>
			<xsl:apply-templates select="@id|node()"/>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="@*"/>
	<xsl:template match="value">
		<value/>
	</xsl:template>
</xsl:stylesheet>