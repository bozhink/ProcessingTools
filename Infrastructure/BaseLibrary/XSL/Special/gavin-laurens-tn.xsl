<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:output method="xml" indent="no" encoding="utf-8" omit-xml-declaration="no" />
  <xsl:preserve-space elements="*" />

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="checklist_taxon_nomenclature/fields/nomenclature/value/p//tn[@type='lower']/tn-part[@type='genus'][normalize-space(@full-name)=''][normalize-space()='']">
    <xsl:variable name="genus">
      <xsl:choose>
        <xsl:when test="count(ancestor::p[position()=1]//tn-part[@type='genus'][normalize-space()!=''])!=0">
          <xsl:value-of select="ancestor::p[position()=1]//tn-part[@type='genus'][normalize-space()!=''][1]" />
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="ancestor::checklist_taxon[position()=1]/fields/genus/value" />
        </xsl:otherwise>
      </xsl:choose>
    </xsl:variable>

    <xsl:element name="{name()}">
      <xsl:apply-templates select="@*" />
      <xsl:attribute name="full-name">
        <xsl:value-of select="$genus" />
      </xsl:attribute>
      <xsl:apply-templates />
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>
