<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mml="http://www.w3.org/1998/Math/MathML">
	<xsl:output method="xml" encoding="UTF-8" indent="yes"/>

	<xsl:template match="/">
		<math-group>
			<xsl:for-each select="//*[name()='math']">
				<mml:math>
					<xsl:attribute name="overflow">scroll</xsl:attribute>
					<xsl:attribute name="id"><xsl:text>eq</xsl:text><xsl:value-of select="position()"/></xsl:attribute>
					<mml:semantics>
						<xsl:attribute name="definitionURL">http://www.w3.org/1998/Math/MathML</xsl:attribute>
						<xsl:attribute name="encoding">UTF-8</xsl:attribute>
						<xsl:apply-templates mode="tonlmmath" select="node()"/>
					</mml:semantics>
				</mml:math>
			</xsl:for-each>
		</math-group>
	</xsl:template>

	<xsl:template mode="tonlmmath" match="*">
		<xsl:for-each select="node()[normalize-space(.)!='']">
			<xsl:choose>
				<xsl:when test="name()=''">
					<xsl:value-of select="."/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:element name="mml:{name()}">
						<xsl:apply-templates mode="tonlmmath" select="."/>
					</xsl:element>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:for-each>
	</xsl:template>
</xsl:stylesheet>