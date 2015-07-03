<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output indent="no" encoding="utf-8" method="xml" omit-xml-declaration="yes"/>
	<xsl:preserve-space elements="*"/>
	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="distribution/value[normalize-space(.)!=''][not(p)]">
		<xsl:element name="{name()}">
			<xsl:apply-templates select="@*"/>
			<p>
				<xsl:apply-templates/>
			</p>
		</xsl:element>
	</xsl:template>
</xsl:stylesheet>