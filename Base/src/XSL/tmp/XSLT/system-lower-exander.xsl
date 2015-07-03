<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" encoding="UTF-8" indent="no" omit-xml-declaration="yes" />
	
	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>
	
	<xsl:template match="tn[contains(tn-part[@type='genus'],'.')][tn-part[@type='species']!='']">
		<xsl:varialbe name="genus">
			<xsl:value-of select="substring-before(tn-part[@type='genus'],'.')"/>
		</xsl:varialbe>
		<xsl:varialbe name="species">
			<xsl:value-of select="tn-part[@type='species']"/>
		</xsl:varialbe>
		<tn>
			<tn-part>
				<xsl:attribute name="type">genus</xsl:attribute>
				<xsl:attribute name="full-name">
					<xsl:for-each select="tn[tn-part[@type='species']=$species][contains(tn-part[@type='genus'][not(contains(normalize-space(.),'.'))],$genus)]">
						<xsl:value-of select="tn-part[@type='genus']"/>
					</xsl:for-each>
				</xsl:attribute>
				<xsl:value-of select="tn-part[@type='genus']"/>
			</tn-part>
			<xsl:if test="normalize-space(tn-part[@type='subgenus'])=''">
				<xsl:text> (</xsl:text>
				<xsl:copy-of select="tn-part[@type='subgenus']"/>
				<xsl:text>)</xsl:text>
			</xsl:if>
			<xsl:if test="normalize-space(tn-part[@type='species'])=''">
				<xsl:text> </xsl:text>
				<xsl:copy-of select="tn-part[@type='species']"/>
			</xsl:if>
			<xsl:if test="normalize-space(tn-part[@type='subspecies'])=''">
				<xsl:text> </xsl:text>
				<xsl:copy-of select="tn-part[@type='subspecies']"/>
			</xsl:if>
		</tn>
	</xsl:template>
</xsl:stylesheet>