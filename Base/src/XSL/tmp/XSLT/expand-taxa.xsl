<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output indent="yes" encoding="UTF-8"/>

	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>

	<xsl:template match="taxon[contains(@genus,'.')][@species]">
		<xsl:variable name="found_genera">
			<xsl:call-template name="expand-genus-with-species">
				<xsl:with-param name="species" select="@species"/>
				<xsl:with-param name="genus" select="@genus"/>
			</xsl:call-template>
		</xsl:variable>
		<taxon>
			<xsl:attribute name="genus">
				<xsl:choose>
					<xsl:when test="$found_genera=''">
						<xsl:value-of select="@genus"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$found_genera"/>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:attribute>
			<xsl:apply-templates select="@subgenus|@species|@subspecies|@full-name"/>
			<xsl:if test="$found_genera!=''">
				<xsl:attribute name="unfold">true</xsl:attribute>
			</xsl:if>
			<xsl:copy-of select="node()"/>
		</taxon>
	</xsl:template>

	<xsl:template name="expand-genus-with-species">
		<xsl:param name="genus"/>
		<xsl:param name="species"/>
		<xsl:variable name="_genus">
			<xsl:choose>
				<xsl:when test="contains($genus,'.')">
					<xsl:value-of select="substring-before($genus,'.')"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$genus"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:for-each select="//taxon[@species=$species][contains(@genus,$_genus)][not(contains(@genus,'.'))]">
			<xsl:value-of select="@genus"/>
			<xsl:if test="position()!=last()">
				<xsl:text>;</xsl:text>
			</xsl:if>
		</xsl:for-each>
	</xsl:template>

	<xsl:template match="@*|*">
		<xsl:copy>
			<xsl:apply-templates select="@*"/>
			<xsl:apply-templates/>
		</xsl:copy>
	</xsl:template>
</xsl:stylesheet>