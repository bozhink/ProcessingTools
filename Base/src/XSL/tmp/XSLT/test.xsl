<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:key name="list" match="taxon[normalize-space(@name)!='']" use="@name" />

	<xsl:template match="/">
		<taxa>
			<xsl:for-each select="//taxon[normalize-space(@name)!=''][generate-id() = generate-id(key('list',@name)[1])]">
				<xsl:sort select="@name" />
				<xsl:apply-templates select="." />
			</xsl:for-each>
		</taxa>
	</xsl:template>

	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()" />
		</xsl:copy>
	</xsl:template>
</xsl:stylesheet>