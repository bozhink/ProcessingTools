<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:tp="http://www.plazi.org/taxpub"
	exclude-result-prefixes="tp">

	<xsl:output encoding="utf-8" indent="yes" method="xml"/>

	<xsl:template match="/">
		<taxa>
			<xsl:for-each select="//tp:taxon-name">
				<xsl:sort/>
				<xsl:variable name="taxon-name">
					<xsl:call-template name="get-taxon-name"/>
				</xsl:variable>
				<xsl:variable name="taxon-type">
					<xsl:call-template name="get-taxon-type"/>
				</xsl:variable>
				<taxon>
					<xsl:attribute name="name">
						<xsl:value-of select="$taxon-name"/>
					</xsl:attribute>
					<xsl:attribute name="type">
						<xsl:value-of select="$taxon-type"/>
					</xsl:attribute>
				</taxon>
			</xsl:for-each>
		</taxa>
	</xsl:template>

	<xsl:template name="get-taxon-name">
		<xsl:choose>
			<xsl:when test="normalize-space(@full-name)!=''">
				<xsl:value-of select="@full-name"/>
			</xsl:when>
			<xsl:when test="count(*)&gt;1">
				<xsl:for-each select="*">
					<xsl:choose>
						<xsl:when test="normalize-space(@full-name)!=''">
							<xsl:value-of select="normalize-space(@full-name)"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="normalize-space(.)"/>
						</xsl:otherwise>
					</xsl:choose>
					<xsl:if test="position()!=last()">
						<xsl:text> </xsl:text>
					</xsl:if>
				</xsl:for-each>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="normalize-space(.)"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template name="get-taxon-type">
		<xsl:choose>
			<xsl:when test="normalize-space(tp:taxon-name-part/@taxon-name-part-type)!=''">
				<xsl:value-of select="normalize-space(tp:taxon-name-part/@taxon-name-part-type)"/>
			</xsl:when>
			<xsl:when test="normalize-space(tn-part/@type)!=''">
				<xsl:value-of select="normalize-space(tn-part/@type)"/>
			</xsl:when>
			<xsl:when test="normalize-space(@type)!=''">
				<xsl:value-of select="@type"/>
			</xsl:when>
			<xsl:when test="normalize-space(@taxon-name-part-type)!=''">
				<xsl:value-of select="@taxon-name-part-type"/>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>