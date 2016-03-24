<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:mml="http://www.w3.org/1998/Math/MathML"
  xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:tp="http://www.plazi.org/taxpub"
  exclude-result-prefixes="xs">

  <xsl:output method="xml" encoding="UTF-8" omit-xml-declaration="no" indent="no" cdata-section-elements="tex-math" />
  <xsl:preserve-space elements="*" />

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="tp:treatment-meta/kwd-group/kwd/named-content[@content-type='family'][normalize-space(.)='FAMILIA']">
    <xsl:variable name="family-name">
      <!-- TOOD: If there are multiple tn/tn-part[@type='family'] this select migth throw. -->
      <xsl:value-of select="normalize-space(./ancestor::sec/title/tn/tn-part[@type='family']/text())"/>
    </xsl:variable>
    <xsl:element name="{name()}">
      <xsl:apply-templates select="@*" />
      <xsl:choose>
        <xsl:when test="$family-name!=''">
          <xsl:value-of select="$family-name"/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="normalize-space()"/>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>
