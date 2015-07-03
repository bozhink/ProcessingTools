<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:fo="http://www.w3.org/1999/XSL/Format"
                xmlns:xs="http://www.w3.org/2001/XMLSchema"
                xmlns:fn="http://www.w3.org/2005/xpath-functions"
                xmlns:tp="http://www.plazi.org/taxpub">

	<xsl:output encoding="UTF-8" indent="no" method="xml"/>
	<xsl:preserve-space elements="*" />

	<xsl:template match="/">
		<xsl:apply-templates select="*"/>
	</xsl:template>

	<xsl:template match="nomenclature/fields/nomenclature/value/p">
		<xsl:variable name="genus">
			<xsl:choose>
				<xsl:when test="count(.//tn-part[@type='genus'])!=0">
					<xsl:value-of select=".//tn-part[@type='genus'][1]"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="../../../../../fields/genus/value"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<p>
			<xsl:apply-templates select="@*" mode="nom" />
			<xsl:apply-templates mode="nom">
				<xsl:with-param name="genus" select="$genus"/>
			</xsl:apply-templates>
		</p>
	</xsl:template>

	<xsl:template match="tn[@type='lower'][count(tn-part[@type='genus']) = 0]" mode="nom">
		<xsl:param name="genus" select="''"/>
		<tn type="lower">
			<tn-part type="genus">
				<xsl:attribute name="full-name">
					<!--<xsl:choose>
						<xsl:when test="count(../..//tn[count(tn-part[@type = 'genus']) != 0]) != 0">
							<xsl:value-of select="../..//tn[count(tn-part[@type = 'genus']) != 0][1]/tn-part[@type = 'genus']" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="../../../../../../../fields/genus/value" />
						</xsl:otherwise>
					</xsl:choose>-->
					<xsl:value-of select="$genus"/>
				</xsl:attribute>
			</tn-part>
			<xsl:apply-templates mode="nom">
				<xsl:with-param name="genus" select="$genus"/>
			</xsl:apply-templates>
		</tn>
	</xsl:template>

	<xsl:template match="@*|node()" mode="nom">
		<xsl:param name="genus" select="''"/>
		<xsl:copy>
			<xsl:apply-templates select="@*" mode="nom"/>
			<xsl:apply-templates mode="nom">
				<xsl:with-param name="genus" select="$genus"/>
			</xsl:apply-templates>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()" />
		</xsl:copy>
	</xsl:template>

</xsl:stylesheet>
