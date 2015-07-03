<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:xs="http://www.w3.org/2001/XMLSchema"
	xmlns:mml="http://www.w3.org/1998/Math/MathML"
	xmlns:xlink="http://www.w3.org/1999/xlink"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	xmlns:tp="http://www.plazi.org/taxpub"
	exclude-result-prefixes="xs">

	<xsl:output method="xml" indent="yes" encoding="UTF-8"/>

	<xsl:param name="smallcase" select="'abcdefghijklmnopqrstuvwxyz'" />
	<xsl:param name="uppercase" select="'ABCDEFGHIJKLMNOPQRSTUVWXYZ'" />

	<xsl:template match="@*|*">
		<xsl:copy>
			<xsl:apply-templates select="@*" />
			<xsl:apply-templates />
		</xsl:copy>
	</xsl:template>

	<xsl:template match="/">
		<floats>
			<figures>
				<xsl:for-each select="//fig">
					<xsl:variable name="label">
						<xsl:value-of select="translate(normalize-space(label), $uppercase, $smallcase)"/>
					</xsl:variable>
					<fig>
						<xsl:apply-templates select="@id"/>
						<xsl:attribute name="type">
							<xsl:choose>
								<xsl:when test="contains($label,'fig')">
									<xsl:text>figure</xsl:text>
								</xsl:when>
								<xsl:when test="contains($label,'map')">
									<xsl:text>map</xsl:text>
								</xsl:when>
								<xsl:when test="contains($label,'pl')">
									<xsl:text>plate</xsl:text>
								</xsl:when>
								<xsl:when test="contains($label,'hab')">
									<xsl:text>habitus</xsl:text>
								</xsl:when>
							</xsl:choose>
						</xsl:attribute>
						<xsl:value-of select="$label"/>
					</fig>
				</xsl:for-each>
			</figures>
		</floats>
	</xsl:template>
</xsl:stylesheet>