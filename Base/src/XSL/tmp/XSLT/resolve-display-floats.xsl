<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:xs="http://www.w3.org/2001/XMLSchema"
	xmlns:mml="http://www.w3.org/1998/Math/MathML"
	xmlns:xlink="http://www.w3.org/1999/xlink"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	xmlns:tp="http://www.plazi.org/taxpub"
	exclude-result-prefixes="xs mml xlink tp xsi">

	<xsl:output method="xml" encoding="utf-8" indent="yes"/>
	<xsl:preserve-space elements="*" />
	<xsl:key name="figs" match="fig" use="@id" />
	<xsl:key name="tables" match="table-wrap" use="table/@id" />

	<xsl:template match="@*|*">
		<xsl:copy>
			<xsl:apply-templates select="@*" />
			<xsl:apply-templates />
		</xsl:copy>
	</xsl:template>

	<xsl:template match="displayFloat[@float-type='fig']">
			<xsl:apply-templates select="key('figs',@rid)[1]" />
	</xsl:template>

	<xsl:template match="displayFloat[@float-type='table']">
		<xsl:for-each select="key('tables',@rid)">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>

	<xsl:template match="floats-group" />
	
	<xsl:template match="comment()">
		<xsl:comment>
			<xsl:value-of select="."/>
		</xsl:comment>
	</xsl:template>
</xsl:stylesheet>