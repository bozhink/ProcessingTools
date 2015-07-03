<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions" xmlns:tp="http://www.plazi.org/taxpub">

	<xsl:output encoding="UTF-8" indent="yes" method="xml"/>
	<xsl:preserve-space elements="*" />
	
	<xsl:template match="/">
		<xsl:apply-templates select="*"/>
	</xsl:template>
	
	<xsl:template match="nomenclature/fields/nomenclature/value/p">
		<p>
			<xsl:apply-templates select="@*" mode="nom" />
			<xsl:apply-templates mode="nom"/>
		</p>
	</xsl:template>
	
	<xsl:template match="tp:taxon-name[count(tp:taxon-name-part[@taxon-name-part-type = 'genus']) = 0] " mode="nom">
		<tp:taxon-name>
			<tp:taxon-name-part taxon-name-part-type="genus">
				<xsl:attribute name="full-name">
					<xsl:choose>
						<xsl:when test="count(../..//tp:taxon-name[count(tp:taxon-name-part[@taxon-name-part-type = 'genus']) != 0]) != 0">
							<xsl:value-of select="../..//tp:taxon-name[count(tp:taxon-name-part[@taxon-name-part-type = 'genus']) != 0] [1]/tp:taxon-name-part[@taxon-name-part-type = 'genus']" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="../../../../../../../fields/genus/value" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:attribute>
			</tp:taxon-name-part>
			<xsl:apply-templates select="./*" mode="nom" />
		</tp:taxon-name>
	</xsl:template>

	<xsl:template match="@*|*" mode="nom">
		<xsl:copy>
			<xsl:apply-templates select="@*" mode="nom"/>
			<xsl:apply-templates mode="nom"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="@*|*">
		<xsl:copy>
			<xsl:apply-templates select="@*" />
			<xsl:apply-templates />
		</xsl:copy>
	</xsl:template>
</xsl:stylesheet>
