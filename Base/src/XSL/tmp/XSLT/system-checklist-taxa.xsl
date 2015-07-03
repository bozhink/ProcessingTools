<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output indent="yes"/>
	<xsl:template match="/">
		<taxa>
			<xsl:apply-templates select="//node()[@id &gt; 418 and @id &lt; 435][normalize-space(.)!='']"/>
		</taxa>
	</xsl:template>
	<xsl:template match="node()[@id &gt; 418 and @id &lt; 435][normalize-space(.)!='']">
		<xsl:copy-of select="."/>
	</xsl:template>
</xsl:stylesheet>