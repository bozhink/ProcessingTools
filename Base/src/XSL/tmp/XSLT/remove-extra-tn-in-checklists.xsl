<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output method="xml" encoding="UTF-8" />

	<xsl:preserve-space elements="*" />

	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()" />
		</xsl:copy>
	</xsl:template>

	<xsl:template match="node()[@id='436']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='435']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='418']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='49']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='417']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='48']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='434']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='433']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='432']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='431']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='430']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='429']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='428']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='427']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='426']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='425']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='424']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='423']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='422']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='421']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='420']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
	<xsl:template match="node()[@id='419']/value/tn">
		<xsl:value-of select="normalize-space(.)"/>
	</xsl:template>
</xsl:stylesheet>