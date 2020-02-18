<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  xmlns:tp="http://www.plazi.org/taxpub">

  <xsl:output method="xml" indent="no" encoding="utf-8" cdata-section-elements="tex-math" />

  <xsl:preserve-space elements="*" />

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="tn-part[name(../..)!='tp:nomenclature']|//tp:taxon-name[name(../..)!='tp:nomenclature']">
    <xsl:apply-templates />
  </xsl:template>
</xsl:stylesheet>
