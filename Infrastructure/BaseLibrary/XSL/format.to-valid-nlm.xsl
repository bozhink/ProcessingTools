<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:mml="http://www.w3.org/1998/Math/MathML"
  xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:tp="http://www.plazi.org/taxpub"
  exclude-result-prefixes="xs">

  <xsl:output method="xml" encoding="UTF-8" omit-xml-declaration="no" indent="no" cdata-section-elements="tex-math"
    doctype-public="-//TaxonX//DTD Taxonomic Treatment Publishing DTD v0 20100105//EN" doctype-system="tax-treatment-NS0.dtd" version="1.0" media-type="text/xml" />

  <xsl:preserve-space elements="*" />

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="tn | tp:taxon-name">
    <tp:taxon-name>
      <xsl:apply-templates select="node()" />
    </tp:taxon-name>
  </xsl:template>

  <xsl:template match="tn-part[normalize-space()=''] | tp:taxon-name-part[normalize-space()='']"></xsl:template>

  <xsl:template match="tn-part | tp:taxon-name-part">
    <tp:taxon-name-part taxon-name-part-type="{@type}{@taxon-name-part-type}">
      <xsl:apply-templates />
    </tp:taxon-name-part>
  </xsl:template>

  <xsl:template match="i | em | Italic | italic">
    <italic>
      <xsl:apply-templates />
    </italic>
  </xsl:template>

  <xsl:template match="b | strong | Bold | bold">
    <bold>
      <xsl:apply-templates />
    </bold>
  </xsl:template>

  <xsl:template match="u | underline">
    <underline>
      <xsl:apply-templates />
    </underline>
  </xsl:template>

  <xsl:template match="s | strike">
    <strike>
      <xsl:apply-templates />
    </strike>
  </xsl:template>

  <xsl:template match="sup">
    <sup>
      <xsl:apply-templates />
    </sup>
  </xsl:template>

  <xsl:template match="sub">
    <sub>
      <xsl:apply-templates />
    </sub>
  </xsl:template>

  <xsl:template match="locality-coordinates[not(@type)]">
    <named-content content-type="{name()}">
      <xsl:apply-templates />
    </named-content>
  </xsl:template>

  <xsl:template match="locality-coordinates[@type='latitude']">
    <named-content content-type="{name()} {@type}">
      <xsl:apply-templates />
    </named-content>
  </xsl:template>

  <xsl:template match="locality-coordinates[@type='longitude']">
    <named-content content-type="{name()} {@type}">
      <xsl:apply-templates />
    </named-content>
  </xsl:template>

  <xsl:template match="institutional_code">
    <named-content content-type="{name()}">
      <xsl:apply-templates />
    </named-content>
  </xsl:template>

  <xsl:template match="institution[@url]">
    <named-content content-type="{name()}">
      <xsl:apply-templates />
    </named-content>
  </xsl:template>

  <xsl:template match="abbrev">
    <xsl:element name="{name()}">
      <xsl:apply-templates select="@*" />
      <xsl:apply-templates mode="abbrev" />
    </xsl:element>
  </xsl:template>

  <xsl:template match="node()" mode="abbrev">
    <xsl:value-of select="string()" />
  </xsl:template>

  <xsl:template match="@*" mode="abbrev"></xsl:template>

  <xsl:template match="def" mode="abbrev">
    <xsl:apply-templates select="." />
  </xsl:template>

  <xsl:template match="object-id | self-uri | uri | ext-link | email">
    <xsl:element name="{name()}">
      <xsl:apply-templates select="@*" />
      <xsl:value-of select="string()" />
    </xsl:element>
  </xsl:template>

  <xsl:template match="conf-name | conf-date | conf-loc | issue-title | issue | issue-part | volume | volume-series | series | edition | fpage | lpage | elocation-id | year | day | month | role | surname | given-names | prefix | suffix">
    <xsl:element name="{name()}">
      <xsl:apply-templates />
    </xsl:element>
  </xsl:template>

  <xsl:template match="xref-group">
  </xsl:template>
</xsl:stylesheet>
