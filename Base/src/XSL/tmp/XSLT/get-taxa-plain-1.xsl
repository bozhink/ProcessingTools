<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:tp="http://www.plazi.org/taxpub"
	exclude-result-prefixes="tp">

	<xsl:output encoding="utf-8" indent="yes" method="xml"/>

	<xsl:key name="taxon-name" match="taxon" use="@name"/>
	<xsl:key name="taxon-type" match="taxon" use="@type"/>

	<xsl:template match="/">
		<taxa>
			<xsl:for-each select="//taxon[generate-id() = generate-id(key('taxon-name', @name)[1])]">
				<taxon>
					<xsl:attribute name="name">
						<xsl:value-of select="@name"/>
					</xsl:attribute>
					<xsl:for-each select="key('taxon-name', @name)[@type = key('taxon-name',@name)[1]/@type]">
						<type id="{generate-id()}" id1="{generate-id(key('taxon-type',@type)[1])}" name="{@name}">
							<xsl:value-of select="@type"/>
						</type>
					</xsl:for-each>
				</taxon>
			</xsl:for-each>
<!-- 			<types> -->
<!-- 				<xsl:for-each select="//taxon"> -->
<!-- 					<xsl:for-each select="key('taxon-type',@type)"> -->
<!-- 						<type> -->
<!-- 							<xsl:value-of select="@type"/> -->
<!-- 						</type> -->
<!-- 					</xsl:for-each> -->
<!-- 				</xsl:for-each> -->
<!-- 			</types> -->
		</taxa>
	</xsl:template>
</xsl:stylesheet>