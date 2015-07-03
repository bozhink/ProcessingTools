<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output encoding="UTF-8" indent="yes" method="xml" omit-xml-declaration="yes"/>
	<xsl:key use="td[@class='ext']/text()" name="ext-key" match="//tr" />
	<xsl:key use="normalize-space(mime)" name="mime-distinct" match="//mime" />

	<xsl:template match="/">
		<table>
			<xsl:for-each select="//table">
				<xsl:for-each select="tr[generate-id() = generate-id(key('ext-key', td[@class='ext']/text())[1])]">
					<xsl:sort/>
					<type>
						<xsl:apply-templates select="td[@class='ext']" />

						<xsl:variable name="node-set">
							<xsl:for-each select="key('ext-key', td[@class='ext']/text())">
								<xsl:apply-templates select="td[@class='mime']" />
								<xsl:apply-templates select="td[@class='name']" />
								<xsl:apply-templates select="td[@class='link']" />
							</xsl:for-each>
						</xsl:variable>
						<xsl:apply-templates select="$node-set/node()"/>
					</type>
				</xsl:for-each>
			</xsl:for-each>
		</table>
	</xsl:template>

	<xsl:template match="td[normalize-space()='']"/>

	<xsl:template match="td[not(normalize-space()='')]">
		<xsl:element name="{@class}">
			<xsl:apply-templates/>
		</xsl:element>
	</xsl:template>

	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>
</xsl:stylesheet>