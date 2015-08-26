<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:mml="http://www.w3.org/1998/Math/MathML"
  xmlns:xlink="http://www.w3.org/1999/xlink"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:tp="http://www.plazi.org/taxpub"
  exclude-result-prefixes="xs">

  <xsl:output method="xml" indent="no" encoding="utf-8" />

  <xsl:include href="format.inc.xsl"/>

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()"/>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="i | em | italic | Italic">
    <i>
      <xsl:apply-templates select="@* | node()"/>
    </i>
  </xsl:template>

  <xsl:template match="b | strong | bold | Bold">
    <b>
      <xsl:apply-templates select="@* | node()"/>
    </b>
  </xsl:template>

  <xsl:template match="bold-italic | Bold-Italic">
    <b>
      <i>
        <xsl:apply-templates select="@* | node()"/>
      </i>
    </b>
  </xsl:template>

  <xsl:template match="tp:taxon-name | tn">
    <tn>
      <xsl:apply-templates select="@* | node()"/>
    </tn>
  </xsl:template>

  <xsl:template match="tp:taxon-name-part | tn-part">
    <tn-part>
      <xsl:apply-templates select="@* | node()"/>
    </tn-part>
  </xsl:template>

  <xsl:template match="tp:taxon-name-part/@taxon-name-part-type">
    <xsl:attribute name="type">
      <xsl:value-of select="."/>
    </xsl:attribute>
  </xsl:template>
</xsl:stylesheet>
