<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:mml="http://www.w3.org/1998/Math/MathML"  xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:tp="http://www.plazi.org/taxpub" exclude-result-prefixes="xs mml xlink tp xsi">

  <xsl:output method="xml" encoding="UTF-8" indent="yes" />

  <xsl:template match="@*|*">
    <xsl:copy>
      <xsl:apply-templates select="@*" />
      <xsl:apply-templates />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="tp:taxon-name-part[@full-name[normalize-space(.)!='']] | tn-part[@full-name[normalize-space(.)!='']]">
    <xsl:element name="{name()}">
      <xsl:apply-templates select="@*" />
      <xsl:value-of select="@full-name" />
    </xsl:element>
    <xsl:text> </xsl:text>
  </xsl:template>

  <xsl:template match="tp:taxon-name-part | tn-part">
    <xsl:element name="{name()}">
      <xsl:apply-templates select="@*" />
      <xsl:value-of select="normalize-space(.)" />
    </xsl:element>
    <xsl:text> </xsl:text>
  </xsl:template>
</xsl:stylesheet>
