<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="em | i">
		<i><xsl:apply-templates select="node()"/></i>
	</xsl:template>

	<xsl:template match="strong | b">
		<b><xsl:apply-templates select="node()"/></b>
	</xsl:template>

	<xsl:template match="span | div | comment-start | comment-end">
		<xsl:apply-templates select="node()"/>
	</xsl:template>

	<xsl:template match="@*[name()='class']"/>
</xsl:stylesheet>