<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:tp="http://www.plazi.org/taxpub">

	<xsl:output encoding="utf-8" indent="yes" method="xml"/>

	<xsl:key name="taxa" use="normalize-space(.)" match="tn"/>
	<xsl:key name="tptaxa" use="normalize-space(.)" match="tp:taxon-name"/>

	<xsl:template match="/">
		<replaces>
			<xsl:for-each select="//tn[count(tn-part)=0][generate-id() = generate-id(key('taxa', .)[1])]">
				<xsl:sort/>
				<replace>
					<A><tn><xsl:value-of select="."/></tn></A>
					<B><tn><tn-part type=""><xsl:value-of select="."/></tn-part></tn></B>
				</replace>
			</xsl:for-each>
			<xsl:for-each select="//tp:taxon-name[count(tp:taxon-name-part)=0][generate-id() = generate-id(key('tptaxa', .)[1])]">
				<xsl:sort/>
				<replace>
					<A><tp:taxon-name><xsl:value-of select="."/></tp:taxon-name></A>
					<B><tp:taxon-name><tp:taxon-name-part taxon-name-part-type=""><xsl:value-of select="."/></tp:taxon-name-part></tp:taxon-name></B>
				</replace>
			</xsl:for-each>
		</replaces>
	</xsl:template>
</xsl:stylesheet>