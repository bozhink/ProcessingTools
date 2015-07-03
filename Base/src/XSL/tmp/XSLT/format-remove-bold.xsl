<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:xs="http://www.w3.org/2001/XMLSchema"
	xmlns:mml="http://www.w3.org/1998/Math/MathML"
	xmlns:xlink="http://www.w3.org/1999/xlink"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	xmlns:tp="http://www.plazi.org/taxpub"
	exclude-result-prefixes="xs">

	<xsl:output method="xml" encoding="UTF-8" indent="yes"/>

	<xsl:template match="/">
		<xsl:apply-templates />
	</xsl:template>

	<xsl:template match="@*|node()">
		<xsl:choose>
			<xsl:when test="name()='article-title' or name()='title' or name()='label' or name()='th'">
				<xsl:element name="{name()}">
					<xsl:call-template name="unbold">
						<xsl:with-param name="select" select="node()"/>
					</xsl:call-template>
				</xsl:element>
			</xsl:when>
			<xsl:when test="name()='table-wrap' and @content-type='key'">
				<table-wrap>
					<xsl:attribute name="content-type">key</xsl:attribute>
					<xsl:attribute name="position">anchor</xsl:attribute>
					<xsl:attribute name="orientation">portrait</xsl:attribute>
					<xsl:call-template name="unbold">
						<xsl:with-param name="select" select="node()"/>
					</xsl:call-template>
				</table-wrap>
			</xsl:when>
			<xsl:otherwise>
				<xsl:copy>
					<xsl:apply-templates select="@*|node()" />
				</xsl:copy>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template name="unbold">
		<xsl:param name="select" select="."/>
		<xsl:for-each select="node()">
			<xsl:choose>
				<xsl:when test="name()='' and not(.='PageBreak')">
					<xsl:value-of select="."/>
				</xsl:when>
				<xsl:when test="name()='' and .='PageBreak'">
					<xsl:comment>PageBreak</xsl:comment>
				</xsl:when>
				<xsl:when test="name()='bold'">
					<xsl:call-template name="unbold">
						<xsl:with-param name="select" select="."/>
					</xsl:call-template>
				</xsl:when>
				<xsl:otherwise>
					<xsl:element name="{name()}">
						<xsl:apply-templates select="@*" />
						<xsl:call-template name="unbold">
							<xsl:with-param name="select" select="."/>
						</xsl:call-template>
					</xsl:element>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:for-each>
	</xsl:template>
</xsl:stylesheet>