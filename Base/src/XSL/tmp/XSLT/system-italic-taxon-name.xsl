<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" encoding="UTF-8" indent="no" omit-xml-declaration="yes" />
	
	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>
	
	<xsl:template match="i[count(node()[name()!=''])=0][matches(normalize-space(.),'^[A-Z][a-z\.-]+(\s[A-Za-z\(\)\.\s-]+)?$')]">
		<i>
			<tn>
				<xsl:value-of select="."/>
			</tn>
		</i>
	</xsl:template>
</xsl:stylesheet>