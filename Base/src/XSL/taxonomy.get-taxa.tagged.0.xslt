<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:tp="http://www.plazi.org/taxpub" exclude-result-prefixes="tp msxsl" xmlns:msxsl="urn:schemas-microsoft-com:xslt">

	<xsl:output encoding="utf-8" indent="yes" method="xml"/>

	<xsl:key name="taxon-name" match="taxon" use="@name"/>
	<xsl:key name="taxon-type" match="type" use="."/>

	<xsl:template match="/">
		<taxa>
			<xsl:for-each select="//tp:taxon-name">
				<taxon>
					<xsl:attribute name="name">
						<xsl:for-each select="node()">
							<xsl:value-of select="normalize-space(.)"/>
							<xsl:if test="position()!=last()">
								<xsl:text> </xsl:text>
							</xsl:if>
						</xsl:for-each>
					</xsl:attribute>
					<xsl:attribute name="full-name">
						<xsl:for-each select="node()">
							<xsl:choose>
								<xsl:when test="normalize-space(@full-name)!=''">
									<xsl:value-of select="@full-name"/>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="normalize-space(.)"/>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:if test="position()!=last()">
								<xsl:text> </xsl:text>
							</xsl:if>
						</xsl:for-each>
					</xsl:attribute>
					<xsl:apply-templates mode="parse"/>
				</taxon>
			</xsl:for-each>
		</taxa>
	</xsl:template>

	<xsl:template match="tp:taxon-name-part" mode="parse">
		<part>
			<xsl:attribute name="type">
				<xsl:value-of select="@taxon-name-part-type"/>
			</xsl:attribute>
			<xsl:attribute name="name">
				<xsl:value-of select="normalize-space(.)"/>
			</xsl:attribute>
			<xsl:attribute name="full-name">
				<xsl:choose>
					<xsl:when test="normalize-space(@full-name)!=''">
						<xsl:value-of select="@full-name"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="normalize-space(.)"/>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:attribute>
			<xsl:value-of select="normalize-space(.)"/>
		</part>
	</xsl:template>

	<xsl:template match="text()" mode="parse">
		<xsl:value-of select="."/>
	</xsl:template>

</xsl:stylesheet>
