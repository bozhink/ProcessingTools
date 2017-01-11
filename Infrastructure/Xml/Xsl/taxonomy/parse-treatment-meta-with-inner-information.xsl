<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:mml="http://www.w3.org/1998/Math/MathML"
  xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:tp="http://www.plazi.org/taxpub"
  exclude-result-prefixes="xs">

  <xsl:output method="xml" indent="no" encoding="utf-8" omit-xml-declaration="no" />
  <xsl:preserve-space elements="*" />

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="tp:taxon-treatment[ancestor::sec/title//tn/tn-part[@type='family']]/tp:treatment-meta/kwd-group/kwd/named-content[@content-type='family'][string(.)='FAMILIA']">
    <xsl:element name="{name(.)}">
      <xsl:apply-templates select="@*"/>
      <xsl:value-of select="./ancestor::sec/title//tn/tn-part[@type='family']"/>
    </xsl:element>
  </xsl:template>
  
  <xsl:template match="tp:taxon-treatment[ancestor::sec/title//tn/tn-part[@type='order']]/tp:treatment-meta/kwd-group/kwd/named-content[@content-type='order'][string(.)='ORDO']">
    <xsl:element name="{name(.)}">
      <xsl:apply-templates select="@*"/>
      <xsl:value-of select="./ancestor::sec/title//tn/tn-part[@type='order']"/>
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>
