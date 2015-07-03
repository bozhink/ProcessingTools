<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output encoding="utf-8" indent="yes" method="xml"/>

	<xsl:key name="taxa" use="normalize-space(.)" match="tn"/>

	<xsl:template match="/">
		<replaces>
			<xsl:for-each select="//tn[count(tn-part[@full-name='']/@full-name)!=0][generate-id() = generate-id(key('taxa', .)[1])]">
				<xsl:sort/>
				<replace>
					<A><xsl:copy-of select="."/></A>
					<B><xsl:copy-of select="."/></B>
				</replace>
			</xsl:for-each>
		</replaces>
	</xsl:template>
</xsl:stylesheet>