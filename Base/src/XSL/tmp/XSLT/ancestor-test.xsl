<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="rank/value">
		<xpath>
			<xsl:text>/</xsl:text>
			<xsl:for-each select="ancestor::part">
				<xsl:value-of select="local-name(.)"/>
				<xsl:if test="position()!=last()">
					<xsl:text>/</xsl:text>
				</xsl:if>
			</xsl:for-each>
		</xpath>
		<xpath>
			<xsl:text>/</xsl:text>
			<xsl:for-each select="ancestor-or-self::*">
				<xsl:value-of select="local-name(.)"/>
				<xsl:if test="position()!=last()">
					<xsl:text>/</xsl:text>
				</xsl:if>
			</xsl:for-each>
		</xpath>
	</xsl:template>
</xsl:stylesheet>