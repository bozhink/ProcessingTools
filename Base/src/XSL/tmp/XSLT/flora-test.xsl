<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:template match="/">
		<xsl:param name="title" select="''"/>
		<xsl:apply-templates/>
	</xsl:template>

	<xsl:template match="@*|*">
		<xsl:param name="title" select="''"/>
		<xsl:copy>
			<xsl:apply-templates select="@*"/>
			<xsl:apply-templates>
				<xsl:with-param name='title' select="$title"/>
			</xsl:apply-templates>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="taxontitle" mode="titles">
		<taxontitle>
			<xsl:apply-templates select="@*"/>
			<xsl:value-of select="normalize-space(.)"/>
		</taxontitle>
	</xsl:template>
	<xsl:template match="@*|*" mode="titles"></xsl:template>

	<xsl:template match="g">
		<xsl:param name="title" select="''"/>
		<xsl:variable name="titles">
			<titles>
				<xsl:for-each select="taxon">
					<xsl:apply-templates mode="titles"/>
				</xsl:for-each>
			</titles>
		</xsl:variable>

		<xsl:for-each select="taxon">
			<xsl:variable name="pos" select="position()"/>
			<xsl:variable name="num" select="./taxontitle[1]/@num"/>

			<xsl:variable name="numTitle">
				<xsl:choose>
					<xsl:when test="matches($num, '^(gen\d+|\d+)$')">
						<xsl:value-of select="$title"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:call-template name="get-position">
							<xsl:with-param name="node-set" select="$titles"/>
							<xsl:with-param name="position" select="$pos"/>
						</xsl:call-template>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:variable>

			<xsl:variable name="newTitle">
				<xsl:if test="normalize-space($num)!=''">
					<xsl:if test="not(contains($num,'gen'))">
						<xsl:text>gen</xsl:text>
						<xsl:value-of select="../@id"/>
						<xsl:text>-</xsl:text>
					</xsl:if>
					<xsl:if test="normalize-space($numTitle)!=''">
						<xsl:value-of select="$numTitle"/>
						<xsl:text>-</xsl:text>
					</xsl:if>
					<xsl:if test="matches($num, '^[a-z]\d+$')">
						<xsl:value-of select="substring($num,1,1)"/>
						<xsl:text>-</xsl:text>
					</xsl:if>
					<xsl:value-of select="$num"/>
				</xsl:if>
			</xsl:variable>

			<xsl:apply-templates select=".">
				<xsl:with-param name='title'>
					<xsl:value-of select="$newTitle"/>
				</xsl:with-param>
			</xsl:apply-templates>
		</xsl:for-each>
	</xsl:template>

	<xsl:template match="taxon">
		<xsl:param name="title" select="''"/>
		<taxon>
			<xsl:attribute name="genus-group">
				<xsl:value-of select="../@id"/>
			</xsl:attribute>
			<xsl:apply-templates>
				<xsl:with-param name='title' select="$title"/>
			</xsl:apply-templates>
		</taxon>
	</xsl:template>

	<xsl:template match="taxontitle">
		<xsl:param name="title" select="''"/>
		<taxontitle>
			<xsl:apply-templates select="@*"/>
			<xsl:if test="normalize-space(@num)!=''">
				<xsl:attribute name="num">
					<xsl:value-of select="$title"/>
				</xsl:attribute>
			</xsl:if>
			<xsl:apply-templates>
				<xsl:with-param name='title' select="$title"/>
			</xsl:apply-templates>
		</taxontitle>
	</xsl:template>

	<xsl:template match="toTaxon[@num]">
		<xsl:param name="title" select="''"/>
		<toTaxon>
			<xsl:apply-templates select="@*"/>
			<xsl:attribute name="num">
				<xsl:value-of select="$title"/>
				<xsl:text>-</xsl:text>
				<xsl:value-of select="@num"/>
			</xsl:attribute>
			<xsl:apply-templates>
				<xsl:with-param name='title' select="$title"/>
			</xsl:apply-templates>
		</toTaxon>
	</xsl:template>

	<xsl:template name="get-position">
		<xsl:param name="node-set"/>
		<xsl:param name="position"/>
		<xsl:choose>
			<xsl:when test="matches($node-set/titles/taxontitle[$position - 1]/@num,'^[a-z]')">
				<xsl:call-template name="get-position">
					<xsl:with-param name="node-set" select="$node-set"/>
					<xsl:with-param name="position" select="$position - 1"/>
				</xsl:call-template>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$node-set/titles/taxontitle[$position - 1]/@num"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>