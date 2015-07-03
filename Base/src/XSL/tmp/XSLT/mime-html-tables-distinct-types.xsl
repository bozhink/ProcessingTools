<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output encoding="UTF-8" indent="yes" method="xml" omit-xml-declaration="yes"/>
	<xsl:key use="td[@class='ext']/text()" name="ext-key" match="//tr" />
	<xsl:key use="normalize-space(.)" name="distinct-key" match="trr" />
	<xsl:key use="mime/text()" name="mime-key" match="trr" />

	<!-- <xsl:template match="/">
		<table>
			<xsl:for-each select="//table">
				<xsl:for-each select="tr">
					<tr>
						<xsl:apply-templates select="ext" />
						<xsl:for-each select="trr[generate-id() = generate-id(key('distinct-key', normalize-space(.))[1])]">
							<trr>
								<xsl:apply-templates/>
							</trr>
						</xsl:for-each>
					</tr>
				</xsl:for-each>
			</xsl:for-each>
		</table>
	</xsl:template> -->

	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>
</xsl:stylesheet>