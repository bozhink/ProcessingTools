<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" indent="no" encoding="utf-8" omit-xml-declaration="no" />

  <xsl:preserve-space elements="*" />

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="tn-part[@type='genus'][normalize-space(@full-name)=''][ancestor::p[count(.//tn-part[@type='genus'][normalize-space(@full-name)!='']) = 1]]">
    <xsl:element name="{name()}">
      <xsl:apply-templates select="@*" />
      <xsl:attribute name="full-name">
        <xsl:value-of select="ancestor::p//tn-part[@type='genus'][normalize-space(@full-name)!='']/@full-name" />
      </xsl:attribute>
      <xsl:apply-templates />
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>
