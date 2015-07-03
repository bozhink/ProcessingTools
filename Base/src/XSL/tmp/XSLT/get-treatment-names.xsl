<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:xs="http://www.w3.org/2001/XMLSchema"
	xmlns:mml="http://www.w3.org/1998/Math/MathML"
	xmlns:xlink="http://www.w3.org/1999/xlink"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	xmlns:tp="http://www.plazi.org/taxpub"
	exclude-result-prefixes="xs">

	<xsl:output method="xml" encoding="UTF-8" indent="yes"/>

	<xsl:template match="/">
		<treatments>
			<xsl:for-each select="//tp:nomenclature/tp:taxon-name">
				<xsl:sort/>
				<treatment>
					<xsl:value-of select="normalize-space(.)"/>
				</treatment>
			</xsl:for-each>
		</treatments>
	</xsl:template>

</xsl:stylesheet>