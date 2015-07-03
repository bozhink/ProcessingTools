<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" encoding="UTF-8" indent="no" omit-xml-declaration="yes" />

	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="i/tn">
		<xsl:variable name="taxon" select="normalize-space(node())"/>
		<xsl:choose>
			<xsl:when test="matches($taxon,'[A-Z][a-z\.-]+\s*\(\s*[A-Z][a-z\.-]+\s*\)\s*[a-z\.-]+')">
				<tn>
					<tn-part type="genus">
						<xsl:value-of select="normalize-space(substring-before($taxon,'('))"/>
					</tn-part>
					<xsl:text> (</xsl:text>
					<tn-part type="subgenus">
						<xsl:value-of select="normalize-space(substring-after($taxon,'('))"/>
					</tn-part>
					<xsl:text>) </xsl:text>
					<tn-part type="species">
						<xsl:value-of select="normalize-space(substring-after($taxon,')'))"/>
					</tn-part>
				</tn>
			</xsl:when>
			<xsl:when test="matches($taxon,'[A-Z][a-z\.-]+\s*\(\s*[A-Z][a-z\.-]+\s*\)')">
				<tn>
					<tn-part type="genus">
						<xsl:value-of select="normalize-space(substring-before($taxon,'('))"/>
					</tn-part>
					<xsl:text> (</xsl:text>
					<tn-part type="subgenus">
						<xsl:value-of select="normalize-space(substring-after($taxon,'('))"/>
					</tn-part>
					<xsl:text>)</xsl:text>
				</tn>
			</xsl:when>
			<xsl:when test="matches($taxon,'[A-Z][a-z\.-]+\s+[a-z-]+')">
				<tn>
					<tn-part type="genus">
						<xsl:value-of select="substring-before($taxon,' ')"/>
					</tn-part>
					<xsl:text> </xsl:text>
					<tn-part type="species">
						<xsl:value-of select="substring-after($taxon,' ')"/>
					</tn-part>
				</tn>
			</xsl:when>
			<xsl:when test="matches($taxon,'[A-Z][a-z\.-]+')">
				<tn>
					<tn-part type="genus">
						<xsl:value-of select="substring-before($taxon,' ')"/>
					</tn-part>
				</tn>
			</xsl:when>
			<xsl:otherwise>
				<tn>
					<xsl:apply-templates select="node()"/>
				</tn>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>