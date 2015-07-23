<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
				xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				xmlns:xs="http://www.w3.org/2001/XMLSchema"
				exclude-result-prefixes="xs"
				xmlns:mml="http://www.w3.org/1998/Math/MathML"
				xmlns:xlink="http://www.w3.org/1999/xlink"
				xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
				xmlns:tp="http://www.plazi.org/taxpub">

	<xsl:output method="xml" indent="no"/>

	<xsl:template match="@* | node()">
		<xsl:copy>
			<xsl:apply-templates select="@* | node()"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="xref|tn|tp:taxon-name|tp:treatment-meta|article/front//*[name()!='p']|ref|locality-coordinates|quantity|date|institution|product|geoname|morphology-part|specimenCode">
		<!-- Delete this nodes-->
	</xsl:template>
</xsl:stylesheet>
