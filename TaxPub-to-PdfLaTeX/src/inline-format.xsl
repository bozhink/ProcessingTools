<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:template name="format-text">
		<xsl:param name="string" select="''" />
		<xsl:value-of select="replace($string, '([~#%_\$\^&#38;])', '\\$1')" />
	</xsl:template>

	<xsl:template match="italic | Italic | em | i">
		<xsl:text><![CDATA[\textit{]]></xsl:text>
		<xsl:apply-templates />
		<xsl:text><![CDATA[}]]></xsl:text>
	</xsl:template>

	<xsl:template match="bold | Bold | strong | b">
		<xsl:text><![CDATA[\textbf{]]></xsl:text>
		<xsl:apply-templates />
		<xsl:text><![CDATA[}]]></xsl:text>
	</xsl:template>

	<xsl:template match="bold-italic | Bold-Italic">
		<xsl:text><![CDATA[\textbf{\textit{]]></xsl:text>
		<xsl:apply-templates />
		<xsl:text><![CDATA[}}]]></xsl:text>
	</xsl:template>
	
	<xsl:template match="xref">
		<xsl:text><![CDATA[\hyperref[]]></xsl:text>
		<xsl:value-of select="@rid"/>
		<xsl:text><![CDATA[]{]]></xsl:text>
		<xsl:apply-templates/>
		<xsl:text><![CDATA[}]]></xsl:text>
	</xsl:template>

</xsl:stylesheet>