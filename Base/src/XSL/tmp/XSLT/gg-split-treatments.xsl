<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:mods="http://www.loc.gov/mods/v3">

	<xsl:output encoding="utf-8" indent="yes" method="xml" omit-xml-declaration="yes"/>
	<xsl:preserve-space elements="*"/>

	<xsl:template match="/">
		<documents>
			<xsl:for-each select="/document/treatment">
				<xsl:variable name="identifier">
					<xsl:call-template name="replace">
						<xsl:with-param name="string" select="/document/@ID-HNS-Pub"/>
						<xsl:with-param name="pattern" select="' '"/>
						<xsl:with-param name="replace" select="''"/>
					</xsl:call-template>
					<xsl:text>-</xsl:text>
					<xsl:call-template name="replace">
						<xsl:with-param name="string" select="normalize-space(subSubSection[@type='nomenclature']/paragraph//taxonomicName[position()=1])"/>
						<xsl:with-param name="pattern" select="' '"/>
						<xsl:with-param name="replace" select="'-'"/>
					</xsl:call-template>
				</xsl:variable>
				<document>
					<xsl:apply-templates select="/document/@*"/>
					<xsl:attribute name="ID-HNS-Pub">
						<xsl:value-of select="$identifier"/>
					</xsl:attribute>
					<xsl:apply-templates mode="mods" select="/document/mods:mods">
						<xsl:with-param name="id" select="$identifier"/>
					</xsl:apply-templates>
					<xsl:apply-templates select="."/>
				</document>
			</xsl:for-each>
		</documents>
	</xsl:template>

	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="@*|node()" mode="mods">
		<xsl:param name="id" select="''"/>
		<xsl:copy>
			<xsl:apply-templates select="@*" mode="mods">
				<xsl:with-param name="id" select="$id"/>
			</xsl:apply-templates>
			<xsl:apply-templates mode="mods">
				<xsl:with-param name="id" select="$id"/>
			</xsl:apply-templates>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="mods:identifier[@type='HNS-Pub']" mode="mods">
		<xsl:param name="id" select="''"/>
		<mods:identifier type="HNS-Pub"><xsl:value-of select="$id"/></mods:identifier>
	</xsl:template>

	<xsl:template name="replace">
		<xsl:param name="string"/>
		<xsl:param name="pattern"/>
		<xsl:param name="replace"/>
		<xsl:choose>
			<xsl:when test="contains($string,$pattern)">
				<xsl:value-of select="substring-before($string,$pattern)"/>
				<xsl:value-of select="$replace"/>
				<xsl:call-template name="replace">
					<xsl:with-param name="string" select="substring-after($string,$pattern)"/>
					<xsl:with-param name="pattern" select="$pattern"/>
					<xsl:with-param name="replace" select="$replace"/>
				</xsl:call-template>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$string"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>