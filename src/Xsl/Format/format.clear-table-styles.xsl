<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  
  <xsl:output method="xml" encoding="utf-8" />

  <xsl:preserve-space elements="*" />

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="table/@width | table/@height | table/@style | table/@class | table/@border"></xsl:template>

  <xsl:template match="tr/@width | tr/@height | tr/@style | tr/@class | tr/@border"></xsl:template>

  <xsl:template match="td/@width | td/@height | td/@style | td/@class | td/@border"></xsl:template>

  <xsl:template match="th/@width | th/@height | th/@style | th/@class | th/@border"></xsl:template>
</xsl:stylesheet>
