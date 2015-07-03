<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" indent="yes" encoding="utf-8"/>
	<xsl:template match="/">
		<references>
			<article>
				<xsl:value-of select="/article/front/article-meta/article-id[@pub-id-type='doi']"/>
			</article>
			<xsl:for-each select="//ref-list">
				<reference-list title="{normalize-space(title)}">
					<xsl:for-each select="ref">
						<reference id="{@id}">
							<xsl:apply-templates select="."/>
						</reference>
					</xsl:for-each>
				</reference-list>
			</xsl:for-each>
		</references>
	</xsl:template>
	<xsl:template match="@*|comment()"/>
	<xsl:template match="node()">
		<xsl:choose>
			<xsl:when test="name()=''">
				<xsl:value-of select="."/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:apply-templates select="node()"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template match="person-group|person-group1">
		<xsl:for-each select="name">
			<xsl:value-of select="surname"/>
			<xsl:text> </xsl:text>
			<xsl:value-of select="given-names"/>
			<xsl:if test="position()!=last()">
				<xsl:text>, </xsl:text>
			</xsl:if>
		</xsl:for-each>
	</xsl:template>
</xsl:stylesheet>
