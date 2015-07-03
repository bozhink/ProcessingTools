<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output encoding="utf-8" indent="yes" omit-xml-declaration="no"/>
	<xsl:template match="/">
		<data-set>
			<xsl:for-each select="//table">
				<xsl:apply-templates/>
			</xsl:for-each>
		</data-set>
	</xsl:template>
	<xsl:template match="thead | th"/>
	<xsl:template match="@*|node()">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="tr[count(td)!=0]">
		<data>
			<xsl:attribute name="extension">
				<xsl:variable name="extension">
					<xsl:value-of select="normalize-space(td[not(contains(text(),'/'))])"/>
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="contains($extension,'.')">
						<xsl:value-of select="substring-after($extension,'.')"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$extension"/>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:attribute>
			<xsl:attribute name="mime-type">
				<xsl:value-of select="normalize-space(td[contains(text(),'/')])"/>
			</xsl:attribute>
		</data>
	</xsl:template>
</xsl:stylesheet>