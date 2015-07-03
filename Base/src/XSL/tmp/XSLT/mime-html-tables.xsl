<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output encoding="UTF-8" indent="yes" method="xml" omit-xml-declaration="yes"/>

	<xsl:key use="normalize-space(.)" name="distinct" match="//tr"/>

	<xsl:template match="/">
		<table>
			<xsl:for-each select="//table">
				<xsl:for-each select=".//tr[generate-id() = generate-id(key('distinct', normalize-space(.))[1])]">
					<xsl:sort/>
					<tr>
						<xsl:choose>
							<xsl:when test="td[@class='ext']">
								<xsl:apply-templates select="td[@class='ext']"/>
							</xsl:when>
							<xsl:otherwise>
								<td class="ext"/>
							</xsl:otherwise>
						</xsl:choose>

						<xsl:choose>
							<xsl:when test="td[@class='mime']">
								<xsl:apply-templates select="td[@class='mime']"/>
							</xsl:when>
							<xsl:otherwise>
								<td class="mime"/>
							</xsl:otherwise>
						</xsl:choose>

						<xsl:choose>
							<xsl:when test="td[@class='name']">
								<xsl:apply-templates select="td[@class='name']"/>
							</xsl:when>
							<xsl:otherwise>
								<td class="name"/>
							</xsl:otherwise>
						</xsl:choose>

						<xsl:choose>
							<xsl:when test="td[@class='link']">
								<xsl:apply-templates select="td[@class='link']"/>
							</xsl:when>
							<xsl:otherwise>
								<td class="link"/>
							</xsl:otherwise>
						</xsl:choose>
					</tr>
				</xsl:for-each>
			</xsl:for-each>
		</table>
	</xsl:template>

	<xsl:template match="th | thead"/>

	<xsl:template match="tbody">
		<xsl:apply-templates select="node()"/>
	</xsl:template>

	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>

</xsl:stylesheet>