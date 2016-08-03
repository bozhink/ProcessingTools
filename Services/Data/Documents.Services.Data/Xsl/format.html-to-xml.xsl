<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:tp="http://www.plazi.org/taxpub" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:mml="http://www.w3.org/1998/Math/MathML">

  <xsl:output indent="no" method="xml" encoding="utf-8" omit-xml-declaration="no" />

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="@class | @title | @href | @elem-name | @target | *[@xlink:type]/@type"></xsl:template>

  <xsl:template match="*[not(@elem-name)]">
    <xsl:apply-templates />
  </xsl:template>
  
  <xsl:template match="*[@role='tooltip']"></xsl:template>

  <xsl:template match="*[@elem-name][normalize-space(@elem-name)!='']">
    <xsl:element name="{@elem-name}">
      <xsl:apply-templates select="@* | node()" />
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>
