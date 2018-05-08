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

  <xsl:template match="back/sec[@sec-type='Index']//p//tn[@type='lower']/tn-part[@type='genus'][normalize-space(@full-name)=''][normalize-space()='']">
    <xsl:variable name="genus">
      <xsl:choose>
        <xsl:when test="count(ancestor::p[position()=1]//tn-part[@type='genus'][normalize-space(@full-name)!='']) = 1">
          <xsl:value-of select="ancestor::p[position()=1]//tn-part[@type='genus'][normalize-space(@full-name)!=''][1]/@full-name" />
        </xsl:when>
        <xsl:otherwise>
          <xsl:text></xsl:text>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:variable>

    <xsl:element name="{name()}">
      <xsl:apply-templates select="@*" />
      <xsl:attribute name="full-name">
        <xsl:value-of select="normalize-space($genus)" />
      </xsl:attribute>
      <xsl:apply-templates />
    </xsl:element>
  </xsl:template>

  <xsl:template match="body//sec[count(title//tn-part[@type='genus'][normalize-space(@full-name)!='']) = 1]//p//tn[@type='lower']/tn-part[@type='genus'][normalize-space(@full-name)=''][normalize-space()='']">
    <xsl:variable name="genus">
      <xsl:choose>
        <xsl:when test="count(ancestor::p[position()=1]//tn-part[@type='genus'][normalize-space(@full-name)!='']) = 0">
          <xsl:value-of select="ancestor::sec[position()=1]/title//tn-part[@type='genus'][normalize-space(@full-name)!=''][1]/@full-name" />
        </xsl:when>
        <xsl:otherwise>
          <xsl:text></xsl:text>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:variable>

    <xsl:element name="{name()}">
      <xsl:apply-templates select="@*" />
      <xsl:attribute name="full-name">
        <xsl:value-of select="normalize-space($genus)" />
      </xsl:attribute>
      <xsl:apply-templates />
    </xsl:element>
  </xsl:template>

  <xsl:template match="body//sec[count(title//tn-part[@type='genus'][normalize-space(@full-name)!='']) = 1]//p/b[1]/i[1]/tn[@type='lower']/tn-part[@type='genus'][normalize-space(@full-name)=''][normalize-space()='']">
    <xsl:call-template name="genus-in-title" />
  </xsl:template>

  <xsl:template match="body//sec[count(title//tn-part[@type='genus'][normalize-space(@full-name)!='']) = 1]//p/i[1]/tn[@type='lower']/tn-part[@type='genus'][normalize-space(@full-name)=''][normalize-space()='']">
    <xsl:call-template name="genus-in-title" />
  </xsl:template>

  <xsl:template name="genus-in-title">
    <xsl:variable name="genus">
      <xsl:value-of select="ancestor::sec[position()=1]/title//tn-part[@type='genus'][normalize-space(@full-name)!=''][1]/@full-name" />
    </xsl:variable>

    <xsl:element name="{name()}">
      <xsl:apply-templates select="@*" />
      <xsl:attribute name="full-name">
        <xsl:value-of select="normalize-space($genus)" />
      </xsl:attribute>
      <xsl:apply-templates />
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>
