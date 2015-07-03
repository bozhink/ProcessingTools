<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="list">
		<xsl:choose>
			<xsl:when test="@list-type='order'">
				<ol>
					<xsl:apply-templates />
				</ol>
			</xsl:when>
			<xsl:otherwise>
				<ul>
					<xsl:apply-templates />
				</ul>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template match="label"/>

	<xsl:template match="list-item">
		<li>
			<xsl:apply-templates />
		</li>
	</xsl:template>

	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>
</xsl:stylesheet>