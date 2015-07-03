<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output encoding="utf-8" method="xml" omit-xml-declaration="yes"/>
	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="node()[@object_id='197']">
		<xsl:variable name="genus_group" select="node()[@object_id='181']|node()[@object_id='180']"/>
		<GENUS-GROUP>
			<xsl:apply-templates select="$genus_group/fields"/>
		</GENUS-GROUP>
		<xsl:apply-templates/>
	</xsl:template>
</xsl:stylesheet>