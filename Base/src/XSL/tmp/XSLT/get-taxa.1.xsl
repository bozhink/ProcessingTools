<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:xs="http://www.w3.org/2001/XMLSchema"
	xmlns:mml="http://www.w3.org/1998/Math/MathML"
	xmlns:xlink="http://www.w3.org/1999/xlink"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	xmlns:tp="http://www.plazi.org/taxpub"
	exclude-result-prefixes="xs xsi mml xlink tp">

	<xsl:output method="xml" encoding="utf-8" indent="yes" />
	<xsl:strip-space elements="taxon"/>

	<xsl:key name="taxa" match="tp:taxon-name" use="normalize-space(.)"/>
	<xsl:key name="pwt-taxa" match="tn" use="normalize-space(.)"/>

	<xsl:template match="/">
		<taxa>
			<xsl:for-each select="//tp:taxon-name[generate-id() = generate-id(key('taxa', .)[1])]">
				<xsl:sort />
				<taxon full-name="{normalize-space(.)}">
					<xsl:for-each select="tp:taxon-name-part[normalize-space(.)!='']">
						<xsl:attribute name="{@taxon-name-part-type}">
							<xsl:value-of select="normalize-space(.)"/>
						</xsl:attribute>
					</xsl:for-each>
					<xsl:for-each select="tp:taxon-name-part">
						<part type="{@taxon-name-part-type}">
							<xsl:value-of select="normalize-space(.)"/>
						</part>
					</xsl:for-each>
				</taxon>
			</xsl:for-each>
			<xsl:for-each select="//tn[generate-id() = generate-id(key('pwt-taxa', .)[1])]">
				<xsl:sort />
				<taxon>
					<xsl:attribute name="type">
						<xsl:value-of select=".//@type"/>
					</xsl:attribute>
					<xsl:value-of select="normalize-space(.)"/>
				</taxon>
			</xsl:for-each>
		</taxa>
	</xsl:template>
</xsl:stylesheet>