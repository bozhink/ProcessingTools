<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xsi:schemaLocation="http://www.taxonx.org/schema/v1 http://www.taxonx.org/schema/v1/taxonx1.xsd http://www.loc.gov/mods/v3 http://www.loc.gov/mods/v3/mods-3-1.xsd http://digir.net/schema/conceptual/darwin/2003/1.0 http://digir.net/schema/conceptual/darwin/2003/1.0/darwin2.xsd"
	xmlns:dwc="http://digir.net/schema/conceptual/darwin/2003/1.0"
	xmlns:tax="http://www.taxonx.org/schema/v1"
	xmlns:mods="http://www.loc.gov/mods/v3"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
	<xsl:template match="/document">
		<documents>
			<xsl:for-each select="treatment">
				<document>
					<xsl:copy>
						<xsl:apply-templates select="/document/@*"/>
					</xsl:copy>
					<xsl:copy-of select="/document/mods"/>
					<xsl:copy-of select="."/>
				</document>
			</xsl:for-each>
		</documents>
	</xsl:template>

	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>
</xsl:stylesheet>